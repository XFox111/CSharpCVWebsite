using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System;
using System.IO;
using System.Drawing;
using System.Threading;

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

			UpdateCache();

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

			UpdateCache();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult UpdateCache()
		{
			if (Directory.Exists(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/Cache"))
				Directory.Delete(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/Cache", true);

			Thread.Sleep(10);
			DirectoryInfo cache = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/Cache");

			foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery"))
			{
				if (file.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
					continue;

				FileInfo info = new FileInfo(file);

				using Bitmap original = new Bitmap(file);
				using Bitmap resized = new Bitmap(600, (int)(600d / original.Width * original.Height));
				using Graphics grapics = Graphics.FromImage(resized);

				grapics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
				grapics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
				grapics.DrawImage(original, 0, 0, resized.Width, resized.Height);

				using Stream writeStream = System.IO.File.Open(cache.FullName + "\\" + info.Name + ".jpg", FileMode.Create);
				resized.Save(writeStream, System.Drawing.Imaging.ImageFormat.Jpeg);
			}

			return RedirectToAction("Index");
		}
	}
}