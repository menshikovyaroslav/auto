using System.ComponentModel.DataAnnotations;

namespace Front.Areas.Moderator.ViewModels
{
	public class EditBrandViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Logo { get; set; }
    }
}
