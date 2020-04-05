using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Helpers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System.Linq;

namespace MyWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class CredentialController : ExtendedController
	{
		const string viewPath = "Areas/Admin/Views/Shared/Credential.cshtml";

		public CredentialController(DatabaseContext context) : base(context) { }

		[HttpGet]
		public IActionResult Index() =>
			View(viewPath);

		[HttpPost]
		public IActionResult Index(string key, string value)
		{
			if(string.IsNullOrWhiteSpace(key))
			{
				ModelState.AddModelError("Validation error", "Unable to identify data to update");
				return View(viewPath);
			}

			if(string.IsNullOrWhiteSpace(value))
			{
				ModelState.AddModelError("Validation error", "No data provided");
				return View(viewPath);
			}

			CredentialModel credential = Database.Users.FirstOrDefault();
			Database.Users.RemoveRange(Database.Users);

			switch (key)
			{
				case "password":
					Database.Users.Add(new CredentialModel
					{
						Email = credential.Email,
						Password = Encryptor.ComputeHash(value)
					});
					break;

				case "email":
					Database.Users.Add(new CredentialModel
					{
						Email = value,
						Password = credential?.Password ?? Encryptor.ComputeHash("qwerty")
					});
					break;

				default:
					ModelState.AddModelError("Processing error", "Provided data is missing or read-only");
					return View(viewPath);
			}

			Database.SaveChanges();

			return Redirect("~/Admin/Logout");
		}
	}
}