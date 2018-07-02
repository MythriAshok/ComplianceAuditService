using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class ComplianceOptions
    {
        public int Compliance_Options_Id { get; set; }
        public string Option_Text { get; set; }
        public int Option_Order { get; set; }
        public int Compliance_Id { get; set; }
    }
}
