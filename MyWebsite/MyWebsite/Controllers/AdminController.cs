﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Helpers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;

#pragma warning disable CA1054 // Uri parameters should not be strings
namespace MyWebsite.Controllers
{
	[Authorize]
	public class AdminController : ExtendedController
	{
		public AdminController(DatabaseContext context) : base(context) { }

		public IActionResult Index() =>
			View();

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login(string ReturnUrl) =>
			View(new CredentialViewModel(Database, ReturnUrl));

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(CredentialViewModel model)
		{
			if (!ModelState.IsValid || model == null)
			{
				ModelState.AddModelError("Authorization error", "Invalid data");
				return View(new CredentialViewModel(Database, model));
			}

			CredentialModel user = Database.Users.FirstOrDefault(i => i.Email == model.Credential.Email);
			if (user == null || !Encryptor.VerifyHash(model?.Credential.Password, user.Password))
			{
				if (!Database.Users.Any())
					goto Authorize;

				ModelState.AddModelError("Authorization error", "Invaild e-mail or password");
				return View(new CredentialViewModel(Database, model));
			}

			Authorize:
			Claim claim = new Claim(ClaimsIdentity.DefaultNameClaimType, user?.Email ?? "root");

			ClaimsIdentity id = new ClaimsIdentity(new Claim[] { claim }, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)).ConfigureAwait(false);

			return Redirect(model.ReturnUrl ?? "/Admin");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
			return RedirectToAction("Login", "Admin");
		}
	}
}