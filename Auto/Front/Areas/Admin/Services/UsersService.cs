using Front.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
			//if (id != null)
			//{
   //             User? user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
			//	if (user != null)
			//	{
			//		_db.Users.Remove(user);
			//		await _db.SaveChangesAsync();
			//		return true;
			//	}
			//}
			return false;
		}

		public async Task<User> EditAsync(string id)
		{
			User user = await _userManager.FindByIdAsync(id);
			return user;
		}

		public async Task<bool> EditAsync(User user)
		{
			//if (user != null)
			//{
			//	_db.Users.Update(user);
			//	await _db.SaveChangesAsync();
			//	return true;
			//}
			return false;
		}
	}
}
