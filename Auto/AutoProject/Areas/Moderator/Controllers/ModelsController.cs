using Front.Areas.Admin.Services;
using Front.Areas.Moderator.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Cars.Controllers
{
	[Authorize(Roles = "Moderator")]
	[Area("Moderator")]
	public class ModelsController : Controller
	{
		private ICarsService _carsService;

		public ModelsController(ICarsService carsService)
		{
			_carsService = carsService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var models = await _carsService.GetAllModelsAsync();
            return View(models);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            var brands = await _carsService.GetAllBrandsAsync();
            var model = new CreateModelViewModel() { AllBrands = brands };

            return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = await _carsService.GetBrandAsync(model.BrandId);
                await _carsService.CreateModelAsync(new Models.Model() { Name = model.Name, Brand = brand });
                return RedirectToAction("Index", "Models");
            }
            return View(model.Name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
			var canDeleteModel = await _carsService.CanDeleteModelAsync(id);
			if (!canDeleteModel)
			{
				TempData["Error"] = "Error. Cannot delete with relations.";
				return RedirectToAction("Index", "Models");
			}

			await _carsService.DeleteModelAsync(id);
            return RedirectToAction("Index", "Models");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _carsService.GetModelAsync(id);
            if (model != null)
            {
                var brands = await _carsService.GetAllBrandsAsync();

                return View(new EditModelViewModel() { AllBrands = brands, BrandId = model.Brand.Id, Name = model.Name });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditModelViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _carsService.GetModelAsync(editModel.Id);
                var brand = await _carsService.GetBrandAsync(editModel.BrandId);
                model.Name = editModel.Name;
                model.Brand = brand;

                await _carsService.EditModelAsync(model);

                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
