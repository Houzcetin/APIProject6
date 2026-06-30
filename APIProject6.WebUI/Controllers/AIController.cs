using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace APIProject6.WebUI.Controllers
{
    public class AIController : Controller
    {
        private readonly IConfiguration _configuration;
        public AIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult CreateRecipeWithOpenAI()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
        {
            var apiKey = _configuration["OpenAI:ApiKey"]?.Trim();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new Exception("API key is not found.");
            }
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-5.4-mini",
                messages = new[]
                {
                    new
                    {
                        role="system",
                        content = "You are an AI recipe assistant for a restaurant. " +
                      "The user may write ingredients in Turkish or English. " +
                      "You must always answer only in English. " +
                      "Do not use any Turkish words. " +
                      "Do not use Turkish section titles such as Malzemeler, Yapılışı, Tarif, Hazırlanışı. " +
                      "Do not use Markdown formatting like ###, **, or bullet symbols. " +
                      "Use this exact plain text format for each recipe: " +
                      "Recipe Name: ... " +
                      "Ingredients: ... " +
                      "Instructions: ..."
                    },
                    new
                    {
                        role="user",
                        content=prompt
                    }
                },
                temperature = 0.5
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponses>();
                var content = result.choices[0].message.content;
                ViewBag.recipe= content;
            }
            else
            {
                ViewBag.recipe ="That is a error: "+response.StatusCode.ToString();
            }

                return View();
        }

        public class OpenAIResponses
        {
            public List<Choice> choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
    }
}
