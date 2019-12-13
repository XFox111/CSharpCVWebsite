using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using System;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ResumeController : Controller
    {
        public ResumeController(DatabaseContext context) =>
            Startup.Database = context;

        public IActionResult Index() =>
            View(Startup.Database.Resume);

        [HttpGet]
        public IActionResult Edit(string id) =>
            View(Startup.Database.Resume.Find(id));

        [HttpPost]
        public IActionResult Edit(Resume model)
        {
            model.LastUpdate = DateTime.Now;
            Startup.Database.Resume.Update(model);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id) =>
            View(Startup.Database.Resume.Find(id));

        [HttpPost]
        public IActionResult Delete(Resume model)
        {
            Startup.Database.Resume.Remove(model);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() =>
            View();

        [HttpPost]
        public IActionResult Create(Resume model)
        {
            model.LastUpdate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            Startup.Database.Resume.Add(model);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}