using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class CompanyViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        public CompanyDetails companydetails { get; set;}
        public List<Country> countrylist { get; set; }

    }
    public enum Auditingfrequency
    {
        Quaterly,
        HalfYearly,
        Annually
    }
}