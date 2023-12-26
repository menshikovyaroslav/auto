using Front.Areas.Admin.Services;
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
            var user = await _accountService.GetUser(email);

            if (user == null) Logout();

            return View("Index", user);
		}

		[HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult BadAuth()
        {
            return View();
        }

        // RedirectToIndex

        [HttpPost]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var form = Request.Form;

            if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            {
                return NotFound();
            }

            string email = form["email"];
            string password = form["password"];

            var user = await _accountService.GetUser(email, password);

            if (user == null)
            {
                //return NotFound();
                return RedirectToAction("BadAuth");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), new Claim("Role", user.Role.ToString()) };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return RedirectToAction("Index");
        }

        //[HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task AccessDenied()
        {
            HttpContext.Response.StatusCode = 403;
            await HttpContext.Response.WriteAsync("Access Denied");
        }
    }
}
