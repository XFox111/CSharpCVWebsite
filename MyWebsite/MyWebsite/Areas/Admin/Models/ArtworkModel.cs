using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Areas.Admin.Models
{
    public class ArtworkModel
    {
        [Required]
        [DisplayName("Title (en)")]
        public string EnglishTitle { get; set; }
        [DisplayName("Title (ru)")]
        public string RussianTitle { get; set; }

        [Required]
        [DisplayName("Description (en)")]
        public string EnglishDescription { get; set; }
        [DisplayName("Description (ru)")]
        public string RussianDescription { get; set; }

        [Required]
        [DisplayName("Created")]
        public DateTime CreationDate { get; set; }
        [Required]
        [DisplayName("File")]
        public IFormFile File { get; set; }
    }
}
