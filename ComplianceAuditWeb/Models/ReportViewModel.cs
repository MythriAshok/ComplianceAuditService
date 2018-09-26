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
        public List<SelectListItem> ComplianceTypeList { get; set; }

        public int companyid { get; set; }
        public int complianceTypeid { get; set; }
        public int branchid { get; set; }
        public int Vendorid { get; set; }
        public string Vendorname { get; set; }
        public ComplianceAudit ComplianceAudit { get; set; }
        public ComplianceAuditAuditTrail ComplianceAuditAuditTrail { get; set; }



        public List<ReportViewModel> reportList { get; set; }
        public List<ComplianceXref> ActList { get; set; }
        public List<ComplianceAudit> CompliancedRuleList { get; set; }
        public List<ComplianceAudit> NonCompliancedRuleListHighRisk { get; set; }
        public List<ComplianceAudit> NonCompliancedRuleListLowRisk { get; set; }
        public List<ComplianceAudit> NonCompliancedRuleListMediumRisk { get; set; }
        public List<ComplianceAudit> PartiallyCompliancedRuleList { get; set; }
        public string ComplianceStatus { get; set; }
        public string NonComplianceStatus { get; set; }

        public string PartiallyComplianceStatus { get; set; }

        public int yearid { get; set; }
        public List<SelectListItem> frequency { get; set; }
        public int frequencyid { get; set; }
        public int frequencyvalue { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDateSecond { get; set; }
        public DateTime EndDateSecond { get; set; }

        public List<compliance_type> compliance_Types { get; set; }

        public List<StartEndDates> startEndDates { get; set; }

    }
        public class StartEndDates
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }


        }
    }
