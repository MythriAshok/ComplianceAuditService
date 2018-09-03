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

       
       [Required(ErrorMessage ="Please select the country")]
        public int Country_Id { get; set; }
        
       [Required(ErrorMessage ="Please select the state")]

        public int State_Id { get; set; }
        [Required(ErrorMessage = "Please select the city")]

        public int City_Id { get; set; }
        public int Org_Hier_ID { get; set; }

     [Required(ErrorMessage ="Postal code is required")]
    // [Range(6,6)]
    [RegularExpression(@"^\d{6}(-\d{4})?$",ErrorMessage ="Please enter valid postal code")]
    //[RegularExpression("^\d{5}$|^\d{5}-\d{4}$",ErrorMessage ="Please enter valid postal code")]
        public string Postal_Code { get; set; }
        public string Branch_Coordinates1 { get; set; }
        public string Branch_Coordinates2 { get; set; }
        public string Branch_CoordinatesURL { get; set; }
    }
}
