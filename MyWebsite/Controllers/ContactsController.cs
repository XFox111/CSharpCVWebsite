using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MyWebsite.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            Dictionary<string, Link> links = JsonConvert.DeserializeObject<Dictionary<string, Link>>(new WebClient().DownloadString($"{Request.Scheme}://{Request.Host}/Links.json"));
            ViewData["contactLinks"] = links.Values.ToList().FindAll(i => i.CanContactMe);
            ViewData["otherLinks"] = links.Values.ToList().FindAll(i => !i.CanContactMe);

            return View();
        }
    }
}