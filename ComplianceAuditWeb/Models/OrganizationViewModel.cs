using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Data;
using System.Collections;

namespace ComplianceAuditWeb.Models
{
    public class OrganizationViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        public CompanyDetails companydetails { get; set; }
        public DataSet Country { get; set; }
        public DataSet State { get; set; }
        public DataSet City { get; set; }
        public IEnumerable CountryList { get; set; }
        public IEnumerable <Country> cList { get; set; }
        public List<Country> CountriesList { get; set; }

        // public City city { get; set; }
        public enum AuditingFrequency
        {
            Quarterly,
            HalfYearly,
            Annaully
        }
       
     
        
    }
    
}