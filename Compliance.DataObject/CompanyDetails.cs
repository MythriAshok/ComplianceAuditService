using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataObject
{
  public  class CompanyDetails
    {
        public int Company_Details_ID { get; set; }
        public int Org_Hier_ID { get; set; }
        //[Required]
        //public string Industry_Type { get; set; }
        public string Formal_Name { get; set; }
       //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
       [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public  DateTime Calender_StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public  DateTime Calender_EndDate  { get; set; }
      
        public string Auditing_Frequency { get; set; }
        [Required(ErrorMessage ="Please enter the website")]
        public string Website { get; set; }
        [Required(ErrorMessage ="Email is required")]
     
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]

        public string Company_EmailID { get; set; }

    
        [Required(ErrorMessage = "You must provide a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Company_ContactNumber1 { get; set; }
   
        public string Company_ContactNumber2 { get; set; }
        public string Compliance_Audit_Type { get; set; }
        [Required(ErrorMessage = "Please select the industry type")]

        public int Industry_Type_ID { get; set; }


        public bool Is_Active  { get; set; }
    }
}