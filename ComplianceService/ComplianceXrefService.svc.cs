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
            compliance.level = "1";
            return helper.insertupdateComplianceXref(compliance, 'I');
        }
        public bool UpdateActs(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            return helper.insertupdateComplianceXref(compliance, 'U');
        }
        public bool insertRules(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = true;
            compliance.level = "1";
            return helper.insertupdateComplianceXref(compliance, 'I');
        }
        public bool UpdateRules(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            return helper.insertupdateComplianceXref(compliance, 'U');
        }
        public string GetComplianceXref()
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds=helper.getComlianceXref(0);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();            
        }

        
    }
}
