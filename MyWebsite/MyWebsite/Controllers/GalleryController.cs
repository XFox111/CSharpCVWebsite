using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Controllers
{
    public class GalleryController : Controller
    {

        public async Task<IActionResult> Index()
        {
            ViewData["Images"] = JsonConvert.DeserializeObject<Image[]>(await new HttpClient().GetStringAsync($"https://{Request.Host}/Gallery.json"));

            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            ViewData["CurrentImage"] = JsonConvert.DeserializeObject<Image[]>(await new HttpClient().GetStringAsync($"https://{Request.Host}/Gallery.json")).First(i => i.FileName == id);

            return View();
        }
            
    }
}