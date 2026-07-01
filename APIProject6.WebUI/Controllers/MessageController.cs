using APIProject6.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static APIProject6.WebUI.Controllers.AIController;

namespace APIProject6.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MessageController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> MessageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7277/api/Messages");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]

        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7277/api/Messages", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7277/api/Messages?id=" + id);
            return RedirectToAction("MessageList");
        }

        [HttpGet]

        public async Task<IActionResult> UpdateMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7277/api/Messages/GetMessage?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);
            return View(value);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7277/api/Messages/", stringContent);
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> AnswerMessageWithOpenAI(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7277/api/Messages/GetMessage?id=" + id);
            if (!responseMessage.IsSuccessStatusCode)
                return RedirectToAction("MessageList");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);
            if (value is null)
                return RedirectToAction("MessageList");

            var prompt = value.MessageDetails;

            var apiKey = _configuration["OpenAI:ApiKey"]?.Trim();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new Exception("API key is not found.");
            }
            var client2 = _httpClientFactory.CreateClient();
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-5.4-mini",
                messages = new[]
                {
                    new
                    {
                        role="system",
                        content = "You are an AI customer support assistant for a restaurant. " +
          "Your task is to reply to customer messages in a detailed, polite, positive, and customer-satisfaction-focused way. " +
          "The user may send messages about reservations, complaints, food quality, service, delivery, menu questions, special requests, events, or general feedback. " +
          "Always respond with a helpful, respectful, warm, and professional tone. " +
          "If the customer has a complaint, first apologize sincerely, show empathy, and then offer a reasonable solution or next step. " +
          "If the customer asks a question, answer clearly and helpfully. " +
          "If the customer gives positive feedback, thank them warmly and encourage them to visit again. " +
          "Do not sound robotic. Do not be rude or defensive. " +
          "Do not make promises that the restaurant cannot guarantee, such as free meals, refunds, or discounts, unless the user explicitly mentions that the restaurant allows it. " +
          "Always protect the restaurant's reputation while making the customer feel valued. " +
          "Write the answer in the same language as the customer's message. " +
          "Do not use Markdown formatting like ###, **, or bullet symbols. " +
          "Use a clean plain text response."
                    },
                    new
                    {
                        role="user",
                        content=prompt
                    }
                },
                temperature = 0.5
            };

            var response = await client2.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponses>();
                ViewBag.answerAI = result?.choices?.FirstOrDefault()?.message?.content
                                   ?? "No response was returned.";
            }
            else
            {
                ViewBag.answerAI = "That is a error: " + response.StatusCode.ToString();
            }


            return View(value);
        }

        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            var client = new HttpClient();
            var apiKey = _configuration["HuggingFace:ApiKey"]?.Trim();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new Exception("Hugging Face API key is not found.");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            try
            {
                var translateRequestBody = new
                {
                    inputs = createMessageDto.MessageDetails,
                };
                var translateJson = System.Text.Json.JsonSerializer.Serialize(translateRequestBody);
                var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");

                var translateResponse = await client.PostAsync("https://router.huggingface.co/hf-inference/models/Helsinki-NLP/opus-mt-tr-en", translateContent);
                var translateResponseString = await translateResponse.Content.ReadAsStringAsync();

                string englishText = createMessageDto.MessageDetails;
                if (translateResponseString.TrimStart().StartsWith("["))
                {
                    var translateDoc = JsonDocument.Parse(translateResponseString);
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                    //ViewBag.v = englishText;
                }

                var toxicRequestBody = new
                {
                    inputs = englishText
                };
                var toxicJson = System.Text.Json.JsonSerializer.Serialize(toxicRequestBody);
                var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");
                var toxicResponse = await client.PostAsync("https://router.huggingface.co/hf-inference/models/unitary/toxic-bert", toxicContent);
                var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync();

                if(toxicResponseString.TrimStart().StartsWith("["))
                {
                    var toxicDoc = JsonDocument.Parse(toxicResponseString);
                    foreach (var item in toxicDoc.RootElement[0].EnumerateArray())
                    {
                        string label = item.GetProperty("label").GetString();
                        double score = item.GetProperty("score").GetDouble();

                        if(score > 0.5)
                        {
                            createMessageDto.Status = "Toxic message";
                            break;
                        }
                    }
                }
                if(string.IsNullOrEmpty(createMessageDto.Status))
                {
                    createMessageDto.Status = "Message received";
                }
            }
            catch (Exception ex)
            {
                createMessageDto.Status = "Waiting for confirmation";
                throw;
            }

            var client2 = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client2.PostAsync("https://localhost:7277/api/Messages", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }
    }

}