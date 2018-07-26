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
    /// <summary>
    /// Organization Helper class to interact with database stored procedures
    /// </summary>
    public class OrganizationHelper
    {
        /// <summary>
        /// fetching the MySql Connection from DBConnection class
        /// </summary>
        MySqlConnection conn = DBConnection.getconnection();
        /// <summary>
        /// A method to insert or update the branch location in the database using parameter char(Flag)
        /// </summary>
        /// <param name="branchLocation"></param>
        /// <param name="Flag"></param>
        /// <returns>BranchLocationID</returns>
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
                    cmd.Parameters.Add("p_Flag", MySqlDbType.VarChar, 1).Value = Flag;
                    cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value = branchLocation.Branch_Id;
                    cmd.Parameters.Add("p_Location_Name", MySqlDbType.VarChar, 75).Value = branchLocation.Branch_Name;
                    cmd.Parameters.Add("p_Address", MySqlDbType.VarChar, 450).Value = branchLocation.Address;
                    cmd.Parameters.Add("p_Country_ID", MySqlDbType.Int32).Value = branchLocation.Country_Id;
                    cmd.Parameters.Add("p_State_ID", MySqlDbType.Int32).Value = branchLocation.State_Id;
                    cmd.Parameters.Add("p_City_ID", MySqlDbType.Int32).Value = branchLocation.City_Id;
                    cmd.Parameters.Add("p_Postal_Code", MySqlDbType.Int32).Value = branchLocation.Postal_Code;
                    cmd.Parameters.Add("p_Branch_Coordinates1", MySqlDbType.VarChar, 100).Value = branchLocation.Branch_Coordinates1;
                    cmd.Parameters.Add("p_Branch_Coordinates2", MySqlDbType.VarChar, 100).Value = branchLocation.Branch_Coordinates2;
                    cmd.Parameters.Add("p_Branch_CoordinateURL", MySqlDbType.VarChar, 100).Value = branchLocation.Branch_CoordinatesURL;
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
        /// <summary>
        /// A method to fetch the dataset of BranchLOcation from the database
        /// </summary>
        /// <param name="Branch_Location_Id"></param>
        /// <returns>dataset of BranchLocation</returns>
        public DataSet getBranchLocation(int Branch_Location_Id)
        {
            DataSet dsBranchLocation = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getBranchLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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
        /// <summary>
        /// A method to delete the records in the BranchLocation Table in database
        /// </summary>
        /// <param name="Branch_Location_Id"></param>
        /// <returns>boolean value</returns>
        public bool deleteBranchLocation(int Branch_Location_Id)
        {
            bool resultBranchLocation = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_deleteBranchLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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
        /// <summary>
        /// A method to insert or update the data of Organization in the database by passing char(Flag)
        /// </summary>
        /// <param name="org"></param>
        /// <param name="Flag"></param>
        /// <returns>OrganizationID</returns>
        public int insertupdateOrganizationHier(Organization org, char Flag)
        {
            int OrganizationId = 0;
            try
            {
                if (org != null)
                {
                    conn.Open();
                    MySqlTransaction tran = conn.BeginTransaction();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateOrganizationHier", conn, tran);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Flag", Flag);
                    cmd.Parameters.AddWithValue("p_Org_Hier_ID", org.Organization_Id);
                    cmd.Parameters.AddWithValue("p_Company_Name", org.Company_Name);
                    cmd.Parameters.AddWithValue("p_Company_Code", org.Company_Id);
                    cmd.Parameters.AddWithValue("p_Parent_Company_ID", org.Parent_Company_Id);
                    cmd.Parameters.AddWithValue("p_Description", org.Description);
                    cmd.Parameters.AddWithValue("p_level", org.Level);
                    cmd.Parameters.AddWithValue("p_Is_Leaf", org.Is_Leaf);
                    cmd.Parameters.AddWithValue("p_Industry_Type", org.Industry_Type);
                    cmd.Parameters.AddWithValue("p_Last_Updated_Date", org.Last_Updated_Date);
                    cmd.Parameters.AddWithValue("p_Location_ID", org.Branch_Id);
                    cmd.Parameters.AddWithValue("p_User_ID", org.User_Id);
                    cmd.Parameters.AddWithValue("p_Is_Active", org.Is_Active);
                    cmd.Parameters.AddWithValue("p_Is_Delete", org.Is_Delete);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    object objorganizationid = cmd.ExecuteScalar();
                    if (objorganizationid != null)
                    {
                        OrganizationId = Convert.ToInt32(objorganizationid);
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
            return OrganizationId;
        }
        /// <summary>
        /// A method to fetch the dataset Organization data from the database
        /// </summary>
        /// <param name="OrgID"></param>
        /// <returns>dataset of Organization</returns>
        public DataSet getOrganizationHier(int OrgID)
        {
            DataSet dsOrganization = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getOrganizationHierJoin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsOrganization);
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

        public DataSet getBranch(int OrgID)
        {
            DataSet dsBranch = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getBranchJoin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsBranch);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsBranch;
        }


        /// <summary>
        /// A method to delete the records of Organization table in the database
        /// </summary>
        /// <param name="Org_Hier_ID"></param>
        /// <returns>boolean value</returns>
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

        public bool DeactivateGroupCompany(int Org_Hier_ID)
        {
            bool resultGroupCompany = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DeactivateOrgHier", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", Org_Hier_ID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultGroupCompany = true;
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
            return resultGroupCompany;
        }

        public bool ActivateGroupCompany(int Org_Hier_ID)
        {
            bool resultGroupCompany = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_ActivateOrgHier", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", Org_Hier_ID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultGroupCompany = true;
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
            return resultGroupCompany;
        }


        public bool DeleteGroupCompany(int OrgID)
        {
            bool resultGroupCompany = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_deleteOrganizationHier", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultGroupCompany = true;
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
            return resultGroupCompany;
        }



        /// <summary>
        /// Method to insert or update the CompanyDetails in the database using char(Flag)
        /// </summary>
        /// <param name="details"></param>
        /// <param name="Flag"></param>
        /// <returns>CompanyDetailsID</returns>
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
        /// <summary>
        /// Method to fetch the dataset of CompanyDetails from the database 
        /// </summary>
        /// <param name="CompanyDetailsId"></param>
        /// <returns>dataset of CompanyDetails</returns>
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
        /// <summary>
        /// Method to delete the record of CompanyDetails table in the database
        /// </summary>
        /// <param name="Company_Details_Id"></param>
        /// <returns>boolean value</returns>
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
        /// <summary>
        /// A method to get the list of all GroupCompanies present in the database
        /// </summary>
        /// <returns>dataset of all groupcompanies present in the database</returns>
        public DataSet getGroupCompanyList()
        {
            DataSet dsGroupCompaniesList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getGroupCompaniesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public DataSet getCompanyList()
        {
            DataSet dsCompaniesList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompanieyList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsCompaniesList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsCompaniesList;
        }

        public DataSet getSpecificCompanyList(int GroupCompanyID)
        {
            DataSet dsCompaniesList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompanyLists", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Parent_Company_ID", GroupCompanyID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsCompaniesList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsCompaniesList;
        }



        public DataSet getCompanyListsforBranch(int GroupCompanyID)
        {
            DataSet dsCompaniesList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompanyListsforBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", GroupCompanyID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsCompaniesList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsCompaniesList;
        }

        public DataSet getBranchList()
        {
            DataSet dsBranchList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getBranchList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsBranchList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsBranchList;
        }


        public DataSet getGroupCompanyListDropDown()
        {
            DataSet dsGroupCompaniesListDropDown = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getGroupCompaniesListDropDown", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsGroupCompaniesListDropDown);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsGroupCompaniesListDropDown;
        }

        public DataSet getCompanyListDropDown(int id)
        {
            DataSet dsCompaniesListDropDown = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompaniesListDropDown", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Parent_Company_ID", id);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsCompaniesListDropDown);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsCompaniesListDropDown;
        }
       

    }
}



       
