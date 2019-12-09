using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class Image
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        [Key]
        public string FileName { get; set; }
    }
}
