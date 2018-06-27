using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.DataObject
{
   public class Compliance
    {
        public int ComplianceId { get; set; }
        public string ComplianceName { get; set; }
        public string ComplianceDescription { get; set; }
        public bool IsHeader { get; set; }
        public int Level { get; set; }
        public int ComplianceOrder { get; set; }
        public int OptionId { get; set; }
        public string RiskCategory { get; set; }
        public string RiskDescription { get; set; }
        public string Recurrance { get; set; }
        public string Form { get; set; }
        public string Type { get; set; }
        public bool IsBestPractise { get; set; }
        public int Version { get; set; }
        public int EffectiveStartDate { get; set; }
        public int EffectiveEndDate { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int UserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
