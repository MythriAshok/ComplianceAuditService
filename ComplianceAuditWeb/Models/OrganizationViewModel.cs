using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class OrganizationViewModel
    {
        public Organization organization { get; set; }
        public Branch branch { get; set; }
        public CompanyDetails companydetails { get; set; }
        public List<Country> Country { get; set; }
        public List<State> State { get; set; }
        public List<City> City { get; set; }
    }
}