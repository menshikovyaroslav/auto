using Front.Areas.Admin.Services;
using Front.Areas.Cars.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
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
            IEnumerable<Brand> brands = await _carsService.GetAllBrandsAsync();
            var model = new HomeViewModel() { Brands = brands };
            return View(model);
        }

        [HttpPost]
        public async Task<HomeViewModel> Index(string[] brandIds)
        {
            IEnumerable<Car> cars;
            IEnumerable<Brand> brands;
            Dictionary<int, Foto> photos;

            brands = await _carsService.GetAllBrandsAsync();

            cars = _carsService.GetFilteredCarsAsync(brandIds);
            int[] carIds = cars.Select(car => car.Id).ToArray();

            photos = await _carsService.GetCarsMainFotosAsync(carIds);

            var model = new HomeViewModel() { Brands = brands, Cars = cars, Fotos = photos };
            return model;
        }
    }
}
