using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
    }
}
