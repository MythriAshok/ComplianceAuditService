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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CompanyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CompanyService.svc or CompanyService.svc.cs at the Solution Explorer and start debugging.
    public class CompanyService : ICompanyService
    {
        public void DoWork()
        {
        }
        public int insertBranchLocation(Branch branchlocation)
        {
            int insertresultbranchid = 0;
            try
            {
                BranchHelper helper = new BranchHelper();
                insertresultbranchid = helper.insertupdateBranchLocation(branchlocation, 'I');
            }
            catch
            {
                throw;
            }
            return insertresultbranchid;
        }

        public int updateBranchLocation(Branch branchlocation)
        {
            int updateresultbranchid = 0;
            try
            {
                BranchHelper helper = new BranchHelper();
                updateresultbranchid = helper.insertupdateBranchLocation(branchlocation, 'U');
            }
            catch
            {
                throw;
            }
            return updateresultbranchid;
        }

        public int insertOrganization(Organization org)
        {
            int insertorgid = 0;
            try
            {
                OrganizationHelper helper = new OrganizationHelper();
                insertorgid = helper.insertupdateOrganizationHier(org, 'I');
            }
            catch
            {
                throw;
            }
            return insertorgid;
        }
        public int updateOrganization(Organization org)
        {
            int insertorgid = 0;
            try
            {
                OrganizationHelper helper = new OrganizationHelper();
                insertorgid = helper.insertupdateOrganizationHier(org, 'I');
            }
            catch
            {
                throw;
            }
            return insertorgid;
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

        public List<Country> BindCountry()
        {
            CountryHelper helper = new CountryHelper();
            helper.getCountryList();
            List<Country> countrylist = new List<Country>();
            return countrylist;
        }
    }
}
