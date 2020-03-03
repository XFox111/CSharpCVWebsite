using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MyWebsite.Models.Databases;

namespace MyWebsite
{
	// TODO: Add reordering for contact links
	// TODO: Complete project admin page
	// TODO: Complete artworks admin page
	// TODO: FoxTube API admin page
	// TODO: Complete homepage
	// TODO: Complete FoxTube page
	// TODO: Add localization system
	// TODO: Rid of JavaScript (use Blazor)
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DatabaseContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("MainDB")));

			services.AddDbContext<FoxTubeDatabaseContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("FoxTubeDB")));

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
				options.LoginPath = new PathString("/Admin/Login"));

			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapAreaControllerRoute(
					name: "projects",
					areaName: "Projects",
					pattern: "Projects/{controller=Projects}/{action=Index}/{id?}");
				endpoints.MapAreaControllerRoute(
					name: "admin",
					areaName: "Admin",
					pattern: "Admin/{controller}/{action=Index}/{id?}");
				endpoints.MapAreaControllerRoute(
					name: "api",
					areaName: "API",
					pattern: "API/{controller}/{action}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
