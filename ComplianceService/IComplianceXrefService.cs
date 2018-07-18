using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IComplianceXrefService" in both code and config file together.
    [ServiceContract]
    public interface IComplianceXrefService
    {
        [OperationContract]
        bool insertActs(ComplianceXref compliance);
        [OperationContract]
        bool insertRules(ComplianceXref compliance);
        [OperationContract]
        bool UpdateActs(ComplianceXref compliance);
        [OperationContract]
        bool UpdateRules(ComplianceXref compliance);
        [OperationContract]
        string GetComplianceXref();
    }
}
