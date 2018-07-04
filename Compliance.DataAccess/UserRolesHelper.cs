using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.DataObject;
using MySql.Data.MySqlClient;
using System.Data;

namespace Compliance.DataAccess
{
    class UserRolesHelper
    {
        MySqlConnection conn = new MySqlConnection();

        public DataTable getUserRole(int RoleID)
        {
            DataTable dtUser = new DataTable();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getUserGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_User_Group_ID", RoleID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtUser);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return dtUser;
        }
        public bool insertUpdateUserRole(UserRoles roles,char flag)
        {
            bool result = false;
            try
            {
                if (roles != null)
                {
                    conn = DBConnection.getconnection();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateRole", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_flag", flag);
                    cmd.Parameters.AddWithValue("p_Role_ID", roles.UserRoleId);
                    cmd.Parameters.AddWithValue("p_Role_Name", roles.UserName);
                    cmd.Parameters.AddWithValue("p_Is_Active", roles.IsGroupRole);
                    cmd.Parameters.AddWithValue("p_Is_Group_Role", roles.IsActive);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                        result = true;
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

            return result;
        }
        
           public DataTable getUserRoleList(int RoleID)
        {
            DataTable dtUser = new DataTable();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getUserRoleList", conn);
                cmd.CommandType = CommandType.StoredProcedure;               
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtUser);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return dtUser;
        }




    }
}