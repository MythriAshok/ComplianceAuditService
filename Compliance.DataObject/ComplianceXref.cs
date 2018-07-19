using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Compliance.DataObject
{
    public class ComplianceXref
    {
        public int Compliance_Xref_ID { get; set; }
        public string Comp_Category { get; set; }
        public string Compliance_Title { get; set; }
        public string Comp_Description { get; set; }
        public string compl_def_consequence { get; set; }
        public bool  Is_Header {get; set;}
        public int level { get; set; }
        public int Comp_Order { get; set; }
        public int Compliance_Parent_ID { get; set; }
        public int Option_ID { get; set; }
        public string Risk_Category { get; set; }
        public string Risk_Description { get; set; }
        public string Recurrence { get; set; }
        public string Form { get; set; }
        public string Type { get; set; }
        public bool Is_Best_Practice { get; set; }
        public int Version { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Effective_Start_Date { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Effective_End_Date { get; set; }
        public int Country_ID { get; set; }
        public int State_ID { get; set; }
        public int City_ID { get; set; }
        public DateTime Last_Updated_Date { get; set; }
        public int User_ID { get; set; }
        public bool Is_Active { get; set; }

    }
}
