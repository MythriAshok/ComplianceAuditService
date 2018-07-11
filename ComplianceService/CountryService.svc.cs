using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataObject;
using Compliance.DataAccess;
using System.Data;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CountryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CountryService.svc or CountryService.svc.cs at the Solution Explorer and start debugging.
    public class CountryService : ICountryService
    {
        public void DoWork()
        {
        }
        public bool GetCountry(Country country)
        {
            int CountryID = 0;
            bool Result = false;
            try
            {
                CountryHelper chelper = new CountryHelper();
                DataSet dscountry = chelper.getCountry();
                string xmlcountry = dscountry.GetXml();
                if (xmlcountry != null)
                {
                    CountryID = country.CompanyId;
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }

            catch
            {
                throw;
            }
            return Result;
        }
    }
}
