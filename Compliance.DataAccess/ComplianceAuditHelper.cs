﻿using System;
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
                        cmd.Parameters.AddWithValue("p_Xref_Comp_Type_Map_ID", audit.Xref_Comp_Type_Map_ID);
                        cmd.Parameters.AddWithValue("p_Org_Hier_ID", audit.Org_Hier_Id);
                        cmd.Parameters.AddWithValue("p_Auditor_ID", audit.Auditor_Id);
                        cmd.Parameters.AddWithValue("p_Audit_Followup_Date", audit.Audit_Followup_Date);
                        cmd.Parameters.AddWithValue("p_Audit_Remarks", audit.Audit_Remarks);
                        cmd.Parameters.AddWithValue("p_Is_Active", audit.Is_Active);
                        cmd.Parameters.AddWithValue("p_Version", audit.Version);
                        cmd.Parameters.AddWithValue("p_Compliance_Status", audit.Audit_Status);
                        cmd.Parameters.AddWithValue("p_Applicability", audit.Applicability);
                        cmd.Parameters.AddWithValue("p_Start_Date", audit.Start_Date);
                        cmd.Parameters.AddWithValue("p_End_Date", audit.End_Date);
                        cmd.Parameters.AddWithValue("p_Risk_Category", audit.Risk_Category);
                        cmd.Parameters.AddWithValue("p_Vendor_ID", audit.Vendor_Id);
                        cmd.Parameters.AddWithValue("p_Evidences", audit.Evidences);

                        //cmd.Parameters.AddWithValue("p_Comp_Schedule_Instance", audit.Compliance_Schedule_Instance);
                        // cmd.Parameters.AddWithValue("p_Penalty_nc", audit.Penalty_nc);
                        //cmd.Parameters.AddWithValue("p_Audit_Date", audit.Audit_Date);
                        // cmd.Parameters.AddWithValue("p_Reviewer_ID", audit.Reviewer_Id);
                        //cmd.Parameters.AddWithValue("p_Review_Comments", audit.Reviewer_Comments) ;
                        //  cmd.Parameters.AddWithValue("p_Last_Updated_Date", MySqlDbType.DateTime).Value = audit.LastUpdatedDate;
                        //  cmd.Parameters.AddWithValue("p_Compliance_Opt_Xref_ID", audit.Compliance_Options_Id);
                        //  cmd.Parameters.AddWithValue("p_Auditor_ID", audit.User_Id);
                        //cmd.Parameters.AddWithValue("p_Part_Compliance_Percent", audit.Part_Compliance_Percent);

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

        public bool insertupdateAuditentries(ComplianceAudit audit, char Flag)
        {
            bool ComplianceAuditResult = false;
            try
            {
                if (audit != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateComplianceAudit", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                        cmd.Parameters.AddWithValue("p_Flag", Flag);
                        cmd.Parameters.AddWithValue("p_Compliance_Audit_ID", audit.Compliance_Audit_Id);
                        cmd.Parameters.AddWithValue("p_Xref_Comp_Type_Map_ID", audit.Xref_Comp_Type_Map_ID);
                        cmd.Parameters.AddWithValue("p_Org_Hier_ID", audit.Org_Hier_Id);
                        cmd.Parameters.AddWithValue("p_Auditor_ID", audit.Auditor_Id);
                        cmd.Parameters.AddWithValue("p_Audit_Followup_Date", audit.Audit_Followup_Date);
                        cmd.Parameters.AddWithValue("p_Audit_Remarks", audit.Audit_Remarks);
                        cmd.Parameters.AddWithValue("p_Is_Active", audit.Is_Active);
                        cmd.Parameters.AddWithValue("p_Version", audit.Version);
                        cmd.Parameters.AddWithValue("p_Compliance_Status", audit.Audit_Status);
                        cmd.Parameters.AddWithValue("p_Applicability", audit.Applicability);
                        cmd.Parameters.AddWithValue("p_Start_Date", audit.Start_Date);
                        cmd.Parameters.AddWithValue("p_End_Date", audit.End_Date);
                        cmd.Parameters.AddWithValue("p_Risk_Category", audit.Risk_Category);
                        cmd.Parameters.AddWithValue("p_Vendor_ID", audit.Vendor_Id);
                        cmd.Parameters.AddWithValue("p_Evidences", audit.Evidences);
                        cmd.Parameters.AddWithValue("p_Periodicity", audit.Periodicity);

                        int objcomplianceauditid = cmd.ExecuteNonQuery();
                        if (objcomplianceauditid > 0)
                        {
                            ComplianceAuditResult = true;
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
        

              public DataSet getComlianceAuditonorg(int org_id,int vendor_id,int version,DateTime sdate,DateTime edate)
        {
            DataSet dsComplianceAudit = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_get_compliance_audit_on_org", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", org_id);
                cmd.Parameters.AddWithValue("p_Vendor_ID", vendor_id);
                cmd.Parameters.AddWithValue("p_Version", version);
                cmd.Parameters.AddWithValue("p_sdate",sdate);
                cmd.Parameters.AddWithValue("p_edate", edate);
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

      

        public DataSet getComlianceXrefDataForSeletedBranch(int OrgID,int VendorID,int complianceType_ID,int Compliance_Parrent_ID)
        {
            DataSet dsComplianceXrefData = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXrefData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);
                cmd.Parameters.AddWithValue("p_Vendor_ID", VendorID);
                cmd.Parameters.AddWithValue("p_compliancety_ID", complianceType_ID);
                cmd.Parameters.AddWithValue("p_Compliance_Parent_ID", Compliance_Parrent_ID);
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

        public DataSet getComlianceActList(int OrgID, int VendorID, int complianceType_ID)
        {
            DataSet dsComplianceXrefData = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("getComplainceActlist", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", OrgID);
                cmd.Parameters.AddWithValue("p_Vendor_ID", VendorID);
                cmd.Parameters.AddWithValue("p_Compliance_Type_ID", complianceType_ID);
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

        public DataSet getSpecificBranchList(int OrgID)
        {
            DataSet dsSpecificBranchList = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getSpecificBranchList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Parent_Company_ID", OrgID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsSpecificBranchList);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsSpecificBranchList;
        }

        public bool insertupdatecustomAuditentries(ComplianceAudit audit)
        {
            bool ComplianceAuditResult = false;
            try
            {
                if (audit != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insert_update_Custom_audit", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_Custom_Audit_ID", audit.Compliance_Audit_Id);
                    cmd.Parameters.AddWithValue("p_Org_Hier_ID", audit.Org_Hier_Id);
                    cmd.Parameters.AddWithValue("p_Auditor_ID", audit.Auditor_Id);
                    cmd.Parameters.AddWithValue("p_Audit_Followup_Date", audit.Audit_Followup_Date);
                    cmd.Parameters.AddWithValue("p_Audit_Remarks", audit.Audit_Remarks);
                    cmd.Parameters.AddWithValue("p_Is_Active", audit.Is_Active);
                    cmd.Parameters.AddWithValue("p_Version", audit.Version);
                    cmd.Parameters.AddWithValue("p_Compliance_Status", audit.Audit_Status);
                    cmd.Parameters.AddWithValue("p_Applicability", audit.Applicability);
                    cmd.Parameters.AddWithValue("p_Start_Date", audit.Start_Date);
                    cmd.Parameters.AddWithValue("p_End_Date", audit.End_Date);
                    cmd.Parameters.AddWithValue("p_Risk_Category", audit.Risk_Category);
                    cmd.Parameters.AddWithValue("p_Vendor_ID", audit.Vendor_Id);
                    cmd.Parameters.AddWithValue("p_Evidences", audit.Evidences);
                    cmd.Parameters.AddWithValue("p_Custom_Xref_ID", audit.Xref_Comp_Type_Map_ID);

                    int objcomplianceauditid = cmd.ExecuteNonQuery();
                    if (objcomplianceauditid > 0)
                    {
                        ComplianceAuditResult = true;
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
            return ComplianceAuditResult;
        }

        public bool updateAuditentries(ComplianceAudit audit)
        {
            bool ComplianceAuditResult = false;
            try
            {
                if (audit != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_update_compliance_audit", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Compliance_Audit_ID", audit.Compliance_Audit_Id);
                    cmd.Parameters.AddWithValue("p_Version", audit.Version);
                    cmd.Parameters.AddWithValue("p_Auditor_ID", audit.Auditor_Id);
                    cmd.Parameters.AddWithValue("p_Vendor_ID", audit.Vendor_Id);
                    cmd.Parameters.AddWithValue("p_Start_Date", audit.Start_Date);
                    cmd.Parameters.AddWithValue("p_End_Date", audit.End_Date);
                    cmd.Parameters.AddWithValue("p_Audit_Remarks", audit.Audit_Remarks);
                    cmd.Parameters.AddWithValue("p_Is_Active", audit.Is_Active);

                    int objcomplianceauditid = cmd.ExecuteNonQuery();
                    if (objcomplianceauditid > 0)
                    {
                        ComplianceAuditResult = true;
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
            return ComplianceAuditResult;
        }

        public DataSet getComplianceTypemappedvendor(int ComplianceID,int branchid)
        {
            DataSet dsMappedCompliance = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_get_ComplianceType_map_Vendor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_ComplinceTypeId", ComplianceID);
                cmd.Parameters.AddWithValue("p_branchid", branchid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dsMappedCompliance);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dsMappedCompliance;
        }
    }
}
