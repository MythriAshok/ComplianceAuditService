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
   public class ComplianceAuditTrailHelper
    {
        MySqlConnection conn = DBConnection.getconnection();

        public bool insertupdateComplianceAuditTrail(List<ComplianceAuditAuditTrail> audittraildata, char Flag)
        {
            bool ComplianceAuditResult = true;
            try
            {
                if (audittraildata != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertComplianceAuditTrail", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (ComplianceAuditAuditTrail audit in audittraildata)
                    {
                        cmd.Parameters.AddWithValue("p_Flag", Flag);
                        cmd.Parameters.AddWithValue("p_Compliance_Audit_ID ", audit.Compliance_Audit_Id);
                        cmd.Parameters.AddWithValue("p_Comp_Schedule_Instance", audit.Compliance_Schedule_Instance);
                        cmd.Parameters.AddWithValue("p_Penalty_nc", audit.Penalty_nc);
                        cmd.Parameters.AddWithValue("p_Audit_Remarks", audit.Audit_Remarks);
                        cmd.Parameters.AddWithValue("p_Audit_artefacts", audit.Audit_ArteFacts);
                        cmd.Parameters.AddWithValue("p_Audit_Date ", audit.Audit_Date);
                        cmd.Parameters.AddWithValue("p_Version", audit.Version);
                        cmd.Parameters.AddWithValue("p_Reviewer_ID", audit.Reviewer_Id);
                        cmd.Parameters.AddWithValue("p_Review_Comments", audit.Reviewer_Comments);
                        //  cmd.Parameters.AddWithValue("p_Last_Updated_Date", MySqlDbType.DateTime).Value = audit.LastUpdatedDate;
                        cmd.Parameters.AddWithValue("p_Audit_Status", audit.Audit_Status);
                        cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", audit.Compliance_Xref_Id);
                        cmd.Parameters.AddWithValue("p_Org_Hier_ID", audit.Org_Hier_Id);
                        cmd.Parameters.AddWithValue("p_Compliance_Opt_Xref_ID", audit.Compliance_Options_Id);
                        cmd.Parameters.AddWithValue("p_Auditor_ID ", audit.Auditor_Id);
                        cmd.Parameters.AddWithValue("p_User_ID", audit.User_Id);
                        cmd.Parameters.AddWithValue("p_Is_Active", audit.Is_Active);
                        cmd.Parameters.AddWithValue("p_Action_Type", audit.Action_Type);
                        int objcomplianceauditid = cmd.ExecuteNonQuery();
                        if (objcomplianceauditid <= 0)
                        {
                            ComplianceAuditResult = false;
                        }
                        cmd.Parameters.Clear();
                    }
                }
                else
                {
                    ComplianceAuditResult = false;
                }
            }
            catch
            {
                ComplianceAuditResult = false;
                throw;
            }
            finally
            {
                conn.Close();
            }
            return ComplianceAuditResult;
        }

        public DataTable getComlianceAuditTrail(int Compliance_Audit_ID)
        {
            DataTable dtComplianceAudit = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceAuditTrail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Audit_ID ", Compliance_Audit_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceAudit);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceAudit;
        }

        public bool deleteComlianceAuditTrail(int Compliance_Audit_ID)
        {
            bool resultComplianceAuditTrail = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceAuditTrail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Audit_ID ",  Compliance_Audit_ID);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultComplianceAuditTrail = true;
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
            return resultComplianceAuditTrail;
        }
    }
}
