using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyWebsite.Models
{
    public class Link
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Order")]
        public int Order { get; set; }

        [DisplayName("Title")]
        public string Title => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru" && !string.IsNullOrWhiteSpace(RussianTitle) ? RussianTitle : EnglishTitle;

        [Required]
        [Column(TypeName = "varchar(20)")]
        [DisplayName("Title (en)")]
        public string EnglishTitle { get; set; }
        [Column(TypeName = "varchar(20)")]
        [DisplayName("Title (ru)")]
        public string RussianTitle { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Username")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        [DisplayName("URL")]
        public string Url { get; set; }
        [Required]
        [DisplayName("May contact")]
        public bool CanContactMe { get; set; } = false;
        [Required]
        [DisplayName("Footer")]
        public bool DisplayInFooter { get; set; } = false;
    }
}
