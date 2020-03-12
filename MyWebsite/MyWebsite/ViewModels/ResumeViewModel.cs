using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System.Globalization;

namespace MyWebsite.ViewModels
{
	public class ResumeViewModel : ViewModelBase
	{
		public ResumeModel Resume { get; }
		public ResumeViewModel(DatabaseContext context, CultureInfo language) : base(context) =>
			Resume = context.Resume.Find(language?.TwoLetterISOLanguageName) ?? context.Resume.Find("en");

		public ResumeViewModel(ResumeModel model, DatabaseContext context) : base(context) =>
			Resume = model;
	}
}