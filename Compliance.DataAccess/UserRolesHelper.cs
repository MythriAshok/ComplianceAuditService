#region Code History
/*CODE HISTORY
 * ============================================================================================================
 *  Version No      DATE       Developer Name        Description
 * ===========================================================================================================
 *  1.0          28-06-2018    Ojeshwini H P        DataAccess Layer for Role Table
 *                                                  It constists the method getUserRole(),getRoleList(),insertUpdateRole(),
 *                                                  and insertRolePrivilege().                                                  
 *  
 */
#endregion

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
   public class UserRolesHelper
    {
        MySqlConnection conn = new MySqlConnection();

        /// <summary>
        /// This method is used to call the storeprocedure 'sp_getRoleList' by passing flag value Pass 0 to get Dataset of UserRoles and 1 for Dataset of GroupRoles.
        /// </summary>
        /// <param name="flag">int</param>
        /// <returns>Dataset of Roles Table</returns>
        public DataSet getRoleList(int flag)
        {
            DataSet dtUser = new DataSet();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getRoleList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_flag", flag);
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

        public DataSet getAllRole(int roleid)
        {
            DataSet dtUser = new DataSet();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getRole", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Role_ID", roleid);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int insertUpdateRole(Roles roles,char flag)
        {
            int roleid = 0;
            try
            {
                if (roles != null)
                {
                    conn = DBConnection.getconnection();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateRole", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_flag", flag);
                    cmd.Parameters.AddWithValue("p_Role_ID", roles.RoleId);
                    cmd.Parameters.AddWithValue("p_Role_Name", roles.RoleName);
                    cmd.Parameters.AddWithValue("p_Is_Active", roles.IsActive); 
                    cmd.Parameters.AddWithValue("p_Is_Group_Role", roles.IsGroupRole);
                    object res = cmd.ExecuteScalar();
                    roleid = Convert.ToInt32(res);                       
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
            return roleid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="privilegeid"></param>
        /// <returns></returns>
        public bool insertRolePrivilege(int roleid, int privilegeid)
        {
            bool result = false;
            try
            {
                  conn = DBConnection.getconnection();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertRolePrivilege", conn);
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    cmd.Parameters.AddWithValue("p_Role_ID", roleid);
                    cmd.Parameters.AddWithValue("p_Privilege_ID", privilegeid);
                cmd.Parameters.AddWithValue("p_Is_Active", 1);
                    int res = cmd.ExecuteNonQuery();
                    if(res>0)
                {
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
       
            public bool DeleteRolePrivilege(int roleid)
        {
            bool result = false;
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DeleteRolePrivilege", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Role_ID", roleid);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
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

        public bool DeleteRole(int roleid)
        {
            bool result = false;
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_deleteRole", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Role_ID", roleid);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
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
    }
}