using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Data;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class CompanyViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        public CompanyDetails companydetails { get; set; }
        public List<SelectListItem> Country { get; set; }
        public List<SelectListItem> State { get; set; }
        public List<SelectListItem> City { get; set; }

        public List<SelectListItem> GroupCompaniesList { get; set; }
        public int GroupCompanyID { get; set; }
        public string GroupCompanyName { get; set; }

        public List<SelectListItem> CompaniesList { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ChildCompanyName { get; set; }

        public List<SelectListItem> IndustryTypeList { get; set; }
       

        public List<SelectListItem> ComplianceList { get; set; }
        public int[] ComplianceID { get; set; }

        public AuditCalender AuditCalender { get; set; }

        public int yearid { get; set; }
        
        public IEnumerable<int> years { get; set; }

        public List<string> branchlist { get; set; }

    }
}