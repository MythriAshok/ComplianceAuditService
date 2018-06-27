using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class State
    {
        public int StateId { get; set; }
        public int StateCode { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
    }
}
