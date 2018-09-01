using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class AuditorpageViewModel
    {
        public List<SelectListItem> companyList { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        public List<Organization> VendorList { get; set; }

        public int companyid { get; set; }
        public int branchid { get; set; }
        public int Vendorid { get; set; }
    }
}