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

        public string getBranchReport(int BranchID, DateTime StartDate, DateTime EndDate, int ComplianceTypeID, int VendorID)
        {
            return bindBranchReport(BranchID, StartDate, EndDate, ComplianceTypeID, VendorID);
        }

        private string bindBranchReport(int BranchID , DateTime StartDate, DateTime EndDate, int ComplianceTypeID, int VendorID)
        {
            ComplianceAudit auditReport = new ComplianceAudit();
            ReportHelper reportHelper = new ReportHelper();
            DataSet dsBranchReport = reportHelper.getBranchComlianceAuditReport(BranchID, StartDate, EndDate, ComplianceTypeID, VendorID);
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


        public string getBranchStatusReport(int BranchID, string status, DateTime StartDate, DateTime EndDate, int ComplianceID, int VendorID)
        {
            return bindBranchStatusReport(BranchID, status, StartDate, EndDate, ComplianceID, VendorID);
        }

        private string bindBranchStatusReport(int BranchID, string status, DateTime StartDate, DateTime EndDate, int ComplianceID, int VendorID)
        {
            ComplianceAudit auditReport = new ComplianceAudit();
            ReportHelper reportHelper = new ReportHelper();
            DataSet dsBranchReport = reportHelper.getBranchStatusComlianceAuditReport(BranchID, status, StartDate, EndDate, ComplianceID, VendorID);
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

        public string getBranchpieReport(int BranchID)
        {
            return bindBranchpieReport(BranchID);
        }

        private string bindBranchpieReport(int BranchID)
        {
            ComplianceAudit auditReport = new ComplianceAudit();
            ReportHelper reportHelper = new ReportHelper();
            DataSet dsBranchReport = reportHelper.getpieBranchComlianceAuditReport(BranchID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranchReport = utilityHelper.ConvertNullsToEmptyString(dsBranchReport);
            string xmlBranchReport = dsBranchReport.GetXml();
            return xmlBranchReport;
        }

        //public string getYearForAuditReport(int OrgID)
        //{
        //    return bindYearForAuditReport(OrgID);
        //}

        //private string bindYearForAuditReport(int OrgID)
        //{
        //    ComplianceAudit auditReport = new ComplianceAudit();
        //    ReportHelper reportHelper = new ReportHelper();
        //    DataSet dsBranchReport = reportHelper.getYearforAuditReports(OrgID);
        //    UtilityHelper utilityHelper = new UtilityHelper();
        //    dsBranchReport = utilityHelper.ConvertNullsToEmptyString(dsBranchReport);
        //    string xmlBranchReport = dsBranchReport.GetXml();
        //    return xmlBranchReport;
        //}
    }
}
