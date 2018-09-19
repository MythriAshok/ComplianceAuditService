using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        //public string  Description { get; set; }
        //public string  ComplianceStatus { get; set; }
        //public string  RiskCategory { get; set; }
        //public string  Remarks { get; set; }
        //public string  CompanyName { get; set; }
        //public string  Evidences { get; set; }


        public List<ReportViewModel> reportList { get; set; }
        public List<ComplianceXref> ActList { get; set; }
        public List<ComplianceAudit> CompliancedRuleList { get; set; }
        public List<ComplianceAudit> NonCompliancedRuleListHighRisk { get; set; }
        public List<ComplianceAudit> NonCompliancedRuleListLowRisk { get; set; }
        public List<ComplianceAudit> NonCompliancedRuleListMediumRisk { get; set; }
        public List<ComplianceAudit> PartiallyCompliancedRuleList { get; set; }
        //public int ParentID { get; set; }
        //public List< ParentID> PID { get; set; }
    
    }
}