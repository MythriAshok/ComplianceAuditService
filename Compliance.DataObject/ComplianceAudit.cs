using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compliance.DataObject
{
   public class ComplianceAudit
    {
        public int Compliance_Audit_Id { get; set; }
        //public int Compliance_Schedule_Instance { get; set; }
        //public string Penalty_nc { get; set; }
        public string Audit_Remarks { get; set; }
        public string Evidences { get; set; }
        [DataType(DataType.Date)]
        public DateTime Audit_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime End_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Audit_Followup_Date { get; set; }
        public int Version { get; set; }
        public int Reviewer_Id { get; set; }
        public string Reviewer_Comments { get; set; }
        public DateTime Last_Updated_Date { get; set; }

        public int Xref_Comp_Type_Map_ID { get; set; }
        public int Org_Hier_Id { get; set; }
        public int Vendor_Id { get; set; }
       // public int Compliance_Options_Id { get; set; }
        public int Auditor_Id { get; set; }
  //      public int User_Id { get; set; }
        public string Audit_Status { get; set; }

        public string Applicability { get; set; }
        public bool Is_Active { get; set; }
        //        public decimal Part_Compliance_Percent { get; set; }
        public string Risk_Category { get; set; }






    }
}
