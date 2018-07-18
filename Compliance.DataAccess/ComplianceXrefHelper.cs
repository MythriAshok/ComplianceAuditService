using System;
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
        public bool insertupdateComplianceXref(ComplianceXref xref, char Flag)
        {
            bool ComplianceXref = false;
            try
            {
                if(xref != null)
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
                    cmd.Parameters.AddWithValue("p_Is_Header", xref.Is_Header);
                    cmd.Parameters.AddWithValue("p_level", xref.level);
                    cmd.Parameters.AddWithValue("p_Option_ID", xref.Option_ID);
                    cmd.Parameters.AddWithValue("p_Risk_Category", xref.Risk_Category);
                    cmd.Parameters.AddWithValue("p_Risk_Description", xref.Risk_Description);
                    cmd.Parameters.AddWithValue("p_Recurrence", xref.Recurrence);
                    cmd.Parameters.AddWithValue("p_Form ", xref.Form);
                    cmd.Parameters.AddWithValue("p_Type", xref.Type);
                    cmd.Parameters.AddWithValue("p_Is_Best_Practice", xref.Is_Best_Practice);
                    cmd.Parameters.AddWithValue("p_Version", xref.Version);
                    cmd.Parameters.AddWithValue("p_Effective_Start_Date", xref.Effective_Start_Date);
                    cmd.Parameters.AddWithValue("p_Effective_End_Date", xref.Effective_End_Date);
                    cmd.Parameters.AddWithValue("p_Country_ID ", xref.Country_ID);
                    cmd.Parameters.AddWithValue("p_State_ID", xref.State_ID);
                    cmd.Parameters.AddWithValue("p_City_ID", xref.City_ID);
                    cmd.Parameters.AddWithValue("p_Last_Updated_Date", xref.Last_Updated_Date);
                    cmd.Parameters.AddWithValue("p_User_ID", xref.User_ID);
                    cmd.Parameters.AddWithValue("p_Is_Active", xref.Is_Active);
                    int objcompliancexref = cmd.ExecuteNonQuery();
                    if (objcompliancexref > 0)
                    {
                        ComplianceXref = true;
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
            return ComplianceXref;
        }
          
        public DataSet getComlianceXref(int Compliance_Xref_ID)
        {
            DataSet dtComplianceXref = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXref",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID ", Compliance_Xref_ID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch 
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return dtComplianceXref;
        }

        public bool deleteComlianceXref(int Compliance_Xref_ID)
        {
            bool resultComplianceXref = false;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXref", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID ", Compliance_Xref_ID);
                int resultCount = cmd.ExecuteNonQuery();
                if(resultCount > 0)
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
    }

    }

    

