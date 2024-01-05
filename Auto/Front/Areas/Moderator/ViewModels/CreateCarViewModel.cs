﻿using Front.Areas.Cars.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class CreateCarViewModel
	{
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ModelId { get; set; }
        public int ColorId { get; set; }
		public int Year { get; set; }
		public int Distance { get; set; }
		public IEnumerable<Brand> AllBrands { get; set; }
		public IEnumerable<Model> AllModels { get; set; }
        public IEnumerable<Color> AllColors { get; set; }
        public List<int> AllYears { get; set; }

        public CreateCarViewModel()
        {
            AllBrands = new List<Brand>();
			AllModels = new List<Model>();
            AllColors = new List<Color>();

            AllYears = new List<int>();
            for (int i = 2000; i <= DateTime.Now.Year + 1; i++)
            {
                AllYears.Add(i);
            }
		}
    }
}
