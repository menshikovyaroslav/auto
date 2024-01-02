using Front.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Front.Areas.Admin.Services
{
	public class UsersService: IUsersService
	{
		private ApplicationContext _db;

		public UsersService(ApplicationContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			//if (_db.Users.Count() != 0)
				return await _db.Users.ToListAsync();
			//else
				return null;
		}

		public async Task<bool> CreateAsync(User user)
		{
			if (user != null)
			{
				_db.Users.Add(user);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
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

		public async Task<User> EditAsync(int? id)
		{
            User? user = null;
			//if (id != null)
			//{
			//	user = await _db.Users.FirstOrDefaultAsync(_ => _.Id == id);
			//}
			return user;
		}

		public async Task<bool> EditAsync(User user)
		{
			if (user != null)
			{
				_db.Users.Update(user);
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
