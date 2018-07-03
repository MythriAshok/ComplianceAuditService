using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataObject;
using Compliance.DataAccess;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CompanyDetailsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CompanyDetailsService.svc or CompanyDetailsService.svc.cs at the Solution Explorer and start debugging.
    public class CompanyDetailsService : ICompanyDetailsService
    {
        public void DoWork()
        {
        }
        public int insertCompanyDetails(CompanyDetails companydetails)
        {
            int insertcompid = 0;
            try
            {
                CompanyDetailsHelper helper = new CompanyDetailsHelper();
                insertcompid = helper.insertupdateCompanyDetails(companydetails, 'I');
            }
            catch
            {
                throw;
            }
            return insertcompid;
        }

        public int updateCompanyDetails(CompanyDetails companydetails)
        {
            int updatecompid = 0;
            try
            {
                CompanyDetailsHelper helper = new CompanyDetailsHelper();
                updatecompid = helper.insertupdateCompanyDetails(companydetails, 'I');
            }
            catch
            {
                throw;
            }
            return updatecompid;
        }

    }
}
