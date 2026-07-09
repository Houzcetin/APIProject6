using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardClaudeAIComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
