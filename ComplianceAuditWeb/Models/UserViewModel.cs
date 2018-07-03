using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class UserViewModel
    {
       public User User { get; set; }
       public UserGroup userGroup { get; set; }
       public UserRoles roles { get; set; } 

    }
}