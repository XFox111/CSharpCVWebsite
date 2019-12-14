using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;

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
            View(Startup.Database.Links.OrderBy(i => i.Order));

        [Route("Projects")]
        public IActionResult Projects() =>
            View();

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View("Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}