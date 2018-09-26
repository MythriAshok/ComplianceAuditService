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

        public DataSet getBranchComlianceAuditReport(int Org_Hier_ID, DateTime StartDate, DateTime EndDate, int ComplianceTypeID)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {
                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getDetailedBranchComplianceAuditReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", Org_Hier_ID);
                cmd.Parameters.AddWithValue("p_Start_Date", StartDate);
                cmd.Parameters.AddWithValue("p_End_Date",EndDate);
                cmd.Parameters.AddWithValue("p_Compliance_Type_ID", ComplianceTypeID);
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

        public DataSet getDetailedBranchACTComlianceAuditReport(int Org_Hier_ID)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getDetailedBranchCompliance_ACTAuditReport", conn);
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

        public DataSet getBranchStatusComlianceAuditReport(int Org_Hier_ID, string status, DateTime StartDate, DateTime EndDate, int ComplianceID)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompiledBranchComplianceAuditReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", Org_Hier_ID);
                cmd.Parameters.AddWithValue("p_Compliance_Status", status);
                cmd.Parameters.AddWithValue("p_Start_Date", StartDate);
                cmd.Parameters.AddWithValue("p_End_Date", EndDate);
                cmd.Parameters.AddWithValue("p_Compliance_Type_ID", ComplianceID);
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

        public DataSet getComplianceStatusBranchACTAuditReport(int Org_Hier_ID, string status)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getACTCompliancedBranchCompliance_ACTAuditReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", Org_Hier_ID);
                cmd.Parameters.AddWithValue("p_Compliance_Status", status);
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

        public DataSet getpieBranchComlianceAuditReport(int Org_Hier_ID)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getpieDetailedBranchComplianceAuditReport", conn);
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



        //public DataSet getYearforAuditReports( int Frequencyid)
        //{
        //    DataSet dsComplianceAudit = new DataSet();
        //    try
        //    {

        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("sp_getYearofAuditReports", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);

        //        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        //        adapter.Fill(dsComplianceAudit);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return dsComplianceAudit;
        //}
    }
}
