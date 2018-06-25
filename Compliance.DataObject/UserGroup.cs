using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class UserGroup
    {
        public int UserGroupId { get; set; }
        public string  UserGroupName { get; set; }
        public string UserGroupDescription { get; set; }
        public int UserRoleId { get; set; }
    }
}
