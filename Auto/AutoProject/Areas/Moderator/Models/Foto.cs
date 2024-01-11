using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Areas.Cars.Models
{
    [Table("Fotos")]
    public class Foto
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
