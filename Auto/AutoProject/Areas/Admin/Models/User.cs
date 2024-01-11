using Front.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Areas.Admin.Models
{
    [Table("Users")]
    public class User : IdentityUser
	{
		public User()
		{
		}

		public User(ViewModel model)
		{
			UserName = model.Login;
			Email = model.Email;
		}
	}
}
