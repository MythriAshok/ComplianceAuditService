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
                return RedirectToAction("AddCompany");
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
            //OrgID = 9;
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getGroupCompany(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));

            organizationViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["CompanyName"].ToString();
            organizationViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["CompanyName"].ToString();
            organizationViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["CompanyName"].ToString();
            organizationViewModel.organization.Is_Active = dsUpdatedData.Tables[0].Rows[0]["CompanyName"].ToString();
            organizationViewModel.organization.Last_Updated_Date =
                organizationViewModel.organization.User_Id=
                organizationViewModel.companydetails.Auditing_Frequency
                organizationViewModel.companydetails.Calender_StartDate
                organizationViewModel.companydetails.Calender_EndDate
                organizationViewModel.companydetails.Company_ContactNumber1
                organizationViewModel.companydetails.Company_ContactNumber2
                organizationViewModel.companydetails.Company_EmailID
                organizationViewModel.companydetails.Formal_Name
                organizationViewModel.companydetails.Industry_Type
                organizationViewModel.companydetails.Website
                organizationViewModel.branch.Address
                organizationViewModel.branch

                organizationViewModel.branch.Address

                organizationViewModel.branch.Address

                organizationViewModel.branch.Address

                organizationViewModel.branch.Address

                organizationViewModel.branch.Address

                organizationViewModel.branch.Address


                or
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
            int stateID = 1;
            int countryID = 1;
            //OrganizationViewModel organizationVM = new OrganizationViewModel();
            CompanyViewModel companyVM = new CompanyViewModel();

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
            companyVM.Country = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                companyVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }


            companyVM.State = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            {
                companyVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            }


            companyVM.City = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            {
                companyVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            }
            return View("AddCompany", companyVM);

        }
        [HttpPost]
        public ActionResult AddCompany(CompanyViewModel companyVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            result = organizationClient.insertCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);
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
            int stateID = 1;
            int countryID = 1;
            //OrganizationViewModel organizationVM = new OrganizationViewModel();
            BranchViewModel branchVM = new BranchViewModel();

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
            branchVM.Country = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                branchVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }


            branchVM.State = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            {
                branchVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            }


            branchVM.City = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            {
                branchVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            }
            return View("AddBranch", branchVM);
        }
        [HttpPost]
        public ActionResult AddBranch(BranchViewModel branchVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            result = organizationClient.insertBranch(branchVM.organization, branchVM.branch);
            if (result != false)
            {
                return View("View");
            }
            else
            {
                return View("View");
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

        [HttpGet]
        public ActionResult ListOfGroupCompanies()
        {
            List<ListOfGroupCompanies> grouplist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlGroupCompanies = organizationservice.GetGroupCompaniesList();

            grouplist.GroupCompanies = strxmlGroupCompanies.AsEnumerable(); // this needs to be cast

            //(IEnumerable<ListOfGroupCompanies>) strxmlGroupCompanies.AsEnumerable();

            
            
            //ListOfGroupCompanies.Add(grouplist);

           // var List = strxmlGroupCompanies.AsEnumerable();

            // ListOfGroupCompanies.Add()
            DataSet dsGroupCompaniesList = new DataSet();
            dsGroupCompaniesList.ReadXml(new StringReader(strxmlGroupCompanies));
            foreach(System.Data.DataRow row in dsGroupCompaniesList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfGroup = new ListOfGroupCompanies { CompanyID = Convert.ToInt32(row[""]), CompanyName = row[""].ToString(),
                    GroupCompanyLogo = row[""].ToString(), IndustryType = row[""].ToString() };
                grouplist.Add(listOfGroup);
            }          
            return View(grouplist);
        }
        [HttpPost]
        public ActionResult ListOfGroupCompanies(ListOfGroupCompanies ListOfGroupCompanies)
        {
            return View();
        }

    }

}