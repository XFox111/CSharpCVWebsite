using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System;
using System.Linq;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class WishlistController : ExtendedController
	{
		private const string ViewPath = "Areas/Admin/Views/Shared/Wishlist.cshtml";
		public WishlistController(DatabaseContext context) : base(context) { }

		[HttpGet]
		public IActionResult Index() =>
			View(ViewPath, Database.Wishlist.FirstOrDefault());

		[HttpPost]
		public IActionResult Index(WishlistModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(ViewPath, model);
			}

			model.LastUpdated = DateTime.Now;

			Database.Wishlist.RemoveRange(Database.Wishlist);
			Database.Wishlist.Add(model);
			Database.SaveChanges();

			return RedirectToAction("Index", "Admin");
		}
	}
}