﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplianceAuditWeb.ComplianceXrefService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ComplianceXrefService.IComplianceXrefService")]
    public interface IComplianceXrefService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/insertActs", ReplyAction="http://tempuri.org/IComplianceXrefService/insertActsResponse")]
        int insertActs(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/insertActs", ReplyAction="http://tempuri.org/IComplianceXrefService/insertActsResponse")]
        System.Threading.Tasks.Task<int> insertActsAsync(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/insertRules", ReplyAction="http://tempuri.org/IComplianceXrefService/insertRulesResponse")]
        int insertRules(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/insertRules", ReplyAction="http://tempuri.org/IComplianceXrefService/insertRulesResponse")]
        System.Threading.Tasks.Task<int> insertRulesAsync(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/insertSection", ReplyAction="http://tempuri.org/IComplianceXrefService/insertSectionResponse")]
        int insertSection(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/insertSection", ReplyAction="http://tempuri.org/IComplianceXrefService/insertSectionResponse")]
        System.Threading.Tasks.Task<int> insertSectionAsync(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/UpdateActs", ReplyAction="http://tempuri.org/IComplianceXrefService/UpdateActsResponse")]
        int UpdateActs(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/UpdateActs", ReplyAction="http://tempuri.org/IComplianceXrefService/UpdateActsResponse")]
        System.Threading.Tasks.Task<int> UpdateActsAsync(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/UpdateRules", ReplyAction="http://tempuri.org/IComplianceXrefService/UpdateRulesResponse")]
        int UpdateRules(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/UpdateRules", ReplyAction="http://tempuri.org/IComplianceXrefService/UpdateRulesResponse")]
        System.Threading.Tasks.Task<int> UpdateRulesAsync(Compliance.DataObject.ComplianceXref compliance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetActs", ReplyAction="http://tempuri.org/IComplianceXrefService/GetActsResponse")]
        string GetActs(int complianceid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetActs", ReplyAction="http://tempuri.org/IComplianceXrefService/GetActsResponse")]
        System.Threading.Tasks.Task<string> GetActsAsync(int complianceid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetSections", ReplyAction="http://tempuri.org/IComplianceXrefService/GetSectionsResponse")]
        string GetSections(int parentid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetSections", ReplyAction="http://tempuri.org/IComplianceXrefService/GetSectionsResponse")]
        System.Threading.Tasks.Task<string> GetSectionsAsync(int parentid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetSpecificsection", ReplyAction="http://tempuri.org/IComplianceXrefService/GetSpecificsectionResponse")]
        string GetSpecificsection(int complianceid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetSpecificsection", ReplyAction="http://tempuri.org/IComplianceXrefService/GetSpecificsectionResponse")]
        System.Threading.Tasks.Task<string> GetSpecificsectionAsync(int complianceid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetRules", ReplyAction="http://tempuri.org/IComplianceXrefService/GetRulesResponse")]
        string GetRules(int parentid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetRules", ReplyAction="http://tempuri.org/IComplianceXrefService/GetRulesResponse")]
        System.Threading.Tasks.Task<string> GetRulesAsync(int parentid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetAuditorId", ReplyAction="http://tempuri.org/IComplianceXrefService/GetAuditorIdResponse")]
        int GetAuditorId(int Branchid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetAuditorId", ReplyAction="http://tempuri.org/IComplianceXrefService/GetAuditorIdResponse")]
        System.Threading.Tasks.Task<int> GetAuditorIdAsync(int Branchid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/getRuleforBranch", ReplyAction="http://tempuri.org/IComplianceXrefService/getRuleforBranchResponse")]
        string getRuleforBranch(int branchid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/getRuleforBranch", ReplyAction="http://tempuri.org/IComplianceXrefService/getRuleforBranchResponse")]
        System.Threading.Tasks.Task<string> getRuleforBranchAsync(int branchid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/inseretActandRuleforBranch", ReplyAction="http://tempuri.org/IComplianceXrefService/inseretActandRuleforBranchResponse")]
        bool inseretActandRuleforBranch(int orgid, int[] ruleid, int userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/inseretActandRuleforBranch", ReplyAction="http://tempuri.org/IComplianceXrefService/inseretActandRuleforBranchResponse")]
        System.Threading.Tasks.Task<bool> inseretActandRuleforBranchAsync(int orgid, int[] ruleid, int userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/DeleteRuleforBranch", ReplyAction="http://tempuri.org/IComplianceXrefService/DeleteRuleforBranchResponse")]
        bool DeleteRuleforBranch(int orgid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/DeleteRuleforBranch", ReplyAction="http://tempuri.org/IComplianceXrefService/DeleteRuleforBranchResponse")]
        System.Threading.Tasks.Task<bool> DeleteRuleforBranchAsync(int orgid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetComplaince", ReplyAction="http://tempuri.org/IComplianceXrefService/GetComplainceResponse")]
        string GetComplaince(int Audit_Type_ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IComplianceXrefService/GetComplaince", ReplyAction="http://tempuri.org/IComplianceXrefService/GetComplainceResponse")]
        System.Threading.Tasks.Task<string> GetComplainceAsync(int Audit_Type_ID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IComplianceXrefServiceChannel : ComplianceAuditWeb.ComplianceXrefService.IComplianceXrefService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ComplianceXrefServiceClient : System.ServiceModel.ClientBase<ComplianceAuditWeb.ComplianceXrefService.IComplianceXrefService>, ComplianceAuditWeb.ComplianceXrefService.IComplianceXrefService {
        
        public ComplianceXrefServiceClient() {
        }
        
        public ComplianceXrefServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ComplianceXrefServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ComplianceXrefServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ComplianceXrefServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int insertActs(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.insertActs(compliance);
        }
        
        public System.Threading.Tasks.Task<int> insertActsAsync(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.insertActsAsync(compliance);
        }
        
        public int insertRules(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.insertRules(compliance);
        }
        
        public System.Threading.Tasks.Task<int> insertRulesAsync(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.insertRulesAsync(compliance);
        }
        
        public int insertSection(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.insertSection(compliance);
        }
        
        public System.Threading.Tasks.Task<int> insertSectionAsync(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.insertSectionAsync(compliance);
        }
        
        public int UpdateActs(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.UpdateActs(compliance);
        }
        
        public System.Threading.Tasks.Task<int> UpdateActsAsync(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.UpdateActsAsync(compliance);
        }
        
        public int UpdateRules(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.UpdateRules(compliance);
        }
        
        public System.Threading.Tasks.Task<int> UpdateRulesAsync(Compliance.DataObject.ComplianceXref compliance) {
            return base.Channel.UpdateRulesAsync(compliance);
        }
        
        public string GetActs(int complianceid) {
            return base.Channel.GetActs(complianceid);
        }
        
        public System.Threading.Tasks.Task<string> GetActsAsync(int complianceid) {
            return base.Channel.GetActsAsync(complianceid);
        }
        
        public string GetSections(int parentid) {
            return base.Channel.GetSections(parentid);
        }
        
        public System.Threading.Tasks.Task<string> GetSectionsAsync(int parentid) {
            return base.Channel.GetSectionsAsync(parentid);
        }
        
        public string GetSpecificsection(int complianceid) {
            return base.Channel.GetSpecificsection(complianceid);
        }
        
        public System.Threading.Tasks.Task<string> GetSpecificsectionAsync(int complianceid) {
            return base.Channel.GetSpecificsectionAsync(complianceid);
        }
        
        public string GetRules(int parentid) {
            return base.Channel.GetRules(parentid);
        }
        
        public System.Threading.Tasks.Task<string> GetRulesAsync(int parentid) {
            return base.Channel.GetRulesAsync(parentid);
        }
        
        public int GetAuditorId(int Branchid) {
            return base.Channel.GetAuditorId(Branchid);
        }
        
        public System.Threading.Tasks.Task<int> GetAuditorIdAsync(int Branchid) {
            return base.Channel.GetAuditorIdAsync(Branchid);
        }
        
        public string getRuleforBranch(int branchid) {
            return base.Channel.getRuleforBranch(branchid);
        }
        
        public System.Threading.Tasks.Task<string> getRuleforBranchAsync(int branchid) {
            return base.Channel.getRuleforBranchAsync(branchid);
        }
        
        public bool inseretActandRuleforBranch(int orgid, int[] ruleid, int userid) {
            return base.Channel.inseretActandRuleforBranch(orgid, ruleid, userid);
        }
        
        public System.Threading.Tasks.Task<bool> inseretActandRuleforBranchAsync(int orgid, int[] ruleid, int userid) {
            return base.Channel.inseretActandRuleforBranchAsync(orgid, ruleid, userid);
        }
        
        public bool DeleteRuleforBranch(int orgid) {
            return base.Channel.DeleteRuleforBranch(orgid);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteRuleforBranchAsync(int orgid) {
            return base.Channel.DeleteRuleforBranchAsync(orgid);
        }
        
        public string GetComplaince(int Audit_Type_ID) {
            return base.Channel.GetComplaince(Audit_Type_ID);
        }
        
        public System.Threading.Tasks.Task<string> GetComplainceAsync(int Audit_Type_ID) {
            return base.Channel.GetComplainceAsync(Audit_Type_ID);
        }
    }
}
