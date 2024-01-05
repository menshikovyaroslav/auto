using Front.Areas.Admin.Models;
using Front.Areas.Cars.Models;
using Microsoft.EntityFrameworkCore;

namespace Front.Areas.Admin.Services
{
	public class CarsService : ICarsService
	{
		private ApplicationContext _db;

		public CarsService(ApplicationContext db)
		{
			_db = db;
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

        public async Task CreateModelAsync(Model model)
        {
            _db.Models.Add(model);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteBrandAsync(int id)
        {
			var brand = _db.Brands.FirstOrDefault(b => b.Id == id);
            _db.Brands.Remove(brand);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = _db.Cars.FirstOrDefault(b => b.Id == id);
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteColorAsync(int id)
        {
            var color = _db.Colors.FirstOrDefault(c => c.Id == id);
            _db.Colors.Remove(color);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int id)
		{
			var model = _db.Models.FirstOrDefault(m => m.Id == id);
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
			return await _db.Brands.Include(b => b.Models).OrderBy(b => b.Name).ToListAsync();
		}

		public async Task<IEnumerable<Car>> GetAllCarsAsync()
		{
			return await _db.Cars.Include(m => m.Model).Include(c => c.Color).Include(m => m.Model.Brand).ToListAsync();
		}

        public async Task<IEnumerable<Color>> GetAllColorsAsync()
        {
            return await _db.Colors.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
		{
			return await _db.Models.Include(m => m.Brand).OrderBy(m => m.Brand.Name).ThenBy(m => m.Name).ToListAsync();
        }

		public async Task<Brand> GetBrandAsync(int id)
        {
            return await _db.Brands.Include(b => b.Models).SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _db.Cars.Include(c => c.Color).Include(c => c.Model).SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Color> GetColorAsync(int id)
        {
            return await _db.Colors.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Model> GetModelAsync(int id)
        {
            return await _db.Models.Include(m => m.Brand).SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
