using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.Areas.Admin.ViewModels;
using Front.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Front.Areas.Admin.Controllers
{
    //[Authorize(Policy = "AdminArea")]
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

		//[HttpGet]
		//public IActionResult Index()
		//{
		//	return View(_userManager.Users.ToList());
		//}

		//[HttpGet]
		//public async Task<IActionResult> Create()
		//{
		//	return View();
		//}

		//[HttpPost]
		//public async Task<IActionResult> Create(User user)
		//{
		//	var result = await _usersService.CreateAsync(user);
		//	if (result != null)
		//	{
		//		return RedirectToAction("Index");
		//	}
		//	return BadRequest();
		//}

		//[HttpPost]
		//public async Task<IActionResult> Delete(int? id)
		//{
		//	//var result = await _usersService.DeleteAsync(id);
		//	//if (result != null)
		//	//{
		//	//	return RedirectToAction("Index");
		//	//}
		//	return NotFound();
		//}

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
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Users", "Users");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Roles");
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
		public IActionResult Roles()
		{
			return View(_roleManager.Roles.ToList());
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			IdentityRole role = await _roleManager.FindByIdAsync(id);
			if (role != null)
			{
				IdentityResult result = await _roleManager.DeleteAsync(role);
			}
			return RedirectToAction("Roles", "Roles");
		}
	}
}
