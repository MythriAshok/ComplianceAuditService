using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;


namespace ComplianceAuditWeb.Models
{
    public class VendorViewModel
    {
        public VendorMaster vendor { get; set; }

        public List<SelectListItem> CompaniesList { get; set; }
        public string ChildCompanyName { get; set; }
        public List<SelectListItem> GroupCompaniesList { get; set; }
        public int CompanyID { get; set; }
        public int GroupCompanyID { get; set; }

       


        public enum VendorType
        {
            IT,
            Manufacturing,
            Financial
        }

        public enum AuditingFrequency
        {
            Quarterly,
            HalfYearly,
            Anually
        }
    }
}