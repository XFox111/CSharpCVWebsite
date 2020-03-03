using Microsoft.AspNetCore.Mvc;
using MyWebsite.ViewModels;
using MyWebsite.Models.Databases;

namespace MyWebsite.Controllers
{
	public class ExtendedController : Controller
	{
		public DatabaseContext Database { get; }

		public ExtendedController(DatabaseContext context) =>
			Database = context;

		new public IActionResult View() =>
			base.View(new ViewModelBase(Database));
	}
}