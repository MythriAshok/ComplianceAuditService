using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceService;
using ComplianceAuditWeb.Models;
//using Compliance.DataObject;
using System.Xml;
using System.Data;
using System.IO;

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
            int stateID=0;
            int countryID=0;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            //viewmodel.Country = new List<Country>();
            //viewmodel.State = new List<State>();
            //viewmodel.City = new List<City>();
            OrganizationService.OrganizationServiceClient organizationservice = new OrganizationService.OrganizationServiceClient();

            string strXMLCities = organizationservice.GetCityList(stateID);
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCountries = organizationservice.GetCountryList();

            organizationVM.City.ReadXml(new StringReader(strXMLCities));
            organizationVM.City.ReadXml(new StringReader(strXMLCities));
            organizationVM.City.ReadXml(new StringReader(strXMLCities));







            //string response = string.Empty;
            //XmlDocument xmlCountries = new XmlDocument();
            //xmlCountries.LoadXml(response);

            //string str = clientgroup.GetCityList();
            //foreach(string item in strlist)
            //{

            //}
            //strlist = str;


            //clientgroup.BindCountry(viewmodel.Country);
            //clientgroup.BindState(viewmodel.State);
            // clientgroup.BindCity(viewmodel.City);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult AddGroupCompany(OrganizationViewModel viewmodel)
        {
            OrganizationService.OrganizationServiceClient clientorg = new OrganizationService.OrganizationServiceClient();
            int BranchId = clientorg.insertBranchLocation(viewmodel.branch);
            if (BranchId > 0)
            {
                viewmodel.organization.Branch_Id = BranchId;
                int OrgId = clientorg.insertOrganization(viewmodel.organization);
                if (OrgId > 0)
                {
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