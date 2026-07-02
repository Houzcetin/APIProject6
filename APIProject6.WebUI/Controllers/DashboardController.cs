using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
