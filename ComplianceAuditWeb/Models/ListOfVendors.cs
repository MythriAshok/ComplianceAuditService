using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class ListOfVendors
    {
        public int[] VendorID { get; set; }
        public int VendorsID { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }

        public int CompanyID { get; set; }
        public string VendorName { get; set; }

        public bool IsActive { get; set; }
        public string Logo { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        public List<SelectListItem> VendorListList { get; set; }
        // public int GroupCompanyID { get; set; }
    }
}