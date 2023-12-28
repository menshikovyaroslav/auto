using Front.Areas.Admin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Front.Areas.Cars.Controllers
{
	//[Authorize(Policy = "ModeratorArea")]
	//[Area("Moderator")]
	public class CarsController : Controller
	{
		private ICarsService _carsService;

		public CarsController(ICarsService carsService)
		{
			_carsService = carsService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var result = await _carsService.GetAllBrandsAsync();
			return View("Index", result);
		}
	}
}
