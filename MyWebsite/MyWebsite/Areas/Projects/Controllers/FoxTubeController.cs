using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Areas.Projects.Controllers
{
    [Area("Projects")]
    public class FoxTubeController : Controller
    {
        public IActionResult Index() =>
            View("Areas/Projects/Views/FoxTube.cshtml");
    }
}