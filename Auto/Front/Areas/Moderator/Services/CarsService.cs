using Front.Areas.Admin.Models;
using Front.Areas.Cars.Models;
using Front.Areas.Moderator.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
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

		public async Task CreateFotoAsync(Foto foto)
		{
			_db.Fotos.Add(foto);
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

        public async Task DeleteFotoAsync(int id)
        {
            var foto = _db.Fotos.FirstOrDefault(f => f.Id == id);
            _db.Fotos.Remove(foto);
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

        public async Task<IEnumerable<Foto>> GetCarFotosAsync(int carId)
        {
			return await _db.Fotos.Where(f => f.CarId == carId).ToListAsync();
        }

        public async Task<int> GetCarIdByFotoIdAsync(int fotoId)
        {
            Foto foto = await _db.Fotos.SingleOrDefaultAsync(f => f.Id == fotoId);
            return foto != null ? foto.CarId : 0;
        }

        public async Task<Color> GetColorAsync(int id)
		{
			return await _db.Colors.SingleOrDefaultAsync(c => c.Id == id);
		}

        public async Task<Foto> GetFotoByIdAsync(int fotoId)
        {
            return await _db.Fotos.SingleOrDefaultAsync(f => f.Id == fotoId);
        }

        public async Task<Model> GetModelAsync(int id)
		{
			return await _db.Models.Include(m => m.Brand).SingleOrDefaultAsync(m => m.Id == id);
		}

		//public async Task<EditCarViewModel> GetSelectedBrandModelsAsync(EditCarViewModel model)
		//{
		//	var thisBrand = await _db.Brands.Include(o => o.Models).FirstOrDefaultAsync(o => o.Id == model.BrandId);

		//	if (thisBrand != null)
		//	{
		//		SelectedBrandModelsDto dto = new SelectedBrandModelsDto();

		//		var modelsForSelectList = thisBrand.Models.Select(o => new
		//			{
		//				ModelId = o.Id,
		//				ModelName = o.Name
		//			}).ToList();

		//		dto.SelectedBrandModels = new SelectList(modelsForSelectList, "ModelId", "ModelName");

		//		model.Dto = dto;
		//		return model;
		//	}

		//	return null;
		//}

        public async Task<IEnumerable<Model>> GetSelectedBrandModelsAsync(int brandId)
        {
            var brand = await _db.Brands.Include(o => o.Models).FirstOrDefaultAsync(o => o.Id == brandId);

            if (brand != null)
            {
                return brand.Models.OrderBy(m => m.Name);
            }

			return null;
        }
    }
}
