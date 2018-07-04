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
                    cmd.Parameters.AddWithValue("p_Flag ", Flag);
                    cmd.Parameters.AddWithValue("p_Org_Hier_ID ", org.Organization_Id);
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

        public DataTable getOrganizationHier(int OrgID)
        {
            DataTable dtOrganization = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getOrganizationHier", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID ", OrgID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtOrganization);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtOrganization;
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
    }
}

       
