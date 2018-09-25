﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplianceAuditWeb.ReportingService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ReportingService.IReportingService")]
    public interface IReportingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/DoWork", ReplyAction="http://tempuri.org/IReportingService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/DoWork", ReplyAction="http://tempuri.org/IReportingService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchReport", ReplyAction="http://tempuri.org/IReportingService/getBranchReportResponse")]
        string getBranchReport(int BranchID, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchReport", ReplyAction="http://tempuri.org/IReportingService/getBranchReportResponse")]
        System.Threading.Tasks.Task<string> getBranchReportAsync(int BranchID, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchStatusReport", ReplyAction="http://tempuri.org/IReportingService/getBranchStatusReportResponse")]
        string getBranchStatusReport(int BranchID, string status, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchStatusReport", ReplyAction="http://tempuri.org/IReportingService/getBranchStatusReportResponse")]
        System.Threading.Tasks.Task<string> getBranchStatusReportAsync(int BranchID, string status, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchRACTeport", ReplyAction="http://tempuri.org/IReportingService/getBranchRACTeportResponse")]
        string getBranchRACTeport(int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchRACTeport", ReplyAction="http://tempuri.org/IReportingService/getBranchRACTeportResponse")]
        System.Threading.Tasks.Task<string> getBranchRACTeportAsync(int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchStatusACTReport", ReplyAction="http://tempuri.org/IReportingService/getBranchStatusACTReportResponse")]
        string getBranchStatusACTReport(int BranchID, string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchStatusACTReport", ReplyAction="http://tempuri.org/IReportingService/getBranchStatusACTReportResponse")]
        System.Threading.Tasks.Task<string> getBranchStatusACTReportAsync(int BranchID, string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchpieReport", ReplyAction="http://tempuri.org/IReportingService/getBranchpieReportResponse")]
        string getBranchpieReport(int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/getBranchpieReport", ReplyAction="http://tempuri.org/IReportingService/getBranchpieReportResponse")]
        System.Threading.Tasks.Task<string> getBranchpieReportAsync(int BranchID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IReportingServiceChannel : ComplianceAuditWeb.ReportingService.IReportingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReportingServiceClient : System.ServiceModel.ClientBase<ComplianceAuditWeb.ReportingService.IReportingService>, ComplianceAuditWeb.ReportingService.IReportingService {
        
        public ReportingServiceClient() {
        }
        
        public ReportingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReportingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReportingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReportingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public string getBranchReport(int BranchID, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID) {
            return base.Channel.getBranchReport(BranchID, StartDate, EndDate, ComplianceID);
        }
        
        public System.Threading.Tasks.Task<string> getBranchReportAsync(int BranchID, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID) {
            return base.Channel.getBranchReportAsync(BranchID, StartDate, EndDate, ComplianceID);
        }
        
        public string getBranchStatusReport(int BranchID, string status, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID) {
            return base.Channel.getBranchStatusReport(BranchID, status, StartDate, EndDate, ComplianceID);
        }
        
        public System.Threading.Tasks.Task<string> getBranchStatusReportAsync(int BranchID, string status, System.DateTime StartDate, System.DateTime EndDate, int ComplianceID) {
            return base.Channel.getBranchStatusReportAsync(BranchID, status, StartDate, EndDate, ComplianceID);
        }
        
        public string getBranchRACTeport(int BranchID) {
            return base.Channel.getBranchRACTeport(BranchID);
        }
        
        public System.Threading.Tasks.Task<string> getBranchRACTeportAsync(int BranchID) {
            return base.Channel.getBranchRACTeportAsync(BranchID);
        }
        
        public string getBranchStatusACTReport(int BranchID, string status) {
            return base.Channel.getBranchStatusACTReport(BranchID, status);
        }
        
        public System.Threading.Tasks.Task<string> getBranchStatusACTReportAsync(int BranchID, string status) {
            return base.Channel.getBranchStatusACTReportAsync(BranchID, status);
        }
        
        public string getBranchpieReport(int BranchID) {
            return base.Channel.getBranchpieReport(BranchID);
        }
        
        public System.Threading.Tasks.Task<string> getBranchpieReportAsync(int BranchID) {
            return base.Channel.getBranchpieReportAsync(BranchID);
        }
    }
}
