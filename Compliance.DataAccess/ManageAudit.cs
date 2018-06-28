using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.DataObject;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Compliance.DataAccess
{
   public class ManageAudit
    {
        MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString);
        public DataTable GetCountry()
        {
            DataTable dtCountry = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_getCountry";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtCountry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtCountry;
        }

        public DataTable GetState(int CountryId)
        {
            DataTable dtState = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_CountryID", OleDbType.Int).Value = CountryId;
                cmd.CommandText = "sp_getState";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtStates);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtState;
        }

        public DataTable GetCity(int StateId)
        {
            DataTable dtCity = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_StateID", SqlDbType.Int).Value = StateId;
                cmd.CommandText = "sp_getCity";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtCity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtCity;
        }

        public int CreateBranchLocation(Branch branchLocation)
        {
            int BranchLocationId = 0;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_LocationID ", SqlDbType.Int).Value = branchLocation.BranchId;
                cmd.Parameters.Add("m_CountryID", SqlDbType.Int).Value = branchLocation.CountryId;
                cmd.Parameters.Add("m_StateID", SqlDbType.Int).Value = branchLocation.StateId;
                cmd.Parameters.Add("m_CityID", SqlDbType.Int).Value = branchLocation.CityId;
                cmd.Parameters.Add("m_Address", SqlDbType.Int).Value = branchLocation.Address;
                cmd.Parameters.Add("m_Location_Name", SqlDbType.Int).Value = branchLocation.BranchName;
                cmd.Parameters.Add("m_Postal_Code", SqlDbType.Int).Value = branchLocation.PostalCode;
                cmd.CommandText = "sp_insertBranchLocation";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objbranchlocationid = cmd.ExecuteScalar();
                if (objBranchLocationId != null)
                {
                    profileid = Convert.ToInt32(objBranchLocationId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return BranchLocationId;
        }

        public DataTable GetBranchLocation(int CountryID,int StateId, int CityID)
        {
            DataTable dtBranchLocation = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_CountryID", SqlDbType.Int).Value = CountryID;
                cmd.Parameters.Add("m_StateID", SqlDbType.Int).Value = StateId;
                cmd.Parameters.Add("m_CityID", SqlDbType.Int).Value = CityID;
                cmd.CommandText = "sp_getBranchLocation";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtBranchLocation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtBranchLocation;
        }

        public int CreateOrganizationHier()
        {
            int OrganizationId = 0;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_LocationID ", SqlDbType.Int).Value = branchLocation.BranchId;
                cmd.Parameters.Add("m_CountryID", SqlDbType.Int).Value = branchLocation.CountryId;
                cmd.Parameters.Add("m_StateID", SqlDbType.Int).Value = branchLocation.StateId;
                cmd.Parameters.Add("m_CityID", SqlDbType.Int).Value = branchLocation.CityId;
                cmd.Parameters.Add("m_Address", SqlDbType.Int).Value = branchLocation.Address;
                cmd.Parameters.Add("m_Location_Name", SqlDbType.Int).Value = branchLocation.BranchName;
                cmd.Parameters.Add("m_Postal_Code", SqlDbType.Int).Value = branchLocation.PostalCode;
                cmd.CommandText = "sp_insertBranchLocation";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objorganizationid = cmd.ExecuteScalar();
                if (objorganizationid != null)
                {
                    profileid = Convert.ToInt32(objorganizationid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return OrganizationId;
        }

    }
}
    

