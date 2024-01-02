using Front.Areas.Admin.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Front.Areas.Admin.Services
{
	public interface IAccountService
    {
		public Task<SignInResult> LoginAsync(LoginViewModel model);
		public Task<IdentityResult> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
		public Task LogoutAsync();
		public Task<IdentityResult> RegisterAsync(RegisterViewModel model);
	}
}
