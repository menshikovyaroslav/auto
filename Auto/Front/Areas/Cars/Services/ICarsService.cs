using Front.Areas.Cars.Models;

namespace Front.Areas.Admin.Services
{
	public interface ICarsService
    {
		public Task<IEnumerable<Brand>> GetAllBrandsAsync();
	}
}
