using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class ListOfGroupCompanies
    {
        public string CompanyName { get; set; }
        public int OrganizationID { get; set; }
        public bool IsActive { get; set; }
        public string Logo { get; set; }
        public List<SelectListItem> GroupCompaniesList { get; set; }
        public int GroupCompanyID { get; set; }
    }
}