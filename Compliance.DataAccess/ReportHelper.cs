using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compliance.DataAccess
{
   public class ReportHelper
    {
        MySqlConnection conn = DBConnection.getconnection();

        public DataSet getBranchComlianceAuditReport(int Org_Hier_ID)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getBranchComplianceAuditReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", Org_Hier_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsComplianceAudit);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsComplianceAudit;
        }
    }
}
