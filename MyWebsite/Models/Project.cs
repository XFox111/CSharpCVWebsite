using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Models
{
    public class Project
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Link { get; set; }
        public string LinkCaption { get; set; }
        public string[] Badges { get; set; }
    }
}
