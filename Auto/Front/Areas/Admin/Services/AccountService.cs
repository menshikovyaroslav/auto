using Front.Areas.Admin.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Front.Areas.Admin.Services
{
    public class AccountService : IAccountService
    {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<SignInResult> LoginAsync(LoginViewModel model)
		{
			return await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
		}

		public async Task<IdentityResult> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
		{
			User user = await _userManager.FindByNameAsync(userName);
			IdentityResult result =
					await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
			return result;
		}

		public async Task LogoutAsync()
		{
			// Удаление аутен. куки.
			await _signInManager.SignOutAsync();
		}

		public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
		{
			User user = new User(model);
			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				// cookies:
				await _signInManager.SignInAsync(user, false);
			}

			return result;
		}

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword)
        {
			return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }
    }
}
