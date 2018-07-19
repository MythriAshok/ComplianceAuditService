using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class ListofComplianceViewModel
    {
       public List<ComplianceXref> Actslist { get; set; }

       public List<ComplianceXref> Sectionlist { get; set; }

       public List<ComplianceXref> Rulelist { get; set; }

        public ListofComplianceViewModel()
        {
            Actslist = new List<ComplianceXref>();
            Sectionlist = new List<ComplianceXref>();
            Rulelist = new List<ComplianceXref>();
        }
    }
}