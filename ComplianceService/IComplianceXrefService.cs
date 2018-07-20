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
        int insertActs(ComplianceXref compliance);
        [OperationContract]
        int insertRules(ComplianceXref compliance);
        [OperationContract]
        int insertSection(ComplianceXref compliance);
        [OperationContract]
        int UpdateActs(ComplianceXref compliance);
        [OperationContract]
        int UpdateRules(ComplianceXref compliance);
        [OperationContract]
        string GetActs();
        [OperationContract]
        string GetSections(int parentid);
        [OperationContract]
        string GetRules(int parentid);


    }
}
