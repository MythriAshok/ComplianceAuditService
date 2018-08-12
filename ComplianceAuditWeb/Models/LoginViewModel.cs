using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComplianceAuditWeb.Models
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Password)]        
        public string UserPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailId { get; set; }
    }
}