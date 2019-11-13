using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using Newtonsoft.Json;

namespace MyWebsite.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Projects(int? id)
        {
            if (id == null)
                return View();
            else
            {
                Project[] projects = JsonConvert.DeserializeObject<Project[]>(await new HttpClient().GetStringAsync($"{Request.Scheme}://{Request.Host}/Projects.json"));

                ViewData["Project"] = projects[id.Value];
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

                return View("Views/Admin/ProjectEditor.cshtml");
            }
        }
    }
}