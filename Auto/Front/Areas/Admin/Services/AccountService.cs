using IntroductionIntoASPmvc.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace IntroductionIntoASPmvc.Areas.Admin.Services
{
    public class AccountService : IAccountService
    {
        private ApplicationContext _context;

        public AccountService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(o => o.Email == email && o.Password == password);
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(o => o.Email == email);
        }
    }
}
