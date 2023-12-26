using IntroductionIntoASPmvc.Areas.Admin.Models;

namespace IntroductionIntoASPmvc.Areas.Admin.Services
{
	public interface IAccountService
    {
        Task<User> GetUser(string email, string password);
        Task<User> GetUser(string email);
    }
}
