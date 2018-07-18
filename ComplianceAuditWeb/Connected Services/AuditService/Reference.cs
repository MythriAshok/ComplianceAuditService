﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplianceAuditWeb.AuditService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AuditService.IAuditService")]
    public interface IAuditService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/DoWork", ReplyAction="http://tempuri.org/IAuditService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/DoWork", ReplyAction="http://tempuri.org/IAuditService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/insertComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/insertComplianceAuditResponse")]
        bool insertComplianceAudit(Compliance.DataObject.ComplianceAudit[] auditdatalist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/insertComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/insertComplianceAuditResponse")]
        System.Threading.Tasks.Task<bool> insertComplianceAuditAsync(Compliance.DataObject.ComplianceAudit[] auditdatalist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/updateComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/updateComplianceAuditResponse")]
        bool updateComplianceAudit(Compliance.DataObject.ComplianceAudit[] auditdatalist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/updateComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/updateComplianceAuditResponse")]
        System.Threading.Tasks.Task<bool> updateComplianceAuditAsync(Compliance.DataObject.ComplianceAudit[] auditdatalist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/getComplianceAuditResponse")]
        bool getComplianceAudit(int ComplianceAuditID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/getComplianceAuditResponse")]
        System.Threading.Tasks.Task<bool> getComplianceAuditAsync(int ComplianceAuditID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/deleteComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/deleteComplianceAuditResponse")]
        bool deleteComplianceAudit(int ComplianceAuditID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/deleteComplianceAudit", ReplyAction="http://tempuri.org/IAuditService/deleteComplianceAuditResponse")]
        System.Threading.Tasks.Task<bool> deleteComplianceAuditAsync(int ComplianceAuditID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/insertComplianceAuditTrail", ReplyAction="http://tempuri.org/IAuditService/insertComplianceAuditTrailResponse")]
        bool insertComplianceAuditTrail(Compliance.DataObject.ComplianceAuditAuditTrail[] auditdatalisttrail);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/insertComplianceAuditTrail", ReplyAction="http://tempuri.org/IAuditService/insertComplianceAuditTrailResponse")]
        System.Threading.Tasks.Task<bool> insertComplianceAuditTrailAsync(Compliance.DataObject.ComplianceAuditAuditTrail[] auditdatalisttrail);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getComplianceAuditTrail", ReplyAction="http://tempuri.org/IAuditService/getComplianceAuditTrailResponse")]
        bool getComplianceAuditTrail(int ComplianceAuditTrailID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getComplianceAuditTrail", ReplyAction="http://tempuri.org/IAuditService/getComplianceAuditTrailResponse")]
        System.Threading.Tasks.Task<bool> getComplianceAuditTrailAsync(int ComplianceAuditTrailID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getCompanyAllocatedToAuditor", ReplyAction="http://tempuri.org/IAuditService/getCompanyAllocatedToAuditorResponse")]
        string getCompanyAllocatedToAuditor(int AuditorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getCompanyAllocatedToAuditor", ReplyAction="http://tempuri.org/IAuditService/getCompanyAllocatedToAuditorResponse")]
        System.Threading.Tasks.Task<string> getCompanyAllocatedToAuditorAsync(int AuditorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getBranchAllocatedToAuditor", ReplyAction="http://tempuri.org/IAuditService/getBranchAllocatedToAuditorResponse")]
        string getBranchAllocatedToAuditor(int AuditorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getBranchAllocatedToAuditor", ReplyAction="http://tempuri.org/IAuditService/getBranchAllocatedToAuditorResponse")]
        System.Threading.Tasks.Task<string> getBranchAllocatedToAuditorAsync(int AuditorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getComplianceXref", ReplyAction="http://tempuri.org/IAuditService/getComplianceXrefResponse")]
        string getComplianceXref(int ComplianceXrefID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuditService/getComplianceXref", ReplyAction="http://tempuri.org/IAuditService/getComplianceXrefResponse")]
        System.Threading.Tasks.Task<string> getComplianceXrefAsync(int ComplianceXrefID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuditServiceChannel : ComplianceAuditWeb.AuditService.IAuditService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuditServiceClient : System.ServiceModel.ClientBase<ComplianceAuditWeb.AuditService.IAuditService>, ComplianceAuditWeb.AuditService.IAuditService {
        
        public AuditServiceClient() {
        }
        
        public AuditServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuditServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuditServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuditServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public bool insertComplianceAudit(Compliance.DataObject.ComplianceAudit[] auditdatalist) {
            return base.Channel.insertComplianceAudit(auditdatalist);
        }
        
        public System.Threading.Tasks.Task<bool> insertComplianceAuditAsync(Compliance.DataObject.ComplianceAudit[] auditdatalist) {
            return base.Channel.insertComplianceAuditAsync(auditdatalist);
        }
        
        public bool updateComplianceAudit(Compliance.DataObject.ComplianceAudit[] auditdatalist) {
            return base.Channel.updateComplianceAudit(auditdatalist);
        }
        
        public System.Threading.Tasks.Task<bool> updateComplianceAuditAsync(Compliance.DataObject.ComplianceAudit[] auditdatalist) {
            return base.Channel.updateComplianceAuditAsync(auditdatalist);
        }
        
        public bool getComplianceAudit(int ComplianceAuditID) {
            return base.Channel.getComplianceAudit(ComplianceAuditID);
        }
        
        public System.Threading.Tasks.Task<bool> getComplianceAuditAsync(int ComplianceAuditID) {
            return base.Channel.getComplianceAuditAsync(ComplianceAuditID);
        }
        
        public bool deleteComplianceAudit(int ComplianceAuditID) {
            return base.Channel.deleteComplianceAudit(ComplianceAuditID);
        }
        
        public System.Threading.Tasks.Task<bool> deleteComplianceAuditAsync(int ComplianceAuditID) {
            return base.Channel.deleteComplianceAuditAsync(ComplianceAuditID);
        }
        
        public bool insertComplianceAuditTrail(Compliance.DataObject.ComplianceAuditAuditTrail[] auditdatalisttrail) {
            return base.Channel.insertComplianceAuditTrail(auditdatalisttrail);
        }
        
        public System.Threading.Tasks.Task<bool> insertComplianceAuditTrailAsync(Compliance.DataObject.ComplianceAuditAuditTrail[] auditdatalisttrail) {
            return base.Channel.insertComplianceAuditTrailAsync(auditdatalisttrail);
        }
        
        public bool getComplianceAuditTrail(int ComplianceAuditTrailID) {
            return base.Channel.getComplianceAuditTrail(ComplianceAuditTrailID);
        }
        
        public System.Threading.Tasks.Task<bool> getComplianceAuditTrailAsync(int ComplianceAuditTrailID) {
            return base.Channel.getComplianceAuditTrailAsync(ComplianceAuditTrailID);
        }
        
        public string getCompanyAllocatedToAuditor(int AuditorID) {
            return base.Channel.getCompanyAllocatedToAuditor(AuditorID);
        }
        
        public System.Threading.Tasks.Task<string> getCompanyAllocatedToAuditorAsync(int AuditorID) {
            return base.Channel.getCompanyAllocatedToAuditorAsync(AuditorID);
        }
        
        public string getBranchAllocatedToAuditor(int AuditorID) {
            return base.Channel.getBranchAllocatedToAuditor(AuditorID);
        }
        
        public System.Threading.Tasks.Task<string> getBranchAllocatedToAuditorAsync(int AuditorID) {
            return base.Channel.getBranchAllocatedToAuditorAsync(AuditorID);
        }
        
        public string getComplianceXref(int ComplianceXrefID) {
            return base.Channel.getComplianceXref(ComplianceXrefID);
        }
        
        public System.Threading.Tasks.Task<string> getComplianceXrefAsync(int ComplianceXrefID) {
            return base.Channel.getComplianceXrefAsync(ComplianceXrefID);
        }
    }
}
