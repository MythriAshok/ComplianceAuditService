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
        public int CreateComplianceXref(ComplianceXref xref, char Flag)
        {
            int ComplianceXrefId = 0;
            try
            { 
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("sp_insertupdateComplianceXref",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_Flag", Flag);
            cmd.Parameters.AddWithValue("p_Compliance_Xref_ID", xref.Compliance_Xref_ID);
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
            cmd.Parameters.AddWithValue("p_Last_Updated_Date", xref.Last_Updated_Date );
            cmd.Parameters.AddWithValue("p_User_ID", xref.User_ID);
           // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            object objcompliancexrefid = cmd.ExecuteScalar();
            if (objcompliancexrefid != null)
                {
                    ComplianceXrefId = Convert.ToInt32(objcompliancexrefid);
                }
            }
            catch 
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return ComplianceXrefId;
        }

          

        public DataTable GetComlianceXref(int ComplianceXrefID)
        {
            DataTable dtComplianceXref = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getComplianceXref",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Compliance_Xref_ID ", ComplianceXrefID);
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
    }

    }

    

