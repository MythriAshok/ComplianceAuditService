#region Code History
/*CODE HISTORY
 * ============================================================================================================
 *  Version No      DATE       Developer Name        Description
 * ===========================================================================================================
 *  1.0          28-06-2018    Ojeshwini H P        DataAccess Layer for Menu Table
 *                                                  It consists of the methods getMenus().
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
    public class MenusHelper
    {
        MySqlConnection conn = new MySqlConnection();
        /// <summary>
        /// This method will return the Dataset of Menus table associated with User_Group_ID by executing the 'sp_getMenus' storeProcedure.
        /// </summary>
        /// <param name="User_Group_ID">UserGroupId is a type of int</param>
        /// <returns>Dataset of Menus Table</returns>
        public DataSet getMenus(int User_Group_ID)
        {
            DataSet dtMenu = new DataSet();
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
