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
	public class ResumeController : ExtendedController
	{
		public ResumeController(DatabaseContext context) : base(context) { }

		public IActionResult Index() =>
			View(Database.Resume);

		[HttpGet]
		public IActionResult Edit(string id) =>
			View(Database.Resume.Find(id));

		[HttpPost]
		public IActionResult Edit(ResumeModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			model.LastUpdate = DateTime.Now;

			Database.Resume.Update(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(string id) =>
			View(Database.Resume.Find(id));

		[HttpPost]
		public IActionResult Delete(ResumeModel model)
		{
			Database.Resume.Remove(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Create() =>
			(this as Controller).View();

		[HttpPost]
		public IActionResult Create(ResumeModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			model.LastUpdate = DateTime.Now;

			if(Database.Resume.Any(i => i.Language == model.Language))
			{
				ModelState.AddModelError("Error", $"Resume with this language ({model.Language}) is already exists");
				return View(model);
			}

			Database.Resume.Add(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}