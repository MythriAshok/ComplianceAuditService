using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Controllers
{
    public class ComplianceManagementController : Controller
    {
        // GET: ComplianceManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateActs()
        {
            ComplianceXref model = new ComplianceXref();
            com
            return View();
        }
    }
}