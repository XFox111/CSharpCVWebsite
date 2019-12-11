using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Areas.API.Models
{
    public class Changelog : Message
    {
        [Required]
        [Column(TypeName = "varchar(5)")]
        public string Version { get; set; }

        public Changelog()
        {
            Icon = "https://xfox111.net/projects-assets/FoxTube/ChangelogIcon.png";
            HeroImage = "https://xfox111.net/projects-assets/FoxTube/ChangelogHero.png";
        }
    }
}
