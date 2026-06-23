using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
