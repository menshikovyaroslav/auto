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

        public async Task DeleteBrandAsync(string id)
        {
			var brand = _db.Brands.FirstOrDefault();
            _db.Brands.Remove(brand);
            await _db.SaveChangesAsync();
        }

        public async Task EditBrandAsync(Brand brand)
        {
            var entity = await _db.Brands.SingleOrDefaultAsync(b => b.Id == brand.Id);
            entity.Name = brand.Name;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
		{
			return await _db.Brands.ToListAsync();
		}

        public async Task<Brand> GetBrandAsync(int id)
        {
            return await _db.Brands.SingleOrDefaultAsync(b => b.Id == id);
        }
    }
}
