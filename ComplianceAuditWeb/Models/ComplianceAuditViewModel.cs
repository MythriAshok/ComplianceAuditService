using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class ComplianceAuditViewModel
    {
        public ComplianceAudit complianceAudit { get; set; }
        public ComplianceAuditAuditTrail complianceXrefAuditTrail { get; set; }
        public List<SelectListItem> complianceBranchMap { get; set; }
        public ComplianceXref complianceXref { get; set; }
        public ComplianceOptions complianceXrefOptions { get; set; }
        public List<ComplianceAudit> ComplianceAuditLists { get; set; }
         public List<SelectListItem> complianceAuditList { get; set; }
  


    }
}