using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compliance.DataObject
{
   public class BranchLocation
    {
        public int Branch_Id { get; set; }
      
        public string Branch_Name { get; set; }
        public string Address { get; set; }
       
        public int Country_Id { get; set; }
    
        public int State_Id { get; set; }

        public int City_Id { get; set; }
        public int Org_Hier_ID { get; set; }

     [Required]
    // [Range(6,6)]
        public string Postal_Code { get; set; }
        public string Branch_Coordinates1 { get; set; }
        public string Branch_Coordinates2 { get; set; }
        public string Branch_CoordinatesURL { get; set; }
    }
}
