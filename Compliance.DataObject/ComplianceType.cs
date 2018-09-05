using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataObject
{
  public  class ComplianceType
    {
        public int ComplianceTypeID { get; set; }
        public string ComplianceTypeName { get; set; }
        public int IndustryTypeID { get; set; }
        public int CountryID { get; set; }
        public string AuditingFrequency { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
       // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable< DateTime> EndDate { get; set; }


      
      

    }
}
