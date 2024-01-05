using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.Areas.Admin.ViewModels;
using Front.Areas.Moderator.ViewModels;
using Front.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Front.Areas.Cars.Controllers
{
	[Authorize(Roles = "Moderator")]
	[Area("Moderator")]
	public class BrandsController : Controller
	{
		private ICarsService _carsService;

		public BrandsController(ICarsService carsService)
		{
			_carsService = carsService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var brands = await _carsService.GetAllBrandsAsync();
            return View(brands);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _carsService.CreateBrandAsync(model.Name);
                return RedirectToAction("Index", "Brands");
            }
            return View(model.Name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carsService.DeleteBrandAsync(id);
            return RedirectToAction("Index", "Brands");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _carsService.GetBrandAsync(id);
            if (brand != null)
            {
                return View(new EditBrandViewModel() { Id = brand.Id, Name = brand.Name });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = await _carsService.GetBrandAsync(model.Id);
                brand.Name = model.Name;
                await _carsService.EditBrandAsync(brand);

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
