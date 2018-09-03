using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class AuditentryViewModel
    {
        public List<SelectListItem> ActList { get; set; }

        public int actid { get; set; }
        
        public List<ComplianceAudit> audits { get; set; }

        public string Compliance_Title { get; set; }

        public string Description { get; set; }

        public string Periodicity { get; set; }

        public string Non_compliance { get; set; }

    }
}