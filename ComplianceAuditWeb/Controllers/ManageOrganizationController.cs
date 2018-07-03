using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Compliance.DataObject;
using ComplianceService;
using ComplianceAuditWeb.Models;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageOrganizationController : Controller
    {
        // GET: ManageOrganization
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddGroup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddGroup(OrganizationViewModel viewModel)
        {
            
            //BranchLocationService1.LocationServiceClient client = new BranchLocationService1.LocationServiceClient();
           // int id = client.insertBranchLocation(branch);
            return View();
        }
    }
}