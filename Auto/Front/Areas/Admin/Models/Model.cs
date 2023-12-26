using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Areas.Admin.Models
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
