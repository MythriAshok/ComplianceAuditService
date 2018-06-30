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
        public DataTable GetCountry()
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

        public DataTable GetState(int CountryId)
        {
            DataTable dtState = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getState",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = CountryId;
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

        public DataTable GetCity(int StateId)
        {
            DataTable dtCity = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCity",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = StateId;
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
