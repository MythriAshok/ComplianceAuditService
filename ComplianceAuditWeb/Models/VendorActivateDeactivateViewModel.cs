using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplianceAuditWeb.Models
{
    public class VendorActivateDeactivateViewModel
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public int VendorBranchID { get; set; }

    }
}