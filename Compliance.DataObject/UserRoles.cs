using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
    public class UserRoles
    {
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsGroupRole { get; set; }
    }
}
