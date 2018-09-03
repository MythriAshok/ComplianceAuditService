using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;


namespace ComplianceAuditWeb.Models
{
    public class VendorViewModel
    {
        public Organization organization { get; set; }
       // public BranchLocation branch { get; set; }
        public CompanyDetails companydetails { get; set; }
        public List<SelectListItem> Country { get; set; }
        public List<SelectListItem> State { get; set; }
        public List<SelectListItem> City { get; set; }

        public List<SelectListItem> GroupCompaniesList { get; set; }
        public int GroupCompanyID { get; set; }
        public string GroupCompanyName { get; set; }

        public List<SelectListItem> CompaniesList { get; set; }
        [Required(ErrorMessage ="Please select the company")]

        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ChildCompanyName { get; set; }

        public List<SelectListItem> ComplianceList { get; set; }
        public int[] ComplianceID { get; set; }
        public int SingleComplianceID { get; set; }


        //public string[] TpeofAuditList { get; set; }

        //public List<SelectListItem> ComplianceAuditTypeList { get; set; }
        //public int[] ComplianceAuditTypeID { get; set; }
        public List<SelectListItem> IndustryTypeList { get; set; }

        

        public enum Auditingfrequency
        {
            Quarterly,
            Half_Yearly,
            Annually
        }

        public enum IndustryType
        {
            IT,
            Manufacturing,
            HeadQuarter
        }

        //public enum AuditType
        //{
        //    LabourAudit,
        //    StatutoryAudit
        //}
    }
}
