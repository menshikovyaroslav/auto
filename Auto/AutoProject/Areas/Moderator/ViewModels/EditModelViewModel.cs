using Front.Areas.Cars.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class EditModelViewModel
	{
        public int Id { get; set; }
        [Required]
		public string Name { get; set; }
		public int BrandId { get; set; }
		public IEnumerable<Brand> AllBrands { get; set; }

		public EditModelViewModel()
		{
			AllBrands = new List<Brand>();
		}
	}
}
