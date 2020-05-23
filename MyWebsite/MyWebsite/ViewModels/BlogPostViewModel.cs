using Google.Apis.Blogger.v3.Data;
using MyWebsite.Models.Databases;

namespace MyWebsite.ViewModels
{
	public class BlogPostViewModel : ViewModelBase
	{
		public Post Post { get; set; }
		public BlogPostViewModel(DatabaseContext context, Post post) : base(context) =>
			Post = post;
	}
}