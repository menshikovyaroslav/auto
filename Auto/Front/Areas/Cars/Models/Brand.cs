using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Areas.Cars.Models
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
    }
}
