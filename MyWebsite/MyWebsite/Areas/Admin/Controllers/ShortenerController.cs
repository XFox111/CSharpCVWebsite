using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class ShortenerController : ExtendedController
	{
		const string ViewPath = "Areas/Admin/Views/Shared/LinkShortener.cshtml";
		public ShortenerController(DatabaseContext context) : base(context) { }

		[HttpGet]
		public IActionResult Index() =>
			View(ViewPath, Database.ShortLinks.ToList());

		[HttpGet]
		public IActionResult Create(string url, string id = "")
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				ModelState.AddModelError(nameof(ArgumentNullException), "Provided url is empty or invalid");
				return View(ViewPath, Database.ShortLinks.ToList());
			}

			if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uri))
			{
				ModelState.AddModelError("InvalidLink", "Provided link for shortening is invalid");
				return View(ViewPath, Database.ShortLinks.ToList());
			}

			if (!string.IsNullOrWhiteSpace(id) && Database.ShortLinks.Find(id) is ShortLinkModel entry)
				entry.Uri = uri;
			else
				Database.ShortLinks.Add(new ShortLinkModel
				{
					Uri = uri,
					LinkId = string.IsNullOrWhiteSpace(id) ? RandomString(6) : id
				});

			Database.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(string id)
		{
			if (Database.ShortLinks.Find(id) is ShortLinkModel link)
			{
				Database.ShortLinks.Remove(link);
				Database.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		private string RandomString(int length)
		{
			string key = string.Empty;
			Random random = new Random();
			const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
			do
			{
				key = new string(Enumerable.Repeat(chars, length)
				  .Select(s => s[random.Next(s.Length)]).ToArray());
			} while (Database.ShortLinks.AsEnumerable().Any(i => i.LinkId == key));

			return key;
		}

		[HttpPost]
		public IActionResult Upload()
		{
			if (Request?.Form?.Files == null || Request.Form.Files.Count < 1)
				throw new ArgumentNullException("Files");

			foreach (IFormFile file in Request.Form.Files)
			{
				if (!string.IsNullOrWhiteSpace(Request.Form["directory"]))
					Directory.CreateDirectory("wwwroot/assets/SharedFiles" + Request.Form["directory"]);
				string path = string.Join('/', new[] { Request.Form["directory"].ToString(), file.FileName });
				using FileStream stream = System.IO.File.Create("wwwroot/assets/SharedFiles" + path);

				if (stream != null)
					file.CopyTo(stream);
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult DeleteFile(string file)
		{
			if (System.IO.File.Exists("wwwroot" + file))
				System.IO.File.Delete("wwwroot" + file);

			if (Database.ShortLinks.AsEnumerable().FirstOrDefault(i => i.Uri.LocalPath == file.Replace("\\", "/")) is ShortLinkModel link)
			{
				Database.ShortLinks.Remove(link);
				Database.SaveChanges();
			}

			return RedirectToAction("Index");
		}
	}
}