using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Models
{
    public class Link
    {
        public string Title { get; set; }
        public string Socicon { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public bool CanContactMe { get; set; }
    }
}
