using Front.Models;
using Front.Areas.Admin.Models;

namespace Front.Areas.Admin.ViewModels
{
	public class EditViewModel : ViewModel
    {
        public EditViewModel(User user)
        {
            Login = user.UserName;
            Email = user.Email;
        }

        public string Id { get; set; }

        public override string Login { get; set; }

        public override string Email { get; set; }
	}
}
