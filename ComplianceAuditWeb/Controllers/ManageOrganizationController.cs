using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Compliance.DataObject;
using ComplianceService;
using ComplianceAuditWeb.Models;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageOrganizationController : Controller
    {
        // GET: ManageOrganization
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddGroupCompany()
        {
            OrganizationViewModel viewmodel = new OrganizationViewModel();
            viewmodel.Country = new List<Country>();
            viewmodel.State = new List<State>();
            viewmodel.City = new List<City>();

            OrganizationHierService.OrganizationServiceClient clientgroup = new OrganizationHierService.OrganizationServiceClient();
            XmlDocument xmlCountries = new XmlDocument();
            xmlCountries.LoadXml(response);
            //clientgroup.BindCountry(viewmodel.Country);
            //clientgroup.BindState(viewmodel.State);
            // clientgroup.BindCity(viewmodel.City);
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult AddGroupCompany(OrganizationViewModel viewmodel)
        {
            OrganizationHierService.OrganizationServiceClient clientorg = new OrganizationHierService.OrganizationServiceClient();
            int BranchId = clientorg.insertBranchLocation(viewmodel.branch);
            if (BranchId > 0)
            {
                viewmodel.organization.Branch_Id = BranchId;
                int OrgId = clientorg.insertOrganization(viewmodel.organization);
                if (OrgId > 0)
                {
                    viewmodel.
                    viewmodel.companydetails.Org_Hier_ID = OrgId;
                    int CompanyDetailsId = clientorg.insertCompanyDetails(viewmodel.companydetails);
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
}