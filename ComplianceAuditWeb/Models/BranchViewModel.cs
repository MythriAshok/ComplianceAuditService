using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Data;
using System.Web.Mvc;


namespace ComplianceAuditWeb.Models
{
    public class BranchViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        //public BranchAuditingfrequency branchAuditingFrequency { get; set; }
       // public TypeOfBranch branchType { get; set; }
        public List<SelectListItem> Country { get; set; }
        public List<SelectListItem> State { get; set; }
        public List<SelectListItem> City { get; set; }

        public List<SelectListItem> CompaniesList { get; set; }
        public string ChildCompanyName { get; set; }
        public List<SelectListItem> GroupCompaniesList { get; set; }
        public int CompanyID { get; set; }
        public int GroupCompanyID { get; set; }


    }
    public enum BranchAuditingfrequency
    {
        Quaterly,
        HalfYearly,
        Annually
    }
    public enum TypeOfBranch
    {
        SalesOffice,
        HeadOffice
    }
}