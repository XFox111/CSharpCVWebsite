using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Badge
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        [DisplayName("ID")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        [Required]
        [DisplayName("Caption")]
        public string Description { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        [DisplayName("Image name")]
        public string Image { get; set; }
    }
}
