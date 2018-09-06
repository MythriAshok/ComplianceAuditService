using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class ListofComplianceViewModel
    {
       public List<ComplianceXref> Actslist { get; set; }

       public List<SelectListItem> CountryList { get; set; }
       public  int countryid { get; set; }
        public List<SelectListItem> IndustryTypeList { get; set; }
        public int industrytypeid { get; set; }
        public List<SelectListItem> ComplianceTypeList { get; set; }
        public int compliancetypeid { get; set; }

        public List<ComplianceXref> Rulelist { get; set; }

        public ListofComplianceViewModel()
        {
            Actslist = new List<ComplianceXref>();
            CountryList = new List<SelectListItem>();
            IndustryTypeList = new List<SelectListItem>();
            ComplianceTypeList = new List<SelectListItem>();
            Rulelist = new List<ComplianceXref>();
        }
    }
}