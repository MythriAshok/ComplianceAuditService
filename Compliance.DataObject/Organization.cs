using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class Organization
    {
        public int OrganizationId { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public int ParentCompanyId { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Leaf { get; set; }
        public string IndustryType { get; set; }
        public int BranchId { get; set; }

        public int UserId { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
