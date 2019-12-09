using System;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;

namespace MyWebsite.Areas.API
{
    [Route("API/[controller]")]
    [Area("API")]
    [ApiController]
    public class FoxTubeController : ControllerBase
    {
        [HttpPost("AddMetrics")]
        public string AddMetrics()
        {
            MetricsPackage metrics = new MetricsPackage
            {
                Title = Request.Form["Title"],
                Content = Request.Form["Content"],
                Version = Request.Form["Version"]
            };

            // TODO: Insert package to database

            return metrics.Id.ToString();
        }

        [HttpGet("Messages")]
        public IActionResult Messages(bool toast = false, DateTime? publishedAfter = null, string lang = "en-US")
        {
            if(toast)
            {

            }

            return NoContent();
        }

        [HttpGet("Changelogs")]
        public IActionResult Changelogs(bool toast = false, string version = null, string lang = "en-US")
        {
            if(toast)
            {

            }

            return NoContent();
        }
    }
}