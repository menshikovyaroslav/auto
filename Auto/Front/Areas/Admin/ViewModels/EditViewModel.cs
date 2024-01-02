using Front.Models;
using Front.Classes;
using Front.Areas.Admin.Models;

namespace Front.Areas.Admin.ViewModels
{
	public class EditViewModel : ViewModel
    {
        public EditViewModel(User user)
        {
            Login = user.UserName;
            Email = user.Email;
            Role = user.Role;
        }

        public string Id { get; set; }

        public override string Login { get; set; }

        public override string Email { get; set; }

		public Role Role { get; set; }
	}
}
