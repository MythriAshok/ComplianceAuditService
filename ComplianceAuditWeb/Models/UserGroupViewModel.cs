using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class UserGroupViewModel
    {
        public UserGroup Group { get; set; }
        
        public List<SelectListItem> Roles { get; set; }

        public List<SelectListItem> Role { get; set; }

    }
}