using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class ComplianceAudit
    {
        public int ComplianceAuditId { get; set; }
        public int ComplianceScheduleInstance { get; set; }
        public string Penalty { get; set; }
        public string AuditRemarks { get; set; }
        public string AuditArteFacts { get; set; }
        public DateTime AuditDate { get; set; }
        public int Version { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerComments { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public int ComplianceId { get; set; }
        public int OrgId { get; set; }
        public int ComplianceOptionsId { get; set; }
        public int AuditorId { get; set; }
        public int UserId { get; set; }
        public string AuditStatus { get; set; }

    }
}
