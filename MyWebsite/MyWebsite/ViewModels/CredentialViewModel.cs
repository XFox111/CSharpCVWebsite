using MyWebsite.Models;
using MyWebsite.Models.Databases;

namespace MyWebsite.ViewModels
{
	public class CredentialViewModel : ViewModelBase
	{
		public CredentialModel Credential { get; set; }
		public CredentialViewModel(DatabaseContext context) : base(context) { }

		public CredentialViewModel() : base(null) { }
		public CredentialViewModel(DatabaseContext context, CredentialViewModel model) : base(context) =>
			Credential = model?.Credential;
	}
}