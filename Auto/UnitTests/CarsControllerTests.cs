using AutoProject.ViewModels;
using Dom.Extensions;
using Front.Areas.Admin.Services;
using Front.Areas.Cars.Models;
using Front.Controllers;
using Front.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Diagnostics.Metrics;

namespace UnitTests
{
    public class CarsControllerTests
    {
        [Fact]
        public async Task IndexReturnsAllOrdersAndCorrectTypes()
        {
            // Arrange:
            var carsService = Substitute.For<ICarsService>();
            carsService.GetAllBrandsAsync().Returns(GetImitatedBrands());
            carsService.GetFilteredCarsAsync(Arg.Any<string[]>(), 1).Returns(GetFilteredCars());
            carsService.GetCarsMainPhotosAsync(Arg.Any<int[]>()).Returns(GetCarsMainPhotos());

            var controller = new CarsController(carsService);

            // Act:
            var result = await controller.Index(new string[3] { "1", "2", "3" }, 1);

            // Assert:
            var isViewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(isViewResult.Model);
            Assert.Equal(GetImitatedBrands().Count(), model.Brands.Count());
        }

        private IEnumerable<Brand> GetImitatedBrands()
        {
            return Substitute.For<List<Brand>>();
        }

        public CarsPaginationViewModel GetFilteredCars()
        {
            return Substitute.For<CarsPaginationViewModel>();
        }

        public Dictionary<int, Photo> GetCarsMainPhotos()
        {
            return Substitute.For<Dictionary<int, Photo>>();
        }
    }
}