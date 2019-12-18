using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using System.IO;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BadgesController : Controller
    {
        public BadgesController(DatabaseContext context) =>
               Startup.Database = context;

        public IActionResult Index() =>
            View(Startup.Database.Badges);

        [HttpGet]
        public IActionResult Edit(string id) =>
            View(Startup.Database.Badges.Find(id));

        [HttpPost]
        public IActionResult Edit(Badge model, IFormFile file = null)
        {
            if(file != null)
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(file.OpenReadStream());
                if (image.Width != 64 || image.Height != 64 || !file.FileName.ToLower().EndsWith(".png"))
                {
                    ViewData["UploadException"] = "error";
                    return View(Startup.Database.Badges.Find(model.Name));
                }
                using (var stream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/wwwroot/images/Badges/" + file.FileName))
                    file.CopyTo(stream);

                return Redirect(Request.Path.Value);
            }

            Startup.Database.Badges.Update(model);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id) =>
            View(Startup.Database.Badges.Find(id));

        [HttpPost]
        public IActionResult Delete(Badge model)
        {
            Startup.Database.Badges.Remove(model);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() =>
            View();

        [HttpPost]
        public IActionResult Create(Badge model, IFormFile file = null)
        {
            if (file != null)
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(file.OpenReadStream());
                if (image.Width != 64 || image.Height != 64 || !file.FileName.ToLower().EndsWith(".png"))
                {
                    ViewData["UploadException"] = "error";
                    return View(Startup.Database.Badges.Find(model.Name));
                }
                using (var stream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/wwwroot/images/Badges/" + file.FileName))
                    file.CopyTo(stream);

                return Redirect(Request.Path.Value);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            Startup.Database.Badges.Add(model);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}