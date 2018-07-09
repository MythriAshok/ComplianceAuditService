using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public bool IsGroupRole { get; set; }
    }
}
