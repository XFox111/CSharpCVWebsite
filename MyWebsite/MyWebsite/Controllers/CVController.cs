using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class CVController : Controller
    {
        public IActionResult Index() =>
            View();
    }
}