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
        MySqlConnection connection = DBConnection.getconnection();
        public int CreateOrganizationHier(Organization org, char Flag)
        {
            int OrganizationId = 0;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insertupdateOrganizationHier";
                cmd.Parameters.AddWithValue("p_Flag ", Flag);
                cmd.Parameters.AddWithValue("p_Org_Hier_ID ", org.OrganizationId);
                cmd.Parameters.AddWithValue("p_Company_Name", org.CompanyName) ;
                cmd.Parameters.AddWithValue("p_Company_ID", org.CompanyId);
                cmd.Parameters.AddWithValue("p_Parent_Company_ID", org.ParentCompanyId);
                cmd.Parameters.AddWithValue("p_Description", org.Description);
                cmd.Parameters.AddWithValue("p_level", org.Level);
                cmd.Parameters.AddWithValue("p_Is_Leaf", org.Leaf);
                cmd.Parameters.AddWithValue("p_Industry_Type", org.IndustryType);
                cmd.Parameters.AddWithValue("p_Last_Updated_Date", org.LastUpdatedDate);
                cmd.Parameters.AddWithValue("p_LocationID", org.BranchId);
                cmd.Parameters.AddWithValue("p_User_ID", org.UserId);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objorganizationid = cmd.ExecuteScalar();
                if (objorganizationid != null)
                {
                    OrganizationId = Convert.ToInt32(objorganizationid);
                }
            }
            catch 
            {
                throw ;
            }
            finally
            {
                connection.Close();
            }
            return OrganizationId;
        }

        public DataTable GetOrganizationHier(int OrgID)
        {
            DataTable dtOrganization = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_getOrganizationHier";
                cmd.Parameters.AddWithValue("p_Org_Hier_ID ", OrgID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtOrganization);
            }
            catch 
            {
                throw ;
            }
            finally
            {
                connection.Close();
            }
            return dtOrganization;
        }

    }
}
