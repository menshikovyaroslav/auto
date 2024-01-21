using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Areas.Cars.Models
{
    [Table("Cars")]
    public class Car
    {
		[Key]
		public int Id { get; set; }
        public Model Model { get; set; }
        public int? Year { get; set; }
        public int? Distance { get; set; }
        public Color Color { get; set; }
        public string? AdditionalInfo { get; set; }
        public List<Photo> Photos { get; set; }

        public Car()
        {
            Photos = new List<Photo>();
        }
    }
}
