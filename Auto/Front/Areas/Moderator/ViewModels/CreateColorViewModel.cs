using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class CreateColorViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
