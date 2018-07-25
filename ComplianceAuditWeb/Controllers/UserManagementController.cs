using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceAuditWeb.Models;
using Compliance.DataObject;
using System.Xml;
using System.Data;
using System.IO;

namespace ComplianceAuditWeb.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult UserManagementdashboard()
        {
            return View("_UserManagementDashboard");
        }

       //GET:insertRoles
        [HttpGet]
        public ActionResult AddRoles()
        {
            RolesViewModel rolesView = new RolesViewModel();
            rolesView.roles = new Roles();
            rolesView.roles.RoleId = 0;
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            string xmldata = client.GetPrivilege();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            rolesView.privilege = new List<SelectListItem>();
            if (ds!=null)
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {

                rolesView.privilege.Add(new SelectListItem() { Text = row["Privilege_Name"].ToString(), Value = row["Privilege_ID"].ToString() });
                }            
            return View("_AddRole", rolesView);
        }

        //Post:insertRoles
        [HttpPost]
        public ActionResult AddRoles(RolesViewModel rolesView)
        {           
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            int roleid=client.insertRoles(rolesView.roles);                        
            client.insertRolePrivilege(roleid, rolesView.PrivilegeId);                                
            return View("CreateUser");
        }

        [HttpGet]
        public ActionResult updateRoles(int Roleid)
        {
            RolesViewModel rolesView = new RolesViewModel();
            rolesView.roles = new Roles();
            rolesView.roles.RoleId = Roleid;
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            DataSet dsrole = new DataSet();
            string xmldata=client.GetRoles(Roleid);
            dsrole.ReadXml(new StringReader(xmldata));
            rolesView.roles.RoleName = Convert.ToString(dsrole.Tables[0].Rows[0]["Role_Name"]);
            rolesView.roles.IsGroupRole = Convert.ToBoolean(dsrole.Tables[0].Rows[0]["Is_Group_Role"]);
            xmldata = client.GetPrivilege();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            rolesView.privilege = new List<SelectListItem>();
            dsrole.Clear();
            xmldata = client.getRolePrivilege(rolesView.roles.RoleId);
            dsrole.ReadXml(new StringReader(xmldata));
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                bool selected = false;
                foreach (System.Data.DataRow roleid in dsrole.Tables[0].Rows)
                {
                    if (Convert.ToInt32(roleid["Privilege_ID"]) == Convert.ToInt32(row["Privilege_ID"]))
                    {
                        selected = true;
                        break;
                    }
                }
                rolesView.privilege.Add(new SelectListItem() { Text = row["Privilege_Name"].ToString(), Value = row["Privilege_ID"].ToString(),Selected=selected });
            }
            return View("_AddRole", rolesView);
        }

        [HttpPost]
        public ActionResult updateRoles(RolesViewModel model)
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            client.updateRoles(model.roles);
            client.DeleteRolePrivilege(model.roles.RoleId);
            client.insertRolePrivilege(model.roles.RoleId, model.PrivilegeId);
            return View("_AddRole",model);
        }

        [HttpGet]
        public ActionResult AddUserGroup()
        {
            UserGroupViewModel GroupView = new UserGroupViewModel();
            GroupView.Group = new UserGroup();
            GroupView.Group.UserGroupId = 0;
            UserService.UserServiceClient Client = new UserService.UserServiceClient();          
            string xmldata = Client.GetRoles(1);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            GroupView.Roles = new List<SelectListItem>();
            GroupView.Roles.Add(new SelectListItem { Text = "--Select Roles--", Value = "0" });
            foreach(System.Data.DataRow row in ds.Tables[0].Rows)
            {
                GroupView.Roles.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
            }
            GroupView.Role = new List<SelectListItem>();
            return View("_AddUserGroup", GroupView);
        }

        [HttpPost]
        public ActionResult AddUserGroup(UserGroupViewModel model)
        {
            UserService.UserServiceClient Client = new UserService.UserServiceClient();           
            Client.insertGroups(model.Group);            
            return View("CreateUser");
        }

        [HttpGet]
        public ActionResult Updateusergroup(int groupid)
        {
            UserService.UserServiceClient Client = new UserService.UserServiceClient();
            UserGroupViewModel model = new UserGroupViewModel();
            DataSet ds = new DataSet();
            string xmldata=Client.GetUserGroup(groupid);
            ds.ReadXml(new StringReader(xmldata));
            model.Group = new UserGroup();
            model.Group.UserGroupName = Convert.ToString(ds.Tables[0].Rows[0]["User_Group_Name"]);
            model.Group.UserGroupDescription = Convert.ToString(ds.Tables[0].Rows[0]["User_Group_Description"]);
            model.Group.UserRoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["Role_ID"]);
            xmldata = Client.GetRoles(1);
            DataSet dsrole = new DataSet();
            dsrole.ReadXml(new StringReader(xmldata));
            model.Roles = new List<SelectListItem>();
            //GroupView.Roles.Add(new SelectListItem { Text = "--Select Roles--", Value = "0" });
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                model.Roles.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
            }

            return View("_AddUserGroup", model);
        }

        [HttpPost]
        public ActionResult Updateusergroup(UserGroupViewModel model)
        {
            UserService.UserServiceClient Client = new UserService.UserServiceClient();
            Client.updateGroups(model.Group);
            return View();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {           
            UserViewModel userviewmodel = new UserViewModel();
            userviewmodel.User = new User();
            userviewmodel.User.UserId = 0;
            UserService.UserServiceClient Client = new UserService.UserServiceClient();                              
            string xmlGroups=Client.GetUserGroup(0);
            DataSet Groups = new DataSet();
            Groups.ReadXml(new StringReader(xmlGroups));
            userviewmodel.UserGroupList = new List<SelectListItem>();
            //userviewmodel.UserGroupList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (System.Data.DataRow row in Groups.Tables[0].Rows)
            {
                userviewmodel.UserGroupList.Add(new SelectListItem { Text = row["User_Group_Name"].ToString(), Value =row["User_Group_ID"].ToString() });
            }
            string xmlRoles=Client.GetRoles(0);
            DataSet dsRoles = new DataSet();
            dsRoles.ReadXml(new StringReader(xmlRoles));
            userviewmodel.RolesList= new List<SelectListItem>();
            //userviewmodel.RolesList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (System.Data.DataRow row in dsRoles.Tables[0].Rows)
            {
                userviewmodel.RolesList.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
            }
            return View("_AddUser", userviewmodel);
        }

        [HttpPost]
        public ActionResult CreateUser(UserViewModel model)
        {
            UserService.UserServiceClient Client = new UserService.UserServiceClient();
            if (ModelState.IsValid)
            {               
                string res = Client.insertUser(model.User);
                if (res != "EXISTS")
                {
                    model.User.UserId = Convert.ToInt32(res);
                    Client.insertUserGroupmember(model.User.UserId, model.UserGroupID);
                    Client.insertUserRole(model.User.UserId, model.RoleID);
                    return View("CreateUser");
                }
                else
                    ModelState.AddModelError("","UserName is already Exists");
            }
            model.User.UserId = 0;
            string xmlGroups = Client.GetUserGroup(0);
            DataSet Groups = new DataSet();
            Groups.ReadXml(new StringReader(xmlGroups));
            model.UserGroupList = new List<SelectListItem>();
            foreach (System.Data.DataRow row in Groups.Tables[0].Rows)
            {
                model.UserGroupList.Add(new SelectListItem { Text = row["User_Group_Name"].ToString(), Value = row["User_Group_ID"].ToString() });
            }
            string xmlRoles = Client.GetRoles(0);
            DataSet dsRoles = new DataSet();
            dsRoles.ReadXml(new StringReader(xmlRoles));
            model.RolesList = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsRoles.Tables[0].Rows)
            {
                model.RolesList.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
            }
            return View("_AddUser",model);
        }

        [HttpGet]
        public ActionResult UpdateUser(int UserId)
        {
            //int userid = 2;
            UserViewModel model = new UserViewModel();
            model.User = new User();
            UserService.UserServiceClient Client = new UserService.UserServiceClient();
            string xmldata = Client.getUser(UserId);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));            
            model.User.UserId = UserId;
            model.User.FirstName = ds.Tables[0].Rows[0]["First_Name"].ToString();
            model.User.MiddleName = Convert.ToString(ds.Tables[0].Rows[0]["Middle_Name"]);
            model.User.LastName = Convert.ToString(ds.Tables[0].Rows[0]["Last_Name"]);
            model.User.ContactNumber = Convert.ToString(ds.Tables[0].Rows[0]["Contact_Number"]);
            model.User.EmailId = Convert.ToString(ds.Tables[0].Rows[0]["Email_ID"]);
            model.User.Gender = Convert.ToString(ds.Tables[0].Rows[0]["Gender"]);
            model.User.LastLogin = Convert.ToDateTime(ds.Tables[0].Rows[0]["Last_Login"]);
            //model.User.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[0]["Is_Active"]);
            model.UserGroupList = new List<SelectListItem>();
            xmldata = Client.GetUserGroup(0);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            xmldata = Client.getUserAssignedGroup(model.User.UserId);
            DataSet dsgroup = new DataSet();
            dsgroup.ReadXml(new StringReader(xmldata));
           
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    bool selected = false;
                    if(dsgroup.Tables.Count>0)
                    foreach (System.Data.DataRow roleid in dsgroup.Tables[0].Rows)
                    {
                        if (Convert.ToInt32(roleid["User_Group_ID"]) == Convert.ToInt32(row["User_Group_ID"]))
                        {
                            selected = true;
                            break;
                        }
                    }
                    model.UserGroupList.Add(new SelectListItem { Text = row["User_Group_Name"].ToString(), Value = row["User_Group_ID"].ToString(), Selected = selected });
                }
            }
            model.RolesList = new List<SelectListItem>();
            ds = new DataSet();
            xmldata = Client.GetRoles(0);
            ds.ReadXml(new StringReader(xmldata));
            xmldata = Client.getUserRoles(model.User.UserId);
            dsgroup = new DataSet();
            dsgroup.ReadXml(new StringReader(xmldata));
            if(ds.Tables.Count>0)
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                bool selected = false;
                if(ds.Tables.Count>0)
                foreach (System.Data.DataRow roleid in dsgroup.Tables[0].Rows)
                {
                    if (Convert.ToInt32(roleid["Role_ID"]) == Convert.ToInt32(row["Role_ID"]))
                    {
                        selected = true;
                        break;
                    }
                }
                model.RolesList.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString(),Selected=selected});
            }
            return View("_AddUser",model);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserViewModel model)
        {
            UserService.UserServiceClient Client = new UserService.UserServiceClient();
            model.User.IsActive = true;
            Client.updateUser(model.User);
            Client.UpdateUserGroupMember(model.User.UserId);           
            Client.insertUserGroupmember(model.User.UserId, model.UserGroupID);
            Client.UpdateUserRole(model.User.UserId);
            Client.insertUserRole(model.User.UserId, model.RoleID);
            return View("CreateUser");
        }
       
        public ActionResult ListofUsers()
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            string xmldata = client.getUser(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            List<User> userlist = new List<User>();
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                userlist.Add(new User { UserId=Convert.ToInt32(row["User_ID"]),FirstName =Convert.ToString(row["First_Name"]), LastName = Convert.ToString(row["Last_Name"]), EmailId = Convert.ToString(row["Email_ID"]) });
            }
            return View("_ListofUsers", userlist);
        }

        //public ActionResult ListofRoles()
        //{
        //    UserService.UserServiceClient client = new UserService.UserServiceClient();
        //    //string xmldata = client.getr(0);

        //    return View("_ListofRoles",);
        //}

        public ActionResult DeleteUser(int UserId)
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            bool res=client.DeleteUser(UserId);
            if (res)
                return View("CreateUser");
            else
                return RedirectToAction("ListofUsers");
        }

        public ActionResult Deleterole(int RoleId)
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            client.DeleteRole(RoleId);
            return View("CreateUser");
        }

        public ActionResult Deletegroup(int GroupId)
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            client.DeleteGroup(GroupId);
            return View("CreateUser");
        }

        public ActionResult Login()
        {
            User user = new User();
            return PartialView("~/Views/Shared/_Login.cshtml", user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                UserService.UserServiceClient client = new UserService.UserServiceClient();
                string xmldata = client.Login(user.EmailId, user.UserPassword);

                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));

                if (ds.Tables.Count > 0)
                {
                    Session["UserId"] = ds.Tables[0].Rows[0]["User_ID"];
                    return RedirectToAction("ListofCompliance", "ComplianceManagement");
                }
            }
            else
                ModelState.AddModelError("", " There was an error with your E-Mail/Password combination. Please try again.");
            return View("~/Views/Shared/_Login.cshtml",user);

        }

    }
}