using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceAuditWeb.Models;
using Compliance.DataObject;
using ComplianceService;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageBranchController : Controller
    {
        // GET: ManageBranch
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddBrnch()
        {
            BranchViewModel viewmodel = new BranchViewModel();
           // viewmodel.countrylist = new List<Country>();
            viewmodel.State = new List<State>();
            viewmodel.City = new List<City>();
            BranchServices.BranchServicesClient clientbranch = new BranchServices.BranchServicesClient();
           // clientbranch.BindCountry(viewmodel.Country);
            clientbranch.BindState(viewmodel.State);
            clientbranch.BindCity(viewmodel.City);
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult AddGroup(OrganizationViewModel viewmodel)
        {
            BranchServices.BranchServicesClient clientbranch = new BranchServices.BranchServicesClient();
            int BranchId = clientbranch.insertBranchLocation(viewmodel.branch);
            if (BranchId > 0)
            {
                viewmodel.organization.Branch_Id = BranchId;
                int OrgId = clientbranch.insertOrganization(viewmodel.organization);
                if (OrgId > 0)
                {
                    viewmodel.companydetails.Org_Hier_ID = OrgId;
                    int CompanyDetailsId = clientbranch.insertCompanyDetails(viewmodel.companydetails);
                }
                else
                {
                    return View();
                }
                return View("AddGroup");
            }
            else
            {
                return View();
            }
        }
}