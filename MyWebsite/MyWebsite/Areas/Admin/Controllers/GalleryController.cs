using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System;
using System.IO;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class GalleryController : ExtendedController
	{
		public GalleryController(DatabaseContext context) : base(context) { }

		public IActionResult Index() =>
			View(Database.Gallery);

		[HttpGet]
		public IActionResult Edit(string id) =>
			View(Database.Gallery.Find(id));

		[HttpPost]
		public IActionResult Edit(ImageModel model)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			Database.Gallery.Update(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(string id) =>
			View(Database.Gallery.Find(id));

		[HttpPost]
		public IActionResult Delete(ImageModel model)
		{
			Database.Gallery.Remove(model);
			Database.SaveChanges();

			System.IO.File.SetAttributes(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/" + model?.FileName, FileAttributes.Normal);
			System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/" + model?.FileName);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Upload() =>
			View(model: null);

		[HttpPost]
		public IActionResult Upload(ImageModel model, IFormFile file)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			if (file == null)
				throw new ArgumentNullException(nameof(file));

			model.FileName = file.FileName;

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			using (var stream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/" + file.FileName))
				file.CopyTo(stream);

			Database.Gallery.Add(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}