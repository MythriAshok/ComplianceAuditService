#region Code History
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
    public class UserHelper
    {
        MySqlConnection conn = new MySqlConnection();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string insertupdateUser(User user,char flag)
        {
            string result="";
             
                try
                {
                    if (user != null)
                    {
                    conn = DBConnection.getconnection();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_flag", flag);
                    cmd.Parameters.AddWithValue("p_User_ID", user.UserId);
                    cmd.Parameters.AddWithValue("p_User_Password", user.UserPassword);
                    cmd.Parameters.AddWithValue("p_First_Name", user.FirstName);
                    cmd.Parameters.AddWithValue("p_Middle_Name", user.MiddleName);
                    cmd.Parameters.AddWithValue("p_Last_Name", user.LastName);
                    cmd.Parameters.AddWithValue("p_Email_ID", user.EmailId);
                    cmd.Parameters.AddWithValue("p_Contact_Number", user.ContactNumber);
                    cmd.Parameters.AddWithValue("p_Gender", user.Gender);
                    cmd.Parameters.AddWithValue("p_Is_Active", user.IsActive);

                    result = Convert.ToString(cmd.ExecuteScalar());
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

        public bool DeleteUser(int userId)
        {
            bool result=false;
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_createuser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_User_ID", userId);
                int res=cmd.ExecuteNonQuery();
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

        public DataTable getUser(int userId)
        {
            DataTable dtUser = new DataTable();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_createuser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_User_ID", userId);
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
