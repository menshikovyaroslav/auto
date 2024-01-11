using Front.Areas.Admin.Services;
using Front.Areas.Moderator.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Cars.Controllers
{
	[Authorize(Roles = "Moderator")]
	[Area("Moderator")]
	public class BrandsController : Controller
	{
		private ICarsService _carsService;
        IWebHostEnvironment _appEnvironment;

        public BrandsController(ICarsService carsService, IWebHostEnvironment appEnvironment)
		{
			_carsService = carsService;
            _appEnvironment = appEnvironment;
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
            var canDeleteBrand = await _carsService.CanDeleteBrandAsync(id);
            if (!canDeleteBrand)
            {
				TempData["Error"] = "Error. Cannot delete with relations.";
				return RedirectToAction("Index", "Brands");
            }
            await _carsService.DeleteBrandAsync(id);
            return RedirectToAction("Index", "Brands");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _carsService.GetBrandAsync(id);
            if (brand != null)
            {
                return View(new EditBrandViewModel() { Id = brand.Id, Name = brand.Name, Logo = brand.Logo });
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

        [HttpPost]
        public async Task<IActionResult> AddLogo(IFormFile uploadedFile, int id)
        {
            if (uploadedFile != null)
            {
                string directoryPath = Path.Combine(_appEnvironment.WebRootPath, "logos");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var extension = Path.GetExtension(uploadedFile.FileName).ToLowerInvariant();
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/logos/" + $"{id}{extension}", FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                var brand = await _carsService.GetBrandAsync(id);
                brand.Logo = $"/logos/{id}{extension}";
                await _carsService.EditBrandAsync(brand);
            }

            return RedirectToAction("Edit", "Brands", new { id = id });
        }
    }
}
