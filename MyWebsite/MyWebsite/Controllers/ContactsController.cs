using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index() =>
            View();
    }
}