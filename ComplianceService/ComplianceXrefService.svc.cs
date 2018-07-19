using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;
using System.Data;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ComplianceXrefService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ComplianceXrefService.svc or ComplianceXrefService.svc.cs at the Solution Explorer and start debugging.
    public class ComplianceXrefService : IComplianceXrefService
    {
        public bool insertActs(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = true;
            compliance.level = 1;
            compliance.Comp_Category = "Act";
            compliance.Comp_Order = 1;
            compliance.Version = 1;
            compliance.Compliance_Parent_ID = 0;
            return helper.insertupdateComplianceXref(compliance,'I');
        }
        public bool insertSection(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = true;
            compliance.level = 2;
            compliance.Comp_Category = "Section";
            compliance.Comp_Order = 2;
            compliance.Version = 1;
            return helper.insertupdateComplianceXref(compliance,'I');
        }
        public bool insertRules(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = true;
            compliance.level = 3;
            compliance.Comp_Category = "Rule";
            compliance.Version = 1;
            return helper.insertupdateComplianceXref(compliance,'I');
        }
        public bool UpdateActs(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            return helper.insertupdateComplianceXref(compliance,'U');
        }
        public bool UpdateRules(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            return helper.insertupdateComplianceXref(compliance,'U');
        }
        public string GetActs()
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds=helper.getAct();
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();            
        }
        public string GetSections()
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getSection();
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
        public string GetRules()
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getRules();
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
    }
}
