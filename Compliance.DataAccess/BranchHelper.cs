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
        MySqlConnection conn = DBConnection.getconnection();
        public int insertupdateBranchLocation(Branch branchLocation,char Flag )
        {
            int BranchLocationId = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_insertupdateBranchLocation",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Flag ", Flag);
                cmd.Parameters.AddWithValue("p_LocationID ", branchLocation.BranchId);
                cmd.Parameters.AddWithValue("p_CountryID", branchLocation.CountryId);
                cmd.Parameters.AddWithValue("p_StateID", branchLocation.StateId);
                cmd.Parameters.AddWithValue("p_CityID", branchLocation.CityId) ;
                cmd.Parameters.AddWithValue("p_Address", branchLocation.Address);
                cmd.Parameters.AddWithValue("p_Location_Name", branchLocation.BranchName);
                cmd.Parameters.AddWithValue("p_Postal_Code", branchLocation.PostalCode);
               // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objbranchlocationid = cmd.ExecuteScalar();
                if (objbranchlocationid != null)
                {
                    BranchLocationId = Convert.ToInt32(objbranchlocationid);
                }
            }
            catch 
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return BranchLocationId;
        }

        public DataTable GetBranchLocation(int BranchLocationId)
        {
            DataTable dtBranchLocation = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getBranchLocation",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = CountryID;
                //cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = StateId;
                cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value = BranchLocationId;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtBranchLocation);
            }
            catch 
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtBranchLocation;
        }

    }
}
