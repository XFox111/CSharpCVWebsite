using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace MyWebsite.Controllers
{
    public class CVController : Controller
    {
        public IActionResult Index() =>
            View();

        public IActionResult Download()
        {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.MarginTop = 25;
            converter.Options.MarginBottom = 25;
            PdfDocument doc = converter.ConvertUrl($"{Request.Scheme}://{Request.Host}/CV/PrintCV?pdfPreview=true");
            byte[] data = doc.Save();
            doc.Close();
            return File(data, "application/pdf", "[Michael Gordeev] CV.pdf");
        }

        public IActionResult PrintCV(bool pdfPreview = false)
        {
            ViewData["pdfPreview"] = pdfPreview;
            return View();
        }
    }
}