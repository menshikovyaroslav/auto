using Front.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;

namespace Front.Areas.Admin.Services
{
	public class UsersService
	{
		UserManager<User> _userManager;

		public UsersService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public List<User> GetAllUsers()
		{
			return _userManager.Users.ToList();
		}

		public async Task<bool> DeleteAsync(int? id)
		{
			return false;
		}

		public async Task<User> EditAsync(string id)
		{
			User user = await _userManager.FindByIdAsync(id);
			return user;
		}

		public async Task<bool> EditAsync(User user)
		{
			return false;
		}
	}
}
