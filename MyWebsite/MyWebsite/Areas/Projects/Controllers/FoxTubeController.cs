using Microsoft.AspNetCore.Mvc;
using MyWebsite.Controllers;
using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using MyWebsite.Areas.Projects.Models;

namespace MyWebsite.Areas.Projects.Controllers
{
    [Area("Projects")]
    public class FoxTubeController : ExtendedController
    {
        readonly DatabaseContext db;
        readonly List<string> paths = new List<string>();
        readonly List<string> files;
        public FoxTubeController(DatabaseContext context) : base(context)
        {
            db = context;

            Scan(Directory.GetCurrentDirectory() + "\\wwwroot\\assets\\FoxTube\\Screenshots\\" + CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName, paths);

            for (int i = 0; i < paths.Count; i++)
                paths[i] = paths[i].Substring(paths[i].IndexOf("Screenshots", System.StringComparison.OrdinalIgnoreCase));

            files = paths.Select(i => i.Substring(i.LastIndexOf('\\') + 1)).ToList();
        }

        public IActionResult Index() =>
            View(new ScreenshotViewModel(db) 
            {
                Paths = paths,
                Names = files
            });

        public IActionResult Screenshot(string id) =>
            View(new ScreenshotViewModel(db)
            {
                Paths = paths,
                Names = files,
                Current = id
            });

        public IActionResult Privacy() =>
            View(new ResumeViewModel(db, CultureInfo.CurrentCulture));

        void Scan(string path, List<string> files)
        {
            foreach (string p in Directory.GetFiles(path))
                files.Add(p);

            foreach (string p in Directory.GetDirectories(path))
                Scan(p, files);
        }
    }
}