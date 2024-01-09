using Dom.Extensions;
using Front.Areas.Cars.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Front.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Brand> Brands { get; set; }

        public HomeViewModel()
        {
            Cars = new List<Car>();
            Brands = new List<Brand>();
        }
    }
}
