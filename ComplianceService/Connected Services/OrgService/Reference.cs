﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplianceService.OrgService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrgService.IOrganizationService")]
    public interface IOrganizationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertOrganization", ReplyAction="http://tempuri.org/IOrganizationService/insertOrganizationResponse")]
        bool insertOrganization(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertOrganization", ReplyAction="http://tempuri.org/IOrganizationService/insertOrganizationResponse")]
        System.Threading.Tasks.Task<bool> insertOrganizationAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateOrganization", ReplyAction="http://tempuri.org/IOrganizationService/updateOrganizationResponse")]
        bool updateOrganization(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateOrganization", ReplyAction="http://tempuri.org/IOrganizationService/updateOrganizationResponse")]
        System.Threading.Tasks.Task<bool> updateOrganizationAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/getGroupCompanyResponse")]
        string getGroupCompany(int orgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getGroupCompany", ReplyAction="http://tempuri.org/IOrganizationService/getGroupCompanyResponse")]
        System.Threading.Tasks.Task<string> getGroupCompanyAsync(int orgID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertCompany", ReplyAction="http://tempuri.org/IOrganizationService/insertCompanyResponse")]
        bool insertCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertCompany", ReplyAction="http://tempuri.org/IOrganizationService/insertCompanyResponse")]
        System.Threading.Tasks.Task<bool> insertCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateCompany", ReplyAction="http://tempuri.org/IOrganizationService/updateCompanyResponse")]
        bool updateCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateCompany", ReplyAction="http://tempuri.org/IOrganizationService/updateCompanyResponse")]
        System.Threading.Tasks.Task<bool> updateCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getCompany", ReplyAction="http://tempuri.org/IOrganizationService/getCompanyResponse")]
        int getCompany();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getCompany", ReplyAction="http://tempuri.org/IOrganizationService/getCompanyResponse")]
        System.Threading.Tasks.Task<int> getCompanyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertBranch", ReplyAction="http://tempuri.org/IOrganizationService/insertBranchResponse")]
        bool insertBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/insertBranch", ReplyAction="http://tempuri.org/IOrganizationService/insertBranchResponse")]
        System.Threading.Tasks.Task<bool> insertBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateBranch", ReplyAction="http://tempuri.org/IOrganizationService/updateBranchResponse")]
        bool updateBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/updateBranch", ReplyAction="http://tempuri.org/IOrganizationService/updateBranchResponse")]
        System.Threading.Tasks.Task<bool> updateBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getBranch", ReplyAction="http://tempuri.org/IOrganizationService/getBranchResponse")]
        int getBranch();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrganizationService/getBranch", ReplyAction="http://tempuri.org/IOrganizationService/getBranchResponse")]
        System.Threading.Tasks.Task<int> getBranchAsync();
        
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
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrganizationServiceChannel : ComplianceService.OrgService.IOrganizationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrganizationServiceClient : System.ServiceModel.ClientBase<ComplianceService.OrgService.IOrganizationService>, ComplianceService.OrgService.IOrganizationService {
        
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
        
        public bool insertOrganization(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertOrganization(org, company, branch);
        }
        
        public System.Threading.Tasks.Task<bool> insertOrganizationAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertOrganizationAsync(org, company, branch);
        }
        
        public bool updateOrganization(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateOrganization(org, company, branch);
        }
        
        public System.Threading.Tasks.Task<bool> updateOrganizationAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateOrganizationAsync(org, company, branch);
        }
        
        public string getGroupCompany(int orgID) {
            return base.Channel.getGroupCompany(orgID);
        }
        
        public System.Threading.Tasks.Task<string> getGroupCompanyAsync(int orgID) {
            return base.Channel.getGroupCompanyAsync(orgID);
        }
        
        public bool insertCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertCompany(org, company, branch);
        }
        
        public System.Threading.Tasks.Task<bool> insertCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertCompanyAsync(org, company, branch);
        }
        
        public bool updateCompany(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateCompany(org, company, branch);
        }
        
        public System.Threading.Tasks.Task<bool> updateCompanyAsync(Compliance.DataObject.Organization org, Compliance.DataObject.CompanyDetails company, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateCompanyAsync(org, company, branch);
        }
        
        public int getCompany() {
            return base.Channel.getCompany();
        }
        
        public System.Threading.Tasks.Task<int> getCompanyAsync() {
            return base.Channel.getCompanyAsync();
        }
        
        public bool insertBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertBranch(org, branch);
        }
        
        public System.Threading.Tasks.Task<bool> insertBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.insertBranchAsync(org, branch);
        }
        
        public bool updateBranch(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateBranch(org, branch);
        }
        
        public System.Threading.Tasks.Task<bool> updateBranchAsync(Compliance.DataObject.Organization org, Compliance.DataObject.BranchLocation branch) {
            return base.Channel.updateBranchAsync(org, branch);
        }
        
        public int getBranch() {
            return base.Channel.getBranch();
        }
        
        public System.Threading.Tasks.Task<int> getBranchAsync() {
            return base.Channel.getBranchAsync();
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
    }
}