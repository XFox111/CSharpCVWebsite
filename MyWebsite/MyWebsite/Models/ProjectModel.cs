using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

#pragma warning disable CA1724
namespace MyWebsite.Models
{
	public class ProjectModel
	{
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("ID")]
		public Guid Id { get; set; }

		[Required]
		[Column(TypeName = "int")]
		[DisplayName("Order")]
		public int Order { get; set; }

		[DisplayName("Title")]
		public string Title => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru" && !string.IsNullOrWhiteSpace(RussianTitle) ? RussianTitle : EnglishTitle;

		[Required]
		[Column(TypeName = "varchar(100)")]
		[DisplayName("Title (en)")]
		public string EnglishTitle { get; set; }
		[Column(TypeName = "varchar(100)")]
		[DisplayName("Title (ru)")]
		public string RussianTitle { get; set; }

		[DisplayName("Caption")]
		public string Description => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru" && !string.IsNullOrWhiteSpace(RussianDescription) ? RussianDescription : EnglishDescription;

		[Required]
		[Column(TypeName = "text")]
		[DisplayName("Description (en)")]
		public string EnglishDescription { get; set; }
		[Column(TypeName = "text")]
		[DisplayName("Description (ru)")]
		public string RussianDescription { get; set; }

		[Column(TypeName = "varchar(50)")]
		[DisplayName("Link")]
		public string Link { get; set; }

		[DisplayName("Link text caption")]
		public string LinkCaption => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru" && !string.IsNullOrWhiteSpace(RussianLinkCaption) ? RussianLinkCaption : EnglishLinkCaption;

		[Column(TypeName = "varchar(50)")]
		[DisplayName("Link text caption (en)")]
		public string EnglishLinkCaption { get; set; }
		[Column(TypeName = "varchar(50)")]
		[DisplayName("Link text caption (ru)")]
		public string RussianLinkCaption { get; set; }

		[Column(TypeName = "varchar(100)")]
		[DisplayName("Badges")]
		public string Badges { get; set; }

		[Column(TypeName = "varchar(255)")]
		[DisplayName("Logo")]
		public string Logo { get; set; }
	}
}