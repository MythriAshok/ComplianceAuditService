using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class complianceActmappingViewModel
    {
        public List<SelectListItem> ComplianceType { get; set; }
        public List<CheckActList> ActList { get; set; }       
        public int compliancetypeid { get; set; }
    }

    public class CheckActList
    {
        public int ActID { get; set; }
        public string ActName {get;set;}

        public bool Checked { get; set; }

    }
}