﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplianceAuditWeb.OrgService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrgService.IOrganizationService")]
    public interface IOrganizationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertOrganization", ReplyAction="http://tempuri.org/IOrganizationService/insertOrganizationResponse")]
        int insertOrganization(Compliance.DataObject.Organization org);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertOrganization", ReplyAction="http://tempuri.org/IOrganizationService/insertOrganizationResponse")]
        System.Threading.Tasks.Task<int> insertOrganizationAsync(Compliance.DataObject.Organization org);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateOrganization", ReplyAction="http://tempuri.org/IOrganizationService/updateOrganizationResponse")]
        bool updateOrganization(Compliance.DataObject.Organization org);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateOrganization", ReplyAction="http://tempuri.org/IOrganizationService/updateOrganizationResponse")]
        System.Threading.Tasks.Task<bool> updateOrganizationAsync(Compliance.DataObject.Organization org);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/getGroupCompanyResponse")]
        string getGroupCompany(int orgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/getGroupCompanyResponse")]
        System.Threading.Tasks.Task<string> getGroupCompanyAsync(int orgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertCompany", ReplyAction="http://tempuri.org/IOrganizationService/insertCompanyResponse")]
        int insertCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertCompany", ReplyAction="http://tempuri.org/IOrganizationService/insertCompanyResponse")]
        System.Threading.Tasks.Task<int> insertCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateCompany", ReplyAction="http://tempuri.org/IOrganizationService/updateCompanyResponse")]
        bool updateCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateCompany", ReplyAction="http://tempuri.org/IOrganizationService/updateCompanyResponse")]
        System.Threading.Tasks.Task<bool> updateCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertBranch", ReplyAction="http://tempuri.org/IOrganizationService/insertBranchResponse")]
        int insertBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertBranch", ReplyAction="http://tempuri.org/IOrganizationService/insertBranchResponse")]
        System.Threading.Tasks.Task<int> insertBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateBranch", ReplyAction="http://tempuri.org/IOrganizationService/updateBranchResponse")]
        bool updateBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateBranch", ReplyAction="http://tempuri.org/IOrganizationService/updateBranchResponse")]
        System.Threading.Tasks.Task<bool> updateBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetCountryList", ReplyAction="http://tempuri.org/IOrganizationService/GetCountryListResponse")]
        string GetCountryList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetCountryList", ReplyAction="http://tempuri.org/IOrganizationService/GetCountryListResponse")]
        System.Threading.Tasks.Task<string> GetCountryListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetStateList", ReplyAction="http://tempuri.org/IOrganizationService/GetStateListResponse")]
        string GetStateList(int countryID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetStateList", ReplyAction="http://tempuri.org/IOrganizationService/GetStateListResponse")]
        System.Threading.Tasks.Task<string> GetStateListAsync(int countryID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetCityList", ReplyAction="http://tempuri.org/IOrganizationService/GetCityListResponse")]
        string GetCityList(int stateID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetCityList", ReplyAction="http://tempuri.org/IOrganizationService/GetCityListResponse")]
        System.Threading.Tasks.Task<string> GetCityListAsync(int stateID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetGroupCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/GetGroupCompaniesListResponse")]
        string GetGroupCompaniesList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetGroupCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/GetGroupCompaniesListResponse")]
        System.Threading.Tasks.Task<string> GetGroupCompaniesListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/GetCompaniesListResponse")]
        string GetCompaniesList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/GetCompaniesListResponse")]
        System.Threading.Tasks.Task<string> GetCompaniesListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetBranchList", ReplyAction="http://tempuri.org/IOrganizationService/GetBranchListResponse")]
        string GetBranchList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetBranchList", ReplyAction="http://tempuri.org/IOrganizationService/GetBranchListResponse")]
        System.Threading.Tasks.Task<string> GetBranchListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GeSpecifictCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/GeSpecifictCompaniesListResponse")]
        string GeSpecifictCompaniesList(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GeSpecifictCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/GeSpecifictCompaniesListResponse")]
        System.Threading.Tasks.Task<string> GeSpecifictCompaniesListAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupCompanyListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getGroupCompanyListDropDownResponse")]
        string getGroupCompanyListDropDown();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupCompanyListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getGroupCompanyListDropDownResponse")]
        System.Threading.Tasks.Task<string> getGroupCompanyListDropDownAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupOrganization", ReplyAction="http://tempuri.org/IOrganizationService/getGroupOrganizationResponse")]
        string getGroupOrganization(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupOrganization", ReplyAction="http://tempuri.org/IOrganizationService/getGroupOrganizationResponse")]
        System.Threading.Tasks.Task<string> getGroupOrganizationAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getCompanyListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getCompanyListDropDownResponse")]
        string getCompanyListDropDown(int groupcompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getCompanyListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getCompanyListDropDownResponse")]
        System.Threading.Tasks.Task<string> getCompanyListDropDownAsync(int groupcompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getCompanyListsforBranch", ReplyAction="http://tempuri.org/IOrganizationService/getCompanyListsforBranchResponse")]
        string getCompanyListsforBranch(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getCompanyListsforBranch", ReplyAction="http://tempuri.org/IOrganizationService/getCompanyListsforBranchResponse")]
        System.Threading.Tasks.Task<string> getCompanyListsforBranchAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeactivateGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeactivateGroupCompanyResponse")]
        bool DeactivateGroupCompany(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeactivateGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeactivateGroupCompanyResponse")]
        System.Threading.Tasks.Task<bool> DeactivateGroupCompanyAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeleteGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeleteGroupCompanyResponse")]
        bool DeleteGroupCompany(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeleteGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeleteGroupCompanyResponse")]
        System.Threading.Tasks.Task<bool> DeleteGroupCompanyAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/ActivateGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/ActivateGroupCompanyResponse")]
        bool ActivateGroupCompany(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/ActivateGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/ActivateGroupCompanyResponse")]
        System.Threading.Tasks.Task<bool> ActivateGroupCompanyAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeactivateCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeactivateCompanyResponse")]
        bool DeactivateCompany(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeactivateCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeactivateCompanyResponse")]
        System.Threading.Tasks.Task<bool> DeactivateCompanyAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeleteCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeleteCompanyResponse")]
        bool DeleteCompany(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeleteCompany", ReplyAction="http://tempuri.org/IOrganizationService/DeleteCompanyResponse")]
        System.Threading.Tasks.Task<bool> DeleteCompanyAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/ActivateCompany", ReplyAction="http://tempuri.org/IOrganizationService/ActivateCompanyResponse")]
        bool ActivateCompany(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/ActivateCompany", ReplyAction="http://tempuri.org/IOrganizationService/ActivateCompanyResponse")]
        System.Threading.Tasks.Task<bool> ActivateCompanyAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeactivateBranch", ReplyAction="http://tempuri.org/IOrganizationService/DeactivateBranchResponse")]
        bool DeactivateBranch(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeactivateBranch", ReplyAction="http://tempuri.org/IOrganizationService/DeactivateBranchResponse")]
        System.Threading.Tasks.Task<bool> DeactivateBranchAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeleteBranch", ReplyAction="http://tempuri.org/IOrganizationService/DeleteBranchResponse")]
        bool DeleteBranch(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/DeleteBranch", ReplyAction="http://tempuri.org/IOrganizationService/DeleteBranchResponse")]
        System.Threading.Tasks.Task<bool> DeleteBranchAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/ActivateBranch", ReplyAction="http://tempuri.org/IOrganizationService/ActivateBranchResponse")]
        bool ActivateBranch(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/ActivateBranch", ReplyAction="http://tempuri.org/IOrganizationService/ActivateBranchResponse")]
        System.Threading.Tasks.Task<bool> ActivateBranchAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getBranch", ReplyAction="http://tempuri.org/IOrganizationService/getBranchResponse")]
        string getBranch(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getBranch", ReplyAction="http://tempuri.org/IOrganizationService/getBranchResponse")]
        System.Threading.Tasks.Task<string> getBranchAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GeSpecifictBranchList", ReplyAction="http://tempuri.org/IOrganizationService/GeSpecifictBranchListResponse")]
        string GeSpecifictBranchList(int CompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GeSpecifictBranchList", ReplyAction="http://tempuri.org/IOrganizationService/GeSpecifictBranchListResponse")]
        System.Threading.Tasks.Task<string> GeSpecifictBranchListAsync(int CompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetVendors", ReplyAction="http://tempuri.org/IOrganizationService/GetVendorsResponse")]
        string GetVendors(int CompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetVendors", ReplyAction="http://tempuri.org/IOrganizationService/GetVendorsResponse")]
        System.Threading.Tasks.Task<string> GetVendorsAsync(int CompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GeSpecifictVendorList", ReplyAction="http://tempuri.org/IOrganizationService/GeSpecifictVendorListResponse")]
        string GeSpecifictVendorList(int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GeSpecifictVendorList", ReplyAction="http://tempuri.org/IOrganizationService/GeSpecifictVendorListResponse")]
        System.Threading.Tasks.Task<string> GeSpecifictVendorListAsync(int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetAllVendors", ReplyAction="http://tempuri.org/IOrganizationService/GetAllVendorsResponse")]
        string GetAllVendors(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetAllVendors", ReplyAction="http://tempuri.org/IOrganizationService/GetAllVendorsResponse")]
        System.Threading.Tasks.Task<string> GetAllVendorsAsync(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetAllVendorsAssignedForBranch", ReplyAction="http://tempuri.org/IOrganizationService/GetAllVendorsAssignedForBranchResponse")]
        string GetAllVendorsAssignedForBranch(int BranchVendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/GetAllVendorsAssignedForBranch", ReplyAction="http://tempuri.org/IOrganizationService/GetAllVendorsAssignedForBranchResponse")]
        System.Threading.Tasks.Task<string> GetAllVendorsAssignedForBranchAsync(int BranchVendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertVendor", ReplyAction="http://tempuri.org/IOrganizationService/insertVendorResponse")]
        int insertVendor(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertVendor", ReplyAction="http://tempuri.org/IOrganizationService/insertVendorResponse")]
        System.Threading.Tasks.Task<int> insertVendorAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateVendor", ReplyAction="http://tempuri.org/IOrganizationService/updateVendorResponse")]
        bool updateVendor(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateVendor", ReplyAction="http://tempuri.org/IOrganizationService/updateVendorResponse")]
        System.Threading.Tasks.Task<bool> updateVendorAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getVendor", ReplyAction="http://tempuri.org/IOrganizationService/getVendorResponse")]
        string getVendor(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getVendor", ReplyAction="http://tempuri.org/IOrganizationService/getVendorResponse")]
        System.Threading.Tasks.Task<string> getVendorAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getDefaultCompanyDetails", ReplyAction="http://tempuri.org/IOrganizationService/getDefaultCompanyDetailsResponse")]
        string getDefaultCompanyDetails(int CompID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getDefaultCompanyDetails", ReplyAction="http://tempuri.org/IOrganizationService/getDefaultCompanyDetailsResponse")]
        System.Threading.Tasks.Task<string> getDefaultCompanyDetailsAsync(int CompID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getorglocation", ReplyAction="http://tempuri.org/IOrganizationService/getorglocationResponse")]
        string getorglocation(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getorglocation", ReplyAction="http://tempuri.org/IOrganizationService/getorglocationResponse")]
        System.Threading.Tasks.Task<string> getorglocationAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getParticularGroupCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/getParticularGroupCompaniesListResponse")]
        string getParticularGroupCompaniesList(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getParticularGroupCompaniesList", ReplyAction="http://tempuri.org/IOrganizationService/getParticularGroupCompaniesListResponse")]
        System.Threading.Tasks.Task<string> getParticularGroupCompaniesListAsync(int OrgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getSpecificBranchListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getSpecificBranchListDropDownResponse")]
        string getSpecificBranchListDropDown(int CompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getSpecificBranchListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getSpecificBranchListDropDownResponse")]
        System.Threading.Tasks.Task<string> getSpecificBranchListDropDownAsync(int CompanyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getSpecificVendorListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getSpecificVendorListDropDownResponse")]
        string getSpecificVendorListDropDown(int pid, int branchid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getSpecificVendorListDropDown", ReplyAction="http://tempuri.org/IOrganizationService/getSpecificVendorListDropDownResponse")]
        System.Threading.Tasks.Task<string> getSpecificVendorListDropDownAsync(int pid, int branchid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrganizationServiceChannel : ComplianceAuditWeb.OrgService.IOrganizationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrganizationServiceClient : System.ServiceModel.ClientBase<ComplianceAuditWeb.OrgService.IOrganizationService>, ComplianceAuditWeb.OrgService.IOrganizationService {
        
        public OrganizationServiceClient() {
        }
        
        public OrganizationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrganizationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrganizationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrganizationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int insertOrganization(Compliance.DataObject.Organization org) {
            return base.Channel.insertOrganization(org);
        }
        
        public System.Threading.Tasks.Task<int> insertOrganizationAsync(Compliance.DataObject.Organization org) {
            return base.Channel.insertOrganizationAsync(org);
        }
        
        public bool updateOrganization(Compliance.DataObject.Organization org) {
            return base.Channel.updateOrganization(org);
        }
        
        public System.Threading.Tasks.Task<bool> updateOrganizationAsync(Compliance.DataObject.Organization org) {
            return base.Channel.updateOrganizationAsync(org);
        }
        
        public string getGroupCompany(int orgID) {
            return base.Channel.getGroupCompany(orgID);
        }
        
        public System.Threading.Tasks.Task<string> getGroupCompanyAsync(int orgID) {
            return base.Channel.getGroupCompanyAsync(orgID);
        }
        
        public int insertCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertCompany(org, company, branch);
        }
        
        public System.Threading.Tasks.Task<int> insertCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertCompanyAsync(org, company, branch);
        }
        
        public bool updateCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateCompany(org, company, branch);
        }
        
        public System.Threading.Tasks.Task<bool> updateCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateCompanyAsync(org, company, branch);
        }
        
        public int insertBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertBranch(org, branch);
        }
        
        public System.Threading.Tasks.Task<int> insertBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertBranchAsync(org, branch);
        }
        
        public bool updateBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateBranch(org, branch);
        }
        
        public System.Threading.Tasks.Task<bool> updateBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateBranchAsync(org, branch);
        }
        
        public string GetCountryList() {
            return base.Channel.GetCountryList();
        }
        
        public System.Threading.Tasks.Task<string> GetCountryListAsync() {
            return base.Channel.GetCountryListAsync();
        }
        
        public string GetStateList(int countryID) {
            return base.Channel.GetStateList(countryID);
        }
        
        public System.Threading.Tasks.Task<string> GetStateListAsync(int countryID) {
            return base.Channel.GetStateListAsync(countryID);
        }
        
        public string GetCityList(int stateID) {
            return base.Channel.GetCityList(stateID);
        }
        
        public System.Threading.Tasks.Task<string> GetCityListAsync(int stateID) {
            return base.Channel.GetCityListAsync(stateID);
        }
        
        public string GetGroupCompaniesList() {
            return base.Channel.GetGroupCompaniesList();
        }
        
        public System.Threading.Tasks.Task<string> GetGroupCompaniesListAsync() {
            return base.Channel.GetGroupCompaniesListAsync();
        }
        
        public string GetCompaniesList() {
            return base.Channel.GetCompaniesList();
        }
        
        public System.Threading.Tasks.Task<string> GetCompaniesListAsync() {
            return base.Channel.GetCompaniesListAsync();
        }
        
        public string GetBranchList() {
            return base.Channel.GetBranchList();
        }
        
        public System.Threading.Tasks.Task<string> GetBranchListAsync() {
            return base.Channel.GetBranchListAsync();
        }
        
        public string GeSpecifictCompaniesList(int OrgID) {
            return base.Channel.GeSpecifictCompaniesList(OrgID);
        }
        
        public System.Threading.Tasks.Task<string> GeSpecifictCompaniesListAsync(int OrgID) {
            return base.Channel.GeSpecifictCompaniesListAsync(OrgID);
        }
        
        public string getGroupCompanyListDropDown() {
            return base.Channel.getGroupCompanyListDropDown();
        }
        
        public System.Threading.Tasks.Task<string> getGroupCompanyListDropDownAsync() {
            return base.Channel.getGroupCompanyListDropDownAsync();
        }
        
        public string getGroupOrganization(int OrgID) {
            return base.Channel.getGroupOrganization(OrgID);
        }
        
        public System.Threading.Tasks.Task<string> getGroupOrganizationAsync(int OrgID) {
            return base.Channel.getGroupOrganizationAsync(OrgID);
        }
        
        public string getCompanyListDropDown(int groupcompanyID) {
            return base.Channel.getCompanyListDropDown(groupcompanyID);
        }
        
        public System.Threading.Tasks.Task<string> getCompanyListDropDownAsync(int groupcompanyID) {
            return base.Channel.getCompanyListDropDownAsync(groupcompanyID);
        }
        
        public string getCompanyListsforBranch(int OrgID) {
            return base.Channel.getCompanyListsforBranch(OrgID);
        }
        
        public System.Threading.Tasks.Task<string> getCompanyListsforBranchAsync(int OrgID) {
            return base.Channel.getCompanyListsforBranchAsync(OrgID);
        }
        
        public bool DeactivateGroupCompany(int OrgID) {
            return base.Channel.DeactivateGroupCompany(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> DeactivateGroupCompanyAsync(int OrgID) {
            return base.Channel.DeactivateGroupCompanyAsync(OrgID);
        }
        
        public bool DeleteGroupCompany(int OrgID) {
            return base.Channel.DeleteGroupCompany(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteGroupCompanyAsync(int OrgID) {
            return base.Channel.DeleteGroupCompanyAsync(OrgID);
        }
        
        public bool ActivateGroupCompany(int OrgID) {
            return base.Channel.ActivateGroupCompany(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> ActivateGroupCompanyAsync(int OrgID) {
            return base.Channel.ActivateGroupCompanyAsync(OrgID);
        }
        
        public bool DeactivateCompany(int OrgID) {
            return base.Channel.DeactivateCompany(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> DeactivateCompanyAsync(int OrgID) {
            return base.Channel.DeactivateCompanyAsync(OrgID);
        }
        
        public bool DeleteCompany(int OrgID) {
            return base.Channel.DeleteCompany(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteCompanyAsync(int OrgID) {
            return base.Channel.DeleteCompanyAsync(OrgID);
        }
        
        public bool ActivateCompany(int OrgID) {
            return base.Channel.ActivateCompany(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> ActivateCompanyAsync(int OrgID) {
            return base.Channel.ActivateCompanyAsync(OrgID);
        }
        
        public bool DeactivateBranch(int OrgID) {
            return base.Channel.DeactivateBranch(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> DeactivateBranchAsync(int OrgID) {
            return base.Channel.DeactivateBranchAsync(OrgID);
        }
        
        public bool DeleteBranch(int OrgID) {
            return base.Channel.DeleteBranch(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteBranchAsync(int OrgID) {
            return base.Channel.DeleteBranchAsync(OrgID);
        }
        
        public bool ActivateBranch(int OrgID) {
            return base.Channel.ActivateBranch(OrgID);
        }
        
        public System.Threading.Tasks.Task<bool> ActivateBranchAsync(int OrgID) {
            return base.Channel.ActivateBranchAsync(OrgID);
        }
        
        public string getBranch(int OrgID) {
            return base.Channel.getBranch(OrgID);
        }
        
        public System.Threading.Tasks.Task<string> getBranchAsync(int OrgID) {
            return base.Channel.getBranchAsync(OrgID);
        }
        
        public string GeSpecifictBranchList(int CompanyID) {
            return base.Channel.GeSpecifictBranchList(CompanyID);
        }
        
        public System.Threading.Tasks.Task<string> GeSpecifictBranchListAsync(int CompanyID) {
            return base.Channel.GeSpecifictBranchListAsync(CompanyID);
        }
        
        public string GetVendors(int CompanyID) {
            return base.Channel.GetVendors(CompanyID);
        }
        
        public System.Threading.Tasks.Task<string> GetVendorsAsync(int CompanyID) {
            return base.Channel.GetVendorsAsync(CompanyID);
        }
        
        public string GeSpecifictVendorList(int BranchID) {
            return base.Channel.GeSpecifictVendorList(BranchID);
        }
        
        public System.Threading.Tasks.Task<string> GeSpecifictVendorListAsync(int BranchID) {
            return base.Channel.GeSpecifictVendorListAsync(BranchID);
        }
        
        public string GetAllVendors(int VendorID) {
            return base.Channel.GetAllVendors(VendorID);
        }
        
        public System.Threading.Tasks.Task<string> GetAllVendorsAsync(int VendorID) {
            return base.Channel.GetAllVendorsAsync(VendorID);
        }
        
        public string GetAllVendorsAssignedForBranch(int BranchVendorID) {
            return base.Channel.GetAllVendorsAssignedForBranch(BranchVendorID);
        }
        
        public System.Threading.Tasks.Task<string> GetAllVendorsAssignedForBranchAsync(int BranchVendorID) {
            return base.Channel.GetAllVendorsAssignedForBranchAsync(BranchVendorID);
        }
        
        public int insertVendor(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company) {
            return base.Channel.insertVendor(org, company);
        }
        
        public System.Threading.Tasks.Task<int> insertVendorAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company) {
            return base.Channel.insertVendorAsync(org, company);
        }
        
        public bool updateVendor(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company) {
            return base.Channel.updateVendor(org, company);
        }
        
        public System.Threading.Tasks.Task<bool> updateVendorAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company) {
            return base.Channel.updateVendorAsync(org, company);
        }
        
        public string getVendor(int OrgID) {
            return base.Channel.getVendor(OrgID);
        }
        
        public System.Threading.Tasks.Task<string> getVendorAsync(int OrgID) {
            return base.Channel.getVendorAsync(OrgID);
        }
        
        public string getDefaultCompanyDetails(int CompID) {
            return base.Channel.getDefaultCompanyDetails(CompID);
        }
        
        public System.Threading.Tasks.Task<string> getDefaultCompanyDetailsAsync(int CompID) {
            return base.Channel.getDefaultCompanyDetailsAsync(CompID);
        }
        
        public string getorglocation(int OrgID) {
            return base.Channel.getorglocation(OrgID);
        }
        
        public System.Threading.Tasks.Task<string> getorglocationAsync(int OrgID) {
            return base.Channel.getorglocationAsync(OrgID);
        }
        
        public string getParticularGroupCompaniesList(int OrgID) {
            return base.Channel.getParticularGroupCompaniesList(OrgID);
        }
        
        public System.Threading.Tasks.Task<string> getParticularGroupCompaniesListAsync(int OrgID) {
            return base.Channel.getParticularGroupCompaniesListAsync(OrgID);
        }
        
        public string getSpecificBranchListDropDown(int CompanyID) {
            return base.Channel.getSpecificBranchListDropDown(CompanyID);
        }
        
        public System.Threading.Tasks.Task<string> getSpecificBranchListDropDownAsync(int CompanyID) {
            return base.Channel.getSpecificBranchListDropDownAsync(CompanyID);
        }
        
        public string getSpecificVendorListDropDown(int pid, int branchid) {
            return base.Channel.getSpecificVendorListDropDown(pid, branchid);
        }
        
        public System.Threading.Tasks.Task<string> getSpecificVendorListDropDownAsync(int pid, int branchid) {
            return base.Channel.getSpecificVendorListDropDownAsync(pid, branchid);
        }
    }
}
