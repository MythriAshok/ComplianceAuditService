using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Compliance.DataObject;

namespace Compliance.DataAccess
{
    public class OrganizationHelper
    {
        MySqlConnection conn = DBConnection.getconnection();
        public int insertupdateBranchLocation(BranchLocation branchLocation, char Flag)
        {
            int BranchLocationId = 0;
            try
            {
                if (branchLocation != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateBranchLocation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("p_Flag ", Flag);
                    cmd.Parameters.Add("p_Flag", MySqlDbType.VarChar, 1).Value = Flag;
                    cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value= branchLocation.Branch_Id;
                    cmd.Parameters.Add("p_Location_Name", MySqlDbType.VarChar, 75).Value = branchLocation.Branch_Name;

                    cmd.Parameters.Add("p_Address", MySqlDbType.VarChar, 450).Value = branchLocation.Address;
                    cmd.Parameters.Add("p_Country_ID", MySqlDbType.Int32).Value = branchLocation.Country_Id;
                    cmd.Parameters.Add("p_State_ID", MySqlDbType.Int32).Value = branchLocation.State_Id;
                    cmd.Parameters.Add("p_City_ID", MySqlDbType.Int32).Value = branchLocation.City_Id;

                    cmd.Parameters.Add("p_Postal_Code", MySqlDbType.Int32).Value = branchLocation.Postal_Code;
                    cmd.Parameters.Add("p_Branch_Coordinates1", MySqlDbType.VarChar,100).Value = branchLocation.Branch_Coordinates1;
                    cmd.Parameters.Add("p_Branch_Coordinates2", MySqlDbType.VarChar,100).Value = branchLocation.Branch_Coordinates2;
                    cmd.Parameters.Add("p_Branch_CoordinateURL", MySqlDbType.VarChar,100).Value = branchLocation.Branch_CoordinatesURL;
                    // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    object objbranchlocationid = cmd.ExecuteScalar();
                    if (objbranchlocationid != null)
                    {
                        BranchLocationId = Convert.ToInt32(objbranchlocationid);
                    }
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
            return BranchLocationId;
        }

        public DataSet getBranchLocation(int Branch_Location_Id)
        {
            DataSet dsBranchLocation = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getBranchLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = CountryID;
                //cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = StateId;
                cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value = Branch_Location_Id;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsBranchLocation);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsBranchLocation;
        }

        public bool deleteBranchLocation(int Branch_Location_Id)
        {
            bool resultBranchLocation = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_deleteBranchLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = CountryID;
                //cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = StateId;
                cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value = Branch_Location_Id;
                object resultCount = cmd.ExecuteScalar();
                if (resultCount != null)
                {
                    resultBranchLocation = true;
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
            return resultBranchLocation;
        }

        public int insertupdateOrganizationHier(Organization org, char Flag)
        {
            int OrganizationId = 0;
            try
            {
                if (org != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateOrganizationHier", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Flag", Flag);
                    cmd.Parameters.AddWithValue("p_Org_Hier_ID", org.Organization_Id);
                    cmd.Parameters.AddWithValue("p_Company_Name", org.Company_Name);
                    cmd.Parameters.AddWithValue("p_Company_ID", org.Company_Id);
                    cmd.Parameters.AddWithValue("p_Parent_Company_ID", org.Parent_Company_Id);
                    cmd.Parameters.AddWithValue("p_Description", org.Description);
                    cmd.Parameters.AddWithValue("p_level", org.Level);
                    cmd.Parameters.AddWithValue("p_Is_Leaf", org.Is_Leaf);
                    cmd.Parameters.AddWithValue("p_Industry_Type", org.Industry_Type);
                    cmd.Parameters.AddWithValue("p_Last_Updated_Date", org.Last_Updated_Date);
                    cmd.Parameters.AddWithValue("p_Location_ID", org.Branch_Id);
                    cmd.Parameters.AddWithValue("p_User_ID", org.User_Id);
                    cmd.Parameters.AddWithValue("p_Is_Active", org.Is_Active);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    object objorganizationid = cmd.ExecuteScalar();
                    if (objorganizationid != null)
                    {
                        OrganizationId = Convert.ToInt32(objorganizationid);
                    }
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
            return OrganizationId;
        }

        public DataSet getOrganizationHier(int OrgID)
        {
            DataSet dsOrganization = new DataSet();
            try
            {
                conn.Open();
               // MySqlTransaction tran= conn.BeginTransaction();
              //  MySqlCommand cmd = new MySqlCommand("sp_getOrganizationHierJoin", conn,tran);
                MySqlCommand cmd = new MySqlCommand("sp_getOrganizationHierJoin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsOrganization);
               // tran.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsOrganization;
        }

        public bool deleteOrganizationHier(int Org_Hier_ID)
        {
            bool resultOrganization = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getOrganizationHier", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID ", Org_Hier_ID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultOrganization = true;
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
            return resultOrganization;
        }


        public int insertupdateCompanyDetails(CompanyDetails details, char Flag)
        {
            int CompanyDetailsId = 0;
            try
            {
                if (details != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateCompanyDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Flag", Flag);
                    cmd.Parameters.AddWithValue("p_Company_Details_ID", details.Company_Details_ID);
                    cmd.Parameters.AddWithValue("p_Org_Hier_ID", details.Org_Hier_ID);
                    cmd.Parameters.AddWithValue("p_Industry_Type", details.Industry_Type);
                    cmd.Parameters.AddWithValue("p_Formal_Name", details.Formal_Name);
                    cmd.Parameters.AddWithValue("p_Calender_StartDate", details.Calender_StartDate);
                    cmd.Parameters.AddWithValue("p_Calender_EndDate", details.Calender_EndDate);
                    cmd.Parameters.AddWithValue("p_Auditing_Frequency", details.Auditing_Frequency);
                    cmd.Parameters.AddWithValue("p_Website", details.Website);
                    cmd.Parameters.AddWithValue("p_Company_Email_ID", details.Company_EmailID);
                    cmd.Parameters.AddWithValue("p_Company_ContactNumber1", details.Company_ContactNumber1);
                    cmd.Parameters.AddWithValue("p_Company_ContactNumber2", details.Company_ContactNumber2);
                    cmd.Parameters.AddWithValue("p_Is_Active", details.Is_Active);
                    // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    object objcompanydetailsid = cmd.ExecuteScalar();
                    if (objcompanydetailsid != null)
                    {
                        CompanyDetailsId = Convert.ToInt32(objcompanydetailsid);
                    }
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
            return CompanyDetailsId;
        }

        public DataSet getCompanyDetails(int CompanyDetailsId)
        {
            DataSet dsCompanyDetails = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompanyDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Company_Details_ID ", CompanyDetailsId);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsCompanyDetails);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsCompanyDetails;
        }

       

        public bool deleteCompanyDetails(int Company_Details_Id)
        {
            bool resultCompanyDetails = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompanyDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Company_Details_ID ", Company_Details_Id);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultCompanyDetails = true;
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
            return resultCompanyDetails;
        }
       


        public DataSet getGroupCompanyList()
        {
            DataSet dsGroupCompaniesList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getGroupCompaniesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("p_Company_Details_ID ", CompanyDetailsId);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsGroupCompaniesList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsGroupCompaniesList;
        }
    }
}



       
