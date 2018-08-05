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
        public string insertUser(User Objuser)
        {
            UserHelper helper = new UserHelper();
            Objuser.IsActive = true;
            string res = helper.insertupdateUser(Objuser, 'I');
            return res;
        }
        public bool updateUser(User Objuser)
        {
            UserHelper helper = new UserHelper();
            string res = helper.insertupdateUser(Objuser, 'U');
            return true;
        }
        public string getUser(int Userid)
        {
            return BindUser(Userid);
        }

        private string BindUser(int Userid)
        {
            UserHelper helper = new UserHelper();
            DataSet ds = helper.getUser(Userid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            string xmldata = ds.GetXml();
            return xmldata;
        }
        public string getAllUser(int Companyid)
        {
            return BindgetAllUser(Companyid);
        }

        private string BindgetAllUser(int Companyid)
        {
            UserHelper helper = new UserHelper();
            DataSet userGroups = helper.getAllUser(Companyid);
            string xmlgroups = userGroups.GetXml();
            return xmlgroups;
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
        public bool insertUserGroupmember(int Userid, int[] Groupid)
        {
            bool res = false;
            UserHelper helper = new UserHelper();
            foreach (var item in Groupid)
            {
                res = helper.insertUserGroupmember(item, Userid);
            }
            return res;
        }

        public bool insertUserRole(int Userid, int[] Roleid)
        {
            bool res = false;
            UserHelper helper = new UserHelper();
            foreach (var item in Roleid)
            {
                res = helper.insertUserRole(item, Userid);
            }
            return res;
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
        public string GetAllRoles(int roleid)
        {
            return BindAllRole(roleid);
        }
        private string BindAllRole(int roleid)
        {
            UserRolesHelper helper = new UserRolesHelper();
            DataSet roles = helper.getAllRole(roleid);
            string xmlroles = roles.GetXml();
            return xmlroles;
        }

      

        public int insertRoles(Roles Role)
        {
            UserRolesHelper helper = new UserRolesHelper();
            Role.IsActive = true;
            int res = helper.insertUpdateRole(Role, 'I');
            return res;
        }

        public bool updateRoles(Roles Role)
        {
            bool result = false;
            UserRolesHelper helper = new UserRolesHelper();
            int res = helper.insertUpdateRole(Role, 'U');
            if (res > 0)
                result = true;
            return result;
        }

        public bool insertRolePrivilege(int Roleid, int[] Privilegeid)
        {
            bool res = false;
            UserRolesHelper helper = new UserRolesHelper();
            foreach (var item in Privilegeid)
            {
                res = helper.insertRolePrivilege(Roleid, item);
            }
            return res;
        }
        public string getRolePrivilege(int Roleid)
        {
            return BindRolePrivilege(Roleid);
        }
        private string BindRolePrivilege(int Roleid)
        {
            UserPrivilegeHelper helper = new UserPrivilegeHelper();
            DataSet privilege = helper.getRolePrivilege(Roleid);
            string xmlPrivilege = privilege.GetXml();
            return xmlPrivilege;
        }
        public string GetPrivilege()
        {
            return BindPrivilege();
        }
        private string BindPrivilege()
        {
            UserPrivilegeHelper helper = new UserPrivilegeHelper();
            DataSet privilege = helper.getPrivilege();
            string xmlPrivilege = privilege.GetXml();
            return xmlPrivilege;
        }
        public bool insertGroups(UserGroup ObjGroup)
        {
            UserGroupHelper helper = new UserGroupHelper();
            ObjGroup.IsActive = true;
            bool res = helper.insertupdateUser(ObjGroup, 'I');
            return res;
        }
        public bool updateGroups(UserGroup ObjGroup)
        {
            UserGroupHelper helper = new UserGroupHelper();
            bool res = helper.insertupdateUser(ObjGroup, 'U');
            return res;
        }
        public string getUserRoles(int Userid)
        {
            return BindUserRole(Userid);
        }
        private string BindUserRole(int Userid)
        {
            UserHelper helper = new UserHelper();
            DataSet ds = helper.getUserRole(Userid);
            string xmldata = ds.GetXml();
            return xmldata;
        }
        public string getUserAssignedGroup(int Userid)
        {
            return BindUserAssignedGroup(Userid);
        }
        private string BindUserAssignedGroup(int Userid)
        {
            UserHelper helper = new UserHelper();
            DataSet ds = helper.getUserAssignedGroup(Userid);
            string xmldata = ds.GetXml();
            return xmldata;
        }

        public bool DeleteUser(int Userid)
        {
            UserHelper helper = new UserHelper();
            bool res = helper.DeleteUser(Userid);
            return res;
        }

        public bool UpdateUserGroupMember(int Userid)
        {
            UserHelper helper = new UserHelper();
            return helper.DeleteUserGroupmember(Userid);
        }

        public bool UpdateUserRole(int Userid)
        {
            UserHelper helper = new UserHelper();
            return helper.DeleteUserRole(Userid);

        }

        public bool DeleteRolePrivilege(int Roleid)
        {
            UserRolesHelper helper = new UserRolesHelper();
            return helper.DeleteRolePrivilege(Roleid);
        }

        public bool DeleteRole(int Roleid)
        {
            UserRolesHelper helper = new UserRolesHelper();
            return helper.DeleteRolePrivilege(Roleid);
        }

        public bool DeleteGroup(int Groupid)
        {
            UserGroupHelper helper = new UserGroupHelper();
            return helper.DeleteGroup(Groupid);
        }

        public string Login(string emailid, string password)
        {
            return GetLoginData(emailid, password);
        }

        private string GetLoginData(string emailid, string password)
        {
            UserHelper helper = new UserHelper();
            User user = new User();
            user.EmailId = emailid;
            user.UserPassword = password;

            DataSet ds = helper.getLoginData(user);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            string xmldata = ds.GetXml();
            return xmldata;
        }

        public string getmenulist(int usergroupid, int parentmenuid)
        {
            return bindmenuslist(usergroupid, parentmenuid);
        }
        private string bindmenuslist(int groupid, int parentmenuid)
        {
            MenusHelper helper = new MenusHelper();
            DataSet ds = helper.getMenus(groupid, parentmenuid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }

        public string getmenu()
        {
            return bindmenu();
        }

        private string bindmenu()
        {
            MenusHelper helper = new MenusHelper();
            DataSet ds = helper.getMenusList();
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
    }
}
