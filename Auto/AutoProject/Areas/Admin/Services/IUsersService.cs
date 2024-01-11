using Front.Areas.Admin.Models;
using Front.Areas.Admin.ViewModels;

namespace Front.Areas.Admin.Services
{
	public interface IUsersService
	{
		public List<User> GetAllUsers();
		public Task<bool> DeleteAsync(int? id);
		public Task<User> EditAsync(string id);
		public Task<bool> EditAsync(EditViewModel editViewModel);
	}
}
