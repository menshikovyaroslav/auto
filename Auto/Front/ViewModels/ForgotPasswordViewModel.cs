using Front.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
	public class ForgotPasswordViewModel
	{
        [Required]
        public string Email { get; set; }
    }
}
