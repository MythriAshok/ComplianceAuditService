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
        MySqlConnection connection = DBConnection.getconnection();

        public bool createComplianceAudit(List<ComplianceAudit> auditdata)
        {
            bool ComplianceAuditResult = true;
            try
            {
                if (auditdata != null)
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_insertupdateComplianceAudit";
                    foreach (ComplianceAudit audit in auditdata)
                    {
                        cmd.Parameters.Add("p_Compliance_Audit_ID ", MySqlDbType.Int32).Value = audit.ComplianceAuditId;
                        cmd.Parameters.Add("p_Comp_Schedule_Instance", MySqlDbType.Int32).Value = audit.ComplianceScheduleInstance;
                        cmd.Parameters.Add("p_Penalty_nc", MySqlDbType.VarChar, 150).Value = audit.Penalty;
                        cmd.Parameters.Add("p_Audit_Remarks", MySqlDbType.VarChar, 150).Value = audit.AuditRemarks;
                        cmd.Parameters.Add("p_Audit_artefacts", MySqlDbType.VarChar, 150).Value = audit.AuditArteFacts;
                        cmd.Parameters.Add("p_Audit_Date ", MySqlDbType.DateTime).Value = audit.AuditDate;
                        cmd.Parameters.Add("p_Version", MySqlDbType.Int32).Value = audit.Version;
                        cmd.Parameters.Add("p_Reviewer_ID", MySqlDbType.Int32).Value = audit.ReviewerId;
                        cmd.Parameters.Add("p_Review_Comments", MySqlDbType.VarChar, 500).Value = audit.ReviewerComments;
                      //  cmd.Parameters.Add("p_Last_Updated_Date", MySqlDbType.DateTime).Value = audit.LastUpdatedDate;
                        cmd.Parameters.Add("p_Audit_Status", MySqlDbType.VarChar, 45).Value = audit.AuditStatus;
                        cmd.Parameters.Add("p_Compliance_Xref_ID", MySqlDbType.Int32).Value = audit.ComplianceId;
                        cmd.Parameters.Add("p_Org_ID", MySqlDbType.Int32).Value = audit.OrgId;
                        cmd.Parameters.Add("p_Compliance_Opt_Xref_ID", MySqlDbType.Int32).Value = audit.ComplianceOptionsId;
                        cmd.Parameters.Add("p_Auditor_ID ", MySqlDbType.Int32).Value = audit.AuditorId;
                        cmd.Parameters.Add("p_User_ID", MySqlDbType.Int32).Value = audit.UserId;
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
            catch (Exception ex)
            {
                ComplianceAuditResult = false;
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return ComplianceAuditResult;
        }

        public DataTable getComlianceAudit(int ComplianceAuditID)
        {
            DataTable dtComplianceAudit = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Compliance_Audit_ID ", MySqlDbType.Int32).Value = ComplianceAuditID;
                cmd.CommandText = "sp_getComplianceAudit";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceAudit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtComplianceAudit;
        }

    }
}
