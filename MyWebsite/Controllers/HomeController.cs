using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    public class HomeController : Controller
    {
        // TODO: Add more specific OpenGraph meta tags
        // TODO: Create custom error page
        // TODO: Update Projects.json and Gallery.json
        // TODO: Complete About page
        // TODO: Localize application
        // TODO: Make authorization system and ability to update website through GUI
        // TODO: Consider a database connection
        public IActionResult Index() =>
            View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
