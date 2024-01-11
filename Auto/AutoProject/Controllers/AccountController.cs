using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpGet]
        public IActionResult BadAuth()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
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
                    TempData["Error"] = "Error. Bad login or pass.";
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
            string errors = string.Empty;

            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData["Error"] = "Error. Unknown email.";
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword1 != model.NewPassword2)
                {
                    TempData["Error"] = "New passwords don't match";
                    return View(model);
                }

                if (string.IsNullOrEmpty(model.OldPassword))
                {
                    TempData["Error"] = "Empty old password";
                    return View(model);
                }

                if (string.IsNullOrEmpty(model.NewPassword1))
                {
                    TempData["Error"] = "Empty new password";
                    return View(model);
                }

                var user = await _accountService.GetUserByPrincipal(User);
                var result = await _accountService.ChangePassword(user, model.OldPassword, model.NewPassword1);

                if (result.Succeeded)
                {
                    TempData["Info"] = "Password changed";
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    TempData["Error"] = "Password not changed";
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
