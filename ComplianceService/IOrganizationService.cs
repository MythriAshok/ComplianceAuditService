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
        bool insertOrganization(Organization org, CompanyDetails company, BranchLocation branch);
        [OperationContract]
        bool updateOrganization(Organization org, CompanyDetails company, BranchLocation branch);
        [OperationContract]
        string getGroupCompany(int orgID);

        [OperationContract]
        bool insertCompany(Organization org, CompanyDetails company, BranchLocation branch);
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

        string getGroupCompanyListDropDown();

        [OperationContract]

        string getCompanyListDropDown(int groupcompanyID);
    }
}

