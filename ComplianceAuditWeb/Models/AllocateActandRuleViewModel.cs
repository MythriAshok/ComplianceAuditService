using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class AllocateActandRuleViewModel
    {
        public List<SelectListItem> Actdropdownlist { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        public List<ComplianceXref> Sectionlist { get; set; }
        public List<ComplianceXref> Rulelist { get; set; }
    }
}