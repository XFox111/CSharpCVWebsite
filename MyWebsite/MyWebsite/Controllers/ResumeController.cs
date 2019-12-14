using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using SelectPdf;
using System.Globalization;

namespace MyWebsite.Controllers
{
    public class ResumeController : Controller
    {
        public ResumeController(DatabaseContext context) =>
            Startup.Database = context;

        public IActionResult Index() =>
            View(Startup.Database.Resume.Find(CultureInfo.CurrentUICulture.Name) ?? Startup.Database.Resume.Find("en-US"));

        public IActionResult Print() =>
            View(Startup.Database.Resume.Find(CultureInfo.CurrentUICulture.Name) ?? Startup.Database.Resume.Find("en-US"));

        public IActionResult Download()
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.MarginTop = 25;
            converter.Options.MarginBottom = 25;
            converter.Options.MarginLeft = 25;
            converter.Options.MarginRight = 25;
            PdfDocument doc = converter.ConvertHtmlString( 
                $@"<html style=""margin-top: -50px"">
                        {(Startup.Database.Resume.Find(CultureInfo.CurrentUICulture.Name) ?? Startup.Database.Resume.Find("en-US")).Content}

                        <link rel=""stylesheet"" type=""text/css"" href=""{Request.Scheme}://{Request.Host}/css/Style.css"" />
                    </html>");
            byte[] data = doc.Save();
            doc.Close();
            return File(data, "application/pdf", "[Michael Gordeev] CV.pdf");
        }
    }
}