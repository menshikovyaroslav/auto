using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Front.Areas.Cars.Models
{
    [Table("Colors")]
    public class Color
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
