using Microsoft.AspNetCore.Mvc;

namespace BookMyTurfwebservices.Controllers
{
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
