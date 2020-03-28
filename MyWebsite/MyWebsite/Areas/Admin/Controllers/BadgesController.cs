using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class BadgesController : ExtendedController
	{
		public BadgesController(DatabaseContext context) : base(context) { }

		[HttpGet]
		public IActionResult Index() =>
			View(Database.Badges);

		[HttpPost]
		public IActionResult Index(IFormFile badgeImage)
		{
			if (badgeImage == null)
				throw new ArgumentNullException(nameof(badgeImage));

			Image image = Image.FromStream(badgeImage.OpenReadStream());

			if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/images/Badges/" + badgeImage.FileName))
				ModelState.AddModelError("Error", $"Badge image with such name ({badgeImage.FileName}) is already esists");
			else if (image.Width < 64 || image.Width != image.Height || !badgeImage.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
				ModelState.AddModelError("Error", "The file must be PNG image with 1:1 aspect ratio and at least 64x64 pixels");
			else
			{
				using Bitmap resized = new Bitmap(64, 64);
				using Graphics graphics = Graphics.FromImage(resized);

				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.DrawImage(image, 0, 0, 64, 64);

				using FileStream stream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/wwwroot/images/Badges/" + badgeImage.FileName);
				resized.Save(stream, ImageFormat.Png);
			}

			return View(Database.Badges);
		}

		[HttpGet]
		public IActionResult Edit(string id) =>
			View(Database.Badges.Find(id));

		[HttpPost]
		public IActionResult Edit(BadgeModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			Database.Badges.Update(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(string id) =>
			View(Database.Badges.Find(id));

		[HttpPost]
		public IActionResult Delete(BadgeModel model)
		{
			if (Database.Projects.ToList().Any(i => i.Badges.Contains(model.Name, StringComparison.InvariantCulture)))
			{
				ModelState.Clear();
				ModelState.AddModelError("Error", "Remove badge references from projects descriptions in order to delete the badge");
				return View(Database.Badges.Find(model?.Name));
			}

			Database.Badges.Remove(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Create() =>
			View(model: null);

		[HttpPost]
		public IActionResult Create(BadgeModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			if (Database.Badges.Any(i => i.Name == model.Name))
			{
				ModelState.AddModelError("Error", $"Badge '{model.Name}' is already exists");
				return View(model);
			}

			Database.Badges.Add(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult DeleteBadgeImage(string id)
		{
			string path = Directory.GetCurrentDirectory() + "/wwwroot/images/Badges/" + id;
			if (System.IO.File.Exists(path))
				System.IO.File.Delete(path);

			return RedirectToAction("Index");
		}
	}
}