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
        public int insertOrganization(Organization org)
        {
            int OrganizationID = 0;
            int BranchLocationID = 0;
            int CompanyDetailsID = 0;
            bool insertResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'I');
                ////if (OrganizationID > 0)
                ////{
                ////    branch.Org_Hier_ID = OrganizationID;
                ////    BranchLocationID = organizationhelper.insertupdateBranchLocation(branch, 'I');
                ////}


                // if (BranchLocationID > 0 && OrganizationID > 0)
                {
                    insertResult = true;
                }
            }
            catch
            {
                throw;
            }
            return OrganizationID;
        }
        /// <summary>
        /// A method to in the sevice layer that interacts with Organization helper class to update the Organization details in the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="company">data object of CompanyDetails</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
        public bool updateOrganization(Organization org)
        {
            int OrganizationID = 0;
            int BranchID = 0;
            // int insertCompanyDetailsID = 0;
            bool updateResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'U');

                //  BranchID = organizationhelper.insertupdateBranchLocation(branch, 'U');
                // insertCompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'U');
                // if (BranchID > 0 && OrganizationID > 0) //insertCompanyDetailsID > 0)
                //{
                updateResult = true;
                // }
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

        private string bindGroupCompany(int orgID)
        {
            // Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsOrganization = orgHelper.getOrganizationHier(orgID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsOrganization = utilityHelper.ConvertNullsToEmptyString(dsOrganization);


            string xmlOrganization = dsOrganization.GetXml();
            return xmlOrganization;
        }

        public string getGroupOrganization(int OrgID)
        {
            return bindGroupOrg(OrgID);
        }

        private string bindGroupOrg(int orgID)
        {
            // Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsOrganization = orgHelper.getOrganizationGroup(orgID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsOrganization = utilityHelper.ConvertNullsToEmptyString(dsOrganization);


            string xmlOrganization = dsOrganization.GetXml();
            return xmlOrganization;
        }












        //public string getGroupCompanyForBranch(int OrgID)
        //{
        //    return bindGroupCompanyForBranch(OrgID);
        //}
        ///// <summary>
        ///// A private method in the service layer that interacts with Organization helper class to bind the Organization data from the database
        ///// </summary>
        ///// <param name="orgID">binds the data of Organization w.r.t specific OrganizationID</param>
        ///// <returns>xmlstring</returns>
        //private string bindGroupCompanyForBranch(int orgID)
        //{
        //    Organization org = new Organization();
        //    OrganizationHelper orgHelper = new OrganizationHelper();
        //    DataSet dsOrganization = orgHelper.getBranch(orgID);
        //    string xmlOrganization = dsOrganization.GetXml();
        //    return xmlOrganization; 
        //}
        /// <summary>
        /// A method in the service layer that interacts with organization helper class to insert the Company details into the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="company">data object of CompanyDetails</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
        public int insertCompany(Organization org, CompanyDetails company, BranchLocation branch)
        {
            int OrganizationID = 0;
            int BranchLocationID = 0;
            int CompanyDetailsID = 0;
            bool insertResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'I');
                if (OrganizationID > 0)
                {
                    branch.Org_Hier_ID = OrganizationID;
                    BranchLocationID = organizationhelper.insertupdateBranchLocation(branch, 'I');



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
            return OrganizationID;
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
                insertOrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'U');

                inserBranchID = organizationhelper.insertupdateBranchLocation(branch, 'U');
                insertCompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'U');
                //if (inserBranchID > 0 || insertOrganizationID > 0 || insertCompanyDetailsID > 0)
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

            UtilityHelper utilityHelper = new UtilityHelper();
            dsCompany = utilityHelper.ConvertNullsToEmptyString(dsCompany);
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
        public int insertBranch(Organization org, BranchLocation branch)
        {
            int OrganizationID = 0;
            int BranchLocationID = 0;
            bool insertResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'I');

                if (OrganizationID > 0)
                {
                    branch.Org_Hier_ID = OrganizationID;
                    BranchLocationID = organizationhelper.insertupdateBranchLocation(branch, 'I');

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
            return OrganizationID;
        }
        /// <summary>
        /// A method to in the sevice layer that interacts with Organization helper class to update the Branch details in the database
        /// </summary>
        /// <param name="org">data object of Organization</param>
        /// <param name="branch">data object of BranchLocation</param>
        /// <returns>boolean value</returns>
        public bool updateBranch(Organization org, BranchLocation branch)
        {
            int OrganizationID = 0;
            int BranchID = 0;
            bool updateResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'U');

                BranchID = organizationhelper.insertupdateBranchLocation(branch, 'U');
                // if (BranchID != 0 && OrganizationID != 0)
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

        private string bindBranch(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsBranch = orgHelper.getBranch(orgID);

            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranch = utilityHelper.ConvertNullsToEmptyString(dsBranch);
            string xmlBranch = dsBranch.GetXml();
            return xmlBranch;
        }
        public string getVendor(int OrgID)
        {
            return bindVendor(OrgID);
        }

        private string bindVendor(int orgID)
        {
            Organization org = new Organization();
            OrganizationHelper orgHelper = new OrganizationHelper();
            DataSet dsBranch = orgHelper.getVendor(orgID);

            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranch = utilityHelper.ConvertNullsToEmptyString(dsBranch);
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


        public int insertVendor(Organization org, CompanyDetails company)
        {
            int OrganizationID = 0;
            int CompanyDetailsID = 0;
            bool insertResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                OrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'I');



                if (OrganizationID > 0)
                {
                    company.Org_Hier_ID = OrganizationID;
                    CompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'I');
                }

                if (OrganizationID > 0 && CompanyDetailsID > 0)

                {
                    insertResult = true;
                }
            }
            catch
            {
                throw;
            }
            return OrganizationID;
        }
    
        public bool updateVendor(Organization org, CompanyDetails company)
        {
            int insertOrganizationID = 0;
            int insertCompanyDetailsID = 0;
            bool updateResult = false;
            try
            {
                OrganizationHelper organizationhelper = new OrganizationHelper();
                insertOrganizationID = organizationhelper.insertupdateOrganizationHier(org, 'U');
                insertCompanyDetailsID = organizationhelper.insertupdateCompanyDetails(company, 'U');
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
        public string GetCountryList()
        {
            return BindCountry();
        }
        private string BindCountry()
        {
            CountryHelper helper = new CountryHelper();
            DataSet dsCountries = helper.getCountry();
            UtilityHelper utilityHelper = new UtilityHelper();
            dsCountries = utilityHelper.ConvertNullsToEmptyString(dsCountries);
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
            UtilityHelper utilityHelper = new UtilityHelper();
            dsStates = utilityHelper.ConvertNullsToEmptyString(dsStates);
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
            UtilityHelper utilityHelper = new UtilityHelper();
            dsCities = utilityHelper.ConvertNullsToEmptyString(dsCities);
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
            UtilityHelper utilityHelper = new UtilityHelper();
            dsGroupCompanies = utilityHelper.ConvertNullsToEmptyString(dsGroupCompanies);
            string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
            return xmlGroupCompaniesList;
        }

        public string GetCompaniesList()
        {
            return BindCompaniesList();
        }
        private string BindCompaniesList()
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsGroupCompanies = OrganizationHelper.getCompanyList();
            UtilityHelper utilityHelper = new UtilityHelper();
            dsGroupCompanies = utilityHelper.ConvertNullsToEmptyString(dsGroupCompanies);
            string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
            return xmlGroupCompaniesList;
        }

        public string getCompanyListsforBranch(int OrgID)
        {
            return bindCompanyListsforBranch(OrgID);
        }
        private string bindCompanyListsforBranch(int OrgID)
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsGroupCompanies = OrganizationHelper.getCompanyListsforBranch(OrgID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsGroupCompanies = utilityHelper.ConvertNullsToEmptyString(dsGroupCompanies);
            string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
            return xmlGroupCompaniesList;
        }




        public string GetBranchList()
        {
            return BindBranchList();
        }

        private string BindBranchList()
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsBranches = OrganizationHelper.getBranchList();
            UtilityHelper utilityHelper = new UtilityHelper();
            dsBranches = utilityHelper.ConvertNullsToEmptyString(dsBranches);
            string xmlBranchList = dsBranches.GetXml();
            return xmlBranchList;
        }







        public string GeSpecifictCompaniesList(int OrgID)
        {
            return BindSpecificCompaniesList(OrgID);
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the list of groupcompanies present in the database
        /// </summary>
        /// <returns></returns>
        private string BindSpecificCompaniesList(int OrgID)
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsCompanies = OrganizationHelper.getSpecificCompanyList(OrgID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsCompanies = utilityHelper.ConvertNullsToEmptyString(dsCompanies);
            string xmlCompaniesList = dsCompanies.GetXml();
            return xmlCompaniesList;
        }

        public string GeSpecifictBranchList(int CompanyID)
        {
            return BindSpecificBranchList(CompanyID);
        }

        private string BindSpecificBranchList(int CompanyID)
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsCompanies = OrganizationHelper.getSpecificBranchList(CompanyID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsCompanies = utilityHelper.ConvertNullsToEmptyString(dsCompanies);
            string xmlCompaniesList = dsCompanies.GetXml();
            return xmlCompaniesList;
        }

        //public string GetSpecificCompaniesList(int GroupCompanyID)
        //{
        //    return BindSpecificCompaniesList(GroupCompanyID);
        //}
        //private string BindSpecificCompaniesList(int groupcompanyID)
        //{
        //    OrganizationHelper OrganizationHelper = new OrganizationHelper();
        //    DataSet dsGroupCompanies = OrganizationHelper.getSpecificCompanyList(groupcompanyID);
        //    string xmlGroupCompaniesList = dsGroupCompanies.GetXml();
        //    return xmlGroupCompaniesList;
        //}






        public string GetVendors(int CompanyID)
        {
            return BindVendor(CompanyID);
        }
        private string BindVendor(int CompanyID)
        {
            VendorHelper vendorhelper = new VendorHelper();
            DataSet vendors = vendorhelper.getVendorList(CompanyID);
            UtilityHelper utilityHelper = new UtilityHelper();
            vendors = utilityHelper.ConvertNullsToEmptyString(vendors);
            string xmlroles = vendors.GetXml();
            return xmlroles;
        }


        public string GeSpecifictVendorList(int BranchID)
        {
            return BindSpecificVendorList(BranchID);
        }

        private string BindSpecificVendorList(int BranchID)
        {
            VendorHelper vendorhelper = new VendorHelper();
            DataSet dsVendors = vendorhelper.getVendorListForBRanch(BranchID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsVendors = utilityHelper.ConvertNullsToEmptyString(dsVendors);
            string xmlVendorsList = dsVendors.GetXml();
            return xmlVendorsList;
        }


        public string GetAllVendors(int VendorID)
        {
            return BindAllVendor(VendorID);
        }
        private string BindAllVendor(int VendorID)
        {
            VendorHelper vendorhelper = new VendorHelper();
            DataSet vendors = vendorhelper.getcompleteVendorList(VendorID);
            UtilityHelper utilityHelper = new UtilityHelper();
            vendors = utilityHelper.ConvertNullsToEmptyString(vendors);
            string xmlvendors = vendors.GetXml();
            return xmlvendors;
        }


        public string GetAllVendorsAssignedForBranch(int BranchVendorID)
        {
            return BindAllVendorAssignedForBranch(BranchVendorID);
        }
        private string BindAllVendorAssignedForBranch(int BranchVendorID)
        {
            VendorHelper vendorhelper = new VendorHelper();
            DataSet vendors = vendorhelper.getcompleteVendorListassignedToBranch(BranchVendorID);
            UtilityHelper utilityHelper = new UtilityHelper();
            vendors = utilityHelper.ConvertNullsToEmptyString(vendors);
            string xmlvendors = vendors.GetXml();
            return xmlvendors;
        }

        public string getDefaultCompanyDetails(int CompID)
        {
            return bindDefaultCompanyDetails(CompID);
        }
        /// <summary>
        /// A private method in the service layer that interacts with Organization helper class to bind the list of groupcompanies present in the database
        /// </summary>
        /// <returns></returns>
        private string bindDefaultCompanyDetails(int CompID)
        {
            OrganizationHelper OrganizationHelper = new OrganizationHelper();
            DataSet dsCompanies = OrganizationHelper.getDefaultCompanyLists(CompID);
            UtilityHelper utilityHelper = new UtilityHelper();
            dsCompanies = utilityHelper.ConvertNullsToEmptyString(dsCompanies);
            string xmlCompaniesList = dsCompanies.GetXml();
            return xmlCompaniesList;
        }


        public string getorglocation(int OrgID)
        {
            return Bindlocation(OrgID);
        }
        private string Bindlocation(int orgid)
        {
            OrganizationHelper helper = new OrganizationHelper();
            DataSet vendors = helper.getBranchLocation(orgid);
            UtilityHelper utilityHelper = new UtilityHelper();
            vendors = utilityHelper.ConvertNullsToEmptyString(vendors);
            string xmlvendors = vendors.GetXml();
            return xmlvendors;
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
