using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
  public  class Menus
    {
        public int Id { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuName { get; set; }
        public string PathUrl { get; set; }
        public bool IsActive { get; set; }
        public int UserGroup { get; set; }

    }
}
