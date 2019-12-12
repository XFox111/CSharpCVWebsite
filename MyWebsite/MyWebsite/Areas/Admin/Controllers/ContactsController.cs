using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ContactsController : Controller
    {
        public ContactsController(DatabaseContext context) =>
            Startup.Database = context;

        public IActionResult Index() =>
            View(Startup.Database.Links);

        [HttpGet]
        public IActionResult Edit(string id) =>
            View(Startup.Database.Links.Find(id));

        [HttpPost]
        public IActionResult Edit(Link model)
        {
            Startup.Database.Links.Update(model);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id) =>
            View(Startup.Database.Links.Find(id));

        [HttpPost]
        public IActionResult Delete(Link link)
        {
            Startup.Database.Links.Remove(link);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() =>
            View();

        [HttpPost]
        public IActionResult Create(Link link)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Authorization error", "Invalid data");
                return View(link);
            }

            Startup.Database.Links.Add(link);
            Startup.Database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}