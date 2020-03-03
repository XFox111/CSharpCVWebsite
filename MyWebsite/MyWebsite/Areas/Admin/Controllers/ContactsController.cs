using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ContactsController : ExtendedController
    {
        public ContactsController(DatabaseContext context) : base(context) { }

        public IActionResult Index() =>
            View(Database.Links);

        [HttpGet]
        public IActionResult Edit(string id) =>
            View(Database.Links.Find(id));

        [HttpPost]
        public IActionResult Edit(LinkModel model)
        {
            Database.Links.Update(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id) =>
            View(Database.Links.Find(id));

        [HttpPost]
        public IActionResult Delete(LinkModel model)
        {
            Database.Links.Remove(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() =>
            View();

        [HttpPost]
        public IActionResult Create(LinkModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            Database.Links.Add(model);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}