using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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

		[HttpPost]
		public IActionResult Create(string url, string id = "")
		{
			if (!string.IsNullOrWhiteSpace(id) && Database.ShortLinks.Find(id) != null)
			{
				ModelState.AddModelError("Duplicate", "Link with such ID already exists");
				return View(ViewPath, Database.ShortLinks.ToList());
			}

			if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uri))
			{
				ModelState.AddModelError("InvalidLink", "Provided link for shortening is invalid");
				return View(ViewPath, Database.ShortLinks.ToList());
			}

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
			} while (Database.ShortLinks.Any(i => i.LinkId == key));

			return key;
		}
	}
}