using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using Newtonsoft.Json;

namespace MyWebsite.Controllers
{
    public class HomeController : Controller
    {
        // TODO: Add more specific OpenGraph meta tags
        // TODO: Create custom error page
        // TODO: Update Projects.json and Gallery.json
        // TODO: Complete About page
        // TODO: Make language switchable
        public IActionResult Index() =>
            View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
