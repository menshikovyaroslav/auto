using Front.Areas.Admin.Models;

namespace Front.Areas.Admin.Services
{
	public interface IAccountService
    {
        Task<User> GetUser(string email, string password);
        Task<User> GetUser(string email);
    }
}
