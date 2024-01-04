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


        public Task<IEnumerable<Model>> GetAllModelsAsync();
        public Task CreateModelAsync(Model model);
		public Task DeleteModelAsync(int id);
		public Task<Model> GetModelAsync(int id);
        public Task EditModelAsync(Model model);
    }
}
