﻿using AutoProject.ViewModels;
using Front.Areas.Cars.Models;

namespace Front.Areas.Admin.Services
{
	public interface ICarsService
    {
		public Task<IEnumerable<Brand>> GetAllBrandsAsync();
		public Task CreateBrandAsync(string name);
        public Task DeleteBrandAsync(int id);
        public Task<Brand> GetBrandAsync(int id);
        public Task EditBrandAsync(Brand brand);
        public Task<bool> CanDeleteBrandAsync(int id);

        public Task<IEnumerable<Color>> GetAllColorsAsync();
        public Task CreateColorAsync(Color color);
        public Task DeleteColorAsync(int id);
        public Task<Color> GetColorAsync(int id);
        public Task EditColorAsync(Color color);
		public Task<bool> CanDeleteColorAsync(int id);

		public Task<IEnumerable<Model>> GetAllModelsAsync();
        public Task<IEnumerable<Model>> GetSelectedBrandModelsAsync(int brandId);
        public Task CreateModelAsync(Model model);
		public Task DeleteModelAsync(int id);
		public Task<Model> GetModelAsync(int id);
        public Task EditModelAsync(Model model);
		public Task<bool> CanDeleteModelAsync(int id);

		public Task<IEnumerable<Car>> GetAllCarsAsync();
        public CarsPaginationViewModel GetFilteredCarsAsync(string[] searchBrandId, int page);
        public Task CreateCarAsync(Car car);
        public Task DeleteCarAsync(int id);
        public Task<Car> GetCarAsync(int id);
        public Task EditCarAsync(Car car);

        public Task CreatePhotoAsync(Photo photo);
        public Task<IEnumerable<Photo>> GetCarPhotosAsync(int carId);
        public Task DeletePhotoAsync(int id);
        public Task<int> GetCarIdByPhotoIdAsync(int photoId);
        public Task<Photo> GetPhotoByIdAsync(int photoId);
        public Task<Dictionary<int, Photo>> GetCarsMainPhotosAsync(int[] carIds);
    }
}
