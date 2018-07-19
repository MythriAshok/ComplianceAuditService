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

        public bool insertupdateComplianceAudit(List<ComplianceAudit> auditdata, char Flag)
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
                        cmd.Parameters.AddWithValue("p_Compliance_Audit_ID", audit.Compliance_Audit_Id);
                        cmd.Parameters.AddWithValue("p_Comp_Schedule_Instance", audit.Compliance_Schedule_Instance);
                        cmd.Parameters.AddWithValue("p_Penalty_nc", audit.Penalty_nc);
                        cmd.Parameters.AddWithValue("p_Audit_Remarks", audit.Audit_Remarks);
                        cmd.Parameters.AddWithValue("p_Audit_artefacts", audit.Audit_ArteFacts);
                        cmd.Parameters.AddWithValue("p_Audit_Date ", audit.Audit_Date);
                        cmd.Parameters.AddWithValue("p_Version", audit.Version);
                        cmd.Parameters.AddWithValue("p_Reviewer_ID", audit.Reviewer_Id);
                        cmd.Parameters.AddWithValue("p_Review_Comments", audit.Reviewer_Comments) ;
                      //  cmd.Parameters.AddWithValue("p_Last_Updated_Date", MySqlDbType.DateTime).Value = audit.LastUpdatedDate;
                        cmd.Parameters.AddWithValue("p_Audit_Status", audit.Audit_Status);
                        cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", audit.Compliance_Xref_Id) ;
                        cmd.Parameters.AddWithValue("p_Org_Hier_ID", audit.Org_Hier_Id);
                        cmd.Parameters.AddWithValue("p_Compliance_Opt_Xref_ID", audit.Compliance_Options_Id);
                        cmd.Parameters.AddWithValue("p_Auditor_ID ", audit.Auditor_Id);
                        cmd.Parameters.AddWithValue("p_User_ID", audit.User_Id) ;
                        cmd.Parameters.AddWithValue("p_Is_Active", audit.Is_Active);
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

        public DataSet getComlianceAudit(int Compliance_Audit_ID)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceAudit", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Audit_ID",  Compliance_Audit_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsComplianceAudit);
            }
            catch
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return dsComplianceAudit;
        }

        public bool deleteComlianceAudit(int Compliance_Audit_ID)
        {
            bool resultComplianceAudit = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_deleteComplianceAudit", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Audit_ID", Compliance_Audit_ID);
                int resultCount = cmd.ExecuteNonQuery();
                if(resultCount > 0)
                {
                    resultComplianceAudit = true;
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
            return resultComplianceAudit;
        }

        public DataSet sp_getAllCompanyBrnachAssignedtoAuditor(int AuditorID)
        {
            DataSet dsAllCompany = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getAllCompanyBrnachAssignedtoAuditor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Auditor_ID", AuditorID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsAllCompany);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsAllCompany; 
        }

      

        public DataSet getComlianceXrefDataForSeletedBranch(int OrgID)
        {
            DataSet dsComplianceXrefData = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXrefData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsComplianceXrefData);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsComplianceXrefData; 
        }
    }
}
