using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyWebsite.Controllers
{
    public class ProjectsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            Project[] projects = JsonConvert.DeserializeObject<Project[]>(await new HttpClient().GetStringAsync($"{Request.Scheme}://{Request.Host}/Projects.json"));

            ViewData["Projects"] = projects;
            ViewData["Badges"] = new Dictionary<string, string>
            {
                { "csharp", "C# Programming language" },
                { "dotnet", ".NET Framework" }, 
                { "xamarin", "Xamarin Framework" },
                { "unity", "Unity Engine" },
                { "uwp", "Universal Windows Platform" },
                { "windows", "Windows Platform" },
                { "win32", "Windows Platform (Win32)" },
                { "android", "Android Platform" },
                { "ios", "iOS Platform" }
            };

            return View();
        }
    }
}