using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class CreateBrandViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
