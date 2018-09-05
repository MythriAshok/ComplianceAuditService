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
        
        public List<Auditentry> auditentries { get; set; }

        public int compliancetypeid { get; set; }

    }

    public class Auditentry
    {
        public List<ComplianceAudit> audits { get; set; }

        public int compliance_Xref_id { get; set; }

        public string Compliance_Title { get; set; }

        public string Description { get; set; }

        public string Periodicity { get; set; }

        public string Non_compliance { get; set; }
    }
}