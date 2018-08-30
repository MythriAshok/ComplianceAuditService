using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace Compliance.DataAccess
{
    public class UtilityHelper
    {
    /// <summary>
    /// Converts the null column in the datable to the string.empty
    /// </summary>
    /// <param name="ds">Dataset</param>
    /// <returns>Dataset</returns>
        public DataSet ConvertNullsToEmptyString(DataSet ds)
        {
            try
            {

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ReadOnly = false;

                        if (string.IsNullOrEmpty(row[i].ToString()))
                            row[i] = string.Empty;
                    }
                }
            }
            catch(Exception ex)
            {
                string mess = "error";
                mess = ex.Message;
                
            }
            return ds;
        }
    }
}
