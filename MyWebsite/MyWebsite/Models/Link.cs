using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DisplayName("Title")]
        public string Title { get; set; }
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
