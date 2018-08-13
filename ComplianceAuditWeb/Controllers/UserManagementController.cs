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
using System.Configuration;

namespace ComplianceAuditWeb.Controllers
{
    public class UserManagementController : Controller
    {
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
            rolesView.Privilege = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {

                    rolesView.Privilege.Add(new SelectListItem() { Text = row["Privilege_Name"].ToString(), Value = row["Privilege_ID"].ToString() });
                }
            }
            return View("_AddRole", rolesView);
        }

        //Post:insertRoles
        [HttpPost]
        public ActionResult AddRoles(RolesViewModel rolesView)
        {
            if (ModelState.IsValid)
            {
                UserService.UserServiceClient client = new UserService.UserServiceClient();
                int roleid = client.insertRoles(rolesView.roles);
                client.insertRolePrivilege(roleid, rolesView.PrivilegeId);
                TempData["Message"] = "Created " + rolesView.roles.RoleName + " Succesfully.";
                return RedirectToAction("AddRoles");
            }
            else
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            return View("_AddRole", rolesView);
        }

        //Get:updateRoles
        [HttpGet]
        public ActionResult updateRoles(int Roleid)
        {
            RolesViewModel rolesView = new RolesViewModel();
            rolesView.roles = new Roles();
            rolesView.roles.RoleId = Roleid;
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            DataSet dsrole = new DataSet();
            string xmldata=client.GetAllRoles(Roleid);
            dsrole.ReadXml(new StringReader(xmldata));
            rolesView.roles.RoleName = Convert.ToString(dsrole.Tables[0].Rows[0]["Role_Name"]);
            rolesView.roles.IsGroupRole = Convert.ToBoolean(Convert.ToInt32(dsrole.Tables[0].Rows[0]["Is_Group_Role"]));
            xmldata = client.GetPrivilege();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            rolesView.Privilege = new List<SelectListItem>();
            dsrole = new DataSet();
            xmldata = client.getRolePrivilege(rolesView.roles.RoleId);
            dsrole.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    bool selected = false;
                    if (dsrole.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow roleid in dsrole.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(roleid["Privilege_ID"]) == Convert.ToInt32(row["Privilege_ID"]))
                            {
                                selected = true;
                                break;
                            }
                        }
                    }
                    rolesView.Privilege.Add(new SelectListItem() { Text = row["Privilege_Name"].ToString(), Value = row["Privilege_ID"].ToString(), Selected = selected });
                }
            }
            return View("_AddRole", rolesView);
        }

        //Post:updateRoles
        [HttpPost]
        public ActionResult updateRoles(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserService.UserServiceClient client = new UserService.UserServiceClient();
                client.updateRoles(model.roles);
                client.DeleteRolePrivilege(model.roles.RoleId);
                client.insertRolePrivilege(model.roles.RoleId, model.PrivilegeId);
                return View("_AddRole", model);
            }
            else
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            return View("_AddRole", model);
        }

        //Get:AddUserGroup
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
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    GroupView.Roles.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
                }
            }
            ds = new DataSet();
            GroupView.Featurelist = new List<SelectListItem>();
            xmldata = Client.getmenu();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    GroupView.Featurelist.Add(new SelectListItem { Text = row["Menu_Name"].ToString(), Value = row["Menu_ID"].ToString() });
                }
            }
            return View("_AddUserGroup", GroupView);
        }

        //Post:AddUserGroup
        [HttpPost]
        public ActionResult AddUserGroup(UserGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserService.UserServiceClient Client = new UserService.UserServiceClient();
                Client.insertGroups(model.Group);
                return View("CreateUser");
            }
            else
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            return View("_AddUserGroup", model);
        }

        //Get:Updateusergroup
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
            if (dsrole.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.Roles.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
                }
            }
            return View("_AddUserGroup", model);
        }

        //Post:Updateusergroup
        [HttpPost]
        public ActionResult Updateusergroup(UserGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserService.UserServiceClient Client = new UserService.UserServiceClient();
                Client.updateGroups(model.Group);
                return View("ListofUsergroup");
            }
            else
            ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            return View("_AddUserGroup", model);
        }

        //Get:AddUser
        [HttpGet]
        public ActionResult AddUser()
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
            //if (dsRoles.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in dsRoles.Tables[0].Rows)
            //    {
            //        userviewmodel.RolesList.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
            //    }
            //}
            TempData["UserGroup"] = userviewmodel.UserGroupList;
            return View("_AddUser", userviewmodel);
        }

        //Post:AddUser
        [HttpPost]
        public ActionResult AddUser(UserViewModel model, HttpPostedFileBase file)
        {
                UserService.UserServiceClient Client = new UserService.UserServiceClient();
                if (ModelState.IsValid)
                {
                if (file != null && file.ContentLength > 0)
                {
                    CommonController common = new CommonController();
                    model.User.photo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    //ModelState.AddModelError("User.Photo", message); 
                }
                    string res = Client.insertUser(model.User);
                    if (res != "EXISTS")
                    {
                        model.User.UserId = Convert.ToInt32(res);
                        Client.insertUserGroupmember(model.User.UserId, model.UserGroupID);
                    //Client.insertUserRole(model.User.UserId, model.RoleID);
                    return RedirectToAction("ListofUsers");
                    }
                    else
                        ModelState.AddModelError("User.EmailId", "UserName is already Exists");
                
                }
                else
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
                model.User.UserId = 0;
            model.UserGroupList = (List<SelectListItem>)TempData["UserGroup"];
                //string xmlGroups = Client.GetUserGroup(0);
                //DataSet Groups = new DataSet();
                //Groups.ReadXml(new StringReader(xmlGroups));
                //model.UserGroupList = new List<SelectListItem>();
                //foreach (System.Data.DataRow row in Groups.Tables[0].Rows)
                //{
                //    model.UserGroupList.Add(new SelectListItem { Text = row["User_Group_Name"].ToString(), Value = row["User_Group_ID"].ToString() });
                //}
                //string xmlRoles = Client.GetRoles(0);
                //DataSet dsRoles = new DataSet();
                //dsRoles.ReadXml(new StringReader(xmlRoles));
                //model.RolesList = new List<SelectListItem>();
                //foreach (System.Data.DataRow row in dsRoles.Tables[0].Rows)
                //{
                //    model.RolesList.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
                //}            
                
            return View("_AddUser", model);
        }

        //Get:UpateUser
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
            model.User.photo = Convert.ToString(ds.Tables[0].Rows[0]["Photo"]);
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
            //ds = new DataSet();
            //xmldata = Client.GetRoles(0);
            //ds.ReadXml(new StringReader(xmldata));
            //xmldata = Client.getUserRoles(model.User.UserId);
            //dsgroup = new DataSet();
            //dsgroup.ReadXml(new StringReader(xmldata));
            //if (ds.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //    {
            //        bool selected = false;
            //        if (ds.Tables.Count > 0)
            //            foreach (System.Data.DataRow roleid in dsgroup.Tables[0].Rows)
            //            {
            //                if (Convert.ToInt32(roleid["Role_ID"]) == Convert.ToInt32(row["Role_ID"]))
            //                {
            //                    selected = true;
            //                    break;
            //                }
            //            }
            //        model.RolesList.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString(), Selected = selected });
            //    }
            //}
            return View("_AddUser",model);
        }

        //Post:UpateUser
        [HttpPost]
        public ActionResult UpdateUser(UserViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    CommonController common = new CommonController();
                    model.User.photo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("User.Photo", message); 
                }
                UserService.UserServiceClient Client = new UserService.UserServiceClient();
                model.User.IsActive = true;
                Client.updateUser(model.User);
                Client.UpdateUserGroupMember(model.User.UserId);
                Client.insertUserGroupmember(model.User.UserId, model.UserGroupID);
                Client.UpdateUserRole(model.User.UserId);
                Client.insertUserRole(model.User.UserId, model.RoleID);
                return View("ListofUsers");
            }
            else
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            return View("ListofUsers");
        }
       
        //
        public ActionResult ListofUsers()
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            string xmldata = client.getAllUser(Convert.ToInt32(Session["GroupCompanyId"]));
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            List<User> userlist = new List<User>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    userlist.Add(new User { UserId = Convert.ToInt32(row["User_ID"]),
                        FirstName = Convert.ToString(row["First_Name"]),
                        MiddleName= Convert.ToString(row["Middle_Name"]),
                        Gender= Convert.ToString(row["Gender"]),
                        ContactNumber= Convert.ToString(row["Contact_Number"]),
                        LastLogin= Convert.ToDateTime(row["Last_Login"]),
                        IsActive= Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        LastName = Convert.ToString(row["Last_Name"]),
                        EmailId = Convert.ToString(row["Email_ID"]) ,
                        photo= Convert.ToString(row["Photo"])
                    });
                }
            }
            return View("_ListofUsers", userlist);
        }

        public ActionResult ListofRoles()
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            string xmldata = client.GetAllRoles(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            RolesViewModel model = new RolesViewModel();
            model.roles = new Roles();
            model.Privilege = new List<SelectListItem>();
            List<RolesViewModel> roles = new List<RolesViewModel>();
            DataSet dsprivilege = new DataSet();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model = new RolesViewModel();
                    model.roles = new Roles();
                    model.Privilege = new List<SelectListItem>();
                    model.roles.RoleId = Convert.ToInt32(row["Role_ID"]);
                    model.roles.RoleName = Convert.ToString(row["Role_Name"]);
                    model.roles.IsGroupRole = Convert.ToBoolean(Convert.ToInt32(row["Is_Group_Role"]));
                    model.roles.IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]));
                    xmldata = client.getRolePrivilege(model.roles.RoleId);
                    dsprivilege.ReadXml(new StringReader(xmldata));
                    if (dsprivilege.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow item in dsprivilege.Tables[0].Rows)
                        {
                            model.Privilege.Add(new SelectListItem { Text = Convert.ToString(item["Privilege_Name"]), Value = Convert.ToString(item["Privilege_ID"]) });
                        }
                    }
                    xmldata = string.Empty;
                    dsprivilege.Clear();
                    roles.Add(model);
                }
            }

            return View("_ListofRoles", roles);
        }

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
            return RedirectToAction("");
        }

        public ActionResult Deletegroup(int GroupId)
        {
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            client.DeleteGroup(GroupId);
            return View("CreateUser");
        }

        public ActionResult Login()
        {
            LoginViewModel user = new LoginViewModel();
            return PartialView("~/Views/Shared/_Login.cshtml", user);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel user)
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
                    string fullname = ds.Tables[0].Rows[0]["First_Name"].ToString() + " " + ds.Tables[0].Rows[0]["Last_Name"].ToString();
                    Session["username"] = fullname;
                    Session["emailid"] = ds.Tables[0].Rows[0]["Email_ID"];
                    Session["GroupCompanyId"] = ds.Tables[0].Rows[0]["Company_ID"];
                    Session["Last_Login"] = ds.Tables[0].Rows[0]["Last_Login"];
                    Session["photo"] = ds.Tables[0].Rows[0]["Photo"];
                    return RedirectToAction("dashboard", "common",new { pid=0});
                    //return View("~/Views/UserManagement/Landing_Page.cshtml");
                }
                else
                    ModelState.AddModelError("",ConfigurationManager.AppSettings["Login_error"]);
            }
            else
                ModelState.AddModelError("",ConfigurationManager.AppSettings["Login_error_null"]);
            return View("~/Views/Shared/_Login.cshtml",user);

        }
        public ActionResult Logout()
        {
            return View("~/Views/Shared/Logout.cshtml");
        }       

    }
}