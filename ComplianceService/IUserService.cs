using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string insertUser(User user);
        [OperationContract]
        bool updateUser(User user);
        [OperationContract]
        string getUser(int Userid);
        [OperationContract]
        string getAllUser(int Companyid);
        [OperationContract]
        string getUserAssignedGroup(int Userid);
        [OperationContract]
        string getUserRoles(int Userid);
        [OperationContract]
        bool insertUserRole(int Userid, int[] Roleid);               
        [OperationContract]
        int insertRoles(Roles Role);
        [OperationContract]
        bool updateRoles(Roles Role);
        [OperationContract]
        string GetRoles(int flag);
        [OperationContract]
        string GetAllRoles(int roleid);
        [OperationContract]
        bool insertRolePrivilege(int Roleid, int[] Privilegeid);
        [OperationContract]
        string GetPrivilege();
        [OperationContract]
        bool DeleteRolePrivilege(int Roleid);
        [OperationContract]
        string getRolePrivilege(int Roleid);
        [OperationContract]
        bool insertUserGroupmember(int Userid, int[] Groupid);
        [OperationContract]
        bool insertGroups(UserGroup group);
        [OperationContract]
        bool updateGroups(UserGroup group);
        [OperationContract]
        string GetUserGroup(int Groupid);
        [OperationContract]
        bool DeleteUser(int Userid);
        [OperationContract]
        bool UpdateUserGroupMember(int Userid);
        [OperationContract]
        bool UpdateUserRole(int Userid);

        [OperationContract]
        bool DeleteGroup(int Groupid);
        [OperationContract]
        bool DeleteRole(int Roleid);

        [OperationContract]
        string Login(string emailid, string password);

        [OperationContract]
        string getmenulist(int userid, int parentmenuid);

        [OperationContract]
        string getmenu();

    }
}
