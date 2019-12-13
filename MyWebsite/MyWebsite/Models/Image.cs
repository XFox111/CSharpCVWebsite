using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Title")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        [DisplayName("Description")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Created")]
        public DateTime CreationDate { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        [DisplayName("File name")]
        public string FileName { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        [DisplayName("Language")]
        public string Language { get; set; }
    }
}
