﻿using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index() =>
            View();
    }
}