using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrganizationService" in both code and config file together.
    [ServiceContract]
    public interface IOrganizationService
    {
        [OperationContract]
        int insertOrganization(Organization org);
        [OperationContract]
        bool updateOrganization(Organization org);
        [OperationContract]
        string getGroupCompany(int orgID);

        [OperationContract]
        int insertCompany(Organization org, CompanyDetails company, BranchLocation branch);
        [OperationContract]
        bool updateCompany(Organization org, CompanyDetails company, BranchLocation branch);

        

        [OperationContract]
        int insertBranch(Organization org,  BranchLocation branch);
        [OperationContract]
        bool updateBranch(Organization org, BranchLocation branch);

       
        [OperationContract]
        string GetCountryList();
        [OperationContract]
        string GetStateList(int countryID);
        [OperationContract]
        string GetCityList(int stateID);

     
       
        [OperationContract]
        string GetGroupCompaniesList();

        [OperationContract]
        string GetCompaniesList();

        [OperationContract]
        string GetBranchList();

        [OperationContract]
        string GeSpecifictCompaniesList(int OrgID);
        [OperationContract]

        string getGroupCompanyListDropDown();

        [OperationContract]
        string getGroupOrganization(int OrgID);
        [OperationContract]

        string getCompanyListDropDown(int groupcompanyID);
        [OperationContract]
        string getCompanyListsforBranch(int OrgID);


        [OperationContract]
        bool DeactivateGroupCompany(int OrgID);
        [OperationContract]
        bool DeleteGroupCompany(int OrgID);
        [OperationContract]
        bool ActivateGroupCompany(int OrgID);

        [OperationContract]


        bool DeactivateCompany(int OrgID);
        [OperationContract]
        bool DeleteCompany(int OrgID);
        [OperationContract]
        bool ActivateCompany(int OrgID);

        [OperationContract]


        bool DeactivateBranch(int OrgID);
        [OperationContract]
        bool DeleteBranch(int OrgID);
        [OperationContract]
        bool ActivateBranch(int OrgID);
        [OperationContract]

        string getBranch(int OrgID);
        [OperationContract]

        string GeSpecifictBranchList(int CompanyID);
        [OperationContract]

        string GetVendors(int CompanyID);
        [OperationContract]


        string GeSpecifictVendorList(int BranchID);
        [OperationContract]

        string GetAllVendors(int VendorID);
        [OperationContract]

        string GetAllVendorsAssignedForBranch(int BranchVendorID);
        [OperationContract]

        int insertVendor(Organization org, CompanyDetails company);
        [OperationContract]

        bool updateVendor(Organization org, CompanyDetails company);
        [OperationContract]
        string getVendor(int OrgID);
        [OperationContract]

        string getDefaultCompanyDetails(int CompID);

        [OperationContract]
        string getorglocation(int OrgID);
        [OperationContract]

        string getParticularGroupCompaniesList(int OrgID);
        [OperationContract]

        string getSpecificBranchListDropDown(int CompanyID);
        [OperationContract]

        string getSpecificVendorListDropDown(int pid, int branchid);
        [OperationContract]

       string GetComplianceType(int IndusrtyTypeID, int CountryID);
        [OperationContract]

        string GetIndustryType();
        [OperationContract]

        int insertcomplianceTypes(int[] ComplianceTypeID, int OrgID);
        [OperationContract]

        int updatecomplianceTypes(int[] ComplianceTypeID, int OrgID);
        [OperationContract]

        string GetAssignedComplianceTypes(int CompID);
        [OperationContract]

        string getDefaultIndustryType(int CompID);
        [OperationContract]

        bool DeleteCompliance(int CompID);
        [OperationContract]

        int insertComplianceTypesMappedWithIndustryType(ComplianceType CompType);
        [OperationContract]

        int updateComplianceTypesMappedWithIndustryType(ComplianceType CompType);
        [OperationContract]

        string GetMappedCompliance(int ComplianceID);


    }
}

