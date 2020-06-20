using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
	public class ShortLinkModel
	{
		[Key]
		[Required]
		[Column(TypeName = "varchar(255)")]
		public string LinkId { get; set; }

		[Required]
		[Column(TypeName = "varchar(255)")]
		public Uri Uri { get; set; }
	}
}