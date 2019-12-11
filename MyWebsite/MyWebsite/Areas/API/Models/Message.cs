using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Areas.API.Models
{
    public class Message
    {
        // Metadata
        [Key]
        [Column(TypeName = "varchar(50)")]
        public string Id { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Language { get; set; } = "en-US";


        // Content
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }


        // Media
        [Column(TypeName = "varchar(255")]
        public string Icon { get; set; } = "https://xfox111.net/projects-assets/FoxTube/DefaultIcon.png";

        [Column(TypeName = "varchar(255")]
        public string HeroImage { get; set; } = "https://xfox111.net/projects-assets/FoxTube/DefaultHero.png";

        [Column(TypeName = "varchar(255)")]
        public string HtmlEmbed { get; set; }
    }
}
