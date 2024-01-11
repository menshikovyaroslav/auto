using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> About()
        {
            return View("About");
        }
    }
}
