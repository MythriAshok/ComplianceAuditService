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
       public IEnumerable<UserGroup> UserGroupList { get; set; }
       public List<Roles> RolesList { get; set; }
      
    }
}
