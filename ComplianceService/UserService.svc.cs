﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;
using System.Data;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        public string insertUser(User user)
        {
            UserHelper helper = new UserHelper();
            string res = helper.insertupdateUser(user, 'I');
            return res;
        }
        public string updateUser(User user)
        {
            UserHelper helper = new UserHelper();
            string res = helper.insertupdateUser(user, 'U');
            return res;
        }


        public string GetUserGroup()
        {
            return BindUserGroup();
        }
       
        private string BindUserGroup()
        {
            UserGroupHelper helper = new UserGroupHelper();
            DataSet userGroups = helper.getUserGroupList();
            string xmlgroups = userGroups.GetXml();
            return xmlgroups;
        }
        public string GetRoles()
        {
            return BindRole();
        }
        private string BindRole()
        {
            UserGroupHelper helper = new UserGroupHelper();
            DataSet roles=helper.getUserGroupList();
            string xmlroles = roles.GetXml();
            return xmlroles;
        }

        public string insertRoles(Roles Role)
        {
            UserRolesHelper helper = new UserRolesHelper();
            helper.insertUpdateRole(Role, 'I');
            return "";
        }
       
        public string updateRoles(Roles Role)
        {
            UserRolesHelper helper = new UserRolesHelper();
            helper.insertUpdateRole(Role, 'U');
            return "";
        }

        public string GetPrivilege()
        {
            return BindPrivilege();
        }
        private string BindPrivilege()
        {
            return "";
        }
        public string insertGroups()
        {
            return "";
        }
        public string updateGroups()
        {
            return "";
        }
    }
}
