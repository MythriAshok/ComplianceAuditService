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
   public class ComplianceXrefAuditTrailHelper
    {

        MySqlConnection conn = DBConnection.getconnection();
        // MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString);
        public int insertupdateComplianceXref(ComplianceXrefAuditTrail xrefaudittrail, char Flag)
        {
            int ComplianceXrefTrailId = 0;
            try
            {
                if (xrefaudittrail != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertComplianceXrefAuditTrail", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Flag", Flag);
                    cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", xrefaudittrail.Compliance_Xref_ID);
                    cmd.Parameters.AddWithValue("p_Comp_Category", xrefaudittrail.Comp_Category);
                    cmd.Parameters.AddWithValue("p_Comp_Description", xrefaudittrail.Comp_Description);
                    cmd.Parameters.AddWithValue("p_Is_Header", xrefaudittrail.Is_Header);
                    cmd.Parameters.AddWithValue("p_level", xrefaudittrail.level);
                    cmd.Parameters.AddWithValue("p_Option_ID", xrefaudittrail.Option_ID);
                    cmd.Parameters.AddWithValue("p_Risk_Category", xrefaudittrail.Risk_Category);
                    cmd.Parameters.AddWithValue("p_Risk_Description", xrefaudittrail.Risk_Description);
                    cmd.Parameters.AddWithValue("p_Recurrence", xrefaudittrail.Recurrence);
                    cmd.Parameters.AddWithValue("p_Form ", xrefaudittrail.Form);
                    cmd.Parameters.AddWithValue("p_Type", xrefaudittrail.Type);
                    cmd.Parameters.AddWithValue("p_Is_Best_Practice", xrefaudittrail.Is_Best_Practice);
                    cmd.Parameters.AddWithValue("p_Version", xrefaudittrail.Version);
                    cmd.Parameters.AddWithValue("p_Effective_Start_Date", xrefaudittrail.Effective_Start_Date);
                    cmd.Parameters.AddWithValue("p_Effective_End_Date", xrefaudittrail.Effective_End_Date);
                    cmd.Parameters.AddWithValue("p_Country_ID ", xrefaudittrail.Country_ID);
                    cmd.Parameters.AddWithValue("p_State_ID", xrefaudittrail.State_ID);
                    cmd.Parameters.AddWithValue("p_City_ID", xrefaudittrail.City_ID);
                    cmd.Parameters.AddWithValue("p_Last_Updated_Date", xrefaudittrail.Last_Updated_Date);
                    cmd.Parameters.AddWithValue("p_User_ID", xrefaudittrail.User_ID);
                    cmd.Parameters.AddWithValue("p_Is_Active", xrefaudittrail.Is_Active);
                    cmd.Parameters.AddWithValue("p_Action_Type", xrefaudittrail.Action_Type);
                    // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    object objcompliancexreftrailid = cmd.ExecuteScalar();
                    if (objcompliancexreftrailid != null)
                    {
                        ComplianceXrefTrailId = Convert.ToInt32(objcompliancexreftrailid);
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
            return ComplianceXrefTrailId;
        }



        public DataTable getComlianceXrefAuditTrail(int Compliance_Xref_ID)
        {
            DataTable dtComplianceXrefAuditTrail = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXref", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID ", Compliance_Xref_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXrefAuditTrail);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXrefAuditTrail;
        }

        public bool deleteComlianceXrefAudittrail(int Compliance_Xref_ID)
        {
            bool resultComplianceXrefTrail = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXref", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID ", Compliance_Xref_ID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultComplianceXrefTrail = true;
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
            return resultComplianceXrefTrail;
        }
    }

}
  