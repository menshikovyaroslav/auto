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

		public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
		{
			return await _db.Brands.ToListAsync();
		}
	}
}
