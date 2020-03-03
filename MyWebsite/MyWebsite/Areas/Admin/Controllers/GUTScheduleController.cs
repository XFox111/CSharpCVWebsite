using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System.Linq;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class GUTScheduleController : ExtendedController
    {
        const string scheduleOffsetId = "gut.schedule.semester.offset";
        const string viewPath = "Areas/Admin/Views/Shared/GUTSchedule.cshtml";

        public GUTScheduleController(DatabaseContext context) : base(context) { }

        [HttpGet]
        public IActionResult Index() =>
            View(viewPath, Database.CustomData.Find(scheduleOffsetId) ?? new CustomData { Key = scheduleOffsetId, Value = "undefined" });

        [HttpPost]
        public IActionResult Index(CustomData model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid data");
                return View(model);
            }

            if(Database.CustomData.Any(i => i.Key == scheduleOffsetId))
                Database.CustomData.Update(model);
            else
                Database.CustomData.Add(model);

            Database.SaveChanges();
            return View(viewPath, model);
        }
    }
}