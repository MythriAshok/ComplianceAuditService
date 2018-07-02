using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Compliance.DataAccess
{
   public class CountryHelper
    {
        MySqlConnection conn = DBConnection.getconnection();
        public DataTable getCountry()
        {
            DataTable dtCountry = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCountry",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtCountry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dtCountry;
        }

        public DataTable getState(int Country_Id)
        {
            DataTable dtState = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getState",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_CountryID",  Country_Id);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtState);
            }
            catch 
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return dtState;
        }

        public DataTable getCity(int State_Id)
        {
            DataTable dtCity = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCity",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_StateID",  State_Id);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtCity);
            }
            catch 
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return dtCity;
        }

    }
}
