using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;
using System.Collections.Generic;

namespace MyWebsite.Areas.Projects.Models
{
	public class ScreenshotViewModel : ViewModelBase
	{
		public List<string> Paths { get; set; }
		public List<string> Names { get; set; }
		public string Current { get; set; }

		public int Position => Names.IndexOf(Current);
		public string Next => Names.Count == Position + 1 ? null : Names[Position + 1];
		public string Previous => Position > 0 ? Names[Position - 1] : null;

		public ScreenshotViewModel(DatabaseContext context) : base(context) { }
	}
}