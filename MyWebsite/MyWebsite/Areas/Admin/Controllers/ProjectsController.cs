using System;
using System.Collections.Generic;
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
	public class ProjectsController : ExtendedController
	{
		public ProjectsController(DatabaseContext context) : base(context) { }

		[HttpGet]
		public IActionResult Index() =>
			View((Database.Projects as IEnumerable<ProjectModel>, Database.Badges as IEnumerable<BadgeModel>));

		[HttpPost]
		public IActionResult Index(Guid[] reorderList)
		{
			if (reorderList?.Length != Database.Projects.Count())
				ModelState.AddModelError("Error", "Invalid or incomplete data recieved");
			else
			{
				for (int i = 0; i < reorderList.Length; i++)
					Database.Projects.Find(reorderList[i]).Order = i;

				Database.SaveChanges();
			}

			return View((Database.Projects as IEnumerable<ProjectModel>, Database.Badges as IEnumerable<BadgeModel>));
		}

		[HttpGet]
		public IActionResult Delete(Guid id)
		{
			ViewData["Badges"] = Database.Badges.ToList();
			return View(Database.Projects.Find(id));
		}

		[HttpPost]
		public IActionResult Delete(ProjectModel model)
		{
			Database.Projects.Remove(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Edit(Guid id)
		{
			ViewData["Badges"] = Database.Badges.ToList();
			return View(Database.Projects.Find(id));
		}

		[HttpPost]
		public IActionResult Edit(ProjectModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				ViewData["Badges"] = Database.Badges.ToList();
				return View(model);
			}

			Database.Projects.Update(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewData["Badges"] = Database.Badges.ToList();
			return View(model: null);
		}

		[HttpPost]
		public IActionResult Create(ProjectModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}
			model.Order = Database.Projects.Count();

			Database.Projects.Add(model);
			Database.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}