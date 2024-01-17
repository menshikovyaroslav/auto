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
        public async Task<IActionResult> Index(string[] brandIds, int page)
        {
            if (brandIds == null || brandIds.Length == 0)
            {
                IEnumerable<Brand> brands = await _carsService.GetAllBrandsAsync();
                var model = new HomeViewModel() { Brands = brands };
                return View(model);
            }
            else
            {
                IEnumerable<Car> cars;
                IEnumerable<Brand> brands;
                Dictionary<int, Foto> photos;

                brands = await _carsService.GetAllBrandsAsync();

                var carsPaginationViewModel = _carsService.GetFilteredCarsAsync(brandIds, page);
                int[] carIds = carsPaginationViewModel.Cars.Select(car => car.Id).ToArray();

                photos = await _carsService.GetCarsMainFotosAsync(carIds);

                var model = new HomeViewModel() { Brands = brands, Cars = carsPaginationViewModel.Cars, Fotos = photos, PageViewModel = carsPaginationViewModel.PageViewModel };
                return View(model);
            }


        }

        [HttpPost]
        public async Task<HomeViewModel> Index(string[] brandIds)
        {
            IEnumerable<Car> cars;
            IEnumerable<Brand> brands;
            Dictionary<int, Foto> photos;

            brands = await _carsService.GetAllBrandsAsync();

            var carsPaginationViewModel = _carsService.GetFilteredCarsAsync(brandIds, 1);
            int[] carIds = carsPaginationViewModel.Cars.Select(car => car.Id).ToArray();

            photos = await _carsService.GetCarsMainFotosAsync(carIds);

            var model = new HomeViewModel() { Brands = brands, Cars = carsPaginationViewModel.Cars, Fotos = photos, PageViewModel = carsPaginationViewModel.PageViewModel };
            return model;
        }
    }
}
