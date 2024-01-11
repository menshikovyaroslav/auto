using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class EditColorViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
