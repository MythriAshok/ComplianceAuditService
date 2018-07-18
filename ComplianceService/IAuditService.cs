using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuditService" in both code and config file together.
    [ServiceContract]
    public interface IAuditService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]

        bool insertComplianceAudit(List<ComplianceAudit> auditdatalist);
        [OperationContract]

        bool updateComplianceAudit(List<ComplianceAudit> auditdatalist);

        [OperationContract]

        bool getComplianceAudit(int ComplianceAuditID);
        [OperationContract]

        bool deleteComplianceAudit(int ComplianceAuditID);
        [OperationContract]

        bool insertComplianceAuditTrail(List<ComplianceAuditAuditTrail> auditdatalisttrail);
        [OperationContract]

        bool updateComplianceAuditTrail(List<ComplianceAuditAuditTrail> auditdatalisttrail);
        [OperationContract]
        bool getComplianceAuditTrail(int ComplianceAuditTrailID);
        [OperationContract]
        bool deleteComplianceAuditTrail(int ComplianceAuditTrailID);
        [OperationContract]

        string getCompany(int AuditorID);
    }
}
