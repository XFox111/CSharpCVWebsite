using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Areas.Admin.Models;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System;
using System.IO;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GalleryController : ExtendedController
    {
        public GalleryController(DatabaseContext context) : base(context) { }

        public IActionResult Index() =>
            View(Database.Gallery);

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (Database.Gallery.Find(id) is ImageModel model)
                return View(model);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(ImageModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            Database.Gallery.Update(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            if (Database.Gallery.Find(id) is ImageModel model)
                return View(model);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(ImageModel model)
        {
            Database.Gallery.Remove(model);
            Database.SaveChanges();

            System.IO.File.SetAttributes(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/" + model?.FileName, FileAttributes.Normal);
            System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/" + model?.FileName);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Upload() =>
            View();

        [HttpPost]
        public IActionResult Upload(ArtworkModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            using (var stream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/wwwroot/images/Gallery/" + model?.File.FileName))
                model.File.CopyTo(stream);

            ImageModel image = new ImageModel
            {
                EnglishTitle = model.EnglishTitle,
                RussianTitle = model.RussianTitle,
                EnglishDescription = model.EnglishDescription,
                RussianDescription = model.RussianDescription,
                CreationDate = model.CreationDate,
                FileName = model.File.FileName
            };

            Database.Gallery.Add(image);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}