using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index() =>
            View();

        public IActionResult Details(string id) =>
            View(model: id);
    }
}