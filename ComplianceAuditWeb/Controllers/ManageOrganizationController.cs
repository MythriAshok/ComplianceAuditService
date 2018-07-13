using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceService;
using ComplianceAuditWeb.Models;
using Compliance.DataObject;
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

        public ActionResult AddGroupCompany()
        {
            int stateID = 1;
            int countryID = 1;
            OrganizationViewModel organizationVM = new OrganizationViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();



            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
          string strXMLCities = organizationservice.GetCityList(stateID);


            //organizationVM.Country.ReadXml(new StringReader(strXMLCountries));

           // DataTable dt = new DataTable();
            DataSet dsCountries = new DataSet();
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
            //dt.ReadXml(new StringReader(strXMLCountries));
            organizationVM.Country = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                organizationVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
        

        organizationVM.State = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            {
                organizationVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            }


            organizationVM.City = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            {
                organizationVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            }
            return View("AddGroupCompany", organizationVM);
}



//organizationVM.State.ReadXml(new StringReader(strXMLStates));
//organizationVM.City.ReadXml(new StringReader(strXMLCities));


//     //organizationVM.Country.AsEnumerable();


//    return View(organizationVM);



[HttpPost]
        public ActionResult AddGroupCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            //if (ModelState.IsValid)
            //{
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                organizationVM.organization.Is_Leaf = false;
                organizationVM.organization.Level = 1;
                result = organizationClient.insertOrganization(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
                if (result != false)
                {
                    return View("AddCompany");
                }
                else
                {
                    return View();
                }
            
           // return View();
        }
        [HttpGet]
        public ActionResult UpdateGroupCompany(int OrgID)
        {
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            Session["data"]= organizationClient.getGroupCompany(OrgID);
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel = (OrganizationViewModel)Session["data"];
            return View(organizationViewModel);
        }
        [HttpPost]
        public ActionResult UpdateGroupCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            result = organizationClient.updateOrganization(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if (result != false)
            {
                return View("UpdateGroupCompany");
            }
            else
            {
                return View();
            }
        
        }
        [HttpGet]
        public ActionResult GetGroupCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetGroupCompany(OrganizationViewModel organizationVM)
        {
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            organizationServiceClient.getGroupCompany(organizationVM.organization.Organization_Id);
            return View();
        }

        [HttpGet]
        public ActionResult AddCompany()
        {
            int stateID = 0;
            int countryID = 0;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);


            //organizationVM.Country.ReadXml(new StringReader(strXMLCountries));
            //organizationVM.State.ReadXml(new StringReader(strXMLStates));
            //organizationVM.City.ReadXml(new StringReader(strXMLCities));
            return View(organizationVM);
            
        }
        [HttpPost]
        public ActionResult AddCompany(CompanyViewModel companyVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            result = organizationClient.insertCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);
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
        public ActionResult UpdateCompany(CompanyViewModel companyVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            result = organizationClient.updateCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);
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
        public ActionResult GetCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetCompany(CompanyViewModel companyVM)
        {
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
           // organizationServiceClient.GetCompany();
            return View();
        }

        [HttpGet]
        public ActionResult AddBranch()
        {
            int stateID = 0;
            int countryID = 0;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);


            //organizationVM.Country.ReadXml(new StringReader(strXMLCountries));
            //organizationVM.State.ReadXml(new StringReader(strXMLStates));
            //organizationVM.City.ReadXml(new StringReader(strXMLCities));
            return View(organizationVM);
        }
        [HttpPost]
        public ActionResult AddBranch(BranchViewModel branchVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            result = organizationClient.insertBranch(branchVM.organization, branchVM.branch);
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
        public ActionResult UpdateBranch(BranchViewModel branchVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            result = organizationClient.updateBranch(branchVM.organization, branchVM.branch);
            if (result != false)
            {
                return View("UpdateBranch");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult GetBranch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetBranch(BranchViewModel branchVM)
        {
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
           // organizationServiceClient.GetBranch();
            return View();
        }

    }
}