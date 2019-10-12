using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            Project[] projects = JsonConvert.DeserializeObject<Project[]>(await new HttpClient().GetStringAsync($"https://{Request.Host}/Projects.json"));

            ViewData["Images"] = projects;

            return View();
        }
    }
}