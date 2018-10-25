using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataObject
{
   public class AuditCalender
    {
        public int AuditCalenderID  { get; set; }
        public int CompanyID  { get; set; }
        public int ComplainceTypeID  { get; set; }
        public DateTime StartDate  { get; set; }
        public Nullable< DateTime> EndDate  { get; set; }
        public int Year { get; set; }
        public int[] newyearid { get; set; }
    }
}
