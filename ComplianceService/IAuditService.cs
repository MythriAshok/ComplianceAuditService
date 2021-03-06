﻿using System;
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
        bool UpdatetAuditEntries(ComplianceAudit auditdata);
        [OperationContract]
        bool insertComplianceAudit(List<ComplianceAudit> auditdatalist);
        [OperationContract]
        bool insertCustomAuditEntries(ComplianceAudit auditdata);
        [OperationContract]
        bool insertAuditEntries(ComplianceAudit auditdata);
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


       


        string getAllCompanyBrnachAssignedtoAuditor(int AuditorID);
        [OperationContract]

        string getComplianceXref(int OrgID, int VendorID, int compliancetypeid, int complianceparentid);
        [OperationContract]
        string getSpecificBranchList(int CompID);

        [OperationContract]

        string getComplianceActList(int OrgID, int VendorID, int compliancetypeid);

        [OperationContract]
        string getcomplianceonorg(int OrgID, int VendorID, int version,DateTime sdate,DateTime edate);

    }
}
