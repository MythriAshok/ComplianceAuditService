using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplianceAuditWeb.Models
{
    public class ResetPasswordModel
    {
        public string ReturnToken { get; set; }
        public string Password { get; set; }
    }
}