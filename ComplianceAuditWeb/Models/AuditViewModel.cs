using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class AuditViewModel
    {
        public ComplianceXref ComplianceXrefData { get; set; }
        public IEnumerable<ComplianceXref> complianceXrefList { get; set; }
        public ComplianceOptions ComplianceOptions { get; set; }
        public ComplianceAudit complianceAudit { get; set; }
        public IEnumerable<ComplianceAudit> complianceAuditList { get; set; }

        public List<SelectListItem> MappedCompany { get; set; }
        public List<SelectListItem> MappedBranch { get; set; }

    }
}