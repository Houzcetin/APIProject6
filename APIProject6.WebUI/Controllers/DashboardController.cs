using APIProject6.WebUI.Dtos.GroupReservationDtos;
using APIProject6.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace APIProject6.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RevenueChart()
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

        [HttpPost]
        public async Task<IActionResult> UpdateGroupReservation(UpdateGroupReservationDto updateGroupReservationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateGroupReservationDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7277/api/GroupReservations/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Update failed." });
        }
    }
}
