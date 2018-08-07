using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class AllocateActandRuleViewModel
    {

        public List<SelectListItem> BranchList { get; set; }
        public List<SelectListItem> VendorList { get; set; }
        public List<SelectListItem> Companylist { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int VendorId { get; set; }       
    }
}