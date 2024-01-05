using Front.Areas.Admin.Services;
using Front.Areas.Moderator.ViewModels;
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
		public async Task<IActionResult> Index()
		{
			var cars = await _carsService.GetAllCarsAsync();
			return View(cars);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            var brands = await _carsService.GetAllBrandsAsync();
            var models = await _carsService.GetAllModelsAsync();
            var colors = await _carsService.GetAllColorsAsync();
            var car = new CreateCarViewModel() { AllBrands = brands, AllModels = models, AllColors = colors };

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarViewModel createModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _carsService.GetModelAsync(createModel.ModelId);
                var color = await _carsService.GetColorAsync(createModel.ColorId);
                await _carsService.CreateCarAsync(new Models.Car() { Model = model, Year = createModel.Year, Distance = createModel.Distance, Color = color });
                return RedirectToAction("Index", "Catalog");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carsService.GetCarAsync(id);
            if (car != null)
            {
                var brands = await _carsService.GetAllBrandsAsync();
                var models = await _carsService.GetAllModelsAsync();
                var colors = await _carsService.GetAllColorsAsync();

                return View(new EditCarViewModel() { AllBrands = brands, AllModels = models, AllColors = colors, BrandId = car.Model.Brand.Id, ModelId = car.Model.Id, ColorId = car.Color.Id, Year = car.Year, Distance = car.Distance });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var car = await _carsService.GetCarAsync(editModel.Id);

                var model = await _carsService.GetModelAsync(editModel.ModelId);
                var brand = await _carsService.GetBrandAsync(editModel.BrandId);
                var color = await _carsService.GetColorAsync(editModel.ColorId);

                car.Model = model;
                car.Color = color;
                car.Distance = editModel.Distance;
                car.Year = editModel.Year;

                await _carsService.EditCarAsync(car);

                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carsService.DeleteCarAsync(id);
            return RedirectToAction("Index", "Catalog");
        }
    }
}
