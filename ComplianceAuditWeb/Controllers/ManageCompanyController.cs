using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceService;
using Compliance.DataObject;
using ComplianceAuditWeb.Models;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageCompanyController : Controller
    {
        // GET: ManageCompany
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddCompany()
        {
            CompanyViewModel viewmodel = new CompanyViewModel();
            viewmodel.countrylist = new List<Country>();
            CompanyServices.CompanyServicesClient clientcompany = new CompanyServices.CompanyServicesClient();
            clientgroup.BindCountry(viewmodel.countrylist);
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult AddCompany(CompanyViewModel viewmodel)
        {
            CompanyServices.CompanyServicesClient clientcompany = new CompanyServices.CompanyServicesClient();
            int BranchId = clientcompany.insertBranchLocation(viewmodel.branch);
            if (BranchId > 0)
            {
                viewmodel.organization.Branch_Id = BranchId;
                int OrgId = clientcompany.insertOrganization(viewmodel.organization);
                if (OrgId > 0)
                {
                    viewmodel.companydetails.Org_Hier_ID = OrgId;
                    int CompanyDetailsId = clientcompany.insertCompanyDetails(viewmodel.companydetails);
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
