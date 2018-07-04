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
            BranchLocationService.LocationServiceClient clientbranch = new BranchLocationService.LocationServiceClient();
            int BranchId = clientbranch.insertBranchLocation(viewModel.branch);
            if (BranchId > 0)
            {
                viewModel.organization.Branch_Id = BranchId;
                OrganizationHierService.OrganizationServiceClient clientorg = new OrganizationHierService.OrganizationServiceClient();
                int OrgId = clientorg.insertOrganization(viewModel.organization);
                if (OrgId > 0)
                {
                    viewModel.companydetails.Org_Hier_ID = OrgId;
                    CompanyDetailService.CompanyDetailsSeriveClient clientcompany = new CompanyDetailService.CompanyDetailsSeriveClient();
                    int CompanyDetailsId = clientcompany.insertCompanyDetails(viewModel.companydetails);
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