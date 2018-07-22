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
        public List<ComplianceXref> complianceXrefList { get; set; }
        public List<ComplianceXref> Section { get; set; }
        public List<ComplianceXref> Rules { get; set; }
        public IEnumerable<ComplianceXref> XrefList { get; set; }
        public ComplianceOptions ComplianceOptions { get; set; }
        public ComplianceAudit complianceAudit { get; set; }
        public List<ComplianceAudit> complianceAuditList { get; set; }
        public IEnumerable<ComplianceAudit> AuditList { get; set; }
        
        public List<SelectListItem> MappedCompany { get; set; }
        public List<SelectListItem> MappedBranch { get; set; }

    }
}