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

        string getComplianceAudit(int ComplianceAuditID);
        [OperationContract]

        bool deleteComplianceAudit(int ComplianceAuditID);
        [OperationContract]

        bool insertComplianceAuditTrail(List<ComplianceAuditAuditTrail> auditdatalisttrail);

        [OperationContract]
        string getComplianceAuditTrail(int ComplianceAuditTrailID);
        [OperationContract]


        string getCompanyAllocatedToAuditor(int AuditorID);

        [OperationContract]


        string getBranchAllocatedToAuditor(int AuditorID);
        [OperationContract]

        string getComplianceXref(int ComplianceXrefID);

    }
}
