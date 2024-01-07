using Front.Models;
using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
	public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword1 { get; set; }

        public string NewPassword2 { get; set; }
    }
}
