using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.Areas.Cars.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
