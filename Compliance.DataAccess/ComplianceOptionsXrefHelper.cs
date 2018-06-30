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
   public  class ComplianceOptionsXrefHelper
    {
        MySqlConnection conn = DBConnection.getconnection();
        public int insertupdateComplianceOptionsXref(ComplianceOptions options, char Flag)
        {
            int ComplianceOptionsId = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_insertupdateComplianceOptionsXref",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Flag", Flag);
                cmd.Parameters.AddWithValue("p_Compliance_Opt_Xerf_ID ", options.ComplianceOptionsId) ;
                cmd.Parameters.AddWithValue("p_Optiond_Text", options.OptionText);
                cmd.Parameters.AddWithValue("p_Option_Order", options.OptionOrder);
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", options.ComplianceId);
                // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objcomplianceoptionsid = cmd.ExecuteScalar();
                if (objcomplianceoptionsid != null)
                {
                    ComplianceOptionsId = Convert.ToInt32(objcomplianceoptionsid);
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
            return ComplianceOptionsId;
        }

        public DataTable GetComplianceOptionsXref(int ComplianceID)
        {
            DataTable dtOrganization = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceOptionsXref",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Opt_Xerf_ID ", ComplianceID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtOrganization);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtOrganization;
        }

    }
}
