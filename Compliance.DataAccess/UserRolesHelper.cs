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
    }
}