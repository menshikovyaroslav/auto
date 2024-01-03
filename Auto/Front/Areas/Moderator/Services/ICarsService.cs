using Front.Areas.Cars.Models;

namespace Front.Areas.Admin.Services
{
	public interface ICarsService
    {
		public Task<IEnumerable<Brand>> GetAllBrandsAsync();
		public Task CreateBrandAsync(string name);
        public Task DeleteBrandAsync(string id);
        public Task<Brand> GetBrandAsync(int id);
        public Task EditBrandAsync(Brand brand);
    }
}
