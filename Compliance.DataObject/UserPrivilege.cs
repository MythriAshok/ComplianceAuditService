using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class UserPrivilege
    {
        public int PrivilegeId { get; set; }
        public string  PrivilegeName { get; set; }
        public bool IsActive { get; set; }
        public string PrivilegeType { get; set; }
    }
}
