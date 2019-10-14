using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyWebsite.Controllers
{
    public class CVController : Controller
    {
        public IActionResult Index() =>
            View();

        public IActionResult Download()
        {
            byte[] data = new WebClient().DownloadData($"https://{Request.Host}/CV.pdf");
            return File(data, "application/pdf", "[Michael Gordeev] CV.pdf");
        }
    }
}