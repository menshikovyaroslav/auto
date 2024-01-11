using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.Areas.Cars.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        private ICarsService _carsService;

        public HomeController(ICarsService carsService)
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

        [HttpGet]
        public async Task<IActionResult> About()
        {
            return View("About");
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


            //var jsonObject = JsonSerializer.Deserialize<JsonObject>(brandId.FromBase64());

            //var selectedBrands = new List<Brand>();

            //if (jsonObject["selectedBrands"] != null && jsonObject["selectedBrands"].ToString() != "[]")
            //    foreach (var selectedBrand in jsonObject["selectedBrands"] as JsonArray)
            //    {

            //    }

            //var seletedBrand = await _carsService.GetBrandAsync(jsonObject["selectedBrand"].ToString().ToInt());
            //selectedBrands.Add(seletedBrand);

            //var brands = await _carsService.GetAllBrandsAsync();
            //var model = new HomeViewModel() { Brands = brands, SelectedBrands = selectedBrands };
            //return View(model);
        }
    }
}
