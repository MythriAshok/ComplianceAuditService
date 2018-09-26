using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Rotativa;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportingService" in both code and config file together.
    [ServiceContract]
    public interface IReportingService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        string getBranchReport(int BranchID, DateTime StartDate, DateTime EndDate, int ComplianceID, int VendorID);
        //[OperationContract]

        //ViewAsPdf GeneratePDF(List<ComplianceAudit> model);
        [OperationContract]

        string getBranchStatusReport(int BranchID, string status, DateTime StartDate, DateTime EndDate, int ComplianceID, int VendorID);
        [OperationContract]

        string getBranchRACTeport(int BranchID);
        [OperationContract]

        string getBranchStatusACTReport(int BranchID, string status);
        [OperationContract]

        string getBranchpieReport(int BranchID);
        //[OperationContract]

        //string getYearForAuditReport(int OrgID);
    }
}
