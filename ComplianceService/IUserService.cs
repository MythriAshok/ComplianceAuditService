﻿using System;
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
        string updateUser(User user);
        [OperationContract]
        string GetUserGroup();
        [OperationContract]
        string GetRoles();
        [OperationContract]
        string insertRoles(Roles Role);
        [OperationContract]
        string updateRoles(Roles Role);
        [OperationContract]
        string GetPrivilege();        
        [OperationContract]
        string insertGroups();
        [OperationContract]
        string updateGroups();
    }    
}
