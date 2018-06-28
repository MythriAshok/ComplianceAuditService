using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Compliance.DataAccess
{
    static class DBConnection
    {
        static public MySqlConnection getconnection()
        {
                string connection = ConfigurationManager.AppSettings["constr"];
                MySqlConnection conn = new MySqlConnection(connection);

            return conn;
        }
    }
}
