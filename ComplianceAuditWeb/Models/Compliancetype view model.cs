using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplianceAuditWeb.Models
{
    public class Compliancetype_view_model
    {
        public List<compliance_type> compliance_Types { get; set; }

        public int branchid { get; set; }

        public int vendorid { get; set; }

        public string vendorname { get; set; }

        //public List<DateTime> date { get; set; }
      

    }

    public class compliance_type
    {
        public int complianceid { get; set; }
        public int auditfrequency { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public String Name { get; set; }
    
    }

    public enum fullcalender
    {
        jan,
        feb,
        mar,
        apr,
        may,
        june,
        july,
        aug,
        sep,
        oct,
        nov,
        dec
    }

    public enum Quateryly
    {
        jan_mar,
        apr_june,
        july_sep,
        oct_dec
    }

    public enum halfyearly
    {
        jan_may,
        june_dec
    }

    
}