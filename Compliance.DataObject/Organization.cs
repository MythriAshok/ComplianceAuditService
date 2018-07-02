﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class Organization
    {
        public int Organization_Id { get; set; }
        public string Company_Name { get; set; }
        public int Company_Id { get; set; }
        public int Parent_Company_Id { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Is_Leaf { get; set; }
        public string Industry_Type { get; set; }
        public int Branch_Id { get; set; }

        public int User_Id { get; set; }

        public DateTime Last_Updated_Date { get; set; }
        public DateTime Is_Active { get; set; }
    }
}
