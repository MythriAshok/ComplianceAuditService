using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class BranchViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        public CompanyDetails companydetails { get; set; }
        public List<Country> countrylist { get; set; }
        public List<State> State { get; set; }
        public List<City> City { get; set; }
    }
    public enum BranchAuditingfrequency
    {
        Quaterly,
        HalfYearly,
        Annually
    }
    public enum TypeOfBranch
    {
        SalesOffice,
        Manufacturing
    }
}