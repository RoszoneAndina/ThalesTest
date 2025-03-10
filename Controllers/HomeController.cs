using Microsoft.AspNetCore.Mvc;

namespace ThalesTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
