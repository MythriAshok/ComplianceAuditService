using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class AboutCompanyViewModel
    {
        public int CompanyID { get; set; }
        public string  CompanyName { get; set; }
        public string  GroupCompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyLogo { get; set; }
        public int ParentCompanyID { get; set; }
        public string Website { get; set; }
        public bool Is_Active { get; set; }
        public bool IsVendor { get; set; }

        public List<SelectListItem> CompaniesList { get; set; }
        public List<AboutCompanyViewModel> AboutGroupCompany { get; set; }
        public List<AboutCompanyViewModel> AboutCompany { get; set; }
        public List<AboutCompanyViewModel> AboutBranch { get; set; }
        public int CompanyListID { get; set; }
        public string CompanyNameList { get; set; }
        public int OrganizationID { get; set; }
        public string IndustryType { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string logo { get; set; }

    }

}
