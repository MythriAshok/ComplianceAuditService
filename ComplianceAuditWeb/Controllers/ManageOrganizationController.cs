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
        /// <summary>
        /// [HttpGet] Action method in the controller to add group company 
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult AddGroupCompany()
        {
            int stateID = 1;
            int countryID = 1;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);
            DataSet dsCountries = new DataSet();
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
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
/// <summary>
/// [HttpPost] Action method to add the group company
/// </summary>
/// <param name="organizationVM"></param>
/// <returns>View</returns>
[HttpPost]
        public ActionResult AddGroupCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            organizationVM.organization.Is_Leaf = false;
            organizationVM.organization.Level = 1;
            organizationVM.organization.Is_Active = true;
            organizationVM.companydetails.Is_Active = true;
            organizationVM.organization.Parent_Company_Id = 0;
            result = organizationClient.insertOrganization(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if (result != false)
            {
                return RedirectToAction("AddCompany");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// Action method to update the Group company
        /// </summary>
        /// <param name="OrgID"></param>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult UpdateGroupCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getGroupCompany(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
            organizationViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
            organizationViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
            organizationViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            organizationViewModel.organization.Is_Active = Convert.ToBoolean(dsUpdatedData.Tables[0].Rows[0]["Is_Active"]);
            organizationViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            organizationViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            organizationViewModel.companydetails.Auditing_Frequency = dsUpdatedData.Tables[0].Rows[0]["Auditing_Frequency"].ToString();
            organizationViewModel.companydetails.Calender_StartDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_StartDate"]);
            organizationViewModel.companydetails.Calender_EndDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_EndDate"]);
            organizationViewModel.companydetails.Company_ContactNumber1 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber1"].ToString();
            organizationViewModel.companydetails.Company_ContactNumber2= dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber2"].ToString();
            organizationViewModel.companydetails.Company_EmailID= dsUpdatedData.Tables[0].Rows[0]["Company_Email_ID"].ToString();
            organizationViewModel.companydetails.Formal_Name= dsUpdatedData.Tables[0].Rows[0]["Formal_Name"].ToString();
            organizationViewModel.companydetails.Industry_Type= dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            organizationViewModel.companydetails.Website= dsUpdatedData.Tables[0].Rows[0]["Website"].ToString();
            organizationViewModel.branch.Address= dsUpdatedData.Tables[0].Rows[0]["Address"].ToString();
            organizationViewModel.branch.Branch_Coordinates1= dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates1"].ToString();
            organizationViewModel.branch.Branch_Coordinates2= dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates2"].ToString();
            organizationViewModel.branch.Branch_CoordinatesURL= dsUpdatedData.Tables[0].Rows[0]["Branch_CoordinatesURL"].ToString();
            organizationViewModel.branch.Branch_Name= dsUpdatedData.Tables[0].Rows[0]["Branch_Name"].ToString();
            organizationViewModel.branch.Country_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Country_Id"]);
            organizationViewModel.branch.City_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["City_ID"]);
            organizationViewModel.branch.State_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["State_ID"]);
            organizationViewModel.branch.Postal_Code = dsUpdatedData.Tables[0].Rows[0]["Postal_Code"].ToString();
            return View(organizationViewModel);
        }
        /// <summary>
        /// Action method to update group company and save updated values 
        /// </summary>
        /// <param name="organizationVM"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Action method to get group company
        /// </summary>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult GetGroupCompany()
        {
            return View();
        }
        /// <summary>
        /// Action method to get the group company
        /// </summary>
        /// <param name="organizationVM"></param>
        /// <returns>view</returns>
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
            CompanyViewModel companyVM = new CompanyViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();



            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);



            DataSet dsCountries = new DataSet();
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
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
            companyVM.companydetails.Is_Active = true;
            companyVM.organization.Is_Active = true;
            companyVM.organization.Level = 2;
            companyVM.organization.Is_Leaf = false;
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
            return View();
        }

        [HttpGet]
        public ActionResult AddBranch()
        {
            int stateID = 1;
            int countryID = 1;
            BranchViewModel branchVM = new BranchViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();



            string strXMLCountries = organizationservice.GetCountryList();
            string strXMLStates = organizationservice.GetStateList(countryID);
            string strXMLCities = organizationservice.GetCityList(stateID);



            DataSet dsCountries = new DataSet();
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
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
            branchVM.organization.Is_Active = true;
            branchVM.organization.Level = 3;
            branchVM.organization.Is_Leaf = true;
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
            return View();
        }

        [HttpGet]
        public ActionResult ListOfGroupCompanies()
        {
            List<ListOfGroupCompanies> grouplist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlGroupCompanies = organizationservice.GetGroupCompaniesList();

           
            DataSet dsGroupCompaniesList = new DataSet();
            dsGroupCompaniesList.ReadXml(new StringReader(strxmlGroupCompanies));
            foreach(System.Data.DataRow row in dsGroupCompaniesList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfGroup = new ListOfGroupCompanies { CompanyID = Convert.ToInt32(row["Company_ID"]), CompanyName = row["Company_Name"].ToString(),
                    IndustryType = row["Industry_Type"].ToString()
                };
                //GroupCompanyLogo = row["Group_Company_Logo"].ToString(), IndustryType = row["Industry_Type"].ToString() };
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