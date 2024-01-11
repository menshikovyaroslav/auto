using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Front.Areas.Cars.Models
{
    [Table("Models")]
    public class Model
    {
		[Key]
		public int Id { get; set; }
        [JsonIgnore]
        public Brand Brand { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public int Year { get; set; }

		public override string ToString()
		{
			return $"{Brand.Name} ({Name})";
		}
	}
}
