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

        public List<SelectListItem> Companylist { get; set; }
        public List<SelectListItem> Sectionlist { get; set; }
        public List<SelectListItem> Rulelist { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public int ActId { get; set; }

        public int SectionId { get; set; }

        public int[] Selectedrule { get; set; }

    }
}