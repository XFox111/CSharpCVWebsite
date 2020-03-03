using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;

namespace MyWebsite.Controllers
{
	public class HomeController : ExtendedController
	{
		public HomeController(DatabaseContext context) : base(context) { }

		[Route("")]
		public IActionResult Index() =>
			View();

		[Route("Contacts")]
		public IActionResult Contacts() =>
			View();

		[Route("Projects")]
		public IActionResult Projects() =>
			View(new ProjectsViewModel(Database));

		[Route("Construction")]
		public IActionResult Construction() =>
			View();

		[Route("Error")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() =>
			View(new ErrorViewModel(Database) { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}