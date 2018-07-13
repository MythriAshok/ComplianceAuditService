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
        bool insertUserGroupmember(int Userid, int Groupid);
        [OperationContract]
        bool insertUserRole(int Userid, int Roleid);
        [OperationContract]
        string GetUserGroup(int Groupid);
        [OperationContract]
        string GetRoles(int flag);
        [OperationContract]
        int insertRoles(Roles Role);
        [OperationContract]
        bool updateRoles(Roles Role);
        [OperationContract]
        bool insertRolePrivilege(int Roleid, int[] Privilegeid);
        [OperationContract]
        string GetPrivilege(int Roleid);
        [OperationContract]
        bool insertGroups(UserGroup group);
        [OperationContract]
        bool updateGroups(UserGroup group);
    }    
}
