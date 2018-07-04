using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;

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

        public List<UserGroup> BindUserGroup()
        {
            UserGroupHelper helper = new UserGroupHelper();
            List<UserGroup> userGroups = new List<UserGroup>();
            helper.getUserGroupList();
            return userGroups;
        }
        public void BindUserRole(List<UserRoles> userRoles)
        {
            UserGroupHelper helper = new UserGroupHelper();
            helper.getUserGroupList();
        }

        public string insertRoles()
        {
            return "";
        }
       
        public string updateRoles()
        { return ""; }
        public void BindPrivilege()
        { }
        public string insertGroups()
        { return ""; }
        public string updateGroups()
        {
            return "";
        }
    }
}
