using System;
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
        public bool insertUser(User user)
        {
            UserHelper helper = new UserHelper();
            string res = helper.insertupdateUser(user, 'I');
            return true;
        }
        public bool updateUser(User user)
        {
            UserHelper helper = new UserHelper();
            string res = helper.insertupdateUser(user, 'U');
            return true;
        }

        public string GetUserGroup(int Groupid)
        {
            return BindUserGroup(Groupid);
        }
       
        private string BindUserGroup(int Groupid)
        {
            UserGroupHelper helper = new UserGroupHelper();
            DataSet userGroups = helper.getUserGroup(Groupid);
            string xmlgroups = userGroups.GetXml();
            return xmlgroups;
        }
        public string GetRoles(int flag)
        {
            return BindRole(flag);
        }
        private string BindRole(int flag)
        {
            UserRolesHelper helper = new UserRolesHelper();
            DataSet roles = helper.getRoleList(flag);
            string xmlroles = roles.GetXml();
            return xmlroles;
        }

        public int insertRoles(Roles Role)
        {
            UserRolesHelper helper = new UserRolesHelper();
            int res=helper.insertUpdateRole(Role, 'I');
            return res;
        }
       
        public bool updateRoles(Roles Role)
        {
            bool result=false;
            UserRolesHelper helper = new UserRolesHelper();
            int res=helper.insertUpdateRole(Role, 'U');
            if (res > 0)
                result = true;
            return result;
        }

        public bool insertRolePrivilege(int Roleid,int[] Privilegeid)
        {
            bool res = false;
            UserRolesHelper helper = new UserRolesHelper();
            foreach (var item in Privilegeid)
            {
              res=helper.insertRolePrivilege(Roleid, item);
            }          
            return res;
        }

        public string GetPrivilege(int Roleid)
        {
            return BindPrivilege(Roleid);
        }
        private string BindPrivilege(int Roleid)
        {
            UserPrivilegeHelper helper = new UserPrivilegeHelper();
            DataSet privilege = helper.getPrivilege();
            string xmlPrivilege = privilege.GetXml();
            return xmlPrivilege;
        }
        public bool insertGroups(UserGroup Group)
        {
            UserGroupHelper helper = new UserGroupHelper();
            bool res=helper.insertupdateUser(Group, 'I');
            return res;
        }
        public bool updateGroups(UserGroup Group)
        {
            UserGroupHelper helper = new UserGroupHelper();
            bool res=helper.insertupdateUser(Group, 'U');
            return res;
        }
    }
}
