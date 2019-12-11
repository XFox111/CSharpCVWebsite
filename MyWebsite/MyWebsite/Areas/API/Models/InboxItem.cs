using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Areas.API.Models
{
    public class InboxItem
    {
        // Metadata
        [Key]
        [Required]
        [Column(TypeName = "varchae(20)")]
        public string Id { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; } = DateTime.Now;


        // Content
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }


        // Media
        [Column(TypeName = "varchar(255")]
        public string Icon { get; set; }

        [Column(TypeName = "varchar(255")]
        public string HeroImage { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string HtmlEmbed { get; set; }
    }
}
