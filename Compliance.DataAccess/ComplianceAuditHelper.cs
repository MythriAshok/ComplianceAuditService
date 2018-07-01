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
   public class ComplianceAuditHelper
    {
        MySqlConnection conn = DBConnection.getconnection();

        public bool createComplianceAudit(List<ComplianceAudit> auditdata, char Flag)
        {
            bool ComplianceAuditResult = true;
            try
            {
                if (auditdata != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateComplianceAudit",conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (ComplianceAudit audit in auditdata)
                    {
                        cmd.Parameters.AddWithValue("p_Flag", Flag);
                        cmd.Parameters.AddWithValue("p_Compliance_Audit_ID ", audit.ComplianceAuditId);
                        cmd.Parameters.AddWithValue("p_Comp_Schedule_Instance", audit.ComplianceScheduleInstance);
                        cmd.Parameters.AddWithValue("p_Penalty_nc", audit.Penalty);
                        cmd.Parameters.AddWithValue("p_Audit_Remarks", audit.AuditRemarks);
                        cmd.Parameters.AddWithValue("p_Audit_artefacts", audit.AuditArteFacts);
                        cmd.Parameters.AddWithValue("p_Audit_Date ", audit.AuditDate);
                        cmd.Parameters.AddWithValue("p_Version", audit.Version);
                        cmd.Parameters.AddWithValue("p_Reviewer_ID", audit.ReviewerId);
                        cmd.Parameters.AddWithValue("p_Review_Comments", audit.ReviewerComments) ;
                      //  cmd.Parameters.AddWithValue("p_Last_Updated_Date", MySqlDbType.DateTime).Value = audit.LastUpdatedDate;
                        cmd.Parameters.AddWithValue("p_Audit_Status", audit.AuditStatus);
                        cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", audit.ComplianceId) ;
                        cmd.Parameters.AddWithValue("p_Org_ID", audit.OrgId);
                        cmd.Parameters.AddWithValue("p_Compliance_Opt_Xref_ID", audit.ComplianceOptionsId);
                        cmd.Parameters.AddWithValue("p_Auditor_ID ", audit.AuditorId);
                        cmd.Parameters.AddWithValue("p_User_ID", audit.UserId) ;
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
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return ComplianceAuditResult;
        }

        public DataTable getComlianceAudit(int ComplianceAuditID)
        {
            DataTable dtComplianceAudit = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceAudit", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Audit_ID ", MySqlDbType.Int32).Value = ComplianceAuditID;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceAudit);
            }
            catch
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceAudit;
        }
    }
}
