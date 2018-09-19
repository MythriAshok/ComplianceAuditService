using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using System.ComponentModel.DataAnnotations;

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
        public ComplianceAudit audits { get; set; }

        public string Applicability { get; set; }
        public string Audit_Status { get; set; }
        public string Audit_Remarks { get; set; }
        public int compliance_Xref_id { get; set; }

        public string Compliance_Title { get; set; }

        public string Description { get; set; }

        public string Periodicity { get; set; }

        public string Non_compliance { get; set; }
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime End_Date { get; set; }
        public int Compliance_Audit_Id { get; set; }
    }
}