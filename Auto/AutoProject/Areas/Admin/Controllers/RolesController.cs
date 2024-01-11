using Front.Areas.Admin.Models;
using Front.Areas.Admin.ViewModels;
using Front.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class RolesController : Controller
	{
		RoleManager<IdentityRole> _roleManager;
		UserManager<User> _userManager;

        public RolesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
        }

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
			{
				var allRoles = _roleManager.Roles.ToList();
				var userRoles = await _userManager.GetRolesAsync(user);

				return View(new EditUserRolesViewModel(id, allRoles, userRoles));
            }
			return NotFound();
        }

		[HttpPost]
		public async Task<IActionResult> EditUser(string userId, List<string> roles)
		{
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index", "Users");
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Roles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model.Name);
        }

		[HttpGet]
		public IActionResult Index()
		{
			return View(_roleManager.Roles.ToList());
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			IdentityRole role = await _roleManager.FindByIdAsync(id);
			if (role != null)
			{
				IdentityResult result = await _roleManager.DeleteAsync(role);
			}
			return RedirectToAction("Index");
		}
	}
}
