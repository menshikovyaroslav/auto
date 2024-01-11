using Front.Models;
using Microsoft.AspNetCore.Identity;

namespace Front.Areas.Admin.ViewModels
{
	public class EditUserRolesViewModel : ViewModel
    {
		public string Id { get; set; }
		public override string Login { get; set; }
		public List<IdentityRole> AllRoles { get; set; }
		public IList<string> UserRoles { get; set; }

        public EditUserRolesViewModel()
        {
        }

        public EditUserRolesViewModel(string id, List<IdentityRole> allRoles, IList<string> userRoles)
		{
			Id = id;
			AllRoles = allRoles;
			UserRoles = userRoles;
		}
	}
}
