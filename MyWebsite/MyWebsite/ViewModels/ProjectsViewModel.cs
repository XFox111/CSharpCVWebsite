using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System.Collections.Generic;
using System.Linq;

namespace MyWebsite.ViewModels
{
	public class ProjectsViewModel : ViewModelBase
	{
		public IEnumerable<ProjectModel> Projects { get; }
		public IEnumerable<BadgeModel> Badges { get; }
		public ProjectsViewModel(DatabaseContext context) : base(context)
		{
			Projects = context.Projects.OrderByDescending(i => i.Id).ToList();
			Badges = context.Badges.ToList();
		}
	}
}