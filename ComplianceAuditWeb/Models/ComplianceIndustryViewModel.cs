using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class ComplianceIndustryViewModel
    {
       // public IndustryType IndustryType { get; set; }
        public ComplianceType ComplianceType { get; set; }
       // public int[] ComplianceID { get; set; }

        //public int CoyntryID { get; set; }
        public string CountryName { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        //public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public List<SelectListItem> IndustryTypeList { get; set; }
        public List<SelectListItem> ComplianceTypeList { get; set; }
        public List<SelectListItem> AuditfrequencyList { get; set; }
    }
}
