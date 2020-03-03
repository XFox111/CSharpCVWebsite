using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Areas.Admin.Models
{
    public enum ReorderDirection { Up, Down }
    public class ReorderModel
    {
        public string ItemId { get; set; }
        public ReorderDirection Direction { get; set; }
    }
}
