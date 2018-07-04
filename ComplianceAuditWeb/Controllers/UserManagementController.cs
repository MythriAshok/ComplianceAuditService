using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceAuditWeb.Models;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRoles()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateUser()
        {           
            UserViewModel userviewmodel = new UserViewModel();
            userviewmodel.UserGroup = new List<UserGroup>();
            UserService.UserServiceClient userServiceClient = new UserService.UserServiceClient();
            //userviewmodel.UserGroup = userServiceClient.BindUserGroup();
            //BindUserRole(userviewmodel.roles);
            return View(userviewmodel);
        }
        [HttpPost]
        public ActionResult CreateUser(UserViewModel userviewmodel)
        {
            UserService.UserServiceClient userServiceClient = new UserService.UserServiceClient();
            string msg=userServiceClient.insertUser(userviewmodel.User);            
            return View();
        }
    }
}