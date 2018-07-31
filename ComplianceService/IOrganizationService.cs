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
        int insertOrganization(Organization org, CompanyDetails company, BranchLocation branch);
        [OperationContract]
        bool updateOrganization(Organization org, CompanyDetails company, BranchLocation branch);
        [OperationContract]
        string getGroupCompany(int orgID);

        [OperationContract]
        int insertCompany(Organization org, CompanyDetails company, BranchLocation branch);
        [OperationContract]
        bool updateCompany(Organization org, CompanyDetails company, BranchLocation branch);

        

        [OperationContract]
        bool insertBranch(Organization org,  BranchLocation branch);
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

        //string getGroupCompanyListDropDown();

        [OperationContract]
        string getGroupOrganization(int OrgID);
        //string getCompanyListDropDown(int groupcompanyID);
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

    }
}

