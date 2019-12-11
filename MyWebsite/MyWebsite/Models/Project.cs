using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ImageName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Link { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string LinkCaption { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Badges { get; set; }
    }
}
