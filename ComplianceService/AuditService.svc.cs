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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuditService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuditService.svc or AuditService.svc.cs at the Solution Explorer and start debugging.
    public class AuditService : IAuditService
    {
        public void DoWork()
        {
        }

        //public bool insertUpdate ComplianceAudit(List<ComplianceAudit> auditdatalist, char Flag)
        //{
        //    bool result = false;
        //    int ComplianceAuditID = 0;
        //    ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
        //    ComplianceAuditID=Convert.ToInt32( complianceAuditHelper.insertupdateComplianceAudit(auditdatalist, Flag));
        //    if(ComplianceAuditID >0)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }
        //    return result;
        //}
    }
}
