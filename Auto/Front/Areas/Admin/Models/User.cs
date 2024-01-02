using Front.Classes;
using Front.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
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
			//_userPhotos = new List<string>();
			//_friends = new List<string>();

			UserName = model.Login;
			Email = model.Email;
			Role = Role.User;
		}

		public Role Role { get; set; }
	}
}
