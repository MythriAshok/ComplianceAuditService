using Compliance.DataObject;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataAccess
{
   public class VendorHelper
    {
        MySqlConnection conn = DBConnection.getconnection();
        public int insertupdateVendor(VendorMaster vendor, char Flag)
        {
            int VendorID = 0;
            try
            {
                if (vendor != null)
                {
                    conn.Open();
                    MySqlTransaction tran = conn.BeginTransaction();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateVendorMaster", conn, tran);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Flag", Flag);
                    cmd.Parameters.AddWithValue("p_Vendor_ID", vendor.VendorID);
                    cmd.Parameters.AddWithValue("p_Vendor_Name", vendor.VendorName);
                    cmd.Parameters.AddWithValue("p_Contact_Person", vendor.ContactPerson);
                    cmd.Parameters.AddWithValue("p_Contact_Number", vendor.ContactNumber);
                    cmd.Parameters.AddWithValue("p_Start_Date", vendor.VendorStartDate);
                    cmd.Parameters.AddWithValue("p_End_Date", vendor.VendorEndDate);
                    cmd.Parameters.AddWithValue("p_Website", vendor.VendorWebsite);
                    cmd.Parameters.AddWithValue("p_Auditing_Frequency", vendor.VendorAuditingFrequency);
                    cmd.Parameters.AddWithValue("p_Vendor_Type", vendor.VendorType);
                    cmd.Parameters.AddWithValue("p_Last_Updated_Date", vendor.LastUpdatedDate);
                    cmd.Parameters.AddWithValue("p_Company_ID", vendor.OrgCompanyID);
                    cmd.Parameters.AddWithValue("p_User_ID", vendor.UserID);
                    cmd.Parameters.AddWithValue("p_Is_Active", vendor.IsActive);
                    cmd.Parameters.AddWithValue("p_Is_Delete", vendor.IsDelete);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    object objvendorid = cmd.ExecuteScalar();
                    if (Convert.ToInt32(objvendorid) != 0)
                    {
                        VendorID = Convert.ToInt32(objvendorid);
                    }
                    tran.Commit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return VendorID;
        }



        public DataSet getVendorList(int PCompanyID)
        {
            DataSet dsVendorList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getSpecificVendorList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Parent_Company_ID", PCompanyID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsVendorList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return dsVendorList;
        }
     public DataSet getcompleteVendorList(int VendorID)
        {
            DataSet dsVendorList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getcompleteVendorList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Vendor_ID", VendorID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsVendorList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return dsVendorList;
        }

     public DataSet getcompleteVendorListassignedToBranch(int BranchVendorID)
        {
            DataSet dsVendorList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_sp_getcompleteVendorListassignedToBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Vender_Branch_ID", BranchVendorID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsVendorList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return dsVendorList;
        }




        public int insertVendorForBranch(int VendorID, int OrgCompanyID, char Flag, DateTime StartDate,Nullable< DateTime> EndDate, bool IsActive)
        {
            int VendorBranchID = 0;
            bool res = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_insertupdateVendorForBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Flag", Flag);
                // cmd.Parameters.AddWithValue("Vendor_Branch_ID", Flag); 
                cmd.Parameters.AddWithValue("p_Vendor_Branch_ID", VendorBranchID);
                cmd.Parameters.AddWithValue("p_Vendor_ID", VendorID);
                cmd.Parameters.AddWithValue("p_Branch_ID", OrgCompanyID);
                cmd.Parameters.AddWithValue("p_Start_Date", StartDate);
                cmd.Parameters.AddWithValue("p_End_Date", EndDate);
                cmd.Parameters.AddWithValue("p_Is_Active", IsActive);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objvendorbranchid = cmd.ExecuteScalar();
                if (objvendorbranchid != null)
                {
                    VendorBranchID = Convert.ToInt32(objvendorbranchid);
                }

                //int count = cmd.ExecuteNonQuery();
                //if (count > 0)
                //{
                //    res = true;
                //}
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return VendorBranchID;
        }

        public DataSet getVendorListForBRanch(int BranchID)
        {
            DataSet dsVendorList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getVendorListForBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Branch_ID", BranchID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsVendorList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return dsVendorList;
        }

        public bool DeactivateVendorForCompany(int VendorID)
        {
            bool vendorForCompany = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DeactivateVendorForCompany", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", VendorID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    vendorForCompany = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return vendorForCompany;
        }

        public bool ActivateVendorForCompany(int VendorID)
        {
            bool vendorForCompany = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ActivateVendorForCompany", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", VendorID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    vendorForCompany = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return vendorForCompany;
        }
        public bool DeactivateVendorForBranch(int BranchVendorID)
        {
            bool vendorForCompany = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DeactivateVendorForBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Vendor_Branch_ID", BranchVendorID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    vendorForCompany = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return vendorForCompany;
        }
        public bool ActivateVendorForBranch(int VendorBranchID)
        {
            bool vendorForCompany = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ActivateVendorForBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Vendor_Branch_ID", VendorBranchID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    vendorForCompany = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return vendorForCompany;
        }
    }
}
