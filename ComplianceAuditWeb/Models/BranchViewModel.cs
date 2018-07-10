using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compliance.DataObject;
using System.Data;

namespace ComplianceAuditWeb.Models
{
    public class BranchViewModel
    {
        public Organization organization { get; set; }
        public BranchLocation branch { get; set; }
        public BranchAuditingfrequency branchAuditingFrequency { get; set; }
        public TypeOfBranch branchType { get; set; }
        public DataTable Country { get; set; }
        public DataTable State { get; set; }
        public DataTable City { get; set; }

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