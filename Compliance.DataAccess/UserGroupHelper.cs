﻿#region Code History
/*CODE HISTORY
 * ============================================================================================================
 *  Version No      DATE       Developer Name        Description
 * ===========================================================================================================
 *  1.0          28-06-2018    Ojeshwini H P        DataAccess Layer for User DataTable
 *                                                  insertupdateUser method
 *                                                  DeleteUser method
 *                                                  getUser method
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
   public class UserGroupHelper
    {
        MySqlConnection conn = new MySqlConnection();
        public DataSet getUserGroup(int UserGroupID)
        {
            DataSet dtUser = new DataSet();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getUserGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_User_Group_ID", UserGroupID);
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

        public bool insertupdateUser(UserGroup usergroup, char flag)
        {
            bool result = false;

            try
            {
                if (usergroup != null)
                {
                    conn = DBConnection.getconnection();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_flag", flag);
                    cmd.Parameters.AddWithValue("User_Group_ID",usergroup.UserGroupId);
                    cmd.Parameters.AddWithValue("p_User_Group_Name", usergroup.UserGroupName);
                    cmd.Parameters.AddWithValue("User_Group_Description", usergroup.UserGroupDescription);
                    cmd.Parameters.AddWithValue("p_Role_ID", usergroup.UserRoleId);
                    int res= cmd.ExecuteNonQuery();
                    if(res>0)
                    result = true;
                }
                result = false;
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