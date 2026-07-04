using APIProject6.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIProject6.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardMainChartComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardMainChartComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync(
                "https://localhost:7277/api/Reservations/GetReservationChart?monthCount=3"
            );

            if (!responseMessage.IsSuccessStatusCode)
            {
                return View(new RevenueChartViewModel());
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<RevenueChartViewModel>(jsonData);

            return View(values ?? new RevenueChartViewModel());
        }
    }
}
