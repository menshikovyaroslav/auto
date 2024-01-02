using Front.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
	public class RegisterViewModel: ViewModel
    {
		public override string Login { get; set; }

        [DataType(DataType.Password)]
        public override string Password { get; set; }

        public override string Email { get; set; }
    }
}
