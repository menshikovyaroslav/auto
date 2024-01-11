using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.Areas.Cars.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

            brands = await _carsService.GetAllBrandsAsync();
            cars = _carsService.GetFilteredCarsAsync(brandIds);

            var model = new HomeViewModel() { Brands = brands, Cars = cars };
            return model;
        }
    }
}
