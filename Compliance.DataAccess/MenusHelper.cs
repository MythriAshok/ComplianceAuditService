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
    class MenusHelper
    {
        MySqlConnection conn = new MySqlConnection();       
        public DataTable getMenus(int User_Group_ID)
        {
            DataTable dtMenu = new DataTable();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getMenus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_User_Group_ID", User_Group_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtMenu);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtMenu;
        }
    }
}
