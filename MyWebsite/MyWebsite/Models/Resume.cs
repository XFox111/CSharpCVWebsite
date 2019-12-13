using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models
{
    public class Resume
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(10)")]
        [DisplayName("Language")]
        public string Language { get; set; }
        [Required]
        [Column(TypeName = "text")]
        [DisplayName("Content")]
        public string Content { get; set; }
        [DisplayName("Last chagnge")]
        public DateTime LastUpdate { get; set; }

    }
}