using MyWebsite.Models;
using MyWebsite.Models.Databases;
using System.Globalization;
using System.Linq;

namespace MyWebsite.ViewModels
{
	public class WishlistViewModel : ViewModelBase
	{
		public WishlistModel Wishlist { get; }
		public string Content { get; }
		public WishlistViewModel(DatabaseContext context, CultureInfo language) : base(context)
		{
			Wishlist = context.Wishlist.FirstOrDefault();
			Content = language?.TwoLetterISOLanguageName == "ru" ? Wishlist?.RussianContent : Wishlist?.EnglishContent;
			Content ??= Wishlist?.EnglishContent;
		}
	}
}