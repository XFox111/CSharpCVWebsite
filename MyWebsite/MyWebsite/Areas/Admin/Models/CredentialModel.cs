using MyWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.Areas.Admin.Models
{
    public class CredentialModel
    {
        public MyWebsite.Models.CredentialModel Current { get; set; } = new MyWebsite.Models.CredentialModel();
        public MyWebsite.Models.CredentialModel Updated { get; set; } = new MyWebsite.Models.CredentialModel();
    }
}