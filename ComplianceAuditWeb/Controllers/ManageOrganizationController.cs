﻿//

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
using System.Web.Script.Serialization;

namespace ComplianceAuditWeb.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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
            ViewBag.CountryList = organizationVM.Country;

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
            
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
       //     int countryiD = 0;
       //     organizationVM.State = new List<SelectListItem>();
       //     DataSet dsStates = new DataSet();
       //     string strXMLStates = organizationClient.GetStateList(countryiD);
       //     foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
       //     {
       //         organizationVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
       //     }
       //     //using (CITYSTATEEntities cITYSTATEEntities = new CITYSTATEEntities())
       //     //{
       //     //    lstcity = (cITYSTATEEntities.CITIES.Where(x => x.StateId == stateiD)).ToList<CITy>();
       //     //}

       //     JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
       //     string results = javaScriptSerializer.Serialize(organizationVM.State);
       //    // return Json(result, JsonRequestBehavior.AllowGet);
       //// }

        bool result = false;
            organizationVM.organization.Is_Leaf = false;
            organizationVM.organization.Level = 1;
            organizationVM.organization.Is_Active = true;
            organizationVM.organization.Is_Delete = false;
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
            //int OrgID = 0;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getGroupCompany(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization  = new Organization();
            organizationViewModel.companydetails = new CompanyDetails();
            organizationViewModel.branch = new BranchLocation();
            // Organization organization = new Organization();
            //CompanyDetails companydetails = new CompanyDetails();
            // BranchLocation branch = new BranchLocation();
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
            organizationViewModel. organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
            organizationViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            //organizationViewModel.organization.Is_Active =(Boolean) dsUpdatedData.Tables[0].Rows[0]["Is_Active"];
            organizationViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            organizationViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            organizationViewModel.companydetails.Auditing_Frequency = dsUpdatedData.Tables[0].Rows[0]["Auditing_Frequency"].ToString();
            organizationViewModel.companydetails.Calender_StartDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_StartDate"]);
            organizationViewModel.companydetails.Calender_EndDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_EndDate"]);
            organizationViewModel.companydetails.Company_ContactNumber1 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber1"].ToString();
            organizationViewModel.companydetails.Company_ContactNumber2 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber2"].ToString();
            organizationViewModel.companydetails.Company_EmailID = dsUpdatedData.Tables[0].Rows[0]["Company_Email_ID"].ToString();
            organizationViewModel.companydetails.Formal_Name = dsUpdatedData.Tables[0].Rows[0]["Formal_Name"].ToString();
            organizationViewModel.companydetails.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            organizationViewModel.companydetails.Website = dsUpdatedData.Tables[0].Rows[0]["Website"].ToString();
            organizationViewModel.branch.Address = dsUpdatedData.Tables[0].Rows[0]["Address"].ToString();
            organizationViewModel.branch.Branch_Coordinates1 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates1"].ToString();
            organizationViewModel.branch.Branch_Coordinates2 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates2"].ToString();
            organizationViewModel.branch.Branch_CoordinatesURL = dsUpdatedData.Tables[0].Rows[0]["Branch_CoordinateURL"].ToString();
            organizationViewModel.branch.Branch_Name = dsUpdatedData.Tables[0].Rows[0]["Location_Name"].ToString();
            organizationViewModel.branch.Postal_Code = dsUpdatedData.Tables[0].Rows[0]["Postal_Code"].ToString();

            organizationViewModel.branch.Country_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Country_ID"]);
            DataSet dsCountries = new DataSet();
            string strXMLCountries = organizationClient.GetCountryList();

            dsCountries.ReadXml(new StringReader(strXMLCountries));
            organizationViewModel.Country = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                organizationViewModel.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
            organizationViewModel.branch.State_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["State_ID"]);

            DataSet dsStates = new DataSet();
            string strXMLStates = organizationClient.GetStateList(organizationViewModel.branch.Country_Id);

            dsStates.ReadXml(new StringReader(strXMLStates));
            organizationViewModel.State = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            {
                organizationViewModel.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            }
            organizationViewModel.branch.City_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["City_ID"]);

            DataSet dsCities = new DataSet();
            string strXMLCities = organizationClient.GetCityList(organizationViewModel.branch.State_Id);

            dsCities.ReadXml(new StringReader(strXMLCities));
            organizationViewModel.City = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            {
                organizationViewModel.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            }
            return View(organizationViewModel);
        }
        /// <summary>
        /// 
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
        public ActionResult DeactivateGroupCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View(organizationViewModel);
        }
        [HttpPost]
        public ActionResult DeactivateGroupCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeactivateGroupCompany(organizationVM.organization.Organization_Id);
            if(result == true)
            {
                return View();
            }
            else
            {
                return View();

            }
        }

        public ActionResult ActivateGroupCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View(organizationViewModel);
        }
        [HttpPost]
        public ActionResult ActivateGroupCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.ActivateGroupCompany(organizationVM.organization.Organization_Id);
            if (result == true)
            {
                return View();
            }
            else
            {
                return View();

            }
        }


        [HttpGet]
        public ActionResult DeleteGroupCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View(organizationViewModel);
        }
        [HttpPost]
        public ActionResult DeleteGroupCompany(OrganizationViewModel organizationViewModel)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeleteGroupCompany(organizationViewModel.organization.Organization_Id);
            if (result == true)
            {
                return View("ListOfGroupCompanies");
            }
            else
            {
                return View();

            }
           // return View();
        }

        [HttpGet]
        public ActionResult AddCompany()
        {
            int stateID = 1;
            int countryID = 1;
            CompanyViewModel companyVM = new CompanyViewModel();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();


            string strXMLGroupCompanyList = organizationservice.getGroupCompanyListDropDown();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            companyVM.GroupCompaniesList = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
            {
                companyVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            }
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
            companyVM.organization.Parent_Company_Id = companyVM.GroupCompanyID;

            result = organizationClient.insertCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);
            
            if (result != false)
            {
                return RedirectToAction("AddBranch");
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
            int id = 2;
            BranchViewModel branchVM = new BranchViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

            string strXMLGroupCompanyList = organizationservice.getGroupCompanyListDropDown();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            branchVM.GroupCompaniesList = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
            {
                branchVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            }


            string strXMLCompanyList = organizationservice.getCompanyListDropDown(id);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            branchVM.CompaniesList = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
            {
                branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            }


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
            branchVM.organization.Parent_Company_Id = branchVM.CompanyID;
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
                ListOfGroupCompanies listOfGroup = new ListOfGroupCompanies { OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    IsActive =Convert.ToBoolean(Convert.ToInt32( row["Is_Active"]))

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