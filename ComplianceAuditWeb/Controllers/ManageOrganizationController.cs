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
        
        [HttpGet]
        public ActionResult AddGroupCompany()
        {
            int stateID=0;
            int countryID=0;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            OrganizationService.OrganizationServiceClient organizationservice = new OrganizationService.OrganizationServiceClient();

            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);


            organizationVM.Country.ReadXml(new StringReader(strXMLCountries));
            organizationVM.State.ReadXml(new StringReader(strXMLStates));
            organizationVM.City.ReadXml(new StringReader(strXMLCities));

            organizationVM.Country.AsEnumerable();

            
            return View(organizationVM);
        }

        [HttpPost]
        public ActionResult AddGroupCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrganizationService.OrganizationServiceClient organizationClient = new OrganizationService.OrganizationServiceClient();
            result = organizationClient.insertOrganization(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if( result!= false)
            {
                return View("AddGroupCompany");
            }
            else
            { 
                return View();
            }
        }
        [HttpGet]
        public ActionResult UpdateGroupCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateGroupCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrganizationService.OrganizationServiceClient organizationClient = new OrganizationService.OrganizationServiceClient();
            result = organizationClient.updateOrganization(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if(result!= false)
            {
                return View("UpdateGroupCompany");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult AddCompany()
        {
            int stateID = 0;
            int countryID = 0;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            OrganizationService.OrganizationServiceClient organizationservice = new OrganizationService.OrganizationServiceClient();

            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);


            organizationVM.Country.ReadXml(new StringReader(strXMLCountries));
            organizationVM.State.ReadXml(new StringReader(strXMLStates));
            organizationVM.City.ReadXml(new StringReader(strXMLCities));
            return View(organizationVM);
        }
        [HttpPost]
        public ActionResult AddCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrganizationService.OrganizationServiceClient organizationClient = new OrganizationService.OrganizationServiceClient();
            result = organizationClient.insertCompany(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if (result != false)
            {
                return View("AddCompany");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult UpdateCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrganizationService.OrganizationServiceClient organizationClient = new OrganizationService.OrganizationServiceClient();
            result = organizationClient.updateCompany(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if (result != false)
            {
                return View("UpdateCompany");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult AddBranch()
        {
            int stateID = 0;
            int countryID = 0;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            OrganizationService.OrganizationServiceClient organizationservice = new OrganizationService.OrganizationServiceClient();

            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);


            organizationVM.Country.ReadXml(new StringReader(strXMLCountries));
            organizationVM.State.ReadXml(new StringReader(strXMLStates));
            organizationVM.City.ReadXml(new StringReader(strXMLCities));
            return View(organizationVM);
        }
        [HttpPost]
        public ActionResult AddBranch(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrganizationService.OrganizationServiceClient organizationClient = new OrganizationService.OrganizationServiceClient();
            result = organizationClient.insertBranch(organizationVM.organization,  organizationVM.branch);
            if (result != false)
            {
                return View("AddBranch");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult UpdateBranch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateBranch(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrganizationService.OrganizationServiceClient organizationClient = new OrganizationService.OrganizationServiceClient();
            result = organizationClient.updateBranch(organizationVM.organization,  organizationVM.branch);
            if (result != false)
            {
                return View("UpdateBranch");
            }
            else
            {
                return View();
            }
        }

    }
}