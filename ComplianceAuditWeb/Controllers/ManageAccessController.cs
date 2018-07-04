using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceAuditWeb.Models;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageAccessController : Controller
    {
        // GET: ManageAccess
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccessManagement accessmanagement)
        {
            AccessManagementServices.AccessManagementServicesClient clientaccess = new AccessManagementServices.AccessManagementServicesClient();
            int UId = clientaccess.GetLoginData(accessmanagement);
            if (UId != 0)
            {
                return View("");
            }
            else
            {
                return View();
            }
        }
    }
}