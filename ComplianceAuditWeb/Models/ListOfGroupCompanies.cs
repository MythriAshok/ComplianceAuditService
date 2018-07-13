using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class ListOfGroupCompanies
    {
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }
        public string IndustryType { get; set; }
        public string GroupCompanyLogo { get; set; }
    }
}