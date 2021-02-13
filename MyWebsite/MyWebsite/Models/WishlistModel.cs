using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
	public class WishlistModel
	{
		[Key]
		[Required]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		[Column(TypeName = "text")]
		[DisplayName("English list")]

		public string EnglishContent { get; set; }
		[Column(TypeName = "text")]
		[DisplayName("Russian list")]
		public string RussianContent { get;set; }
		[DisplayName("Last chagnge")]
		public DateTime LastUpdated { get; set; }
	}
}