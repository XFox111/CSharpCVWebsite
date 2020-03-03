using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;

namespace MyWebsite.Controllers
{
	public class GalleryController : ExtendedController
	{
		public GalleryController(DatabaseContext context) : base(context) { }

		public IActionResult Index() =>
			View(new ArtworkViewModel(Database));

		public IActionResult Details(string id) =>
			View(new ArtworkViewModel(Database, id));
	}
}