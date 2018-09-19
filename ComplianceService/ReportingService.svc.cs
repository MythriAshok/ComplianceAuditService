using Compliance.DataAccess;
using Compliance.DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Rotativa;
using System.Collections;
using Rotativa.Options;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReportingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReportingService.svc or ReportingService.svc.cs at the Solution Explorer and start debugging.
    public class ReportingService : IReportingService
    {
        public void DoWork()
        {
        }

        public string getBranchReport(int BranchID)
        {
            return bindBranchReport(BranchID);
        }

        private string bindBranchReport(int BranchID)
        {
            ComplianceAudit auditReport = new ComplianceAudit();
            ReportHelper reportHelper = new ReportHelper();
            DataSet dsBranchReport = reportHelper.getBranchComlianceAuditReport(BranchID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranchReport = utilityHelper.ConvertNullsToEmptyString(dsBranchReport);
            string xmlBranchReport = dsBranchReport.GetXml();
            return xmlBranchReport;
        }

        public string getBranchRACTeport(int BranchID)
        {
            return bindBranchACTReport(BranchID);
        }

        private string bindBranchACTReport(int BranchID)
        {
            ComplianceAudit auditReport = new ComplianceAudit();
            ReportHelper reportHelper = new ReportHelper();
            DataSet dsBranchReport = reportHelper.getDetailedBranchACTComlianceAuditReport(BranchID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranchReport = utilityHelper.ConvertNullsToEmptyString(dsBranchReport);
            string xmlBranchReport = dsBranchReport.GetXml();
            return xmlBranchReport;
        }


        public string getBranchStatusReport(int BranchID, string status)
        {
            return bindBranchStatusReport(BranchID, status);
        }

        private string bindBranchStatusReport(int BranchID, string status)
        {
            ComplianceAudit auditReport = new ComplianceAudit();
            ReportHelper reportHelper = new ReportHelper();
            DataSet dsBranchReport = reportHelper.getBranchStatusComlianceAuditReport(BranchID, status);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranchReport = utilityHelper.ConvertNullsToEmptyString(dsBranchReport);
            string xmlBranchReport = dsBranchReport.GetXml();
            return xmlBranchReport;
        }

        public string getBranchStatusACTReport(int BranchID, string status)
        {
            return bindBranchStatusACTReport(BranchID, status);
        }

        private string bindBranchStatusACTReport(int BranchID, string status)
        {
            ComplianceAudit auditReport = new ComplianceAudit();
            ReportHelper reportHelper = new ReportHelper();
            DataSet dsBranchReport = reportHelper.getComplianceStatusBranchACTAuditReport(BranchID, status);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranchReport = utilityHelper.ConvertNullsToEmptyString(dsBranchReport);
            string xmlBranchReport = dsBranchReport.GetXml();
            return xmlBranchReport;
        }


    }
}
