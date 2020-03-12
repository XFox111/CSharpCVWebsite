using System.Collections.Generic;
using System.Linq;
using MyWebsite.Models;
using MyWebsite.Models.Databases;

namespace MyWebsite.ViewModels
{
	public class ViewModelBase
	{
		public IEnumerable<LinkModel> Links { get; }

		public ViewModelBase(DatabaseContext context) =>
			Links = context?.Links.OrderBy(i => i.Order);
	}
}