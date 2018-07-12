﻿using System;
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
