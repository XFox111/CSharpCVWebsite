using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyWebsite.Models
{
	public class ImageModel
	{
		[Key]
		[Column(TypeName = "varchar(20)")]
		[DisplayName("File name")]
		public string FileName { get; set; }

		[DisplayName("Title")]
		public string Title => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru" && !string.IsNullOrWhiteSpace(RussianTitle) ? RussianTitle : EnglishTitle;

		[Required]
		[Column(TypeName = "varchar(100)")]
		[DisplayName("Title (en)")]
		public string EnglishTitle { get; set; }
		[Column(TypeName = "varchar(100)")]
		[DisplayName("Title (ru)")]
		public string RussianTitle { get; set; }

		[DisplayName("Description")]
		public string Description => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru" && !string.IsNullOrWhiteSpace(RussianDescription) ? RussianDescription : EnglishDescription;

		[Required]
		[Column(TypeName = "text")]
		[DisplayName("Description (en)")]
		public string EnglishDescription { get; set; }
		[Column(TypeName = "text")]
		[DisplayName("Description (ru)")]
		public string RussianDescription { get; set; }

		[Required]
		[DisplayName("Created")]
		public DateTime CreationDate { get; set; }
	}
}