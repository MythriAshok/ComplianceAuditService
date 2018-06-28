using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class OrganizationViewModel
    {
        public Organization organization { get; set; }
        public Branch branch { get; set; }
    }
}