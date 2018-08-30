using Compliance.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class GroupCompanyViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }

        //public List<SelectListItem> Country { get; set; }

        //public List<SelectListItem> State { get; set; }
        //public List<SelectListItem> City { get; set; }

        //public enum AuditingFrequency
        //{
        //    Quarterly,
        //    HalfYearly,
        //    Annaully
        //}

        //public enum IndustryType
        //{
        //    IT,
        //    Manufacturing
        //}



    }

}
