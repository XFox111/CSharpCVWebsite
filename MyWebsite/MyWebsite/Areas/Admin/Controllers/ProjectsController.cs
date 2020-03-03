using System.Collections.Generic;
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

        public IActionResult Index() =>
            View((Database.Projects as IEnumerable<ProjectModel>, Database.Badges as IEnumerable<BadgeModel>));

        [HttpGet]
        public IActionResult Delete(decimal id) =>
            View(Database.Projects.Find(id));

        [HttpPost]
        public IActionResult Delete(ProjectModel model)
        {
            Database.Projects.Remove(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() =>
            View();

        [HttpPost]
        public IActionResult Create(ProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            Database.Projects.Add(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }
    }

    public class ProjectEditorModel
    {
        public double Id { get; set; }
        public string EnglishTitle { get; set; }
        public string RussianTitle { get; set; }
        public string EnglishDescription { get; set; }
        public string RussianDescription { get; set; }
        public string Link { get; set; }
        public string EnglishLinkCaption { get; set; }
        public string RussianLinkCaption { get; set; }
    }
}