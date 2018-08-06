using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplianceAuditWeb.Models
{
    public class AboutCompanyViewModel
    {
        public int CompanyID { get; set; }
        public string  CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyLogo { get; set; }
        public int ParentCompanyID { get; set; }
        public string Website { get; set; }
    }

    }
