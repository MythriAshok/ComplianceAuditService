﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Compliance.DataObject;
using System.ComponentModel.DataAnnotations;

namespace ComplianceAuditWeb.Models
{
    public class ComplianceViewModel
    {
        public ComplianceXref Compliance { get; set; }
        
        public List<SelectListItem> Countrylist { get; set; }

        public List<SelectListItem> Statelist { get; set; }

        public List<SelectListItem> Citylist { get; set; }
        [Required]
        public int ActTypeID { get; set; }
        public enum RiskCategory
        {
            High,
            Medium,
            Low
        }
      
        public List<SelectListItem> ActType { get; set; }
        public List<SelectListItem> ComplianceType { get; set; }


    }

  

}