using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;
using SelectPdf;
using System.Globalization;

namespace MyWebsite.Controllers
{
	public class ResumeController : ExtendedController
	{
		public ResumeController(DatabaseContext context) : base(context) { }

		public IActionResult Index()
		{
			ResumeViewModel model = new ResumeViewModel(Database, CultureInfo.CurrentUICulture);
			if (model.Resume == null)
				return NotFound();
			model.Resume.Content = model.Resume.Content
				.Replace("%WEBSITE%", $"{Request.Scheme}://{Request.Host}/", true, CultureInfo.InvariantCulture)
				.Replace("%PHONE_NUMBER%", Database.Links.Find("phone")?.Username, true, CultureInfo.InvariantCulture)
				.Replace("%EMAIL%", Database.Links.Find("outlook")?.Username, true, CultureInfo.InvariantCulture);
			return View(model);
		}

		public IActionResult Print() =>
			Index();

		public IActionResult Download()
		{
			HtmlToPdf converter = new HtmlToPdf();
			converter.Options.MarginTop = 25;
			converter.Options.MarginBottom = 25;
			converter.Options.MarginLeft = 25;
			converter.Options.MarginRight = 25;

			PdfDocument doc = converter.ConvertUrl($"{Request.Scheme}://{Request.Host}/Resume/Print#preview");
			byte[] data = doc.Save();
			doc.Close();

			return File(data, "application/pdf", "[Michael Gordeev] CV.pdf");
		}
	}
}