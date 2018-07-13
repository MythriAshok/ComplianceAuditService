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
    // NOTE: You can use the "Rename" command on the "Refa ctor" menu to change the class name "OrganizationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrganizationService.svc or OrganizationService.svc.cs at the Solution Explorer and start debugging.
    public class OrganizationService : IOrganizationService
    {
        public bool insertOrganization(Organization org, CompanyDetails company, BranchLocation branch)
        {
            int OrganizationID = 0;
            int BranchLocationID = 0;
            int CompanyDetailsID = 0;
            bool insertResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();

                BranchLocationID = organizationhelper.insertupdateBranchLocation(branch, 'I');
                if (BranchLocationID > 0)
                {
                    org.Branch_Id = BranchLocationID;
                    OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'I');
                    if (OrganizationID > 0)
                    {
                        company.Org_Hier_ID = OrganizationID;
                        CompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'I');
                    }
                }
                if (BranchLocationID > 0 || OrganizationID > 0)
                {
                    insertResult = true;
                }
            }
            catch
            {
                throw;
            }
            return insertResult;
        }
        public bool updateOrganization(Organization org, CompanyDetails company, BranchLocation branch)
        {
            int insertOrganizationID = 0;
            int inserBranchID = 0;
            int insertCompanyDetailsID = 0;
            bool updateResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                inserBranchID = organizationhelper.insertupdateBranchLocation(branch, 'U');
                insertOrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'U');
                insertCompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'U');
                if (inserBranchID > 0 || insertOrganizationID > 0 || insertCompanyDetailsID > 0)
                {
                    updateResult = true;
                }
            }
            catch
            {
                throw;
            }
            return updateResult;
        }

        //public string getOrganization(int orgID)
        //{
        //    string xmlOrganization = "";
        //    string xmlCompanyDetails = "";
        //    string xmlBranchLocation = "";
        //    Organization org = new Organization();
        //    OrganizationHelper orgHelper = new OrganizationHelper();
        //    DataSet dsorganization = orgHelper.getOrganizationHier(orgID);
        //    xmlOrganization = dsorganization.GetXml();
        //    DataSet dsBranchLocation = orgHelper.getBranchLocation(org.Branch_Id);
        //    xmlBranchLocation = dsBranchLocation.GetXml();
        //    DataSet dsCompanyDetails = orgHelper.getCompanyDetails(org.Company_Id);
        //    xmlCompanyDetails = dsCompanyDetails.GetXml();
        //    Tuple<string, string, string> tuple = new Tuple<string, string, string>(xmlBranchLocation, xmlOrganization, xmlCompanyDetails);
        //    string strGroupCompany = tuple.ToString();
        //    return strGroupCompany;
        //}

        public string getGroupCompany(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsOrganization = orgHelper.getOrganizationHier(orgID);
            string xmlOrganization = dsOrganization.GetXml();
            return xmlOrganization;
        }


        public bool insertCompany(Organization org, CompanyDetails company, BranchLocation branch)
        {
            int OrganizationID = 0;
            int BranchLocationID = 0;
            int CompanyDetailsID = 0;
            bool insertResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                BranchLocationID = organizationhelper.insertupdateBranchLocation(branch, 'I');
                if (BranchLocationID > 0)
                {
                    org.Branch_Id = BranchLocationID;
                    OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'I');
                    if (OrganizationID > 0)
                    {
                        company.Org_Hier_ID = OrganizationID;
                        CompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'I');
                    }
                }
                if (BranchLocationID > 0 || OrganizationID > 0)
                {
                    insertResult = true;
                }
            }
            catch
            {
                throw;
            }
            return insertResult;
        }

        public bool updateCompany(Organization org, CompanyDetails company, BranchLocation branch)
        {
            int insertOrganizationID = 0;
            int inserBranchID = 0;
            int insertCompanyDetailsID = 0;
            bool updateResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                inserBranchID = organizationhelper.insertupdateBranchLocation(branch, 'U');
                insertOrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'U');
                insertCompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'U');
                if (inserBranchID > 0 || insertOrganizationID > 0 || insertCompanyDetailsID > 0)
                {
                    updateResult = true;
                }
            }
            catch
            {
                throw;
            }
            return updateResult;
        }

        public string getCompany(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsOrganizationCompany = orgHelper.getOrganizationHier(orgID);
            string xmlOrganizationCompany = dsOrganizationCompany.GetXml();
            return xmlOrganizationCompany;
        }



        public bool insertBranch(Organization org, BranchLocation branch)
        {
            int OrganizationID = 0;
            int BranchLocationID = 0;
            bool insertResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                BranchLocationID = organizationhelper.insertupdateBranchLocation(branch, 'I');
                if (BranchLocationID > 0)
                {
                    org.Branch_Id = BranchLocationID;
                    OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'I');
                }
                if (BranchLocationID != 0 && OrganizationID != 0)
                {
                    insertResult = true;
                }
            }
            catch
            {
                throw;
            }
            return insertResult;
        }

        public bool updateBranch(Organization org, BranchLocation branch)
        {
            int insertOrganizationID = 0;
            int inserBranchID = 0;
            bool updateResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                inserBranchID = organizationhelper.insertupdateBranchLocation(branch, 'U');
                insertOrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'U');
                if (inserBranchID != 0 || insertOrganizationID != 0)
                {
                    updateResult = true;
                }
            }
            catch
            {
                throw;
            }
            return updateResult;
        }


        public string getBranch(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsOrganizationBranch = orgHelper.getOrganizationHier(orgID);
            string xmlOrganizationBranch = dsOrganizationBranch.GetXml();
            return xmlOrganizationBranch;
        }















        public string GetCountryList()
        {
            return BindCountry();
        }
        private string BindCountry()
        {
            CountryHelper helper = new CountryHelper();
            DataSet dsCountries = helper.getCountry();
            string xmlCountries = dsCountries.GetXml();
            return xmlCountries;
        }

        public string GetStateList(int countryID)
        {
            return BindState(countryID);
        }
        private string BindState(int countryID)
        {
            CountryHelper countryhelper = new CountryHelper();
            DataSet dsStates = countryhelper.getState(countryID);
            string xmlStates = dsStates.GetXml();
            return xmlStates;
        }

        public string GetCityList(int stateID)
        {
            return BindCity(stateID);
        }
        private string BindCity(int stateID)
        {
            CountryHelper countryhelper = new CountryHelper();
            DataSet dsCities = countryhelper.getCity(stateID);
            string xmlCities = dsCities.GetXml();
            return xmlCities;
        }

        public string GetGroupCompaniesList()
        {
            return BindGroupCompaniesList();
        }
        private string BindGroupCompaniesList()
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsGroupCompanies = OrganizationHelper.getGroupCompanyList();
            string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
            return xmlGroupCompaniesList;
        }
    }
}
