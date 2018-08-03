using Compliance.DataAccess;
using Compliance.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VendorService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select VendorService.svc or VendorService.svc.cs at the Solution Explorer and start debugging.
    public class VendorService : IVendorService
    {
     

        public int insertVendor(VendorMaster vendor)
        {
            int VendorID = 0;
            bool insertResult = false;
            try
            {
                VendorHelper vendorhelper = new VendorHelper();
                VendorID = vendorhelper.insertupdateVendor(vendor, 'I');
                if (VendorID>0)
                {
                    insertResult = true;
                }
            }
            catch
            {
                throw;
            }
            return VendorID;
        }

        public int updateVendor(VendorMaster vendor)
        {
            int VendorID = 0;
            bool updateResult = false;
            try
            {
                VendorHelper vendorhelper = new VendorHelper();
                VendorID = vendorhelper.insertupdateVendor(vendor, 'U');
                if (VendorID > 0)
                {
                    updateResult = true;
                }
            }
            catch
            {
                throw;
            }
            return VendorID;
        }

        public int insertVendorForBranch(int[] VendorID, int OrgCompanyID)
        {
            int VendorBranchID = 0;
            bool insertResult = false;
            try
            {
                VendorHelper vendorhelper = new VendorHelper();
                foreach (var item in VendorID)
                {
                    insertResult =Convert.ToBoolean( vendorhelper.insertVendorForBranch(item, OrgCompanyID, 'I'));
                }
            }
            catch
            {
                throw;
            }
            return VendorBranchID;
        }

    }
}
