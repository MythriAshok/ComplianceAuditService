using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class ComplianceViewModel
    {
        public ComplianceXref Compliance { get; set; }
        public List<SelectListItem> Countrylist { get; set; }

        public List<SelectListItem> Statelist { get; set; }

        public List<SelectListItem> Citylist { get; set; }

        public bool Is_StateLaw { get; set; }

        public bool Is_RegionalLaw { get; set; }
        public enum RiskCategory
        {
            High,
            Medium,
            Low
        }

        public enum ActType
        {
            Union_Level,
            State_Level,
            Regional_Level
        }
    }

  

}