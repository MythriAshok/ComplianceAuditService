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
        public DataSet getCountry()
        {
            DataSet dsCountry = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCountry", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsCountry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dsCountry;
        }

        public DataSet getState(int CountryID)
        {
            DataSet dsState = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getState", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Country_ID", CountryID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsState);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsState;
        }

        public DataSet getCity(int StateID)
        {
            DataSet dsCity = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCity", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_State_ID", StateID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsCity);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsCity;
        }
    }
}

