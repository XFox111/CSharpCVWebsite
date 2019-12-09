using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using System.Linq;

namespace MyWebsite.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(DatabaseContext context) =>
            Startup.Database = context;

        public IActionResult Index() =>
            View();

        [Route("Contacts")]
        public IActionResult Contacts() =>
            View();

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View("Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
