using Front.Areas.Admin.Controllers;
using Front.Areas.Admin.Models;
using Front.Areas.Admin.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO.Abstractions;

namespace Front
{
	public class Program
	{
		public static void Main()
		{
			var builder = WebApplication.CreateBuilder();

			builder.Services.AddMvc();

            builder.Services.AddScoped<IAccountService, AccountService>();
			builder.Services.AddScoped<ICarsService, CarsService>();
            builder.Services.AddSingleton<IFileSystem, FileSystem>();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            builder.Services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationContext>();

            var app = builder.Build();

			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Users}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("moderator", "{area:exists}/{controller=Catalog}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Account}/{action=Index}/{id?}");
			});

			app.Run();
		}
	}
}