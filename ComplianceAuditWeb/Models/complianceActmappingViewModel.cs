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

        public List<SelectListItem> Country { get; set; }

        public List<SelectListItem> IndustryType { get; set; }

        public int industryid { get; set; }

        public int countryid { get; set; }
        public int compliancetypeid { get; set; }
    }

    public class CheckActList
    {
        public int ActID { get; set; }
        public string ActName {get;set;}

        public bool Checked { get; set; }

    }
}