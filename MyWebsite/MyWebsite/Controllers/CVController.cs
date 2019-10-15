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
            PdfDocument doc = converter.ConvertUrl($"https://{Request.Host}/CV");
            byte[] data = doc.Save();
            doc.Close();
            return File(data, "application/pdf", "[Michael Gordeev] CV.pdf");
        }
    }
}