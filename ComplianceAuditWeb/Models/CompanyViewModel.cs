using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Data;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class CompanyViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        public CompanyDetails companydetails { get; set;}
        public List<SelectListItem> Country { get; set; }
        public List<SelectListItem> State { get; set; }
        public List<SelectListItem> City { get; set; }

        public List<SelectListItem> GroupCompaniesList { get; set; }
        public int GroupCompanyID { get; set; }


    }
    public enum Auditingfrequency
    {
        Quaterly,
        HalfYearly,
        Annually
    }

    public enum IndustryType
    {
        IT,
        Manufacturing
    }
}