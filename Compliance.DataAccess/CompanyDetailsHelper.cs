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
        MySqlConnection conn = DBConnection.getconnection();
        public int insertupdateCompanyDetails(CompanyDetails details, char Flag)
        {
            int CompanyDetailsId = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_insertupdateCompanyDetails",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Flag", Flag);
                cmd.Parameters.AddWithValue("p_Company_Details ", details.Company_Details_ID ) ;
                cmd.Parameters.AddWithValue("p_Org_Hier_ID", details.Org_Hier_ID);
                cmd.Parameters.AddWithValue("p_Industry_Type", details.Industry_Type) ;
                cmd.Parameters.AddWithValue("p_Formal_Name", details.Formal_Name);
                cmd.Parameters.AddWithValue("p_Calender_StartDate", details.Calender_StartDate);
                cmd.Parameters.AddWithValue("p_Calender_EndDate", details.Calender_EndDate);
                cmd.Parameters.AddWithValue("p_Auditing_Frequency", details.Auditing_Frequency) ;
                cmd.Parameters.AddWithValue("p_Website", details.Website);
                cmd.Parameters.AddWithValue("p_Company_EmailID", details.Company_EmailID);
                cmd.Parameters.AddWithValue("p_Company_ContactNumber1", details.Company_ContactNumber1);
                cmd.Parameters.AddWithValue("p_Company_ContactNumber2", details.Company_ContactNumber2);
               // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                object objcompanydetailsid = cmd.ExecuteScalar();
                if (objcompanydetailsid != null)
                {
                    CompanyDetailsId = Convert.ToInt32(objcompanydetailsid);
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
            return CompanyDetailsId;
        }

        public DataTable GetCompanyDetails(int CompanyDetailsId)
        {
            DataTable dtCompanyDetails = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getCompanyDetails",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Company_Details_ID ", CompanyDetailsId) ;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtCompanyDetails);
            }
            catch 
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return dtCompanyDetails;
        }

    }
}
