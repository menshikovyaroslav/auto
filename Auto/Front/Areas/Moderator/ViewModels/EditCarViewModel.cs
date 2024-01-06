using Front.Areas.Cars.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class EditCarViewModel
	{
        public int Id { get; set; }
		public int BrandId { get; set; }

        [Required]
		public int ModelId { get; set; }
		public int ColorId { get; set; }
		public int? Year { get; set; }
		public int? Distance { get; set; }
        public IEnumerable<Brand> AllBrands { get; set; }
		public IEnumerable<Model> AllModels { get; set; }
		public IEnumerable<Color> AllColors { get; set; }
        public IEnumerable<Foto> CarFotos { get; set; }
        public List<int> AllYears { get; set; }

		public EditCarViewModel()
		{
			AllBrands = new List<Brand>();
			AllModels = new List<Model>();
			AllColors = new List<Color>();
            CarFotos = new List<Foto>();

            AllYears = new List<int>();
			for (int i = 2000; i <= DateTime.Now.Year + 1; i++)
			{
				AllYears.Add(i);
			}
		}
	}
}
