using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Badge
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Image { get; set; }
    }
}
