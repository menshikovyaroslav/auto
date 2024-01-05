using Front.Areas.Admin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Front.Areas.Cars.Controllers
{
	[Authorize(Roles = "Moderator")]
	[Area("Moderator")]
	public class CatalogController : Controller
	{
		private ICarsService _carsService;

		public CatalogController(ICarsService carsService)
		{
			_carsService = carsService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View(_carsService.GetAllBrandsAsync());
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Brands()
		{
			return View("Index");
		}
	}
}
