﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplianceAuditWeb.UserService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserService.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertUser", ReplyAction="http://tempuri.org/IUserService/insertUserResponse")]
        string insertUser(Compliance.DataObject.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertUser", ReplyAction="http://tempuri.org/IUserService/insertUserResponse")]
        System.Threading.Tasks.Task<string> insertUserAsync(Compliance.DataObject.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateUser", ReplyAction="http://tempuri.org/IUserService/updateUserResponse")]
        bool updateUser(Compliance.DataObject.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateUser", ReplyAction="http://tempuri.org/IUserService/updateUserResponse")]
        System.Threading.Tasks.Task<bool> updateUserAsync(Compliance.DataObject.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUser", ReplyAction="http://tempuri.org/IUserService/getUserResponse")]
        string getUser(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUser", ReplyAction="http://tempuri.org/IUserService/getUserResponse")]
        System.Threading.Tasks.Task<string> getUserAsync(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getAllUser", ReplyAction="http://tempuri.org/IUserService/getAllUserResponse")]
        string getAllUser(int Companyid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getAllUser", ReplyAction="http://tempuri.org/IUserService/getAllUserResponse")]
        System.Threading.Tasks.Task<string> getAllUserAsync(int Companyid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUserAssignedGroup", ReplyAction="http://tempuri.org/IUserService/getUserAssignedGroupResponse")]
        string getUserAssignedGroup(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUserAssignedGroup", ReplyAction="http://tempuri.org/IUserService/getUserAssignedGroupResponse")]
        System.Threading.Tasks.Task<string> getUserAssignedGroupAsync(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUserRoles", ReplyAction="http://tempuri.org/IUserService/getUserRolesResponse")]
        string getUserRoles(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUserRoles", ReplyAction="http://tempuri.org/IUserService/getUserRolesResponse")]
        System.Threading.Tasks.Task<string> getUserRolesAsync(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertUserRole", ReplyAction="http://tempuri.org/IUserService/insertUserRoleResponse")]
        bool insertUserRole(int Userid, int[] Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertUserRole", ReplyAction="http://tempuri.org/IUserService/insertUserRoleResponse")]
        System.Threading.Tasks.Task<bool> insertUserRoleAsync(int Userid, int[] Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertRoles", ReplyAction="http://tempuri.org/IUserService/insertRolesResponse")]
        int insertRoles(Compliance.DataObject.Roles Role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertRoles", ReplyAction="http://tempuri.org/IUserService/insertRolesResponse")]
        System.Threading.Tasks.Task<int> insertRolesAsync(Compliance.DataObject.Roles Role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateRoles", ReplyAction="http://tempuri.org/IUserService/updateRolesResponse")]
        bool updateRoles(Compliance.DataObject.Roles Role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateRoles", ReplyAction="http://tempuri.org/IUserService/updateRolesResponse")]
        System.Threading.Tasks.Task<bool> updateRolesAsync(Compliance.DataObject.Roles Role);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetRoles", ReplyAction="http://tempuri.org/IUserService/GetRolesResponse")]
        string GetRoles(int flag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetRoles", ReplyAction="http://tempuri.org/IUserService/GetRolesResponse")]
        System.Threading.Tasks.Task<string> GetRolesAsync(int flag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllRoles", ReplyAction="http://tempuri.org/IUserService/GetAllRolesResponse")]
        string GetAllRoles(int roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetAllRoles", ReplyAction="http://tempuri.org/IUserService/GetAllRolesResponse")]
        System.Threading.Tasks.Task<string> GetAllRolesAsync(int roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertRolePrivilege", ReplyAction="http://tempuri.org/IUserService/insertRolePrivilegeResponse")]
        bool insertRolePrivilege(int Roleid, int[] Privilegeid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertRolePrivilege", ReplyAction="http://tempuri.org/IUserService/insertRolePrivilegeResponse")]
        System.Threading.Tasks.Task<bool> insertRolePrivilegeAsync(int Roleid, int[] Privilegeid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetPrivilege", ReplyAction="http://tempuri.org/IUserService/GetPrivilegeResponse")]
        string GetPrivilege();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetPrivilege", ReplyAction="http://tempuri.org/IUserService/GetPrivilegeResponse")]
        System.Threading.Tasks.Task<string> GetPrivilegeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteRolePrivilege", ReplyAction="http://tempuri.org/IUserService/DeleteRolePrivilegeResponse")]
        bool DeleteRolePrivilege(int Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteRolePrivilege", ReplyAction="http://tempuri.org/IUserService/DeleteRolePrivilegeResponse")]
        System.Threading.Tasks.Task<bool> DeleteRolePrivilegeAsync(int Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getRolePrivilege", ReplyAction="http://tempuri.org/IUserService/getRolePrivilegeResponse")]
        string getRolePrivilege(int Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getRolePrivilege", ReplyAction="http://tempuri.org/IUserService/getRolePrivilegeResponse")]
        System.Threading.Tasks.Task<string> getRolePrivilegeAsync(int Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertUserGroupmember", ReplyAction="http://tempuri.org/IUserService/insertUserGroupmemberResponse")]
        bool insertUserGroupmember(int Userid, int[] Groupid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertUserGroupmember", ReplyAction="http://tempuri.org/IUserService/insertUserGroupmemberResponse")]
        System.Threading.Tasks.Task<bool> insertUserGroupmemberAsync(int Userid, int[] Groupid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertGroups", ReplyAction="http://tempuri.org/IUserService/insertGroupsResponse")]
        bool insertGroups(Compliance.DataObject.UserGroup group);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/insertGroups", ReplyAction="http://tempuri.org/IUserService/insertGroupsResponse")]
        System.Threading.Tasks.Task<bool> insertGroupsAsync(Compliance.DataObject.UserGroup group);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateGroups", ReplyAction="http://tempuri.org/IUserService/updateGroupsResponse")]
        bool updateGroups(Compliance.DataObject.UserGroup group);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateGroups", ReplyAction="http://tempuri.org/IUserService/updateGroupsResponse")]
        System.Threading.Tasks.Task<bool> updateGroupsAsync(Compliance.DataObject.UserGroup group);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserGroup", ReplyAction="http://tempuri.org/IUserService/GetUserGroupResponse")]
        string GetUserGroup(int Groupid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserGroup", ReplyAction="http://tempuri.org/IUserService/GetUserGroupResponse")]
        System.Threading.Tasks.Task<string> GetUserGroupAsync(int Groupid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        bool DeleteUser(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        System.Threading.Tasks.Task<bool> DeleteUserAsync(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUserGroupMember", ReplyAction="http://tempuri.org/IUserService/UpdateUserGroupMemberResponse")]
        bool UpdateUserGroupMember(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUserGroupMember", ReplyAction="http://tempuri.org/IUserService/UpdateUserGroupMemberResponse")]
        System.Threading.Tasks.Task<bool> UpdateUserGroupMemberAsync(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUserRole", ReplyAction="http://tempuri.org/IUserService/UpdateUserRoleResponse")]
        bool UpdateUserRole(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUserRole", ReplyAction="http://tempuri.org/IUserService/UpdateUserRoleResponse")]
        System.Threading.Tasks.Task<bool> UpdateUserRoleAsync(int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteGroup", ReplyAction="http://tempuri.org/IUserService/DeleteGroupResponse")]
        bool DeleteGroup(int Groupid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteGroup", ReplyAction="http://tempuri.org/IUserService/DeleteGroupResponse")]
        System.Threading.Tasks.Task<bool> DeleteGroupAsync(int Groupid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteRole", ReplyAction="http://tempuri.org/IUserService/DeleteRoleResponse")]
        bool DeleteRole(int Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteRole", ReplyAction="http://tempuri.org/IUserService/DeleteRoleResponse")]
        System.Threading.Tasks.Task<bool> DeleteRoleAsync(int Roleid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Login", ReplyAction="http://tempuri.org/IUserService/LoginResponse")]
        string Login(string emailid, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Login", ReplyAction="http://tempuri.org/IUserService/LoginResponse")]
        System.Threading.Tasks.Task<string> LoginAsync(string emailid, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getmenulist", ReplyAction="http://tempuri.org/IUserService/getmenulistResponse")]
        string getmenulist(int userid, int parentmenuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getmenulist", ReplyAction="http://tempuri.org/IUserService/getmenulistResponse")]
        System.Threading.Tasks.Task<string> getmenulistAsync(int userid, int parentmenuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getmenu", ReplyAction="http://tempuri.org/IUserService/getmenuResponse")]
        string getmenu();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getmenu", ReplyAction="http://tempuri.org/IUserService/getmenuResponse")]
        System.Threading.Tasks.Task<string> getmenuAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : ComplianceAuditWeb.UserService.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<ComplianceAuditWeb.UserService.IUserService>, ComplianceAuditWeb.UserService.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string insertUser(Compliance.DataObject.User user) {
            return base.Channel.insertUser(user);
        }
        
        public System.Threading.Tasks.Task<string> insertUserAsync(Compliance.DataObject.User user) {
            return base.Channel.insertUserAsync(user);
        }
        
        public bool updateUser(Compliance.DataObject.User user) {
            return base.Channel.updateUser(user);
        }
        
        public System.Threading.Tasks.Task<bool> updateUserAsync(Compliance.DataObject.User user) {
            return base.Channel.updateUserAsync(user);
        }
        
        public string getUser(int Userid) {
            return base.Channel.getUser(Userid);
        }
        
        public System.Threading.Tasks.Task<string> getUserAsync(int Userid) {
            return base.Channel.getUserAsync(Userid);
        }
        
        public string getAllUser(int Companyid) {
            return base.Channel.getAllUser(Companyid);
        }
        
        public System.Threading.Tasks.Task<string> getAllUserAsync(int Companyid) {
            return base.Channel.getAllUserAsync(Companyid);
        }
        
        public string getUserAssignedGroup(int Userid) {
            return base.Channel.getUserAssignedGroup(Userid);
        }
        
        public System.Threading.Tasks.Task<string> getUserAssignedGroupAsync(int Userid) {
            return base.Channel.getUserAssignedGroupAsync(Userid);
        }
        
        public string getUserRoles(int Userid) {
            return base.Channel.getUserRoles(Userid);
        }
        
        public System.Threading.Tasks.Task<string> getUserRolesAsync(int Userid) {
            return base.Channel.getUserRolesAsync(Userid);
        }
        
        public bool insertUserRole(int Userid, int[] Roleid) {
            return base.Channel.insertUserRole(Userid, Roleid);
        }
        
        public System.Threading.Tasks.Task<bool> insertUserRoleAsync(int Userid, int[] Roleid) {
            return base.Channel.insertUserRoleAsync(Userid, Roleid);
        }
        
        public int insertRoles(Compliance.DataObject.Roles Role) {
            return base.Channel.insertRoles(Role);
        }
        
        public System.Threading.Tasks.Task<int> insertRolesAsync(Compliance.DataObject.Roles Role) {
            return base.Channel.insertRolesAsync(Role);
        }
        
        public bool updateRoles(Compliance.DataObject.Roles Role) {
            return base.Channel.updateRoles(Role);
        }
        
        public System.Threading.Tasks.Task<bool> updateRolesAsync(Compliance.DataObject.Roles Role) {
            return base.Channel.updateRolesAsync(Role);
        }
        
        public string GetRoles(int flag) {
            return base.Channel.GetRoles(flag);
        }
        
        public System.Threading.Tasks.Task<string> GetRolesAsync(int flag) {
            return base.Channel.GetRolesAsync(flag);
        }
        
        public string GetAllRoles(int roleid) {
            return base.Channel.GetAllRoles(roleid);
        }
        
        public System.Threading.Tasks.Task<string> GetAllRolesAsync(int roleid) {
            return base.Channel.GetAllRolesAsync(roleid);
        }
        
        public bool insertRolePrivilege(int Roleid, int[] Privilegeid) {
            return base.Channel.insertRolePrivilege(Roleid, Privilegeid);
        }
        
        public System.Threading.Tasks.Task<bool> insertRolePrivilegeAsync(int Roleid, int[] Privilegeid) {
            return base.Channel.insertRolePrivilegeAsync(Roleid, Privilegeid);
        }
        
        public string GetPrivilege() {
            return base.Channel.GetPrivilege();
        }
        
        public System.Threading.Tasks.Task<string> GetPrivilegeAsync() {
            return base.Channel.GetPrivilegeAsync();
        }
        
        public bool DeleteRolePrivilege(int Roleid) {
            return base.Channel.DeleteRolePrivilege(Roleid);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteRolePrivilegeAsync(int Roleid) {
            return base.Channel.DeleteRolePrivilegeAsync(Roleid);
        }
        
        public string getRolePrivilege(int Roleid) {
            return base.Channel.getRolePrivilege(Roleid);
        }
        
        public System.Threading.Tasks.Task<string> getRolePrivilegeAsync(int Roleid) {
            return base.Channel.getRolePrivilegeAsync(Roleid);
        }
        
        public bool insertUserGroupmember(int Userid, int[] Groupid) {
            return base.Channel.insertUserGroupmember(Userid, Groupid);
        }
        
        public System.Threading.Tasks.Task<bool> insertUserGroupmemberAsync(int Userid, int[] Groupid) {
            return base.Channel.insertUserGroupmemberAsync(Userid, Groupid);
        }
        
        public bool insertGroups(Compliance.DataObject.UserGroup group) {
            return base.Channel.insertGroups(group);
        }
        
        public System.Threading.Tasks.Task<bool> insertGroupsAsync(Compliance.DataObject.UserGroup group) {
            return base.Channel.insertGroupsAsync(group);
        }
        
        public bool updateGroups(Compliance.DataObject.UserGroup group) {
            return base.Channel.updateGroups(group);
        }
        
        public System.Threading.Tasks.Task<bool> updateGroupsAsync(Compliance.DataObject.UserGroup group) {
            return base.Channel.updateGroupsAsync(group);
        }
        
        public string GetUserGroup(int Groupid) {
            return base.Channel.GetUserGroup(Groupid);
        }
        
        public System.Threading.Tasks.Task<string> GetUserGroupAsync(int Groupid) {
            return base.Channel.GetUserGroupAsync(Groupid);
        }
        
        public bool DeleteUser(int Userid) {
            return base.Channel.DeleteUser(Userid);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteUserAsync(int Userid) {
            return base.Channel.DeleteUserAsync(Userid);
        }
        
        public bool UpdateUserGroupMember(int Userid) {
            return base.Channel.UpdateUserGroupMember(Userid);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateUserGroupMemberAsync(int Userid) {
            return base.Channel.UpdateUserGroupMemberAsync(Userid);
        }
        
        public bool UpdateUserRole(int Userid) {
            return base.Channel.UpdateUserRole(Userid);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateUserRoleAsync(int Userid) {
            return base.Channel.UpdateUserRoleAsync(Userid);
        }
        
        public bool DeleteGroup(int Groupid) {
            return base.Channel.DeleteGroup(Groupid);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteGroupAsync(int Groupid) {
            return base.Channel.DeleteGroupAsync(Groupid);
        }
        
        public bool DeleteRole(int Roleid) {
            return base.Channel.DeleteRole(Roleid);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteRoleAsync(int Roleid) {
            return base.Channel.DeleteRoleAsync(Roleid);
        }
        
        public string Login(string emailid, string password) {
            return base.Channel.Login(emailid, password);
        }
        
        public System.Threading.Tasks.Task<string> LoginAsync(string emailid, string password) {
            return base.Channel.LoginAsync(emailid, password);
        }
        
        public string getmenulist(int userid, int parentmenuid) {
            return base.Channel.getmenulist(userid, parentmenuid);
        }
        
        public System.Threading.Tasks.Task<string> getmenulistAsync(int userid, int parentmenuid) {
            return base.Channel.getmenulistAsync(userid, parentmenuid);
        }
        
        public string getmenu() {
            return base.Channel.getmenu();
        }
        
        public System.Threading.Tasks.Task<string> getmenuAsync() {
            return base.Channel.getmenuAsync();
        }
    }
}
