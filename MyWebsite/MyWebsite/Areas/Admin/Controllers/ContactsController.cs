using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System;
using System.Linq;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class ContactsController : ExtendedController
	{
		public ContactsController(DatabaseContext context) : base(context) { }

		public IActionResult Index() =>
			View(Database.Links);

		[HttpPost]
		public IActionResult Index(string[] reorderList)
		{
			if(reorderList?.Length != Database.Links.Count())
			{
				ModelState.AddModelError("Error", "Invalid or incomplete data recieved");
				return View(Database.Links);
			}

			for (int i = 0; i < reorderList.Length; i++)
				Database.Links.Find(reorderList[i]).Order = i;
			
			Database.SaveChanges();

			return View(Database.Links);
		}

		[HttpGet]
		public IActionResult Edit(string id) =>
			View(Database.Links.Find(id));

		[HttpPost]
		public IActionResult Edit(LinkModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			Database.Links.Update(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(string id) =>
			View(Database.Links.Find(id));

		[HttpPost]
		public IActionResult Delete(LinkModel model)
		{
			Database.Links.Remove(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Create() =>
			View(model: null);

		[HttpPost]
		public IActionResult Create(LinkModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}
			model.Order = Database.Links.Count();

			if (Database.Links.Any(i => i.Name == model.Name))
			{
				ModelState.AddModelError("Error", $"Link '{model.Name}' is already exists");
				return View(model);
			}

			Database.Links.Add(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}