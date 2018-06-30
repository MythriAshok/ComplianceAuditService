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
        MySqlConnection connection = DBConnection.getconnection();
        // MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString);
        public int CreateComplianceXref(ComplianceXref xref)
        {
            int ComplianceXrefId = 0;
            try
            { 
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("m_Compliance_Xref_ID", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Comp_Category", MySqlDbType.VarChar,45).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Comp_Description", MySqlDbType.VarChar, 45).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Is_Header", MySqlDbType.Bit).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_level", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Comp_Order", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Option_ID", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Risk_Category", MySqlDbType.VarChar, 45).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Risk_Description", MySqlDbType.VarChar, 45).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Recurrence", MySqlDbType.VarChar, 45).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Form ", MySqlDbType.VarChar, 45).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Type", MySqlDbType.VarChar, 45).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Is_Best_Practice", MySqlDbType.Bit).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Version", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Effective_Start_Date", MySqlDbType.DateTime).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Effective_End_Date", MySqlDbType.DateTime).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Country_ID ", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_State_ID", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_City_ID", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_Last_Updated_Date", MySqlDbType.DateTime).Value = xref.Compliance_Xref_ID;
            cmd.Parameters.Add("m_User_ID", MySqlDbType.Int32).Value = xref.Compliance_Xref_ID;
                cmd.CommandText = "sp_insertComplianceXref";
           // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            object objcompliancexrefid = cmd.ExecuteScalar();
            if (objcompliancexrefid != null)
                {
                    ComplianceXrefId = Convert.ToInt32(objcompliancexrefid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return ComplianceXrefId;
        }

          

        public DataTable GetComlianceXref(int ComplianceXrefID)
        {
            DataTable dtComplianceXref = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_Compliance_Xref_ID ", MySqlDbType.Int32).Value = ComplianceXrefID;
                cmd.CommandText = "sp_getComplianceXref";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtComplianceXref);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtComplianceXref;
        }
    }

    }

    

