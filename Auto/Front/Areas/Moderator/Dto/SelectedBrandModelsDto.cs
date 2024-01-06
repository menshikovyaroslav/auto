using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Front.Areas.Cars.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front.Areas.Cars.Models
{
    public class SelectedBrandModelsDto
	{
		public SelectList SelectedBrandModels { get; set; }
		public string SelectedBrand { get; set; }

        public SelectedBrandModelsDto()
        {
            SelectedBrandModels = new SelectList(new List<Model>());
        }
    }
}
