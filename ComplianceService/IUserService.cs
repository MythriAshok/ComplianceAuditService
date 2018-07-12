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
        bool insertUser(User user);
        [OperationContract]
        bool updateUser(User user);
        [OperationContract]
        string GetUserGroup(int Groupid);
        [OperationContract]
        string GetRoles();
        [OperationContract]
        bool insertRoles(Roles Role);
        [OperationContract]
        bool updateRoles(Roles Role);
        [OperationContract]
        string GetPrivilege(int Roleid);
        [OperationContract]
        string insertGroups();
        [OperationContract]
        string updateGroups();
    }    
}
