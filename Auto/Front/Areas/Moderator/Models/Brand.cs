using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Front.Areas.Cars.Models
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public ICollection<Model> Models { get; set; }

        public Brand()
        {
            Models = new List<Model>();
        }
    }
}
