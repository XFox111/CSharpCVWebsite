using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;

namespace MyWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GalleryController : Controller
    {
        public GalleryController(DatabaseContext context) =>
            Startup.Database = context;

        public IActionResult Index() =>
            View(Startup.Database.Gallery);


    }
}