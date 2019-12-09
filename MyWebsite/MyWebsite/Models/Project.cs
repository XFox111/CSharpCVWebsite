using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Link { get; set; }
        public string LinkCaption { get; set; }
        public string Badges { get; set; }
    }
}
