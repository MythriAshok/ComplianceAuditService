using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplianceAuditWeb.Models
{
    public class OrgActivateDeactivateViewModel
    {

        public int CompanyID { get; set; }
       
        public string CompanyName { get; set; }


    }
}