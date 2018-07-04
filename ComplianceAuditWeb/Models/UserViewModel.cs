using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class UserViewModel
    {
       public User User { get; set; }
       public List<UserGroup> UserGroup { get; set; }
       public List<UserRoles> Roles { get; set; }
      
    }
}
