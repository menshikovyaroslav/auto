using AutoProject.ViewModels;
using Dom.Extensions;
using Front.Areas.Admin.Models;
using Front.Areas.Cars.Models;
using Front.ViewModels;
using Microsoft.EntityFrameworkCore;
using Color = Front.Areas.Cars.Models.Color;

namespace Front.Areas.Admin.Services
{
	public class CarsService : ICarsService
	{
		private ApplicationContext _db;

		public CarsService(ApplicationContext db)
		{
			_db = db;
		}

        public async Task<bool> CanDeleteBrandAsync(int id)
        {
            var hasModels = await _db.Models
				.AnyAsync(m => m.Brand.Id == id);
            return !hasModels;
        }

		public async Task<bool> CanDeleteColorAsync(int id)
		{
			var hasCars = await _db.Cars
				.AnyAsync(c => c.Color.Id == id);
			return !hasCars;
		}

		public async Task<bool> CanDeleteModelAsync(int id)
		{
			var hasCars = await _db.Cars
				.AnyAsync(c => c.Model.Id == id);
			return !hasCars;
		}

		public async Task CreateBrandAsync(string name)
		{
			_db.Brands.Add(new Brand { Name = name });
			await _db.SaveChangesAsync();
		}

		public async Task CreateCarAsync(Car car)
		{
			_db.Cars.Add(car);
			await _db.SaveChangesAsync();
		}

		public async Task CreateColorAsync(Color color)
		{
			_db.Colors.Add(color);
			await _db.SaveChangesAsync();
		}

		public async Task CreatePhotoAsync(Photo photo)
		{
			_db.Photos.Add(photo);
			await _db.SaveChangesAsync();
		}

		public async Task CreateModelAsync(Model model)
		{
			_db.Models.Add(model);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteBrandAsync(int id)
		{
			var brand = await _db.Brands.FirstOrDefaultAsync(b => b.Id == id);
			_db.Brands.Remove(brand);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteCarAsync(int id)
		{
			var car = await _db.Cars.FirstOrDefaultAsync(b => b.Id == id);
			_db.Cars.Remove(car);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteColorAsync(int id)
		{
			var color = await _db.Colors.FirstOrDefaultAsync(c => c.Id == id);
			_db.Colors.Remove(color);
			await _db.SaveChangesAsync();
		}

        public async Task DeletePhotoAsync(int id)
        {
            var photo = await _db.Photos.FirstOrDefaultAsync(f => f.Id == id);
            _db.Photos.Remove(photo);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int id)
		{
			var model = await _db.Models.FirstOrDefaultAsync(m => m.Id == id);
			_db.Models.Remove(model);
			await _db.SaveChangesAsync();
		}

		public async Task EditBrandAsync(Brand brand)
		{
			_db.Brands.Update(brand);
			await _db.SaveChangesAsync();
		}

		public async Task EditCarAsync(Car car)
		{
			_db.Cars.Update(car);
			await _db.SaveChangesAsync();
		}

		public async Task EditColorAsync(Color color)
		{
			_db.Colors.Update(color);
			await _db.SaveChangesAsync();
		}

		public async Task EditModelAsync(Model model)
		{
			_db.Models.Update(model);
			await _db.SaveChangesAsync();
		}

		public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
		{
			return await _db.Brands
				//.Include(b => b.Models)
				.OrderBy(b => b.Name)
				.ToListAsync();
		}

		public async Task<IEnumerable<Car>> GetAllCarsAsync()
		{
			return await _db.Cars
				.Include(m => m.Model)
				.Include(c => c.Color)
				.Include(m => m.Model.Brand)
				.ToListAsync();
		}

        public CarsPaginationViewModel GetFilteredCarsAsync(string[] searchBrandIds, int page)
        {
            int pageSize = 5;

            List<Car> filteredCars = new List<Car>();

            if (searchBrandIds != null && searchBrandIds.Length > 0)
			{
                var brandIds = searchBrandIds.Select(s => s.ToInt()).ToList();
                var carsInCondition = _db.Cars
                    .Include(c => c.Model)
                    .Include(c => c.Model.Brand)
                    .Include(c => c.Color)
                    .Where(c => brandIds.Contains(c.Model.Brand.Id));

                filteredCars.AddRange(carsInCondition);
			}

            var count = filteredCars.Count();
            filteredCars = filteredCars.Skip((page - 1) * pageSize).Take(pageSize).ToList();

			var pageModel = new PageViewModel(count, page, pageSize);
			var carsPaginationViewModel = new CarsPaginationViewModel() { PageViewModel = pageModel, Cars = filteredCars };

            return carsPaginationViewModel;
        }

        public async Task<IEnumerable<Color>> GetAllColorsAsync()
		{
			return await _db.Colors.OrderBy(c => c.Name).ToListAsync();
		}

		public async Task<IEnumerable<Model>> GetAllModelsAsync()
		{
			return await _db.Models
				.Include(m => m.Brand)
				.OrderBy(m => m.Brand.Name)
				.ThenBy(m => m.Name).ToListAsync();
		}

		public async Task<Brand> GetBrandAsync(int id)
		{
			return await _db.Brands
				//.Include(b => b.Models)
				.SingleOrDefaultAsync(b => b.Id == id);
		}

		public async Task<Car> GetCarAsync(int id)
		{
			return await _db.Cars
				.Include(c => c.Color)
				.Include(c => c.Model)
				.SingleOrDefaultAsync(b => b.Id == id);
		}

        public async Task<IEnumerable<Photo>> GetCarPhotosAsync(int carId)
        {
			return await _db.Photos
				.Where(f => f.CarId == carId)
				.ToListAsync();
        }

        public async Task<int> GetCarIdByPhotoIdAsync(int photoId)
        {
            Photo photo = await _db.Photos.SingleOrDefaultAsync(f => f.Id == photoId);
            return photo != null ? photo.CarId : 0;
        }

        public async Task<Color> GetColorAsync(int id)
		{
			return await _db.Colors.SingleOrDefaultAsync(c => c.Id == id);
		}

        public async Task<Photo> GetPhotoByIdAsync(int photoId)
        {
            return await _db.Photos.SingleOrDefaultAsync(f => f.Id == photoId);
        }

        public async Task<Model> GetModelAsync(int id)
		{
			return await _db.Models
				.Include(m => m.Brand)
				.SingleOrDefaultAsync(m => m.Id == id);
		}

        public async Task<IEnumerable<Model>> GetSelectedBrandModelsAsync(int brandId)
        {
            var models = await _db.Models
				.Include(o => o.Brand)
				.Where(o => o.Brand.Id == brandId).ToListAsync();

			return models;
        }

        public async Task<Dictionary<int, Photo>> GetCarsMainPhotosAsync(int[] carIds)
        {
            var result = new Dictionary<int, Photo>();
            var photos = await _db.Photos
                                    .Where(f => carIds.Contains(f.CarId))
                                    .GroupBy(f => f.CarId)
                                    .Select(group => group.OrderBy(f => f.Id).First())
                                    .ToListAsync();

            foreach (var photo in photos)
            {
                result[photo.CarId] = photo;
            }

            return result;
        }
    }
}
