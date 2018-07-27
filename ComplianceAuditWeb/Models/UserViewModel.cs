using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using System.ComponentModel.DataAnnotations;

namespace ComplianceAuditWeb.Models
{
    public class UserViewModel
    {
      public User User { get; set; }
      public List<SelectListItem> UserGroupList { get; set; }
      public int[] UserGroupID { get; set; }
      public List<SelectListItem> RolesList { get; set; }
      public int[] RoleID { get; set; }
      [DataType(DataType.Password)]
      public int ConformPassword { get; set; }        
    }
}
