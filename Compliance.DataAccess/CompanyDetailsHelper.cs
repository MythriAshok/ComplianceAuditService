using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Compliance.DataObject;

namespace Compliance.DataAccess
{
   public class CompanyDetailsHelper
    {
        MySqlConnection connection = DBConnection.getconnection();
        public int CreateCompanyDetails(CompanyDetails details)
        {
            int CompanyDetailsId = 0;
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insertCompanyDetails";
                cmd.Parameters.Add("p_iD ", MySqlDbType.Int32).Value = details.iD;
                cmd.Parameters.Add("p_Org_Hier_ID", MySqlDbType.Int32).Value = details.Org_Hier_ID;
                cmd.Parameters.Add("p_Industry_Type", MySqlDbType.VarChar, 45).Value = details.Industry_Type;
                cmd.Parameters.Add("p_Formal_Name", MySqlDbType.VarChar, 45).Value = details.Formal_Name;
                cmd.Parameters.Add("p_Calender_StartDate", MySqlDbType.DateTime).Value = details.Calender_StartDate;
                cmd.Parameters.Add("p_Calender_EndDate", MySqlDbType.DateTime).Value = details.Calender_EndDate;
                cmd.Parameters.Add("p_Auditing_Frequency", MySqlDbType.VarChar, 45).Value = details.Auditing_Frequency;
                cmd.Parameters.Add("p_Website", MySqlDbType.VarChar, 45).Value = details.Website;
                cmd.Parameters.Add("p_Company_EmailID", MySqlDbType.Int32).Value = details.Company_EmailID;
                cmd.Parameters.Add("p_Company_ContactNumber1", MySqlDbType.DateTime).Value = details.Company_ContactNumber1;
                cmd.Parameters.Add("p_Company_ContactNumber2", MySqlDbType.DateTime).Value = details.Company_ContactNumber2;
               // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objcompanydetailsid = cmd.ExecuteScalar();
                if (objcompanydetailsid != null)
                {
                    CompanyDetailsId = Convert.ToInt32(objcompanydetailsid);
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
            return CompanyDetailsId;
        }

        public DataTable GetCompanyDetails(int CompanyDetailsId)
        {
            DataTable dtCompanyDetails = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Company_Details_ID ", MySqlDbType.Int32).Value = CompanyDetailsId;
                cmd.CommandText = "sp_getCompanyDetails";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtCompanyDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dtCompanyDetails;
        }

    }
}
