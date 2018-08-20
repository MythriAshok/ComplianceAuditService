using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public string GroupCompanyName { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        public int BranchID { get; set; }

        public List<SelectListItem> VendorList { get; set; }
        public int[] VendorID { get; set; }
        public int VendorsID { get; set; }
        public string VendorName { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime VendorStartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public Nullable< DateTime> VendorEndDate { get; set; }
        public bool IsVendorActive { get; set; }




    }
    public enum BranchAuditingfrequency
    {
        Quarterly,
        Half_Yearly,
        Annually
    }
    public enum TypeOfBranch
    {
        Sales_Office,
        Head_Office
    }
}