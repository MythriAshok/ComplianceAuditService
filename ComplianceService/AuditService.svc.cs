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


        public string getComplianceAudit(int ComplianceAuditID)
        {
            return bindComplianceAudit(ComplianceAuditID);
        }
        private string bindComplianceAudit(int ComplianceAuditID)
        {
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            DataSet dsComplianceAudit =  complianceAuditHelper.getComlianceAudit(ComplianceAuditID);
            string strxmlComplianceAudit = dsComplianceAudit.GetXml();
            return strxmlComplianceAudit;
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



        public string getComplianceAuditTrail(int ComplianceAuditTrailID)
        {
            return bindComplianceAuditTrail(ComplianceAuditTrailID);
        }

        private string bindComplianceAuditTrail(int ComplianceAuditTrailID)
        {
            ComplianceAuditTrailHelper complianceAuditHelper = new ComplianceAuditTrailHelper();
            DataSet dsComplianceAuditTrail = complianceAuditHelper.getComlianceAuditTrail(ComplianceAuditTrailID);
            string strxmlComplianceAuditTrail = dsComplianceAuditTrail.GetXml();
            return strxmlComplianceAuditTrail;

            
        }

        public string getAllCompanyBrnachAssignedtoAuditor(int AuditotID)
        {
            return bindAllCompanyBrnachAssignedtoAuditor(AuditotID);
        }

        private string bindAllCompanyBrnachAssignedtoAuditor(int AuditotID)
        {
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            DataSet dsCompliance = complianceAuditHelper.sp_getAllCompanyBrnachAssignedtoAuditor(AuditotID);
            string strxmlCompliance = dsCompliance.GetXml();
            return strxmlCompliance;
        }





        public string getComplianceXref(int OrgID)
        {
            return bindComplianceXref(OrgID);
        }

        private string bindComplianceXref(int OrgID)
        {
            ComplianceAuditHelper complianceAuditHelper = new ComplianceAuditHelper();
            DataSet dsCompliance = complianceAuditHelper.getComlianceXrefDataForSeletedBranch(OrgID);
            string strxmlCompliance = dsCompliance.GetXml();
            return strxmlCompliance;
        }
       

    }
}