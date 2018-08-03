using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataObject
{
   public class VendorMaster
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorType { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string VendorWebsite { get; set; }
        public string VendorAuditingFrequency { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime VendorStartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime VendorEndDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int OrgCompanyID { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
