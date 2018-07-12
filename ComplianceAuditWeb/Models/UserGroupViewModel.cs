using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class UserGroupViewModel
    {
        public UserGroup Group { get; set; }
        
        public Roles Roles { get; set; }
    }
}