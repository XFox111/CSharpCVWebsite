using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}