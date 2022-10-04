using Microsoft.AspNetCore.Mvc;

namespace PollBall.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello from controller");
        }
    }
}
