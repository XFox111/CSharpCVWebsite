using MyWebsite.Models.Databases;

namespace MyWebsite.ViewModels
{
	public class ErrorViewModel : ViewModelBase
	{
		public ErrorViewModel(DatabaseContext context) : base(context) { }

		public string RequestId { get; set; }
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}