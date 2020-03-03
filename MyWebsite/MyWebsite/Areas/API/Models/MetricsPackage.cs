using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Areas.API.Models
{
    public class MetricsPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Content { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string Version { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
