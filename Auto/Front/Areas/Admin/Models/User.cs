using Front.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front.Areas.Admin.Models
{
    [Table("Users")]
    public class User
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public Role Role { get; set; } = Role.User;
	}
}
