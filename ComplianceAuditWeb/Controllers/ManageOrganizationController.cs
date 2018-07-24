#region Code History
/*CODE HISTORY
 * ============================================================================================================
 *  Version No      DATE       Developer Name        Description
 * ===========================================================================================================
 *  1.0          28-06-2018    Mythri A        DataAccess Layer for UserPrivilege
 *                                                  The methods defined here are getRolePrivilege() and getPrivilege().
 *  
 */
#endregion

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
            int stateID = 0;
            int countryID = 0;
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            organizationVM.organization = new Organization();
            organizationVM.organization.Organization_Id = 0;
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
            if (dsStates.Tables.Count > 0)
            {
                organizationVM.State = new List<SelectListItem>();
                foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
                {
                    organizationVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }


            if (dsCities.Tables.Count > 0)
            {
                organizationVM.City = new List<SelectListItem>();
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    organizationVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            return View("_Organization", organizationVM);
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
            string strXMLStates =   organizationClient.GetStateList(organizationVM.branch.Country_Id);
            DataSet dsStates = new DataSet();
            dsStates.ReadXml(new StringReader(strXMLStates));
            organizationVM.State = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            {
                organizationVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            }



            bool result = false;
            organizationVM.organization.User_Id = 1;
            organizationVM.organization.Is_Leaf = false;
            organizationVM.organization.Level = 1;
            organizationVM.organization.Is_Active = true;
            organizationVM.organization.Is_Delete = false;
            organizationVM.organization.Parent_Company_Id = 0;
            result = organizationClient.insertOrganization(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if (result != false)
            {
                return RedirectToAction("ListOfGroupCompanies");
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
           
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getGroupCompany(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization  = new Organization();
            organizationViewModel.companydetails = new CompanyDetails();
            organizationViewModel.branch = new BranchLocation();
           
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Organization_Id =Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            organizationViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
            organizationViewModel. organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
            organizationViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            //organizationViewModel.organization.Is_Active =(Boolean) dsUpdatedData.Tables[0].Rows[0]["Is_Active"];
            organizationViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            organizationViewModel.organization.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            organizationViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            organizationViewModel.companydetails.Company_Details_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Company_Details_ID"]);
            organizationViewModel.companydetails.Org_Hier_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            organizationViewModel.companydetails.Auditing_Frequency = dsUpdatedData.Tables[0].Rows[0]["Auditing_Frequency"].ToString();
            organizationViewModel.companydetails.Calender_StartDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_StartDate"]);
            organizationViewModel.companydetails.Calender_EndDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_EndDate"]);
            organizationViewModel.companydetails.Company_ContactNumber1 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber1"].ToString();
            organizationViewModel.companydetails.Company_ContactNumber2 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber2"].ToString();
            organizationViewModel.companydetails.Company_EmailID = dsUpdatedData.Tables[0].Rows[0]["Company_Email_ID"].ToString();
            organizationViewModel.companydetails.Formal_Name = dsUpdatedData.Tables[0].Rows[0]["Formal_Name"].ToString();
            organizationViewModel.companydetails.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            organizationViewModel.companydetails.Website = dsUpdatedData.Tables[0].Rows[0]["Website"].ToString();
            organizationViewModel.branch.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
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
            organizationVM.companydetails.Org_Hier_ID = organizationVM.organization.Organization_Id;
            organizationVM.organization.Branch_Id = organizationVM.branch.Branch_Id;
            result = organizationClient.updateOrganization(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if (result != false)
            {
                return RedirectToAction("ListOfGroupCompanies");
            }
            else
            {
                return RedirectToAction("ListOfGroupCompanies");
            }
        
        }
        /// <summary>
        /// Action method to get group company
        /// </summary>
        /// <returns>view</returns>
      
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
                return RedirectToAction("ListOfGroupCompanies");
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
                return RedirectToAction("ListOfGroupCompanies");
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
                return RedirectToAction("ListOfGroupCompanies");
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
            CompanyViewModel companyVM = new CompanyViewModel();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            companyVM.organization = new Organization();
            companyVM.organization.Organization_Id = 0;
            companyVM.organization.User_Id = 1;

            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
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
                return RedirectToAction("ListOfCompanies");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult UpdateCompany(int OrgID)
        {
            //int OrgID = 0;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getGroupCompany(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
            CompanyViewModel companyVM = new CompanyViewModel();
            companyVM.organization = new Organization();
            companyVM.companydetails = new CompanyDetails();
            companyVM.branch = new BranchLocation();

            companyVM.organization.Organization_Id = OrgID;
            companyVM.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            companyVM.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
            companyVM.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
            companyVM.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            //organizationViewModel.organization.Is_Active =(Boolean) dsUpdatedData.Tables[0].Rows[0]["Is_Active"];
            companyVM.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            companyVM.organization.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            companyVM.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            companyVM.companydetails.Company_Details_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Company_Details_ID"]);
            companyVM.companydetails.Org_Hier_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            companyVM.companydetails.Auditing_Frequency = dsUpdatedData.Tables[0].Rows[0]["Auditing_Frequency"].ToString();
            companyVM.companydetails.Calender_StartDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_StartDate"]);
            companyVM.companydetails.Calender_EndDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_EndDate"]);
            companyVM.companydetails.Company_ContactNumber1 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber1"].ToString();
            companyVM.companydetails.Company_ContactNumber2 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber2"].ToString();
            companyVM.companydetails.Company_EmailID = dsUpdatedData.Tables[0].Rows[0]["Company_Email_ID"].ToString();
            companyVM.companydetails.Formal_Name = dsUpdatedData.Tables[0].Rows[0]["Formal_Name"].ToString();
            companyVM.companydetails.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            companyVM.companydetails.Website = dsUpdatedData.Tables[0].Rows[0]["Website"].ToString();
            companyVM.branch.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            companyVM.branch.Address = dsUpdatedData.Tables[0].Rows[0]["Address"].ToString();
            companyVM.branch.Branch_Coordinates1 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates1"].ToString();
            companyVM.branch.Branch_Coordinates2 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates2"].ToString();
            companyVM.branch.Branch_CoordinatesURL = dsUpdatedData.Tables[0].Rows[0]["Branch_CoordinateURL"].ToString();
            companyVM.branch.Branch_Name = dsUpdatedData.Tables[0].Rows[0]["Location_Name"].ToString();
            companyVM.branch.Postal_Code = dsUpdatedData.Tables[0].Rows[0]["Postal_Code"].ToString();

            companyVM.branch.Country_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Country_ID"]);
            DataSet dsCountries = new DataSet();
            string strXMLCountries = organizationClient.GetCountryList();

            dsCountries.ReadXml(new StringReader(strXMLCountries));
            companyVM.Country = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                companyVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
            companyVM.branch.State_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["State_ID"]);

            DataSet dsStates = new DataSet();
            string strXMLStates = organizationClient.GetStateList(companyVM.branch.Country_Id);

            dsStates.ReadXml(new StringReader(strXMLStates));
            companyVM.State = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            {
                companyVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            }
            companyVM.branch.City_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["City_ID"]);

            DataSet dsCities = new DataSet();
            string strXMLCities = organizationClient.GetCityList(companyVM.branch.State_Id);

            dsCities.ReadXml(new StringReader(strXMLCities));
            companyVM.City = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            {
                companyVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            }
            return View(companyVM);
        }

        [HttpPost]
        public ActionResult UpdateCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            organizationVM.companydetails.Org_Hier_ID = organizationVM.organization.Organization_Id;
            organizationVM.organization.Branch_Id = organizationVM.branch.Branch_Id;
            result = organizationClient.updateCompany(organizationVM.organization, organizationVM.companydetails, organizationVM.branch);
            if (result != false)
            {
                return View("ListOfCompanies");
            }
            else
            {
                return View();
            }

        }


       

        [HttpGet]
        public ActionResult DeactivateCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("DeactivateGroupCompany",organizationViewModel);
        }
        [HttpPost]
        public ActionResult DeactivateCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeactivateCompany(organizationVM.organization.Organization_Id);
            if (result == true)
            {
                return RedirectToAction("ListOfCompanies");
            }
            else
            {
                return View();

            }
        }

        public ActionResult ActivateCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("ActivateGroupCompany",organizationViewModel);
        }
        [HttpPost]
        public ActionResult ActivateCompany(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.ActivateCompany(organizationVM.organization.Organization_Id);
            if (result == true)
            {
                return RedirectToAction("ListOfCompanies");
            }
            else
            {
                return View();

            }
        }

        [HttpGet]
        public ActionResult DeleteCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("DeleteGroupCompany",organizationViewModel);
        }
        [HttpPost]
        public ActionResult DeleteCompany(OrganizationViewModel organizationViewModel)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeleteCompany(organizationViewModel.organization.Organization_Id);
            if (result == true)
            {
                return RedirectToAction("ListOfCompanies");
            }
            else
            {
                return View();

            }
        }





        [HttpGet]
        public ActionResult AddBranch()
        {
            int stateID = 1;
            int countryID = 1;
            int id = 2;
            BranchViewModel branchVM = new BranchViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            branchVM.organization = new Organization();
            branchVM.organization.Organization_Id = 0;
            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            branchVM.GroupCompaniesList = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
            {
                branchVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            }


            //string strXMLCompanyList = organizationservice.GetCompaniesList(id);
            //DataSet dsCompanyList = new DataSet();
            //dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            //branchVM.CompaniesList = new List<SelectListItem>();
            //foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
            //{
            //    branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            //}


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
            return View("_Branch", branchVM);
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
                return View("ListOfBranches");
            }
            else
            {
                return View("View");
            }
        }
        [HttpGet]
        public ActionResult UpdateBranch(int OrgID)
        {
            //int OrgID = 0;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getGroupCompany(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
            BranchViewModel branchViewModel = new BranchViewModel();
            branchViewModel.organization = new Organization();
            branchViewModel.branch = new BranchLocation();

            branchViewModel.organization.Organization_Id = OrgID;
            branchViewModel.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            branchViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
            branchViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
            branchViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            //organizationViewModel.organization.Is_Active =(Boolean) dsUpdatedData.Tables[0].Rows[0]["Is_Active"];
            branchViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            branchViewModel.organization.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            branchViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            branchViewModel.branch.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            branchViewModel.branch.Address = dsUpdatedData.Tables[0].Rows[0]["Address"].ToString();
            branchViewModel.branch.Branch_Coordinates1 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates1"].ToString();
            branchViewModel.branch.Branch_Coordinates2 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates2"].ToString();
            branchViewModel.branch.Branch_CoordinatesURL = dsUpdatedData.Tables[0].Rows[0]["Branch_CoordinateURL"].ToString();
            branchViewModel.branch.Branch_Name = dsUpdatedData.Tables[0].Rows[0]["Location_Name"].ToString();
            branchViewModel.branch.Postal_Code = dsUpdatedData.Tables[0].Rows[0]["Postal_Code"].ToString();

            branchViewModel.branch.Country_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Country_ID"]);
            DataSet dsCountries = new DataSet();
            string strXMLCountries = organizationClient.GetCountryList();

            dsCountries.ReadXml(new StringReader(strXMLCountries));
            branchViewModel.Country = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                branchViewModel.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
            branchViewModel.branch.State_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["State_ID"]);

            DataSet dsStates = new DataSet();
            string strXMLStates = organizationClient.GetStateList(branchViewModel.branch.Country_Id);

            dsStates.ReadXml(new StringReader(strXMLStates));
            branchViewModel.State = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            {
                branchViewModel.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            }
            branchViewModel.branch.City_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["City_ID"]);

            DataSet dsCities = new DataSet();
            string strXMLCities = organizationClient.GetCityList(branchViewModel.branch.State_Id);

            dsCities.ReadXml(new StringReader(strXMLCities));
            branchViewModel.City = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            {
                branchViewModel.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            }
            return View(branchViewModel);
        }

        [HttpPost]
        public ActionResult UpdateBranch(BranchViewModel BranchVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            BranchVM.organization.Branch_Id = BranchVM.branch.Branch_Id;
            result = organizationClient.updateBranch(BranchVM.organization, BranchVM.branch);
            if (result != false)
            {
                return View("ListofBranch");
            }
            else
            {
                return View();
            }

        }
      

        [HttpGet]
        public ActionResult DeactivateBranch(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getBranch(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("DeactivateGroupCompany",organizationViewModel);
        }
        [HttpPost]
        public ActionResult DeactivateBranch(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeactivateBranch(organizationVM.organization.Organization_Id);
            if (result == true)
            {
                return View("ListOfBranches");
            }
            else
            {
                return View();

            }
        }

        public ActionResult ActivateBranch(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getBranch(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("ActivateGroupCompany",organizationViewModel);
        }
        [HttpPost]
        public ActionResult ActivateBranch(OrganizationViewModel organizationVM)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.ActivateBranch(organizationVM.organization.Organization_Id);
            if (result == true)
            {
                return View("ListOfBranch");
            }
            else
            {
                return View();

            }
        }













        [HttpGet]
        public ActionResult DeleteBranch(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel.organization = new Organization();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getBranch(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Company_Name = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("DeleteGroupCompany",organizationViewModel);
        }
        [HttpPost]
        public ActionResult DeleteBranch(OrganizationViewModel organizationViewModel)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeleteBranch(organizationViewModel.organization.Organization_Id);
            if (result == true)
            {
                return View("ListOfBranches");
            }
            else
            {
                return View();

            }
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
                ListOfGroupCompanies listOfGroup = new ListOfGroupCompanies
                { OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
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
            return View("ListOfGroupCompanies");
        }

        [HttpGet]
        public ActionResult ListOfCompanies()
        {
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

            CompanyViewModel companyVM = new CompanyViewModel();
            companyVM.organization = new Organization();
            companyVM.organization.Organization_Id = 0;
            companyVM.organization.User_Id = 1;

            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            companyVM.GroupCompaniesList = new List<SelectListItem>();
            foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
            {
                companyVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            }



            List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();
            string strxmlCompanies = organizationservice.GetSpecificCompaniesList(companyVM.organization.Organization_Id);


            DataSet dsSpecificCompaniesList = new DataSet();
            dsSpecificCompaniesList.ReadXml(new StringReader(strxmlCompanies));
            foreach (System.Data.DataRow row in dsSpecificCompaniesList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfCompany = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

                };
                //GroupCompanyLogo = row["Group_Company_Logo"].ToString(), IndustryType = row["Industry_Type"].ToString() };
                companylist.Add(listOfCompany);
            }
            return View(companylist);
        }
        [HttpPost]
        public ActionResult ListOfCompanies(ListOfGroupCompanies ListOfGroupCompanies)
        {
            return View("ListOfCompanies");
        }

        [HttpGet]
        public ActionResult ListOfBranches()
        {
            List<ListOfGroupCompanies> branchList = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlBranches = organizationservice.GetBranchList();


            DataSet dsBranchesList = new DataSet();
            dsBranchesList.ReadXml(new StringReader(strxmlBranches));
            foreach (System.Data.DataRow row in dsBranchesList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

                };
                //GroupCompanyLogo = row["Group_Company_Logo"].ToString(), IndustryType = row["Industry_Type"].ToString() };
                branchList.Add(listOfBranch);
            }
            return View(branchList);
        }
        [HttpPost]
        public ActionResult ListOfBranches(ListOfGroupCompanies ListOfGroupCompanies)
        {
            return View("ListOfBranches");
        }

        public JsonResult getstate(int countryid)
        {
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string strXMLStates = client.GetStateList(countryid);          
            return Json(strXMLStates);
        }

    }

}