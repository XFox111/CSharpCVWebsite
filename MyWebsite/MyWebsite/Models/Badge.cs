using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyWebsite.Models
{
    public class Badge
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        [DisplayName("ID")]
        public string Name { get; set; }

        [DisplayName("Caption")]
        public string Description => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru" && !string.IsNullOrWhiteSpace(RussianDescription) ? RussianDescription : EnglishDescription;

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Caption (en)")]
        public string EnglishDescription { get; set; }
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Caption (ru)")]
        public string RussianDescription { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Required]
        [DisplayName("Image name")]
        public string Image { get; set; }
    }
}
