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
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(45)]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression("^(\\+?d{10})$", ErrorMessage = "Please enter a proper Phone number.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number", Prompt = "(1234567890")]
        public string ContactNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
