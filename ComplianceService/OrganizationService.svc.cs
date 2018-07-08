using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;
using System.Data;
using System.Xml;
using System.IO;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrganizationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrganizationService.svc or OrganizationService.svc.cs at the Solution Explorer and start debugging.
    public class OrganizationService : IOrganizationService
    {
        public void DoWork()
        {
        }
        public int insertOrganization(Organization org, CompanyDetails company, Branch branch)
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


        public string GetCountryList() 
        {
            XmlDocument xmlCountries = BindCountry();
            return xmlCountries.InnerText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public string GetCountryListIOS() //create private mthd n move this into tht n this methd returns nly string
        //{
        //    XmlDocument xmlCountries = BindCountry();
        //    string str = Convert.ToString(xmlCountries);
        //    return str;

        //}

        private XmlDocument BindCountry()
        {
            XmlDocument xmlCountries = new XmlDocument();
            CountryHelper helper = new CountryHelper();
            DataTable dtCountries = helper.getCountryList();
           // dt.ReadXml();
            return xmlCountries;
        }


        public string GetStateList()
        {
            XmlDocument xmlStates = BindState();
            return xmlStates.InnerText;
        }
        private XmlDocument BindState()
        {
            XmlDocument xmlStates = new XmlDocument();
            CountryHelper helper = new CountryHelper();
            DataTable dtStates = helper.getStateList();
            //List<State> statelist = new List<State>();
            return xmlStates;
        }

        public string GetCityList(int stateId)
        {
            return BindCity(stateId);
            
        }
        private string BindCity(int stateId)
        {
            CountryHelper countryhelper = new CountryHelper();
            DataSet dsCities = countryhelper.getCity(stateId);
            string xmlstr = dsCities.GetXml();

            

            return xmlstr;
        }
    }
}
