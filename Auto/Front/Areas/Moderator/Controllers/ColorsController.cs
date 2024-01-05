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
	public class ColorsController : Controller
	{
		private ICarsService _carsService;

		public ColorsController(ICarsService carsService)
		{
			_carsService = carsService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var colors = await _carsService.GetAllColorsAsync();
            return View(colors);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _carsService.CreateColorAsync(new Models.Color() { Name = model.Name });
                return RedirectToAction("Index", "Colors");
            }
            return View(model.Name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carsService.DeleteColorAsync(id);
            return RedirectToAction("Index", "Colors");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var color = await _carsService.GetColorAsync(id);
            if (color != null)
            {
                return View(new EditColorViewModel() { Id = color.Id, Name = color.Name });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var color = await _carsService.GetColorAsync(model.Id);
                color.Name = model.Name;
                await _carsService.EditColorAsync(color);

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
