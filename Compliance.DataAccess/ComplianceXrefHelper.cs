﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.DataObject;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Compliance.DataAccess
{
    public class ComplianceXrefHelper
    {
        MySqlConnection conn = DBConnection.getconnection();
        // MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString);
        public int insertupdateComplianceXref(ComplianceXref xref, char Flag)
        {
            int ComplianceXref = 0;
            try
            {
                if (xref != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateComplianceXref", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Flag", Flag);
                    cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", xref.Compliance_Xref_ID);
                    cmd.Parameters.AddWithValue("p_Compliance_Parent_ID", xref.Compliance_Parent_ID);
                    cmd.Parameters.AddWithValue("p_Compliance_Title", xref.Compliance_Title);
                    cmd.Parameters.AddWithValue("p_Comp_Category", xref.Comp_Category);
                    cmd.Parameters.AddWithValue("p_Comp_Description", xref.Comp_Description);
                    cmd.Parameters.AddWithValue("p_compl_def_consequence", xref.compl_def_consequence);
                    cmd.Parameters.AddWithValue("p_Is_Header", xref.Is_Header);
                    cmd.Parameters.AddWithValue("p_level", xref.level);
                   // cmd.Parameters.AddWithValue("p_Comp_Order", xref.Comp_Order);
                    cmd.Parameters.AddWithValue("p_Risk_Category", xref.Risk_Category);
                    cmd.Parameters.AddWithValue("p_Risk_Description", xref.Risk_Description);
                    cmd.Parameters.AddWithValue("p_Periodicity", xref.Periodicity);
                    //cmd.Parameters.AddWithValue("p_Form", xref.Form);
                    //cmd.Parameters.AddWithValue("p_Type", xref.Type);
                    //cmd.Parameters.AddWithValue("p_Is_Best_Practice", xref.Is_Best_Practice);
                    cmd.Parameters.AddWithValue("p_Version", xref.Version);
                    cmd.Parameters.AddWithValue("p_Effective_Start_Date", xref.Effective_Start_Date);
                    cmd.Parameters.AddWithValue("p_Effective_End_Date", xref.Effective_End_Date);
                    cmd.Parameters.AddWithValue("p_Country_ID", xref.Country_ID);
                    cmd.Parameters.AddWithValue("p_State_ID", xref.State_ID);
                    cmd.Parameters.AddWithValue("p_City_ID", xref.City_ID);
                    cmd.Parameters.AddWithValue("p_User_ID", xref.User_ID);
                    cmd.Parameters.AddWithValue("p_Is_Active", xref.Is_Active);
                    ComplianceXref = Convert.ToInt32(cmd.ExecuteScalar());
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
            return ComplianceXref;
        }

        public DataSet getComlianceXref(int Audit_Type_ID)
        {
            DataSet dtComplianceXref = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXref", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Audit_Type_ID", Audit_Type_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXref;
        }

        public DataSet getComlianceXrefonType(int Audit_Type_ID,int CountryID,int StateID,int CityID,int flag)
        {
            DataSet dtComplianceXref = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXreftype", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Audit_Type_ID", Audit_Type_ID);
                cmd.Parameters.AddWithValue("p_Country_ID", CountryID);
                cmd.Parameters.AddWithValue("p_State_ID", StateID);
                cmd.Parameters.AddWithValue("p_City_ID", CityID);
                cmd.Parameters.AddWithValue("flag", flag);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXref;
        }

        public DataSet getAct(int compliance_xref_ID)
        {
            DataSet dtComplianceXref = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getActs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", compliance_xref_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXref;
        }

        public DataSet getlineitems(int parentid)
        {
            DataSet dtComplianceXref = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getlineitems", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Parent_ID", parentid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXref;
        }
        
             public DataSet getSpecifiySection(int complianceid)
        {
            DataSet dtComplianceXref = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getSpecifiySection", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", complianceid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXref;
        }
        public DataSet getRules(int parentid)
        {
            DataSet dtComplianceXref = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getRules", conn);
                cmd.Parameters.AddWithValue("p_Compliance_Parent_ID", parentid);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXref;
        }

        public bool deleteComlianceXref(int Org_Hier_ID,int Vendor_Id)
        {
            bool resultComplianceXref = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_DeleteComplianceBranchMapping", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", Org_Hier_ID);
                cmd.Parameters.AddWithValue("p_Vendor_ID", Vendor_Id);
                int resultCount = cmd.ExecuteNonQuery();
                if (resultCount > 0)
                {
                    resultComplianceXref = true;
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
            return resultComplianceXref;
        }

        public int getAuditorforBranch(int BranchId)
        {
            int Auditorid = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getAuditorforBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Branch_Id", BranchId);
                Auditorid = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return Auditorid;
        }

        public bool insertActAndRuleforBranch(int orgid,int ruleid,int userid,int vendorid,int compliancetypeid)
        {
            bool res = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_insertupdateComplianceBranchMapping", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_ID", ruleid);
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", orgid);
                cmd.Parameters.AddWithValue("p_UpdatedByLogin_ID", userid);
                cmd.Parameters.AddWithValue("p_Is_Active", 1);
                cmd.Parameters.AddWithValue("p_Vendor_Id", vendorid);
                cmd.Parameters.AddWithValue("p_Compliancetypeid", compliancetypeid);
                int count=cmd.ExecuteNonQuery();
                if(count>0)
                {
                    res = true;
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
            return res;
        }

        public DataSet getRuleforBranch(int branchid,int vendorid)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getRuleforBranch", conn);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("p_Org_ID", branchid);
                cmd.Parameters.AddWithValue("p_vendor_ID", vendorid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return ds;

        }

        public DataSet getSpecificcompliance(int complianceid)
        {            
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("getspecificcompliance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", complianceid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetComplianceType(int compliancetypeid)
        {
             DataSet ds=new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceType", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_compliance_type_ID", compliancetypeid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }

        public bool insertxreftypemapping(int xrefid,int compliancetypeid, string flag)
        {
            bool res= false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("spinsertxref_comp_type_mapping", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Type_ID", compliancetypeid);
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", xrefid);
                res=Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (flag == "Act")
                {
                    conn.Close();
                    insertrulecompliancetypemapping(xrefid, compliancetypeid);
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
            return res;
        }
        private void insertrulecompliancetypemapping(int xrefid, int compliancetypeid)
        {            
            DataSet ds = new DataSet();
            try
            {                                              
                ds=getlineitems(xrefid);
                foreach(System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    insertxreftypemapping(Convert.ToInt32(row["Compliance_Xref_ID"]), compliancetypeid, "Rule");
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                
            }
            return ;
        }
        public bool deletexreftypemapping(int compliancetypeid,int Complianceid)
        {
            bool res = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_delete_xref_comp_type_mapping", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Type_ID", compliancetypeid);
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", Complianceid);
                res = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return res;
        }

        public DataSet GetxrefComplianceMapping(int compliancetypeid,int complianceXrefid)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_get_xref_comp_type_mapping", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Type_ID", compliancetypeid);
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", complianceXrefid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }

        public DataSet Checkparentcompliancetype(int complianceparentid)
        {
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_checkparentcompliancetype", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Type_ID", complianceparentid);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }

        public int insertupdateCustomComplianceXref(ComplianceXref xref)
        {
            int ComplianceXref = 0;
            try
            {
                if (xref != null)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertupdateComplianceXref", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Custom_Xref_ID", xref.Compliance_Xref_ID);
                    cmd.Parameters.AddWithValue("p_Compliance_Parent_ID", xref.Compliance_Parent_ID);
                    cmd.Parameters.AddWithValue("p_Compliance_Title", xref.Compliance_Title);
                    cmd.Parameters.AddWithValue("p_Comp_Category", xref.Comp_Category);
                    cmd.Parameters.AddWithValue("p_Comp_Description", xref.Comp_Description);
                    cmd.Parameters.AddWithValue("p_compl_def_consequence", xref.compl_def_consequence);
                    cmd.Parameters.AddWithValue("p_Is_Header", xref.Is_Header);
                    cmd.Parameters.AddWithValue("p_Level", xref.level);
                    cmd.Parameters.AddWithValue("p_Comp_Order", xref.Comp_Order);
                    cmd.Parameters.AddWithValue("p_Risk_Description", xref.Risk_Description);
                    cmd.Parameters.AddWithValue("p_Periodicity", xref.Periodicity);  
                    cmd.Parameters.AddWithValue("p_Is_Best_Practice", xref.Is_Best_Practice);
                    cmd.Parameters.AddWithValue("p_Version", xref.Version);
                    cmd.Parameters.AddWithValue("p_Effective_Start_Date", xref.Effective_Start_Date);
                    cmd.Parameters.AddWithValue("p_Effective_End_Date", xref.Effective_End_Date);
                    cmd.Parameters.AddWithValue("p_Country_ID", xref.Country_ID);
                    cmd.Parameters.AddWithValue("p_State_ID", xref.State_ID);
                    cmd.Parameters.AddWithValue("p_City_ID", xref.City_ID);
                    cmd.Parameters.AddWithValue("p_User_ID", xref.User_ID);
                    cmd.Parameters.AddWithValue("p_Compliance_Type_ID", xref.Compliance_Type_ID);
                                  
                    ComplianceXref = Convert.ToInt32(cmd.ExecuteScalar());
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
            return ComplianceXref;
        }

    }
}
    

