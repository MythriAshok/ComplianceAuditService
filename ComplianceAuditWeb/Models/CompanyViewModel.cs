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


        //public string[] TpeofAuditList { get; set; }

        //public List<SelectListItem> ComplianceAuditTypeList { get; set; }
        //public int[] ComplianceAuditTypeID { get; set; }



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