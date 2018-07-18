﻿using System;
using System.Collections.Generic;
using System.Data;
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

        public bool insertComplianceAudit(List<ComplianceAudit> auditdatalist)
        {
            bool result = false;
            int ComplianceAuditID = 0;
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            ComplianceAuditID = Convert.ToInt32(complianceAuditHelper.insertupdateComplianceAudit(auditdatalist, 'I'));
            if (ComplianceAuditID > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool updateComplianceAudit(List<ComplianceAudit> auditdatalist)
        {
            bool result = false;
            int ComplianceAuditID = 0;
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            ComplianceAuditID = Convert.ToInt32(complianceAuditHelper.insertupdateComplianceAudit(auditdatalist, 'U'));
            if (ComplianceAuditID > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }



        public bool getComplianceAudit(int ComplianceAuditID)
        {
            bool result = false;
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            ComplianceAuditID = Convert.ToInt32(complianceAuditHelper.deleteComlianceAudit(ComplianceAuditID));
            if (ComplianceAuditID > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool deleteComplianceAudit(int ComplianceAuditID)
        {
            bool result = false;
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            ComplianceAuditID = Convert.ToInt32(complianceAuditHelper.deleteComlianceAudit(ComplianceAuditID));
            if (ComplianceAuditID > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool insertComplianceAuditTrail(List<ComplianceAuditAuditTrail> auditdatalisttrail)// when data is updated or deleted from audit table
        {
            bool result = false;
            int ComplianceAuditTrailID = 0;
            ComplianceAuditTrailHelper complianceAuditTrailHelper = new ComplianceAuditTrailHelper();
            ComplianceAuditTrailID = Convert.ToInt32(complianceAuditTrailHelper.insertupdateComplianceAuditTrail(auditdatalisttrail));
            if (ComplianceAuditTrailID > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

       

        public bool getComplianceAuditTrail(int ComplianceAuditTrailID)
        {
            bool result = false;
            ComplianceAuditTrailHelper complianceAuditTrailHelper = new ComplianceAuditTrailHelper();
            ComplianceAuditTrailID = Convert.ToInt32(complianceAuditTrailHelper.getComlianceAuditTrail(ComplianceAuditTrailID));
            if (ComplianceAuditTrailID > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

      


        public string getCompanyAllocatedToAuditor(int AuditorID)
        {
            return bindCompanyAllocatedToAuditor(AuditorID);
        }

        private string bindCompanyAllocatedToAuditor( int AuditorID)
        {
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            DataSet dsCompanies = complianceAuditHelper.getAllCompaniesAllocatedToAuditor(AuditorID);
            string strxmlAllCompanies = dsCompanies.GetXml();
            return strxmlAllCompanies;
        }

        public string getBranchAllocatedToAuditor(int AuditorID)
        {
            return bindBranchAllocatedToAuditor(AuditorID);
        }

        private string bindBranchAllocatedToAuditor(int AuditorID)
        {
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            DataSet dsCompanies = complianceAuditHelper.getAllBranchAllocatedToAuditor(AuditorID);
            string strxmlAllBranches = dsCompanies.GetXml();
            return strxmlAllBranches;
        }

        public string getComplianceXref(int BranchID)
        {
            return bindComplianceXref(BranchID);
        }

        private string bindComplianceXref(int BranchID)
        {
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            DataSet dsCompliance = complianceAuditHelper.getComlianceXrefDataForSeletedBranch(BranchID);
            string strxmlCompliance = dsCompliance.GetXml();
            return strxmlCompliance;
        }
       

    }
}
