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
        public string createUser(User user)
        {
            string result="";
             
                try
                {
                if (user != null)
                {
                    conn = DBConnection.getconnection();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_createuser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userPassword", user.UserPassword);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                    cmd.Parameters.AddWithValue("@ContactNumber", user.ContactNumber);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

                    object ires = (cmd.ExecuteScalar());
                    result = ires.ToString();
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

        public bool DeleteUser(User user)
        {
            bool result=false;
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_createuser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                int res=cmd.ExecuteNonQuery();
                if(res>0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }


    }
}
