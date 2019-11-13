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
        [HttpGet("Arts")]
        public async Task<IActionResult> Index()
        {
            return View(JsonConvert.DeserializeObject<Image[]>(await new HttpClient().GetStringAsync($"{Request.Scheme}://{Request.Host}/Gallery.json")));
        }

        [HttpGet("Image")]
        public async Task<IActionResult> Details(string id)
        {
            return View(JsonConvert.DeserializeObject<Image[]>(await new HttpClient().GetStringAsync($"https://{Request.Host}/Gallery.json")).First(i => i.FileName == id));
        }
            
    }
}