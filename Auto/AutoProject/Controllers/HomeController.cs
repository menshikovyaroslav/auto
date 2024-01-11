using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Cars");
        }

        [HttpGet]
        public IActionResult About()
        {
            return View("About");
        }
    }
}
