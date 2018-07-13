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
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult insertRoles()
        {
            RolesViewModel rolesView = new RolesViewModel();
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            string xmldata = client.GetPrivilege(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            rolesView.privilege = new List<SelectListItem>();
            if (ds!=null)
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                rolesView.privilege.Add(new SelectListItem() { Text = row["Privilege_Name"].ToString(), Value = row["Privilege_ID"].ToString() });
                }            
            return View("_insertRole", rolesView);
        }

        [HttpPost]
        public ActionResult insertRoles(RolesViewModel rolesView)
        {           
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            int roleid=client.insertRoles(rolesView.roles);                        
            client.insertRolePrivilege(roleid, rolesView.PrivilegeId);                                
            return View("CreateUser");
        }

        [HttpGet]
        public ActionResult UserGroup()
        {
            UserGroupViewModel GroupView = new UserGroupViewModel();
            UserService.UserServiceClient Client = new UserService.UserServiceClient();          
            string xmldata = Client.GetRoles(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            GroupView.Roles = new List<SelectListItem>();
            GroupView.Roles.Add(new SelectListItem { Text = "--Select Roles--", Value = "0" });
            foreach(System.Data.DataRow row in ds.Tables[0].Rows)
            {
                GroupView.Roles.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
            }
            return View("_insertUserGroup", GroupView);
        }
        [HttpPost]
        public ActionResult UserGroup(UserGroupViewModel GroupView)
        {            
            UserService.UserServiceClient Client = new UserService.UserServiceClient();           
            Client.insertGroups(GroupView.Group);            
            return View();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {           
            UserViewModel userviewmodel = new UserViewModel();         
            UserService.UserServiceClient Client = new UserService.UserServiceClient();                              
            string xmlGroups=Client.GetUserGroup(0);
            DataSet Groups = new DataSet();
            Groups.ReadXml(new StringReader(xmlGroups));
            userviewmodel.UserGroupList = new List<SelectListItem>();
            userviewmodel.UserGroupList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (System.Data.DataRow row in Groups.Tables[0].Rows)
            {
                userviewmodel.UserGroupList.Add(new SelectListItem { Text = row["User_Group_Name"].ToString(), Value =row["User_Group_ID"].ToString() });
            }
            string xmlRoles=Client.GetRoles(0);
            Groups.ReadXml(new StringReader(xmlRoles));
            userviewmodel.RolesList= new List<SelectListItem>();
            userviewmodel.RolesList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (System.Data.DataRow row in Groups.Tables[0].Rows)
            {
                userviewmodel.RolesList.Add(new SelectListItem { Text = row["Role_Name"].ToString(), Value = row["Role_ID"].ToString() });
            }
            return View("_insertUser", userviewmodel);
        }
        [HttpPost]
        public ActionResult CreateUser(UserViewModel userviewmodel)
        {
            UserService.UserServiceClient Client = new UserService.UserServiceClient();          
            bool result=Client.insertUser(userviewmodel.User);
           
            return View();
        }

       
    }
}