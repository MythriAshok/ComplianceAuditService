﻿using System;
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public  DateTime Calender_StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public  DateTime Calender_EndDate  { get; set; }
      
        public string Auditing_Frequency { get; set; }
        [Required]
        public string Website { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        // [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]

        public string Company_EmailID { get; set; }

     //   [RegularExpression("^(\\+?d{10})$", ErrorMessage = "Please enter a proper Phone number.")]
        //[DataType(DataType.PhoneNumber)]
        //[Display(Name = "Contact Number1", Prompt = "(1234567890")]
        //[Required]

        //[Phone]
        [Required(ErrorMessage = "You must provide a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Company_ContactNumber1 { get; set; }
      //  [RegularExpression("^(\\+?d{10})$", ErrorMessage = "Please enter a proper Phone number.")]
      //  [DataType(DataType.PhoneNumber)]
      //  [Display(Name = "Contact Number2", Prompt = "(1234567890")]
        public string Company_ContactNumber2 { get; set; }
        public string Compliance_Audit_Type { get; set; }
        public int Industry_Type_ID { get; set; }


        public bool Is_Active  { get; set; }
    }
}