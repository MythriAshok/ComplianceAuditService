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

        public int createUser(User user)
        {
            int userid;
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_createuser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userPassword", user.UserPassword);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                cmd.Parameters.AddWithValue("@", user.LastName);
                cmd.Parameters.AddWithValue("@LastName", user.EmailId);
                cmd.Parameters.AddWithValue("@ContactNumber", user.ContactNumber);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                cmd.Parameters.AddWithValue("@LastLogin", user.LastLogin);

               userid =Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }

            return userid;
        }

    }
}
