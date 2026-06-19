using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
