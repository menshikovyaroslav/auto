using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Front.Areas.Admin.ViewModels;
using Front.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
    //[Authorize(Policy = "AdminArea")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

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

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            //var result = await _usersService.DeleteAsync(id);
            //if (result != null)
            //{
            //	return RedirectToAction("Index");
            //}
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(new EditViewModel(user));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Role = model.Role;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Users()
        {
            return View(_userManager.Users.ToList());
        }
    }
}
