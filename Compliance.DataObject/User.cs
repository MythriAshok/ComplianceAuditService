using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Compliance.DataObject
{
   public class User
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        //[Required(false,"First Name is Required","",string)]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        //[RegularExpression("^([a-zA-Z0- 9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)*(\\.[a-zA-Z]{2, 5})$",ErrorMessage="Please enter the valid email address.")]        
        public string EmailId { get; set; }
        [Required]
        [Phone]
        //[RegularExpression("^((/+)?(/d{2}[-]))?(/d{10}){1}?$")]
        public string ContactNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        public string photo { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }
        public int Company_Id { get; set; }
    }
}
