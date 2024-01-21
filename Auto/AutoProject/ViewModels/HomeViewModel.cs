using Front.Areas.Cars.Models;

namespace Front.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public HomeViewModel()
        {
            Cars = new List<Car>();
            Brands = new List<Brand>();
            PageViewModel = new PageViewModel(0, 1, 5);
        }
    }
}
