﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntroductionIntoASPmvc.Areas.Admin.Models
{
    [Table("Cars")]
    public class Car
    {
		[Key]
		public int Id { get; set; }
        public Model Model { get; set; }
        public int? Year { get; set; }
        public int? Distance { get; set; }
        public string? Color { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
