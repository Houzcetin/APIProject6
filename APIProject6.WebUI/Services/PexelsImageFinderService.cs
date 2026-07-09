using Newtonsoft.Json.Linq;

namespace APIProject6.WebUI.Services
{
    public class PexelsImageFinderService : IImageFinderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PexelsImageFinderService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetImageUrlAsync(string query)
        {
            var apiKey = _configuration["Pexels:ApiKey"]?
                .Trim()
                .Replace("\r", "")
                .Replace("\n", "");

            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(query))
            {
                return GetDefaultImage();
            }

            try
            {
                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Add("Authorization", apiKey);

                var url =
                    $"https://api.pexels.com/v1/search?query={Uri.EscapeDataString(query)}&per_page=1&orientation=landscape";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return GetDefaultImage();
                }

                var jsonData = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(jsonData);

                var imageUrl = jsonObject["photos"]?
                    .FirstOrDefault()?["src"]?["medium"]?
                    .ToString();

                return string.IsNullOrWhiteSpace(imageUrl)
                    ? GetDefaultImage()
                    : imageUrl;
            }
            catch
            {
                return GetDefaultImage();
            }
        }

        private static string GetDefaultImage()
        {
            return "/yummy-red-1.0.0/assets/img/menu/menu-item-1.png";
        }
    }
}