using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System.Linq;
using System;
using MyWebsite.ViewModels;
using System.Globalization;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class GUTScheduleController : ExtendedController
	{
		const string viewPath = "Areas/Admin/Views/Shared/GUTSchedule.cshtml";

		private GUTScheduleDatabaseContext db;

		public GUTScheduleController(DatabaseContext context, GUTScheduleDatabaseContext databaseContext) : base(context) =>
			db = databaseContext;

		[HttpGet]
		public IActionResult Index()
		{
			ViewData["Policies"] = db.PrivacyPolicies;
			return View(viewPath, db.OffsetDates.FirstOrDefault());
		}


		[HttpPost]
		public IActionResult Index(CustomData model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View(model);
			}

			db.OffsetDates.RemoveRange(db.OffsetDates);
			db.OffsetDates.Add(model);

			db.SaveChanges();

			return Index();
		}

		[HttpGet]
		public IActionResult CreatePolicy()
		{
			ViewData["Caption"] = "privacy policy";
			return View(viewName: "Areas/Admin/Views/Resume/Create.cshtml");
		}

		[HttpPost]
		public IActionResult CreatePolicy(ResumeModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View("Areas/Admin/Views/Resume/Create.cshtml", model);
			}

			model.LastUpdate = DateTime.Now;

			if (db.PrivacyPolicies.Any(i => i.Language == model.Language))
			{
				ModelState.AddModelError("Error", $"Resume with this language ({model.Language}) is already exists");
				return View("Areas/Admin/Views/Resume/Create.cshtml", model);
			}

			db.PrivacyPolicies.Add(model);
			db.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult DeletePolicy(string id)
		{
			ViewData["Caption"] = "privacy policy";
			return View("Areas/Admin/Views/Resume/Delete.cshtml", db.PrivacyPolicies.Find(id));
		}

		[HttpPost]
		public IActionResult DeletePolicy(ResumeModel model)
		{
			db.PrivacyPolicies.Remove(model);
			db.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult EditPolicy(string id)
		{
			ViewData["Caption"] = "privacy policy";
			return View("Areas/Admin/Views/Resume/Edit.cshtml", db.PrivacyPolicies.Find(id));
		}

		[HttpPost]
		public IActionResult EditPolicy(ResumeModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Error", "Invalid data");
				return View("Areas/Admin/Views/Resume/Edit.cshtml", model);
			}

			model.LastUpdate = DateTime.Now;

			db.PrivacyPolicies.Update(model);
			db.SaveChanges();

			return RedirectToAction("Index");
		}

		[AllowAnonymous]
		[Route("/Projects/GUTSchedule/PrivacyPolicy")]
		public IActionResult PrivacyPolicy() =>
			View("Areas/Projects/Views/GUTSchedule/PrivacyPolicy.cshtml", new ResumeViewModel(db.PrivacyPolicies.Find(CultureInfo.CurrentCulture.TwoLetterISOLanguageName) ?? db.PrivacyPolicies.Find("en") ?? db.PrivacyPolicies.Find("ru"), Database));
	}
}