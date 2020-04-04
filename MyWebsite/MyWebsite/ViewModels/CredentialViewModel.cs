using MyWebsite.Models;
using MyWebsite.Models.Databases;

#pragma warning disable CA1054 // Uri parameters should not be strings
#pragma warning disable CA1056 // Uri properties should not be strings
namespace MyWebsite.ViewModels
{
	public class CredentialViewModel : ViewModelBase
	{
		public CredentialModel Credential { get; set; }
		public string ReturnUrl { get; set; }
		public CredentialViewModel(DatabaseContext context) : base(context) { }

		public CredentialViewModel(DatabaseContext context, string returnUrl) : base(context) =>
			ReturnUrl = returnUrl;

		public CredentialViewModel() : base(null) { }
		public CredentialViewModel(DatabaseContext context, CredentialViewModel model) : base(context) =>
			Credential = model?.Credential;
	}
}