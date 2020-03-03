using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System;
using System.Globalization;
using System.IO;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BadgesController : ExtendedController
    {
        public BadgesController(DatabaseContext context) : base(context) { }

        public IActionResult Index() =>
            View(Database.Badges);

        [HttpGet]
        public IActionResult Edit(string id) =>
            View(Database.Badges.Find(id));

        [HttpPost]
        public IActionResult Edit(BadgeModel model, IFormFile file = null)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (file != null)
                UploadFile(file, model);

            Database.Badges.Update(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id) =>
            View(Database.Badges.Find(id));

        [HttpPost]
        public IActionResult Delete(BadgeModel model)
        {
            Database.Badges.Remove(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() =>
            View();

        [HttpPost]
        public IActionResult Create(BadgeModel model, IFormFile file = null)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (file != null)
                return UploadFile(file, model);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            Database.Badges.Add(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        private IActionResult UploadFile(IFormFile file, BadgeModel model)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(file.OpenReadStream());
            if (image.Width != 64 || image.Height != 64 || !file.FileName.EndsWith(".PNG", true, CultureInfo.InvariantCulture))
            {
                ViewData["UploadException"] = "error";
                return View(model);
            }
            using (var stream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/wwwroot/images/Badges/" + file.FileName))
                file.CopyTo(stream);

            return Redirect(Request.Path.Value);
        }
    }
}