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
        public List<Organization> Organization { get; set; }
        public IEnumerable<Organization> GroupCompanies { get; set; }
        public List<SelectListItem> GroupCompany { get; set; }

        public string GroupCompanyLogo { get; set; }

        public List<CompanyDetails> CompanyDetails { get; set; }
        public List<BranchLocation> BranchLocation { get; set; }

    }
}