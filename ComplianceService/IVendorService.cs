using Compliance.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVendorService" in both code and config file together.
    [ServiceContract]
    public interface IVendorService
    {
        [OperationContract]
        int insertVendor(VendorMaster vendor);

        [OperationContract]
        int updateVendor(VendorMaster vendor);

        [OperationContract]
        bool insertVendorForBranch(int[] VendorID, int OrgCompanyID, DateTime StartDate,Nullable< DateTime> EndDate, bool IsActive);
        [OperationContract]
        string DeactivateVendorForCompany(int VendorID);
        [OperationContract]
        string ActivateVendorForCompany(int VendorID);
        [OperationContract]
        string DeactivateVendorForBranch(int VendorBranchID);
        [OperationContract]
        string ActivateVendorForBranch(int VendorBranchID);


    }
}
