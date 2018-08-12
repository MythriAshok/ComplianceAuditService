using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class AllocateActandRuleViewModel
    {
        public List<Organization> Branch { get; set; }
        public List<Organization> Vendor { get; set; }
        public List<SelectListItem> Companylist { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int VendorId { get; set; }

        public List<SelectListItem> ActType { get; set; }
        public int ActTypeID { get; set; }

        public List<SelectListItem> AuditType { get; set; }

        public int AuditTypeID { get; set; }

        public string Name { get; set; }
    }
}