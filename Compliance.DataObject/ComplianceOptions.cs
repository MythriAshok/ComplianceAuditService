using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class ComplianceOptions
    {
        public int ComplianceOptionsId { get; set; }
        public string OptionText { get; set; }
        public int OptionOrder { get; set; }
        public int ComplianceId { get; set; }
    }
}
