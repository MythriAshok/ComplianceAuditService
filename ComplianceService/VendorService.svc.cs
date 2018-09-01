using Compliance.DataAccess;
using Compliance.DataObject;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool insertVendorForBranch(int VendorID, int OrgCompanyID,DateTime StartDate, Nullable<DateTime> EndDate, bool IsActive)
        {
            bool insertResult = false;
            try
            {
                VendorHelper vendorhelper = new VendorHelper();
               // foreach (var item in VendorID)
                {
                    insertResult =Convert.ToBoolean( vendorhelper.insertVendorForBranch(VendorID, OrgCompanyID, 'I',StartDate,EndDate,IsActive));
                }
            }
            catch
            {
                throw;
            }
            return insertResult;
        }

        public string DeactivateVendorForCompany(int VendorID)
        {
            string vendor = "";
            VendorHelper vendorHelper = new VendorHelper();
            vendor=Convert.ToString( vendorHelper.DeactivateVendorForCompany(VendorID));
            if(vendor != null)
            {
                return vendor;
            }
            else
            {
                return "Not deactivated";
            }
        }

          public string DeleteVendorForCompany(int VendorID)
        {
            string vendor = "";
            OrganizationHelper vendorHelper = new OrganizationHelper();
            vendor=Convert.ToString( vendorHelper.deleteVendorUnderCompany(VendorID));
            if(vendor != null)
            {
                return vendor;
            }
            else
            {
                return "Not deleted";
            }
        }

        public string ActivateVendorForCompany(int VendorID)
        {
            string vendor = "";
            VendorHelper vendorHelper = new VendorHelper();
            vendor = Convert.ToString(vendorHelper.ActivateVendorForCompany(VendorID));
            if (vendor != null)
            {
                return vendor;
            }
            else
            {
                return "Not activated";
            }
        }
        public string DeactivateVendorForBranch(int[] VendorID, int BranchID)
        {
            string vendor = "";
            VendorHelper vendorHelper = new VendorHelper();
            foreach(var item in VendorID)
            vendor = Convert.ToString(vendorHelper.DeactivateVendorForBranch((item), BranchID));
            if (vendor != null)
            {
                return vendor;
            }
            else
            {
                return "Not deactivated";
            }
        }
        public string ActivateVendorForBranch(int BranchID)
        {
            string vendor = "";
            VendorHelper vendorHelper = new VendorHelper();
            vendor = Convert.ToString(vendorHelper.ActivateVendorForBranch(BranchID));
            if (vendor != null)
            {
                return vendor;
            }
            else
            {
                return "Not deactivated";
            }
        }

        public string GetAssignedVendorsforBranch(int BranchID)
        {
            return BindAssignedVendorsforBranch(BranchID);
        }
        private string BindAssignedVendorsforBranch(int BranchID)
        {
            OrganizationHelper vendorhelper = new OrganizationHelper();
            DataSet vendors = vendorhelper.getVendorsForBranch(BranchID);
            UtilityHelper utilityHelper = new UtilityHelper();
            vendors = utilityHelper.ConvertNullsToEmptyString(vendors);
            string xmlroles = vendors.GetXml();
            return xmlroles;
        }
       public string GetBranchesAssociatedWithVendors(int VendorID)
        {
            return BindBranchesAssociatedWithVendors(VendorID);
        }
        private string BindBranchesAssociatedWithVendors(int VendorID)
        {
            OrganizationHelper vendorhelper = new OrganizationHelper();
            DataSet vendors = vendorhelper.getBranchAssociatedWithVendors(VendorID);
            UtilityHelper utilityHelper = new UtilityHelper();
            vendors = utilityHelper.ConvertNullsToEmptyString(vendors);
            string xmlroles = vendors.GetXml();
            return xmlroles;
        }

    }
}
