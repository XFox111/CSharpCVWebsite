using Google.Apis.Blogger.v3.Data;
using MyWebsite.Models.Databases;

namespace MyWebsite.ViewModels
{
	public class BlogListViewModel : ViewModelBase
	{
		public BlogListViewModel(DatabaseContext context, PostList list) : base(context) =>
			Posts = list;
		public PostList Posts { get; }
		public int PageNumber { get; set; }
		public string SearchTerm { get; set; }
	}
}