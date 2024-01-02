using Front.Classes;
using Front.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
	public class LoginViewModel : ViewModel
	{
		public override string Login { get; set; }

		[Required(ErrorMessage = $"{_errorMessage}")]
		[DataType(DataType.Password)]
		public override string Password { get; set; }

		[Display(Name = "Запомнить?")]
		public bool RememberMe { get; set; }
        public Role Role { get; set; } = Role.User;
        public string? ReturnUrl { get; set; }
	}
}
