using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compliance.DataObject
{
   public class BranchLocation
    {
        public int Branch_Id { get; set; }
        [Required]
        public string Branch_Name { get; set; }
        [Required]
        public string Address { get; set; }
        public int Country_Id { get; set; }
        public int State_Id { get; set; }
        public int City_Id { get; set; }
        [Required]
        public string Postal_Code { get; set; }
        public string Branch_Coordinates1 { get; set; }
        public string Branch_Coordinates2 { get; set; }
        public string Branch_CoordinatesURL { get; set; }
    }
}
