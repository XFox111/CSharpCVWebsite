using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Resume
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string Language { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }
    }
}