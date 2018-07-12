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
            ds.ReadXml(new StreamReader(xmldata));
            DataTable dt = ds.Tables[0];
            rolesView.privilege = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dt.Rows)
            {
                rolesView.privilege.Add(new SelectListItem() { Text = row["Privilege_Name"].ToString(), Value = row["Privilege_ID"].ToString() });
            }           
            return View("_insertRole", rolesView);
        }

        [HttpPost]
        public ActionResult insertRoles(RolesViewModel rolesView)
        {           
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            client.Open();
            bool result=client.insertRoles(rolesView.roles);
            client.Close();
            return View();
        }

        [HttpGet]
        public ActionResult UserGroup()
        {
            UserGroupViewModel userGroupView = new UserGroupViewModel();
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            string xmldata = client.GetRoles();
            return View("_insertRole");
        }
        [HttpPost]
        public ActionResult UserGroup(UserGroupViewModel userGroupView)
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {           
            UserViewModel userviewmodel = new UserViewModel();         
            UserService.UserServiceClient userServiceClient = new UserService.UserServiceClient();
            //string response = string.Empty;           
            string xmlGroups=userServiceClient.GetUserGroup();
            DataTable Groups = new DataTable();
            Groups.ReadXml(new StringReader(xmlGroups));
            userviewmodel.UserGroupList = (IEnumerable<UserGroup>)Groups.AsEnumerable();
            //XmlDocument xmlCountries = new XmlDocument();
            //xmlCountries.LoadXml(response);

            //userviewmodel.UserGroup = userServiceClient.BindUserGroup();
            //userServiceClient.BindUserRole(userviewmodel.roles);
            return View(userviewmodel);
        }
        [HttpPost]
        public ActionResult CreateUser(UserViewModel userviewmodel)
        {
            UserService.UserServiceClient userServiceClient = new UserService.UserServiceClient();
            bool result=userServiceClient.insertUser(userviewmodel.User);            
            return View();
        }

       
    }
}