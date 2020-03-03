using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System.Collections.Generic;
using System.Linq;

namespace MyWebsite.ViewModels
{
	public class ArtworkViewModel : ViewModelBase
	{
		public List<ImageModel> Images { get; }

		public string CurrentId { get; set; }
		public ImageModel Current => Images.FirstOrDefault(i => i.FileName == CurrentId);

		public string Next { get; }
		public string Previous { get; }

		public ArtworkViewModel(DatabaseContext context) : base(context) =>
			Images = context.Gallery.OrderByDescending(i => i.CreationDate).ToList();

		public ArtworkViewModel(DatabaseContext context, string id) : base(context)
		{
			Images = context.Gallery.OrderByDescending(i => i.CreationDate).ToList();
			CurrentId = id;

			if (Images.IndexOf(Current) != Images.Count - 1)
				Previous = Images[Images.IndexOf(Current) + 1].FileName;
			if (Images.IndexOf(Current) != 0)
				Next = Images[Images.IndexOf(Current) - 1].FileName;
		}
	}
}