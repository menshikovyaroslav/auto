using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
	public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword1 { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword2 { get; set; }
    }
}
