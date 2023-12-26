﻿using Front.Areas.Admin.Models;

namespace Front.Areas.Admin.Services
{
	public interface IUsersService
	{
		public Task<IEnumerable<User>> GetAllUsersAsync();
		public Task<bool> CreateAsync(User user);
		public Task<bool> DeleteAsync(int? id);
		public Task<User> EditAsync(int? id);
		public Task<bool> EditAsync(User user);
	}
}
