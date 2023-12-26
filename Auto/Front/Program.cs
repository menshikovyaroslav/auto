using IntroductionIntoASPmvc.Areas.Admin.Controllers;
using IntroductionIntoASPmvc.Areas.Admin.Models;
using IntroductionIntoASPmvc.Areas.Admin.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace IntroductionIntoASPmvc
{
	public class Program
	{
		public static void Main()
		{
			var builder = WebApplication.CreateBuilder();

			builder.Services.AddMvc();

			builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
				o =>
				{
					o.LoginPath = "/home/login";
					o.AccessDeniedPath = "/home/accessdenied";
				});
			builder.Services.AddAuthorization(o =>
			{
				o.AddPolicy("AdminArea", policy =>
				{
					policy.RequireClaim("Role", "Admin");
				});
			});

			var app = builder.Build();

			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();
			app.UseStaticFiles();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("area", "{area:exists}/{controller=Users}/{action=Index}/{id?}");
				endpoints.MapControllerRoute("default", "{controller=Account}/{action=Index}/{id?}");
			});

			app.Run();
		}
	}
}