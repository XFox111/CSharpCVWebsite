using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
	public class CustomData
	{
		[Key]
		[Required]
		[Column(TypeName = "varchar(255)")]
		public string Key { get; set; }
		[Required]
		[Column(TypeName = "varchar(255)")]
		public string Value { get; set; }
	}
}