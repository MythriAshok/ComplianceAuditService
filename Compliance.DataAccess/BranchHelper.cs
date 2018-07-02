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
                if (branchLocation != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateBranchLocation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Flag ", Flag);
                    cmd.Parameters.AddWithValue("p_LocationID ", branchLocation.Branch_Id);
                    cmd.Parameters.AddWithValue("p_CountryID", branchLocation.Country_Id);
                    cmd.Parameters.AddWithValue("p_StateID", branchLocation.State_Id);
                    cmd.Parameters.AddWithValue("p_CityID", branchLocation.City_Id);
                    cmd.Parameters.AddWithValue("p_Address", branchLocation.Address);
                    cmd.Parameters.AddWithValue("p_Location_Name", branchLocation.Branch_Name);
                    cmd.Parameters.AddWithValue("p_Postal_Code", branchLocation.Postal_Code);
                    cmd.Parameters.AddWithValue("p_Branch_Coordinates1", branchLocation.Branch_Coordinates1);
                    cmd.Parameters.AddWithValue("p_Branch_Coordinates2", branchLocation.Branch_Coordinates2);
                    cmd.Parameters.AddWithValue("p_Branch_CoordinatesURL", branchLocation.Branch_CoordinatesURL);
                    // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    object objbranchlocationid = cmd.ExecuteScalar();
                    if (objbranchlocationid != null)
                    {
                        BranchLocationId = Convert.ToInt32(objbranchlocationid);
                    }
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

        public DataTable getBranchLocation(int Branch_Location_Id)
        {
            DataTable dtBranchLocation = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getBranchLocation",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = CountryID;
                //cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = StateId;
                cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value = Branch_Location_Id;
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

        public bool deleteBranchLocation(int Branch_Location_Id)
        {
            bool resultBranchLocation = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_deleteBranchLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("p_CountryID", MySqlDbType.Int32).Value = CountryID;
                //cmd.Parameters.Add("p_StateID", MySqlDbType.Int32).Value = StateId;
                cmd.Parameters.Add("p_Location_ID", MySqlDbType.Int32).Value = Branch_Location_Id;
                object resultCount = cmd.ExecuteScalar();
                if(resultCount != null)
                {
                    resultBranchLocation = true;
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
            return resultBranchLocation;
        }

    }
}
