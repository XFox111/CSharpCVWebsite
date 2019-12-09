using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class Link
    {
        [Key]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public bool CanContactMe { get; set; }
    }
}
