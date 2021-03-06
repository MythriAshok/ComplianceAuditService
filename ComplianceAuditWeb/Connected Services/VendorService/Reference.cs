﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplianceAuditWeb.VendorService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="VendorService.IVendorService")]
    public interface IVendorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/insertVendor", ReplyAction="http://tempuri.org/IVendorService/insertVendorResponse")]
        int insertVendor(Compliance.DataObject.VendorMaster vendor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/insertVendor", ReplyAction="http://tempuri.org/IVendorService/insertVendorResponse")]
        System.Threading.Tasks.Task<int> insertVendorAsync(Compliance.DataObject.VendorMaster vendor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/updateVendor", ReplyAction="http://tempuri.org/IVendorService/updateVendorResponse")]
        int updateVendor(Compliance.DataObject.VendorMaster vendor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/updateVendor", ReplyAction="http://tempuri.org/IVendorService/updateVendorResponse")]
        System.Threading.Tasks.Task<int> updateVendorAsync(Compliance.DataObject.VendorMaster vendor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/insertVendorForBranch", ReplyAction="http://tempuri.org/IVendorService/insertVendorForBranchResponse")]
        bool insertVendorForBranch(int VendorID, int OrgCompanyID, System.DateTime StartDate, System.Nullable<System.DateTime> EndDate, bool IsActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/insertVendorForBranch", ReplyAction="http://tempuri.org/IVendorService/insertVendorForBranchResponse")]
        System.Threading.Tasks.Task<bool> insertVendorForBranchAsync(int VendorID, int OrgCompanyID, System.DateTime StartDate, System.Nullable<System.DateTime> EndDate, bool IsActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/DeactivateVendorForCompany", ReplyAction="http://tempuri.org/IVendorService/DeactivateVendorForCompanyResponse")]
        string DeactivateVendorForCompany(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/DeactivateVendorForCompany", ReplyAction="http://tempuri.org/IVendorService/DeactivateVendorForCompanyResponse")]
        System.Threading.Tasks.Task<string> DeactivateVendorForCompanyAsync(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/ActivateVendorForCompany", ReplyAction="http://tempuri.org/IVendorService/ActivateVendorForCompanyResponse")]
        string ActivateVendorForCompany(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/ActivateVendorForCompany", ReplyAction="http://tempuri.org/IVendorService/ActivateVendorForCompanyResponse")]
        System.Threading.Tasks.Task<string> ActivateVendorForCompanyAsync(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/DeactivateVendorForBranch", ReplyAction="http://tempuri.org/IVendorService/DeactivateVendorForBranchResponse")]
        string DeactivateVendorForBranch(int[] VendorID, int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/DeactivateVendorForBranch", ReplyAction="http://tempuri.org/IVendorService/DeactivateVendorForBranchResponse")]
        System.Threading.Tasks.Task<string> DeactivateVendorForBranchAsync(int[] VendorID, int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/ActivateVendorForBranch", ReplyAction="http://tempuri.org/IVendorService/ActivateVendorForBranchResponse")]
        string ActivateVendorForBranch(int VendorBranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/ActivateVendorForBranch", ReplyAction="http://tempuri.org/IVendorService/ActivateVendorForBranchResponse")]
        System.Threading.Tasks.Task<string> ActivateVendorForBranchAsync(int VendorBranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/DeleteVendorForCompany", ReplyAction="http://tempuri.org/IVendorService/DeleteVendorForCompanyResponse")]
        string DeleteVendorForCompany(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/DeleteVendorForCompany", ReplyAction="http://tempuri.org/IVendorService/DeleteVendorForCompanyResponse")]
        System.Threading.Tasks.Task<string> DeleteVendorForCompanyAsync(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/GetAssignedVendorsforBranch", ReplyAction="http://tempuri.org/IVendorService/GetAssignedVendorsforBranchResponse")]
        string GetAssignedVendorsforBranch(int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/GetAssignedVendorsforBranch", ReplyAction="http://tempuri.org/IVendorService/GetAssignedVendorsforBranchResponse")]
        System.Threading.Tasks.Task<string> GetAssignedVendorsforBranchAsync(int BranchID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/GetBranchesAssociatedWithVendors", ReplyAction="http://tempuri.org/IVendorService/GetBranchesAssociatedWithVendorsResponse")]
        string GetBranchesAssociatedWithVendors(int VendorID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVendorService/GetBranchesAssociatedWithVendors", ReplyAction="http://tempuri.org/IVendorService/GetBranchesAssociatedWithVendorsResponse")]
        System.Threading.Tasks.Task<string> GetBranchesAssociatedWithVendorsAsync(int VendorID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IVendorServiceChannel : ComplianceAuditWeb.VendorService.IVendorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VendorServiceClient : System.ServiceModel.ClientBase<ComplianceAuditWeb.VendorService.IVendorService>, ComplianceAuditWeb.VendorService.IVendorService {
        
        public VendorServiceClient() {
        }
        
        public VendorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VendorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VendorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VendorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int insertVendor(Compliance.DataObject.VendorMaster vendor) {
            return base.Channel.insertVendor(vendor);
        }
        
        public System.Threading.Tasks.Task<int> insertVendorAsync(Compliance.DataObject.VendorMaster vendor) {
            return base.Channel.insertVendorAsync(vendor);
        }
        
        public int updateVendor(Compliance.DataObject.VendorMaster vendor) {
            return base.Channel.updateVendor(vendor);
        }
        
        public System.Threading.Tasks.Task<int> updateVendorAsync(Compliance.DataObject.VendorMaster vendor) {
            return base.Channel.updateVendorAsync(vendor);
        }
        
        public bool insertVendorForBranch(int VendorID, int OrgCompanyID, System.DateTime StartDate, System.Nullable<System.DateTime> EndDate, bool IsActive) {
            return base.Channel.insertVendorForBranch(VendorID, OrgCompanyID, StartDate, EndDate, IsActive);
        }
        
        public System.Threading.Tasks.Task<bool> insertVendorForBranchAsync(int VendorID, int OrgCompanyID, System.DateTime StartDate, System.Nullable<System.DateTime> EndDate, bool IsActive) {
            return base.Channel.insertVendorForBranchAsync(VendorID, OrgCompanyID, StartDate, EndDate, IsActive);
        }
        
        public string DeactivateVendorForCompany(int VendorID) {
            return base.Channel.DeactivateVendorForCompany(VendorID);
        }
        
        public System.Threading.Tasks.Task<string> DeactivateVendorForCompanyAsync(int VendorID) {
            return base.Channel.DeactivateVendorForCompanyAsync(VendorID);
        }
        
        public string ActivateVendorForCompany(int VendorID) {
            return base.Channel.ActivateVendorForCompany(VendorID);
        }
        
        public System.Threading.Tasks.Task<string> ActivateVendorForCompanyAsync(int VendorID) {
            return base.Channel.ActivateVendorForCompanyAsync(VendorID);
        }
        
        public string DeactivateVendorForBranch(int[] VendorID, int BranchID) {
            return base.Channel.DeactivateVendorForBranch(VendorID, BranchID);
        }
        
        public System.Threading.Tasks.Task<string> DeactivateVendorForBranchAsync(int[] VendorID, int BranchID) {
            return base.Channel.DeactivateVendorForBranchAsync(VendorID, BranchID);
        }
        
        public string ActivateVendorForBranch(int VendorBranchID) {
            return base.Channel.ActivateVendorForBranch(VendorBranchID);
        }
        
        public System.Threading.Tasks.Task<string> ActivateVendorForBranchAsync(int VendorBranchID) {
            return base.Channel.ActivateVendorForBranchAsync(VendorBranchID);
        }
        
        public string DeleteVendorForCompany(int VendorID) {
            return base.Channel.DeleteVendorForCompany(VendorID);
        }
        
        public System.Threading.Tasks.Task<string> DeleteVendorForCompanyAsync(int VendorID) {
            return base.Channel.DeleteVendorForCompanyAsync(VendorID);
        }
        
        public string GetAssignedVendorsforBranch(int BranchID) {
            return base.Channel.GetAssignedVendorsforBranch(BranchID);
        }
        
        public System.Threading.Tasks.Task<string> GetAssignedVendorsforBranchAsync(int BranchID) {
            return base.Channel.GetAssignedVendorsforBranchAsync(BranchID);
        }
        
        public string GetBranchesAssociatedWithVendors(int VendorID) {
            return base.Channel.GetBranchesAssociatedWithVendors(VendorID);
        }
        
        public System.Threading.Tasks.Task<string> GetBranchesAssociatedWithVendorsAsync(int VendorID) {
            return base.Channel.GetBranchesAssociatedWithVendorsAsync(VendorID);
        }
    }
}
