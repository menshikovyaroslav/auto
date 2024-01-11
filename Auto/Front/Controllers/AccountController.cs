using Dom.Extensions;
using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.Areas.Cars.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Front.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;
        private ICarsService _carsService;

        public AccountController(IAccountService accountService, SignInManager<User> signInManager, ICarsService carsService)
        {
            _accountService = accountService;
            _signInManager = signInManager;
            _carsService = carsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands = await _carsService.GetAllBrandsAsync();
            var model = new HomeViewModel() { Brands = brands };
            return View(model);
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
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);


                if (result.Succeeded)
                {
                    // Принадлежит ли URL приложению.
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    TempData["Error"] = "Error. Bad login or pass.";
                    //ModelState.AddModelError("", "Неверный логин и (или) пароль");
                }
            }

            return View(model);



        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
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

        [HttpPost]
        public async Task<IActionResult> Index(string[] brandIds)
        {
            IEnumerable<Car> cars;
            IEnumerable<Brand> brands;

            brands = await _carsService.GetAllBrandsAsync();
            cars = _carsService.GetFilteredCarsAsync(brandIds);

            //var model = new HomeViewModel() { Brands = brands, Cars = cars };
            return View(cars);


            //var jsonObject = JsonSerializer.Deserialize<JsonObject>(brandId.FromBase64());

            //var selectedBrands = new List<Brand>();

            //if (jsonObject["selectedBrands"] != null && jsonObject["selectedBrands"].ToString() != "[]")
            //    foreach (var selectedBrand in jsonObject["selectedBrands"] as JsonArray)
            //    {

            //    }
            
            //var seletedBrand = await _carsService.GetBrandAsync(jsonObject["selectedBrand"].ToString().ToInt());
            //selectedBrands.Add(seletedBrand);

            //var brands = await _carsService.GetAllBrandsAsync();
            //var model = new HomeViewModel() { Brands = brands, SelectedBrands = selectedBrands };
            //return View(model);
        }
    }
}
