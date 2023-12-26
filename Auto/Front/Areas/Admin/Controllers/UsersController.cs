using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminArea")]
    [Area("Admin")]
	public class UsersController : Controller
	{
		private IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
			// ViewBag.MyField = "name"; Просто пишем имя переменной (MyField) -- тут же и объявляется автоматом.
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var result = await _usersService.GetAllUsersAsync();
			//if (result != null)
			{
				return View(result);
			}
			return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(User user)
		{
			var result = await _usersService.CreateAsync(user);
			if (result != null)
			{
				return RedirectToAction("Index");
			}
			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
			var result = await _usersService.DeleteAsync(id);
			if (result != null)
			{
				return RedirectToAction("Index");
			}
			return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			var result = await _usersService.EditAsync(id);
			if (result != null)
			{
				return View(result);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(User user)
		{
			var result = await _usersService.EditAsync(user);
			if (result)
			{
				return RedirectToAction("Index");
			}
			return BadRequest();
		}
	}
}
