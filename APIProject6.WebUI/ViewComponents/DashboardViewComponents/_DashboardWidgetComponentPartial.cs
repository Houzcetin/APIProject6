using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardWidgetComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
