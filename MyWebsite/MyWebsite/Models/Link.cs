using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Url { get; set; }
        [Required]
        public bool CanContactMe { get; set; } = false;
        [Required]
        public bool DisplayInFooter { get; set; } = false;
    }
}
