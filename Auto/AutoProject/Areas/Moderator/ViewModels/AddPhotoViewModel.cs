using Front.Areas.Cars.Models;

namespace Front.Areas.Moderator.ViewModels
{
	public class AddPhotoViewModel
    {
        public int CarId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Photo> Photos { get; set; }

        public AddPhotoViewModel()
        {
            Photos = new List<Photo>();
        }
    }
}
