using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Data;

namespace ComplianceAuditWeb.Models
{
    public class OrganizationViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        public CompanyDetails companydetails { get; set; }
        public DataTable Country { get; set; }
        public DataTable State { get; set; }
        public DataTable City { get; set; }

       // public City city { get; set; }
     
        
    }
    
}