using Front.Areas.Cars.Models;

namespace Front.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public Dictionary<int, Foto> Fotos { get; set; }

        public HomeViewModel()
        {
            Cars = new List<Car>();
            Brands = new List<Brand>();
            Fotos = new Dictionary<int, Foto>();
        }
    }
}
