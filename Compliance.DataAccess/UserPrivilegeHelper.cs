#region Code History
/*CODE HISTORY
 * ============================================================================================================
 *  Version No      DATE       Developer Name        Description
 * ===========================================================================================================
 *  1.0          28-06-2018    Ojeshwini H P        DataAccess Layer for UserPrivilege
 *                                                  The methods defined here are getRolePrivilege
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
   public class UserPrivilegeHelper
    {
        MySqlConnection conn = new MySqlConnection();

        /// <summary>
        /// This method accepts the interger value as input parameter and returns Dataset of Privilege
        /// </summary>
        /// <param name="Role_ID"></param>
        /// <returns>Dataset of Privilege for given RoleID.If RoleID is zero then returns all Privilege in DataTable</returns>
        public DataSet getRolePrivilege(int Role_ID)
        {
            DataSet dtmenus = new DataSet();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getRolePrivilege", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Role_ID", Role_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtmenus);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtmenus;
        }

        public DataSet getPrivilege()
        {
            DataSet dtmenus = new DataSet();
            try
            {
                conn = DBConnection.getconnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getPrivilege", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtmenus);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtmenus;
        }

    }
}
