using Front.Areas.Admin.Services;
using Front.Areas.Cars.Models;
using Front.Areas.Moderator.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Abstractions;

namespace Front.Areas.Cars.Controllers
{
    [Authorize(Roles = "Moderator")]
    [Area("Moderator")]
    public class CatalogController : Controller
    {
        private ICarsService _carsService;
        IWebHostEnvironment _appEnvironment;
        private readonly IFileSystem _fileSystem;

        public CatalogController(ICarsService carsService, IWebHostEnvironment appEnvironment, IFileSystem fileSystem)
        {
            _carsService = carsService;
            _appEnvironment = appEnvironment;
            _fileSystem = fileSystem;
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
                var models = await _carsService.GetSelectedBrandModelsAsync(car.Model.Brand.Id);
                var colors = await _carsService.GetAllColorsAsync();
                var photos = await _carsService.GetCarPhotosAsync(id);

                return View(new EditCarViewModel() { AllBrands = brands, AllModels = models, AllColors = colors, BrandId = car.Model.Brand.Id, ModelId = car.Model.Id, ColorId = car.Color.Id, Year = car.Year, Distance = car.Distance, Id = id, Photos = photos });
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
        public async Task<IActionResult> UpdateModels(int brandId)
        {
            var models = await _carsService.GetSelectedBrandModelsAsync(brandId);
            return Json(models);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carsService.DeleteCarAsync(id);
            return RedirectToAction("Index", "Catalog");
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(IFormFile uploadedFile, int id)
        {
            if (uploadedFile != null)
            {
                string directoryPath = Path.Combine(_appEnvironment.WebRootPath, "photos", id.ToString());

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var photoGuid = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(uploadedFile.FileName).ToLowerInvariant();

                string path = $"/photos/{id}/{photoGuid}{extension}";
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Photo photo = new Photo() { Name = $"{photoGuid}{extension}", Path = path, CarId = id };

                await _carsService.CreatePhotoAsync(photo);
            }

            return RedirectToAction("Edit", "Catalog", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _carsService.GetPhotoByIdAsync(id);
            var carId = await _carsService.GetCarIdByPhotoIdAsync(id);
            await _carsService.DeletePhotoAsync(id);

            string directoryPath = Path.Combine(_appEnvironment.WebRootPath, "photos", carId.ToString());

            if (Directory.Exists(directoryPath))
            {
                string path = Path.Combine(_appEnvironment.WebRootPath, "photos", carId.ToString(), photo.Name);

                if (_fileSystem.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return RedirectToAction("Edit", "Catalog", new
            {
                id = carId
            });
        }
    }
}
