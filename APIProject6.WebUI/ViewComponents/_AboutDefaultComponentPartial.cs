using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.ViewComponents
{
    public class _AboutDefaultComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
