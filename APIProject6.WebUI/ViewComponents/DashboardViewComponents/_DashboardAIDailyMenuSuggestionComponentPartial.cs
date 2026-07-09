using APIProject6.WebUI.Dtos.AISuggestionDtos;
using APIProject6.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace APIProject6.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardAIDailyMenuSuggestionComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IImageFinderService _imageFinderService;

        public _DashboardAIDailyMenuSuggestionComponentPartial(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IImageFinderService imageFinderService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _imageFinderService = imageFinderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var apiKey = CleanHeaderValue(_configuration["OpenAI:ApiKey"]);
            var model = CleanHeaderValue(_configuration["OpenAI:Model"]) ?? "gpt-5.4-mini";

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return View(GetFallbackSuggestions());
            }

            var openAiClient = _httpClientFactory.CreateClient();

            openAiClient.BaseAddress = new Uri("https://api.openai.com/");
            openAiClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            string prompt = @"
Create exactly 4 AI daily food suggestions for a restaurant dashboard.

Rules:
- All content must be in English.
- ProductId must be between 1001 and 9999.
- Name must be a realistic restaurant food name.
- Category must be one of these values: Chef Pick, Popular, Healthy, High Profit, Dessert, New Idea.
- Price must be realistic for a restaurant in Türkiye.
- Price must be between 80 and 500 Turkish Lira.
- Price should not use decimal values.
- Reason must be short and clear.
- ImageSearchQuery must be a short English search phrase for finding a relevant food photo.
- Do not generate ImageUrl. ImageUrl will be added later by the backend.
- Generate different suggestions each time.
Diversity rules:
- Do not repeat common dishes too often.
- Avoid using these dishes unless absolutely necessary: burger, pasta, quinoa bowl, cheesecake, salmon bowl, chicken bowl.
- Use different cuisines in the same response.
- At least one suggestion must be from Turkish, Greek, Italian, Mexican, Japanese, Korean, Chinese, Indian, French, Spanish, Georgian, or Middle Eastern cuisine.
- Include different food types: one main dish, one starter or side, one dessert or drink, and one chef special.
- Generate different suggestions each time.

Random seed:
- {DateTime.Now.Ticks}
";

            var body = new
            {
                model = model,
                instructions = "You are an AI chef assistant for a restaurant dashboard. Return only structured JSON.",
                input = prompt,
                text = new
                {
                    format = new
                    {
                        type = "json_schema",
                        temperature = 1.2,
                        name = "ai_daily_menu_suggestions",
                        strict = true,
                        schema = new
                        {
                            type = "object",
                            additionalProperties = false,
                            properties = new
                            {
                                Suggestions = new
                                {
                                    type = "array",
                                    minItems = 4,
                                    maxItems = 4,
                                    items = new
                                    {
                                        type = "object",
                                        additionalProperties = false,
                                        properties = new
                                        {
                                            ProductId = new
                                            {
                                                type = "integer"
                                            },
                                            Name = new
                                            {
                                                type = "string"
                                            },
                                            Category = new
                                            {
                                                type = "string",
                                                @enum = new[]
                                                {
                                                    "Chef Pick",
                                                    "Popular",
                                                    "Healthy",
                                                    "High Profit",
                                                    "Dessert",
                                                    "New Idea"
                                                }
                                            },
                                            Price = new
                                            {
                                                type = "number"
                                            },
                                            Reason = new
                                            {
                                                type = "string"
                                            },
                                            ImageSearchQuery = new
                                            {
                                                type = "string"
                                            }
                                        },
                                        required = new[]
                                        {
                                            "ProductId",
                                            "Name",
                                            "Category",
                                            "Price",
                                            "Reason",
                                            "ImageSearchQuery"
                                        }
                                    }
                                }
                            },
                            required = new[]
                            {
                                "Suggestions"
                            }
                        }
                    }
                }
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                var response = await openAiClient.PostAsync("v1/responses", content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return View(GetFallbackSuggestions());
                }

                var aiContent = ExtractOutputText(responseJson);

                if (string.IsNullOrWhiteSpace(aiContent))
                {
                    return View(GetFallbackSuggestions());
                }

                var result = JsonConvert.DeserializeObject<AIDailyMenuSuggestionsResponseDto>(aiContent);

                if (result?.Suggestions == null || !result.Suggestions.Any())
                {
                    return View(GetFallbackSuggestions());
                }

                foreach (var item in result.Suggestions)
                {
                    item.ImageUrl = await _imageFinderService.GetImageUrlAsync(item.ImageSearchQuery);
                }

                return View(result.Suggestions);
            }
            catch
            {
                return View(GetFallbackSuggestions());
            }
        }

        private static string? ExtractOutputText(string responseJson)
        {
            var jsonObject = JObject.Parse(responseJson);

            var directOutputText = jsonObject["output_text"]?.ToString();

            if (!string.IsNullOrWhiteSpace(directOutputText))
            {
                return directOutputText;
            }

            var outputArray = jsonObject["output"] as JArray;

            if (outputArray == null)
            {
                return null;
            }

            foreach (var outputItem in outputArray)
            {
                var contentArray = outputItem["content"] as JArray;

                if (contentArray == null)
                {
                    continue;
                }

                foreach (var contentItem in contentArray)
                {
                    var text = contentItem["text"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        return text;
                    }
                }
            }

            return null;
        }

        private static string? CleanHeaderValue(string? value)
        {
            return value?
                .Trim()
                .Replace("\r", "")
                .Replace("\n", "");
        }

        private static List<AIDailyMenuSuggestionsItem> GetFallbackSuggestions()
        {
            return new List<AIDailyMenuSuggestionsItem>
            {
                new AIDailyMenuSuggestionsItem
                {
                    ProductId = 1001,
                    Name = "Grilled Chicken Bowl",
                    Category = "Healthy",
                    Price = 220,
                    Reason = "Recommended as a balanced and healthy lunch option.",
                    ImageSearchQuery = "grilled chicken bowl restaurant food",
                    ImageUrl = "/yummy-red-1.0.0/assets/img/menu/menu-item-1.png"
                },
                new AIDailyMenuSuggestionsItem
                {
                    ProductId = 1002,
                    Name = "Creamy Mushroom Pasta",
                    Category = "Chef Pick",
                    Price = 260,
                    Reason = "A warm dinner choice with rich flavor.",
                    ImageSearchQuery = "creamy mushroom pasta restaurant plate",
                    ImageUrl = "/yummy-red-1.0.0/assets/img/menu/menu-item-2.png"
                },
                new AIDailyMenuSuggestionsItem
                {
                    ProductId = 1003,
                    Name = "Spicy Beef Burger",
                    Category = "Popular",
                    Price = 280,
                    Reason = "Recommended because burger-style meals are customer favorites.",
                    ImageSearchQuery = "spicy beef burger restaurant",
                    ImageUrl = "/yummy-red-1.0.0/assets/img/menu/menu-item-3.png"
                },
                new AIDailyMenuSuggestionsItem
                {
                    ProductId = 1004,
                    Name = "Chocolate Souffle",
                    Category = "Dessert",
                    Price = 140,
                    Reason = "Good dessert option to increase average order value.",
                    ImageSearchQuery = "chocolate souffle dessert restaurant",
                    ImageUrl = "/yummy-red-1.0.0/assets/img/menu/menu-item-4.png"
                }
            };
        }
    }
}