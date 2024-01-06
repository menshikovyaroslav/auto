using Front.Areas.Cars.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class AddFotoViewModel
    {
        public int CarId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Foto> Fotos { get; set; }

        public AddFotoViewModel()
        {
            Fotos = new List<Foto>();
        }
    }
}
