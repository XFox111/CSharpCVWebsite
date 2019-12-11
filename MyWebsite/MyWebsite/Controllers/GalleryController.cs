using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using System.Linq;

namespace MyWebsite.Controllers
{
    public class GalleryController : Controller
    {
        public GalleryController(DatabaseContext context) =>
            Startup.Database = context;

        public IActionResult Index() =>
            View();

        public IActionResult Details(string id) =>
            View(Startup.Database.Gallery.FirstOrDefault(i => i.FileName == id));
    }
}