using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compliance.DataObject
{
   public class Organization
    {
        public int Organization_Id { get; set; }
        [Required(ErrorMessage ="Please enter the group company name")]
        public string Company_Name { get; set; }
        public int Company_Id { get; set; }
        

        public int Parent_Company_Id { get; set; }
      

        public string Description { get; set; }
        public int Level { get; set; }
        public bool Is_Leaf { get; set; }
        public bool Is_Delete { get; set; }
        public string Industry_Type { get; set; }
       

        public int User_Id { get; set; }

        public DateTime Last_Updated_Date { get; set; }
        public bool Is_Active { get; set; }
        public string logo { get; set; }
        public bool Is_Vendor { get; set; }


    }
}
