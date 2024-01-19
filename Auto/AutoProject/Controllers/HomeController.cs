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

        [HttpGet]
        public IActionResult DocumentationUser()
        {
            return View("DocumentationUser");
        }

        [HttpGet]
        public IActionResult DocumentationAdmin()
        {
            return View("DocumentationAdmin");
        }

        [HttpGet]
        public IActionResult DocumentationModerator()
        {
            return View("DocumentationModerator");
        }
    }
}
