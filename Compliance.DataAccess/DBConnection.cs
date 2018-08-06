﻿using System;
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
    /// <summary>
    /// This is a static method which returns the Mysql connection.
    /// </summary>
    /// <returns>Mysql closed connection</returns>
        static public MySqlConnection getconnection()
        {
            // MySqlConnection conn = new MySqlConnection("Server = 208.91.198.197; Port = 3306; Database = auditmoduledb; Uid = Auditdbuser; Pwd = Audit@123");
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["constr"]);
             //MySqlConnection conn = new MySqlConnection("Server = 127.0.0.1; Port = 3306; Database = auditmoduledb; Uid = root; Pwd = Ovi@1996");
            return conn;
        }
    }
}
