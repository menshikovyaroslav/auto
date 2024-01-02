using Front.Areas.Admin.Services;
using Front.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Front.Controllers
{
	public class AccountController : Controller
	{
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var email = HttpContext.User.Identity.Name;
			//var user = await _accountService.GetUser(email);

			//if (user == null) Logout();

			return View("Index");
		}

		[Authorize]
		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ChangePasswordAsync(string OldPassword, string NewPassword)
		{
			var errors = string.Empty;

			var result = await _accountService.ChangePasswordAsync(User.Identity.Name, OldPassword, NewPassword);

			if (result.Succeeded)
			{
				return Ok("Вы успешно сменили пароль.");
			}
			else
			{
				//ModelState.AddModelError(string.Empty, "Неверный пароль");
				//foreach (var error in result.Errors)
				//{
				//    errors += $"{error.Description}<br>";
				//}
				errors += "Неверный пароль";
			}

			return Content(errors);
		}


        [HttpGet]
        public async Task<IActionResult> About()
        {
            return View("About");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
			//if (HttpContext.User.Identity.IsAuthenticated)
			//{
			//    return RedirectToAction("Index");
			//}
			//return View();

			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

        [HttpGet]
        public IActionResult BadAuth()
        {
            return View();
        }

        // RedirectToIndex

        [HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
        {
			if (ModelState.IsValid)
			{
				var result =
					await _accountService.LoginAsync(model);
				if (result.Succeeded)
				{
					// Принадлежит ли URL приложению.
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "Неверный логин и (или) пароль");
				}
			}

			return View(model);



		}

        //[HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
			await _accountService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task AccessDenied()
        {
            HttpContext.Response.StatusCode = 403;
            await HttpContext.Response.WriteAsync("Access Denied");
        }

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			string errors = "";

			if (ModelState.IsValid)
			{
				var result = await _accountService.RegisterAsync(model);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Account");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						errors += $"error={error}, desc={error.Description}\r\n";

                        ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}
	}
}
