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
        MySqlConnection connection = DBConnection.getconnection();
        public int CreateComplianceOptionsXref(ComplianceOptions options)
        {
            int ComplianceOptionsId = 0;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Compliance_Opt_Xerf_ID ", MySqlDbType.Int32).Value = options.ComplianceOptionsId;
                cmd.Parameters.Add("p_Optiond_Text", MySqlDbType.VarChar, 45).Value = options.OptionText;
                cmd.Parameters.Add("p_Option_Order", MySqlDbType.Int32).Value = options.OptionOrder;
                cmd.Parameters.Add("p_Compliance_Xref_ID", MySqlDbType.Int32).Value = options.ComplianceId;
                cmd.CommandText = "sp_insertComplianceOptionsXref";
               // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objcomplianceoptionsid = cmd.ExecuteScalar();
                if (objcomplianceoptionsid != null)
                {
                    ComplianceOptionsId = Convert.ToInt32(objcomplianceoptionsid);
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
            return ComplianceOptionsId;
        }

        public DataTable GetComplianceOptionsXref(int ComplianceID)
        {
            DataTable dtOrganization = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Compliance_Opt_Xerf_ID ", MySqlDbType.Int32).Value = ComplianceID;
                cmd.CommandText = "sp_getComplianceOptionsXref";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtOrganization);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtOrganization;
        }

    }
}
