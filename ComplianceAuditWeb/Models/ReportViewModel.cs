using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class ReportViewModel
    {
        public List<SelectListItem> companyList { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        public List<Organization> VendorList { get; set; }

        public int companyid { get; set; }
        public int branchid { get; set; }
        public int Vendorid { get; set; }
        public ComplianceAudit ComplianceAudit { get; set; }
        public ComplianceAuditAuditTrail ComplianceAuditAuditTrail { get; set; }
        //public CustomAudit ComplianceAudit { get; set; }
        //public CustomAuditAuditTrail ComplianceAuditAuditTrail { get; set; }

        public string  Description { get; set; }
        public string  ComplianceStatus { get; set; }
        public string  RiskCategory { get; set; }
        public string  Remarks { get; set; }
        public string  CompanyName { get; set; }
        public string  Evidences { get; set; }


        public List<ReportViewModel> reportList { get; set; }

    }
}