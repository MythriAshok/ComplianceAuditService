﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataObject
{
  public  class ComplianceAuditAuditTrail
    {
        public int Compliance_Audit_Id { get; set; }
        public int Compliance_Schedule_Instance { get; set; }
        public string Penalty_nc { get; set; }
        public string Audit_Remarks { get; set; }
        public string Audit_ArteFacts { get; set; }
        public DateTime Audit_Date { get; set; }
        public int Version { get; set; }
        public int Reviewer_Id { get; set; }
        public string Reviewer_Comments { get; set; }
        public DateTime Last_Update_dDate { get; set; }

        public int Compliance_Xref_Id { get; set; }
        public int Org_Hier_Id { get; set; }
        public int Compliance_Options_Id { get; set; }
        public int Auditor_Id { get; set; }
        public int User_Id { get; set; }
        public string Audit_Status { get; set; }
        public bool Is_Active { get; set; }
        public string Action_Type { get; set; }
    }
}
