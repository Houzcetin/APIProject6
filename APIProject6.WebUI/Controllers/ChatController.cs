using Microsoft.AspNetCore.Mvc;

namespace APIProject6.WebUI.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult SendChatWithAI()
        {
            return View();
        }
    }
}
