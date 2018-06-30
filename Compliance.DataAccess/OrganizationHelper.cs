using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Compliance.DataAccess
{
    public class OrganizationHelper
    {
        public int CreateOrganizationHier(Organization org)
        {
            int OrganizationId = 0;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_Org_ID ", MySqlDbType.Int32).Value = org.OrganizationId;
                cmd.Parameters.Add("m_Company_Name", MySqlDbType.VarChar, 45).Value = org.CompanyName;
                cmd.Parameters.Add("m_Company_ID", MySqlDbType.Int32).Value = org.CompanyId;
                cmd.Parameters.Add("m_Parent_Company_ID", MySqlDbType.Int32).Value = org.ParentCompanyId;
                cmd.Parameters.Add("m_Description", MySqlDbType.VarChar, 45).Value = org.Description;
                cmd.Parameters.Add("m_level", MySqlDbType.Bit).Value = org.Level;
                cmd.Parameters.Add("m_Is_Leaf", MySqlDbType.Bit).Value = org.Leaf;
                cmd.Parameters.Add("m_Industry_Type", MySqlDbType.VarChar, 45).Value = org.IndustryType;
                cmd.Parameters.Add("m_Last_Updated_Date", MySqlDbType.DateTime).Value = org.LastUpdatedDate;
                cmd.Parameters.Add("m_LocationID", MySqlDbType.Int32).Value = org.BranchId;
                cmd.Parameters.Add("m_User_ID", MySqlDbType.Int32).Value = org.UserId;
                cmd.CommandText = "sp_insertOrganizationHier";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objorganizationid = cmd.ExecuteScalar();
                if (objorganizationid != null)
                {
                    OrganizationId = Convert.ToInt32(objorganizationid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                cmd.Parameters.Add("m_Org_ID ", MySqlDbType.Int32).Value = OrgID;
                cmd.CommandText = "sp_getOrganizationHier";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtOrganization);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtOrganization;
        }

    }
}
