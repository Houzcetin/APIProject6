using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
    }
}
