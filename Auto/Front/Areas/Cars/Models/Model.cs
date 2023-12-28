using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Front.Areas.Cars.Models;

namespace Front.Areas.Cars.Models
{
    [Table("Models")]
    public class Model
    {
		[Key]
		public int Id { get; set; }
        public Brand Brand { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public int Year { get; set; }
    }
}
