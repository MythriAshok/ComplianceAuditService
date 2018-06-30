using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Compliance.DataObject;
using System.Data;

namespace Compliance.DataAccess
{
   public class BranchHelper
    {
        MySqlConnection connection = DBConnection.getconnection();
        public int createBranchLocation(Branch branchLocation)
        {
            int BranchLocationId = 0;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insertupdateBranchLocation";
                cmd.Parameters.Add("p_LocationID ", MySqlDbType.Int32).Value = branchLocation.BranchId;
                cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = branchLocation.CountryId;
                cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = branchLocation.StateId;
                cmd.Parameters.Add("p_CityID", MySqlDbType.Int32).Value = branchLocation.CityId;
                cmd.Parameters.Add("p_Address", MySqlDbType.Int32).Value = branchLocation.Address;
                cmd.Parameters.Add("p_Location_Name", MySqlDbType.Int32).Value = branchLocation.BranchName;
                cmd.Parameters.Add("p_Postal_Code", MySqlDbType.Int32).Value = branchLocation.PostalCode;
               // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objbranchlocationid = cmd.ExecuteScalar();
                if (objbranchlocationid != null)
                {
                    BranchLocationId = Convert.ToInt32(objbranchlocationid);
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

        public DataTable GetBranchLocation(int BranchLocationId)
        {
            DataTable dtBranchLocation = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
               // cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = CountryID;
                //cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = StateId;
                cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value = BranchLocationId;
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

    }
}
