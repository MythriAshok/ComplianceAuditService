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
    /// <summary>
    /// Service layer of Organization
    /// </summary>
    public class OrganizationService : IOrganizationService
    {
        /// <summary>
        /// A method in the service layer that interacts with organization helper class to insert the Organization details into the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="company">data object of CompanyDetails</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
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
                if (BranchLocationID > 0 && OrganizationID > 0 && CompanyDetailsID > 0)
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
        /// <summary>
        /// A method to in the sevice layer that interacts with Organization helper class to update the Organization details in the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="company">data object of CompanyDetails</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
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

        public bool DeactivateGroupCompany(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.DeactivateGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool ActivateGroupCompany(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.ActivateGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool DeleteGroupCompany(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.DeleteGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }



        /// <summary>
        /// A public method in the service layer that interacts with the Organization helper class to fetch the data of groupcompany from the database
        /// </summary>
        /// <param name="OrgID">fetches the data of Organization w.r.t specofic OrganizationID</param>
        /// <returns>xml string</returns>
        public string getGroupCompany(int OrgID)
        {
            return bindGroupCompany(OrgID);
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the Organization data from the database
        /// </summary>
        /// <param name="orgID">binds the data of Organization w.r.t specific OrganizationID</param>
        /// <returns>xmlstring</returns>
        private string bindGroupCompany(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsOrganization = orgHelper.getOrganizationHier(orgID);
            string xmlOrganization = dsOrganization.GetXml();
            return xmlOrganization;
        }
        /// <summary>
        /// A method in the service layer that interacts with organization helper class to insert the Company details into the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="company">data object of CompanyDetails</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
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
        /// <summary>
        /// A method to in the sevice layer that interacts with Organization helper class to update the Company details in the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="company">data object of CompanyDetails</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
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
        /// <summary>
        /// A public method in the service layer that interacts with the Organization helper class to fetch the data of groupcompany from the database
        /// </summary>
        /// <param name="OrgID">fetches the data of Organization w.r.t specofic OrganizationID</param>
        /// <returns>xml string</returns>
        public string getCompany(int OrgID)
        {
            return bindCompany(OrgID);
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the Organization data from the database
        /// </summary>
        /// <param name="orgID">binds the data of Organization w.r.t specific OrganizationID</param>
        /// <returns>xmlstring</returns>
        private string bindCompany(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsCompany = orgHelper.getOrganizationHier(orgID);
            string xmlCompany = dsCompany.GetXml();
            return xmlCompany;
        }

        public bool DeactivateCompany(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.DeactivateGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool ActivateCompany(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.ActivateGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool DeleteCompany(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.DeleteGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }



        /// <summary>
        /// A method in the service layer that interacts with organization helper class to insert the Branch details into the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
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
        /// <summary>
        /// A method to in the sevice layer that interacts with Organization helper class to update the Branch details in the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
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
        /// <summary>
        /// A public method in the service layer that interacts with the Organization helper class to fetch the data of groupcompany from the database
        /// </summary>
        /// <param name="OrgID">fetches the data of Organization w.r.t specofic OrganizationID</param>
        /// <returns>xml string</returns>
        public string getBranch(int OrgID)
        {
            return bindBranch(OrgID);
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the Organization data from the database
        /// </summary>
        /// <param name="orgID">binds the data of Organization w.r.t specific OrganizationID</param>
        /// <returns>xmlstring</returns>
        private string bindBranch(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsBranch = orgHelper.getBranch(orgID);
            string xmlBranch = dsBranch.GetXml();
            return xmlBranch;
        }

        public bool DeactivateBranch(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.DeactivateGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool ActivateBranch(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.ActivateGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool DeleteBranch(int OrgID)
        {
            bool status = false;
            OrganizationHelper organizationHelper = new OrganizationHelper();
            string result = organizationHelper.DeleteGroupCompany(OrgID).ToString();
            if (result != null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }





        /// <summary>
        /// A method in the service layer  that interacts with bindCountry method,to fetch the Country list from the database.
        /// </summary>
        /// <returns>BindCountry method which has xml string value</returns>
        public string GetCountryList()
        {
            return BindCountry();
        }
        /// <summary>
        /// A private method in the service layer that interacts with Country helper class to bind the Countries in the database
        /// </summary>
        /// <returns>xml string</returns>
        private string BindCountry()
        {
            CountryHelper helper = new CountryHelper();
            DataSet dsCountries = helper.getCountry();
            string xmlCountries = dsCountries.GetXml();
            return xmlCountries;
        }
        /// <summary>
        /// A method in the service layer that interacts with the private method, BindState.
        /// </summary>
        /// <param name="countryID">Gets the list of states w.r.t specific CountryID</param>
        /// <returns>xml string</returns>
        public string GetStateList(int countryID)
        {
            return BindState(countryID);
        }
        /// <summary>
        /// A private method in the service layer that interacts with Country helper class to bind the States from the database
        /// </summary>
        /// <param name="countryID">Gets the list of states w.r.t specific CountryID</param>
        /// <returns>xml string</returns>
        private string BindState(int countryID)
        {
            CountryHelper countryhelper = new CountryHelper();
            DataSet dsStates = countryhelper.getState(countryID);
            string xmlStates = dsStates.GetXml();
            return xmlStates;
        }
        /// <summary>
        /// A method in the service layer that interacts with the private method, BindCity.
        /// </summary>
        /// <param name="stateID">Gets the list of states w.r.t specific StateID</param>
        /// <returns>xml string</returns>
        public string GetCityList(int stateID)
        {
            return BindCity(stateID);
        }
        /// <summary>
        /// A private method in the service layer that interacts with Country helper class to bind the Cities from the database
        /// </summary>
        /// <param name="stateID">Gets the list of cities w.r.t specific StateID</param>
        /// <returns>xml string</returns>
        private string BindCity(int stateID)
        {
            CountryHelper countryhelper = new CountryHelper();
            DataSet dsCities = countryhelper.getCity(stateID);
            string xmlCities = dsCities.GetXml();
            return xmlCities;
        }
        /// <summary>
        /// A method in the service layer that interacts with private method BindGroupCompanies to fetch the list of group companies present in the database
        /// </summary>
        /// <returns>xml string</returns>
        public string GetGroupCompaniesList()
        {
            return BindGroupCompaniesList();
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the list of groupcompanies present in the database
        /// </summary>
        /// <returns></returns>
        private string BindGroupCompaniesList()
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsGroupCompanies = OrganizationHelper.getGroupCompanyList();
            string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
            return xmlGroupCompaniesList;
        }



        public string GetCompaniesList()
        {
            return BindCompaniesList();
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the list of groupcompanies present in the database
        /// </summary>
        /// <returns></returns>
        private string BindCompaniesList()
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsCompanies = OrganizationHelper.getCompanyList();
            string xmlCompaniesList = dsCompanies.GetXml();
            return xmlCompaniesList;
        }


        public string GetBranchList()
        {
            return BindBranchList();
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the list of groupcompanies present in the database
        /// </summary>
        /// <returns></returns>
        private string BindBranchList()
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsBranches = OrganizationHelper.getBranchList();
            string xmlBranchList = dsBranches.GetXml();
            return xmlBranchList;
        }
    }
}





//        public string getGroupCompanyListDropDown()
//        {
//            return BindGroupCompaniesListDropDown();
//        }
//        /// <summary>
//        /// A private method in the service layer that interacts with Organization helper class to bind the list of groupcompanies present in the database
//        /// </summary>
//        /// <returns></returns>
//        private string BindGroupCompaniesListDropDown()
//        {
//            OrganizationHelper OrganizationHelper = new OrganizationHelper();
//            DataSet dsGroupCompanies = OrganizationHelper.getGroupCompanyListDropDown();
//            string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
//            return xmlGroupCompaniesList;
//        }

//public string getCompanyListDropDown(int groupcompanyID)
//{
//    return BindCompaniesListDropDown(groupcompanyID);
//}
///// <summary>
///// A private method in the service layer that interacts with Organization helper class to bind the list of groupcompanies present in the database
///// </summary>
///// <returns></returns>
//private string BindCompaniesListDropDown(int groupcompanyID)
//{
//    OrganizationHelper OrganizationHelper = new OrganizationHelper();
//    DataSet dsGroupCompanies = OrganizationHelper.getCompanyListDropDown(groupcompanyID);
//    string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
//    return xmlGroupCompaniesList;
//}



//    }
//}
