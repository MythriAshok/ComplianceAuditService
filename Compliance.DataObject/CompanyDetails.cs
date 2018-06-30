using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataObject
{
  public  class CompanyDetails
    {
        public int Company_Details_ID { get; set; }
        public int Org_Hier_ID { get; set; }
        public string Industry_Type { get; set; }
        public string Formal_Name { get; set; }
        public DateTime Calender_StartDate { get; set; } 
        public DateTime Calender_EndDate  { get; set; }
        public string Auditing_Frequency { get; set; }
        public string Website { get; set; }
        public int Company_EmailID { get; set; }
        public string Company_ContactNumber1 { get; set; }
        public string Company_ContactNumber2  { get; set; }
    }
}
