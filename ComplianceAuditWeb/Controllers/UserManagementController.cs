﻿using System;
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
        [HttpPost]
        public ActionResult insertRoles(Roles roles)
        {           
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            client.Open();
            string result=client.insertRoles(roles);
            client.Close();
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
            string msg=userServiceClient.insertUser(userviewmodel.User);            
            return View();
        }

        [HttpGet]
        public ActionResult UserGroup()
        {
            return View();
        }
    }
}