using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.ViewComponents.DefaultMenuViewComponents
{
    public class _DefaultMenuViewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
