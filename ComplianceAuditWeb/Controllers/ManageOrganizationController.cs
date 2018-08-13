﻿#region Code History
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
using System.Configuration;
using System.Web.UI.WebControls;
using System.Dynamic;

namespace ComplianceAuditWeb.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ManageOrganizationController : Controller
    {
        // GET: ManageOrganization
      
        /// <summary>
        /// [HttpGet] Action method in the controller to add group company 
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult AddGroupCompany()
        {
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            organizationVM.organization = new Organization();
            organizationVM.organization.Organization_Id = 0;
         
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            organizationVM.organization.Is_Leaf = false;
            organizationVM.organization.Level = 1;
            organizationVM.organization.Is_Active = true;
            organizationVM.organization.Is_Delete = false;
            organizationVM.organization.Is_Vendor = false;
            organizationVM.organization.Parent_Company_Id = 0;
            return View("_Organization", organizationVM);
        }
        /// <summary>
        /// [HttpPost] Action method to add the group company
        /// </summary>
        /// <param name="object of OrganizationViewModel"></param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult AddGroupCompany(OrganizationViewModel organizationVM, HttpPostedFileBase file)
        {
            
           if (ModelState.IsValid)
           {
                if (file != null)
                {
                    CommonController common = new CommonController();
                    organizationVM.organization.logo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }
                
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();

            int id = 0;
            organizationVM.organization.User_Id = Convert.ToInt32(Session["UserID"]);
            organizationVM.organization.Is_Leaf = false;
            organizationVM.organization.Level = 1;
            organizationVM.organization.Is_Active = true;
            organizationVM.organization.Is_Delete = false;
            organizationVM.organization.Is_Vendor = false;
                organizationVM.organization.Parent_Company_Id = 0;
            id = organizationClient.insertOrganization(organizationVM.organization);
            if (id != 0)
            {
                Session["ParentCompanyID"] = organizationVM.organization.Parent_Company_Id;

                TempData["Success"] = "Group Company created successfully!!!";
                return RedirectToAction("AboutCompany", new { id = id });
            }
        }
            else
            {
                ModelState.AddModelError("Company_Name", "Group Company did not create succesfully");
            }
            return RedirectToAction("AddGroupCompany");

        }


        /// <summary>
        /// Action method to update the Group compan
        /// </summary>
        /// <param name="OrgID"></param>
        /// <returns>view</returns>
        [HttpGet]
        public ActionResult UpdateGroupCompany(int OrgID )
        {
            
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getGroupOrganization(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
            organizationViewModel.organization = new Organization();
            

            organizationViewModel.organization.Organization_Id = OrgID;
            organizationViewModel.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            organizationViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
            organizationViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
            organizationViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            //organizationViewModel.organization.Is_Active =(Boolean) dsUpdatedData.Tables[0].Rows[0]["Is_Active"];
            organizationViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            organizationViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            organizationViewModel.organization.logo = dsUpdatedData.Tables[0].Rows[0]["logo"].ToString();
            Session["Logo"] = organizationViewModel.organization.logo;
            return View("_Organization",organizationViewModel);
        }
        /// <summary>
        /// 
        /// Action method to update group company and save updated values 
        /// </summary>
        /// <param name="organizationVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateGroupCompany(OrganizationViewModel organizationVM, HttpPostedFileBase file)
        {
            organizationVM.organization.logo =Convert.ToString( Session["Logo"]);
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    CommonController common = new CommonController();
                    organizationVM.organization.logo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }
                bool result = false;
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
               result = organizationClient.updateOrganization(organizationVM.organization);
                if (result != false)
                {
                    TempData["Success"] = "Updated Successfully!!!";
                    return RedirectToAction("AboutCompany", new { id = organizationVM.organization.Organization_Id });
                }
            }
            ModelState.AddModelError("Company_Name", "Error occurred while updating");
            return RedirectToAction("UpdateGroupCompany", new { OrgID = organizationVM.organization.Organization_Id });
        }


            
        /// <summary>
        /// Action method to get group company
        /// </summary>
        /// <returns>view</returns>

        [HttpGet]
        public ActionResult DeactivateGroupCompany(int OrgID)
        {
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupOrganization(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View(orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeactivateGroupCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                result = organizationServiceClient.DeactivateGroupCompany(orgActivateDeactivateViewModel.CompanyID);
                if (result == true)
                {
                    TempData["Success"] = "Deactivated Successfully!!!";
                    TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                    return RedirectToAction("GroupCompanyList");
                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult ActivateGroupCompany(int OrgID)
        {
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupOrganization(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View(orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult ActivateGroupCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                result = organizationServiceClient.ActivateGroupCompany(orgActivateDeactivateViewModel.CompanyID);
                if (result == true)
                {
                    TempData["Success"] = "Activated Successfully!!!";
                    TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                    return RedirectToAction("GroupCompanyList");
                }

            }
            return View();
        }


        [HttpGet]
        public ActionResult DeleteGroupCompany(int OrgID)
        {
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupOrganization(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View(orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeleteGroupCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                result = organizationServiceClient.DeleteGroupCompany(orgActivateDeactivateViewModel.CompanyID);
                if (result == true)
                {
                    TempData["Success"] = "Deleted Successfully!!!";
                    TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                    return RedirectToAction("GroupCompanyList");
                }
            }
            return View();
        }









        [HttpGet]
        public ActionResult AddCompany()
        {
            
            //int stateID = 0;
            //int countryID = 0;
            CompanyViewModel companyVM = new CompanyViewModel();
           

            
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            companyVM.organization = new Organization();
            companyVM.organization.Organization_Id = 0;
            companyVM.branch = new BranchLocation();
            companyVM.branch.Branch_Id = 0;
            companyVM.companydetails = new CompanyDetails();
            companyVM.companydetails.Company_Details_ID = 0;
            companyVM.organization.Parent_Company_Id =Convert.ToInt32( TempData["ParentCompanyID"]);
            if (companyVM.organization.Parent_Company_Id > 0)
            {
                companyVM.GroupCompanyID = companyVM.organization.Parent_Company_Id;
            }

            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            companyVM.GroupCompaniesList = new List<SelectListItem>();
            companyVM.GroupCompaniesList.Add(new SelectListItem { Text = "--Select Group Company--", Value = "0" });
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    companyVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            string strXMLCountries = organizationservice.GetCountryList();
            //string strXMLStates = organizationservice.GetStateList(countryID);
            //string strXMLCities = organizationservice.GetCityList(stateID);



            DataSet dsCountries = new DataSet();
            //DataSet dsStates = new DataSet();
            //DataSet dsCities = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            //dsStates.ReadXml(new StringReader(strXMLStates));
            //dsCities.ReadXml(new StringReader(strXMLCities));
            companyVM.Country = new List<SelectListItem>();
            companyVM.Country.Add(new SelectListItem { Text = "--Select Country--", Value = "0" });
            if (dsCountries.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    companyVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }


            companyVM.State = new List<SelectListItem>();
            companyVM.State.Add(new SelectListItem { Text = "--Select State--", Value = "0" });
            //if (dsStates.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            //    {
            //        companyVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            //    }
            //}


            companyVM.City = new List<SelectListItem>();
            companyVM.City.Add(new SelectListItem { Text = "--Select City--", Value = "0" });
            //if (dsCities.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            //    {
            //        companyVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            //    }
            //}

            return View("_Company", companyVM);

        }
        [HttpPost]
        public ActionResult AddCompany(CompanyViewModel companyVM, HttpPostedFileBase file)
        {
           
            if(companyVM.companydetails.Calender_StartDate == null)
            {
                companyVM.companydetails.Calender_StartDate =Convert.ToDateTime( DateTime.MinValue.ToString("dd-MM-yyyy"));
            }
            if (companyVM.companydetails.Calender_EndDate == null)
            {
                companyVM.companydetails.Calender_EndDate = Convert.ToDateTime(DateTime.MaxValue.ToString("dd-MM-yyyy"));
            }
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    CommonController common = new CommonController();
                    companyVM.organization.logo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                companyVM.companydetails.Is_Active = true;
                companyVM.organization.Is_Active = true;
                companyVM.organization.Level = 2;
                companyVM.organization.Is_Leaf = false;
                companyVM.organization.Is_Vendor = false;
                companyVM.organization.User_Id = Convert.ToInt32(Session["UserID"]);
                companyVM.organization.Parent_Company_Id = companyVM.GroupCompanyID;

                int id = organizationClient.insertCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);

                if (id != 0)
                {
                    Session["ParentCompanyID"] = companyVM.organization.Parent_Company_Id;
                    TempData["Success"] = "Company created Successfully!!!";
                    return RedirectToAction("AboutCompany", new { id = id });
                }
            }
        
            else
            {
                ModelState.AddModelError("Error", "Oops!Something went wrong");

            }
            return RedirectToAction("AddCompany");

        }


        [HttpGet]
        public ActionResult UpdateCompany(int OrgID)
        {
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
            companyVM.organization.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Active"]));
            companyVM.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            //companyVM.organization.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
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
            companyVM.companydetails.Website = dsUpdatedData.Tables[0].Rows[0]["Website"].ToString();
           companyVM.branch.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            companyVM.branch.Address = dsUpdatedData.Tables[0].Rows[0]["Address"].ToString();
            companyVM.branch.Branch_Coordinates1 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates1"].ToString();
            companyVM.branch.Branch_Coordinates2 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates2"].ToString();
            companyVM.branch.Branch_CoordinatesURL = dsUpdatedData.Tables[0].Rows[0]["Branch_CoordinateURL"].ToString();
            companyVM.branch.Branch_Name = dsUpdatedData.Tables[0].Rows[0]["Location_Name"].ToString();
            companyVM.branch.Postal_Code = dsUpdatedData.Tables[0].Rows[0]["Postal_Code"].ToString();
            companyVM.organization.logo = dsUpdatedData.Tables[0].Rows[0]["logo"].ToString();

            companyVM.branch.Country_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Country_ID"]);
            DataSet dsCountries = new DataSet();
            string strXMLCountries = organizationClient.GetCountryList();

            dsCountries.ReadXml(new StringReader(strXMLCountries));
            companyVM.Country = new List<SelectListItem>();
            if (dsCountries.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    companyVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }
            companyVM.branch.State_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["State_ID"]);

            DataSet dsStates = new DataSet();
            string strXMLStates = organizationClient.GetStateList(companyVM.branch.Country_Id);

            dsStates.ReadXml(new StringReader(strXMLStates));
            companyVM.State = new List<SelectListItem>();
            if (dsStates.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
                {
                    companyVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            companyVM.branch.City_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["City_ID"]);

            DataSet dsCities = new DataSet();
            string strXMLCities = organizationClient.GetCityList(companyVM.branch.State_Id);

            dsCities.ReadXml(new StringReader(strXMLCities));
            companyVM.City = new List<SelectListItem>();
            if (dsCities.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    companyVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            companyVM.GroupCompanyID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);

            string strXMLGroupCompanyList = organizationClient.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            companyVM.GroupCompaniesList = new List<SelectListItem>();
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    companyVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }

            Session["Logo"] = companyVM.organization.logo;

            return View("_Company", companyVM);
        }

        [HttpPost]
        public ActionResult UpdateCompany(CompanyViewModel companyVM, HttpPostedFileBase file)
        {
            if (companyVM.companydetails.Calender_StartDate == null)
            {
                companyVM.companydetails.Calender_StartDate = Convert.ToDateTime(DateTime.MinValue.ToString("dd-MM-yyyy"));
            }
            if (companyVM.companydetails.Calender_EndDate == null)
            {
                companyVM.companydetails.Calender_EndDate = Convert.ToDateTime(DateTime.MaxValue.ToString("dd-MM-yyyy"));
            }
            companyVM.organization.logo = Convert.ToString(Session["Logo"]);
            bool result = false;
            if (file != null)
            {
                CommonController common = new CommonController();
                companyVM.organization.logo = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                string message = common.UploadFile(file, filePath);
                ModelState.AddModelError("org_hier.logo", message);
            }

            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            companyVM.companydetails.Org_Hier_ID = companyVM.organization.Organization_Id;
            companyVM.branch.Org_Hier_ID = companyVM.organization.Organization_Id;
            companyVM.organization.Level = 2;
            companyVM.organization.Parent_Company_Id = companyVM.GroupCompanyID;
            result = organizationClient.updateCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);
            if (result != false)
            {
                TempData["Success"] = "Updated Successfully!!!";
                return RedirectToAction("AboutCompany",new { id = companyVM.organization.Organization_Id });

            }
            else
            {
                return RedirectToAction("UpdateCompany",new { OrgID = companyVM.organization.Organization_Id });
            }

        }




        [HttpGet]
        public ActionResult DeactivateCompany(int OrgID)
        {
           
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("DeactivateGroupCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeactivateCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeactivateCompany(orgActivateDeactivateViewModel.CompanyID);
            if (result == true)
            {
                TempData["Success"] = "Deactivated successfully";
                TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                return RedirectToAction("ListofCompany",new { GroupCompanyid = orgActivateDeactivateViewModel.ParentCompanyID });
            }
            else
            {
                return View();

            }
        }

        public ActionResult ActivateCompany(int OrgID)
        {
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("ActivateGroupCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult ActivateCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                result = organizationServiceClient.ActivateCompany(orgActivateDeactivateViewModel.CompanyID);
                if (result == true)
                {

                    TempData["Success"] = "Activated successfully";
                    TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                    return RedirectToAction("ListofCompany", new { GroupCompanyid = orgActivateDeactivateViewModel.ParentCompanyID });
                }
                else
                {
                    return View();

                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteCompany(int OrgID)
        {
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getGroupCompany(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32( dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("DeleteGroupCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeleteCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                result = organizationServiceClient.DeleteCompany(orgActivateDeactivateViewModel.CompanyID);
                if (result == true)
                {

                    TempData["Success"] = "Deleted successfully";
                    TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                    return RedirectToAction("ListofCompany", new { GroupCompanyid = orgActivateDeactivateViewModel.ParentCompanyID });

                }
                else
                {
                    return View();

                }
            }
            return View();
        }





        [HttpGet]
        public ActionResult AddBranch()
        {
            //int stateID = 0;
            //int countryID = 0;
            int id = 0;
            BranchViewModel branchVM = new BranchViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            branchVM.organization = new Organization();
            branchVM.organization.Organization_Id = 0;
            branchVM.branch = new BranchLocation();
            branchVM.branch.Branch_Id = 0;
            
            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            branchVM.GroupCompaniesList = new List<SelectListItem>();
            branchVM.GroupCompaniesList.Add(new SelectListItem { Text = "--Select Group Company--", Value = "0" });
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    branchVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }

            }
            string strXMLCompanyList = organizationservice.GeSpecifictCompaniesList(id);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            branchVM.CompaniesList = new List<SelectListItem>();
            branchVM.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
            if (dsCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                {
                    branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            

            string strXMLCountries = organizationservice.GetCountryList();
            //string strXMLStates = organizationservice.GetStateList(countryID);
            //string strXMLCities = organizationservice.GetCityList(stateID);



            DataSet dsCountries = new DataSet();
            //DataSet dsStates = new DataSet();
            //DataSet dsCities = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            //dsStates.ReadXml(new StringReader(strXMLStates));
            //dsCities.ReadXml(new StringReader(strXMLCities));
            branchVM.Country = new List<SelectListItem>();
            branchVM.Country.Add(new SelectListItem { Text = "--Select Country--", Value = "0" });
            if (dsCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    branchVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }


            branchVM.State = new List<SelectListItem>();
            branchVM.State.Add(new SelectListItem { Text = "--Select State--", Value = "0" });
            //if (dsStates.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
            //    {
            //        branchVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
            //    }
            //}


            branchVM.City = new List<SelectListItem>();
            branchVM.City.Add(new SelectListItem { Text = "--Select City--", Value = "0" });
            //if (dsCities.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
            //    {
            //        branchVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
            //    }
            //}
            ////int CompanyID = 0;
           
            //string xmlVndors = organizationservice.GetVendors(CompanyID);
            //DataSet dsVendors = new DataSet();
            //dsVendors.ReadXml(new StringReader(xmlVndors));
            //branchVM.VendorList = new List<SelectListItem>();

            ////userviewmodel.RolesList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            //if (dsVendors.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in dsVendors.Tables[0].Rows)
            //    {
            //        branchVM.VendorList.Add(new SelectListItem { Text = row["Vendor_Name"].ToString(), Value = row["Vendor_ID"].ToString() });
            //    }
            //}









            return View("_Branch", branchVM);
        }
        [HttpPost]
        public ActionResult AddBranch(BranchViewModel branchVM, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    CommonController common = new CommonController();
                    branchVM.organization.logo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }

                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                branchVM.organization.Is_Active = true;
                branchVM.organization.Level = 3;
                branchVM.organization.Is_Leaf = true;
                branchVM.organization.Is_Vendor = false;
                branchVM.organization.Parent_Company_Id = branchVM.CompanyID;
                branchVM.organization.User_Id =Convert.ToInt32( Session["UserID"]);
                int id =Convert.ToInt32( organizationClient.insertBranch(branchVM.organization, branchVM.branch));
                //bool vendorresult = vendorServiceClient.insertVendorForBranch(branchVM.VendorID, id,branchVM.VendorStartDate
                //    , branchVM.VendorEndDate, branchVM.IsVendorActive);
                if (id!=0)
                {
                    //Session["CompanyName"] = branchVM.organization.Company_Name;
                    //Session["CompanyDescription"] = branchVM.organization.Description;
                    //Session["CompanyID"] = id;
                    Session["ParentCompanyID"] = branchVM.organization.Parent_Company_Id;

                    // TempData["id"] = id;
                    TempData["Success"] = "Branch created successfully";
                    return RedirectToAction("AboutBranch", new { id = id } );
                }
                else
                {
                    return View("View");
                }
            }
            else
            {
                return View("_Branch");
            }
        }


        [HttpGet]
        public ActionResult UpdateBranch(int OrgID)
        {
            //int OrgID = 0;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getBranch(OrgID);
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
            //branchViewModel.organization.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            branchViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            branchViewModel.branch.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            branchViewModel.branch.Address = dsUpdatedData.Tables[0].Rows[0]["Address"].ToString();
            branchViewModel.branch.Branch_Coordinates1 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates1"].ToString();
            branchViewModel.branch.Branch_Coordinates2 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates2"].ToString();
            branchViewModel.branch.Branch_CoordinatesURL = dsUpdatedData.Tables[0].Rows[0]["Branch_CoordinateURL"].ToString();
            branchViewModel.branch.Branch_Name = dsUpdatedData.Tables[0].Rows[0]["Location_Name"].ToString();
            branchViewModel.branch.Postal_Code = dsUpdatedData.Tables[0].Rows[0]["Postal_Code"].ToString();

            branchViewModel.organization.logo = dsUpdatedData.Tables[0].Rows[0]["logo"].ToString();




            branchViewModel.branch.Country_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Country_ID"]);
            DataSet dsCountries = new DataSet();
            string strXMLCountries = organizationClient.GetCountryList();

            dsCountries.ReadXml(new StringReader(strXMLCountries));
            branchViewModel.Country = new List<SelectListItem>();
            if (dsCountries.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    branchViewModel.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }
            branchViewModel.branch.State_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["State_ID"]);

            DataSet dsStates = new DataSet();
            string strXMLStates = organizationClient.GetStateList(branchViewModel.branch.Country_Id);

            dsStates.ReadXml(new StringReader(strXMLStates));
            branchViewModel.State = new List<SelectListItem>();
            if (dsStates.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
                {
                    branchViewModel.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            branchViewModel.branch.City_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["City_ID"]);

            DataSet dsCities = new DataSet();
            string strXMLCities = organizationClient.GetCityList(branchViewModel.branch.State_Id);

            dsCities.ReadXml(new StringReader(strXMLCities));
            branchViewModel.City = new List<SelectListItem>();
            if (dsCities.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    branchViewModel.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }


           // branchViewModel.GroupCompanyID = 36;//Convert.ToInt32((BranchViewModel) Session["GroupCompanyID"]);
           
            string strXMLGroupCompanyList = organizationClient.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            branchViewModel.GroupCompaniesList = new List<SelectListItem>();
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    branchViewModel.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }

            branchViewModel.CompanyID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);

            string strXMLCompanyList = organizationClient.getCompanyListsforBranch(branchViewModel.CompanyID);
           // string strXMLCompanyList = organizationClient.GeSpecifictCompaniesList(branchViewModel.CompanyID);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            branchViewModel.CompaniesList = new List<SelectListItem>();
            if (dsCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                {
                    branchViewModel.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }

            Session["Logo"] = branchViewModel.organization.logo;

            return View("_Branch",branchViewModel);
        }

        [HttpPost]
        public ActionResult UpdateBranch(BranchViewModel BranchVM, HttpPostedFileBase file)
        {
            BranchVM.organization.logo = Convert.ToString(Session["Logo"]);
            if (file != null)
            {
                CommonController common = new CommonController();
                BranchVM.organization.logo = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                string message = common.UploadFile(file, filePath);
                ModelState.AddModelError("org_hier.logo", message);
            }
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            BranchVM.branch.Org_Hier_ID = BranchVM.organization.Organization_Id;
            BranchVM.organization.Parent_Company_Id = BranchVM.CompanyID;
            result =  organizationClient.updateBranch(BranchVM.organization, BranchVM.branch);
            if (result != false)
            {
                TempData["Success"] = "Branch Updated successfully";
                return RedirectToAction("AboutBranch", new { id = BranchVM.organization.Organization_Id });
            }
            else
            {
                return RedirectToAction("UpdateBranch", new { OrgID = BranchVM.organization.Organization_Id });

            }

        }


        [HttpGet]
        public ActionResult DeactivateBranch(int OrgID)
        {
            // OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            //organizationViewModel.organization = new Organization();
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getBranch(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32( dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("DeactivateGroupCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeactivateBranch(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeactivateBranch(orgActivateDeactivateViewModel.CompanyID);
            if (result == true)
            {
                TempData["Success"] = "Deactivated successfully";
                TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                return RedirectToAction("BranchList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });

            }
            else
            {
                return RedirectToAction("DeactivateBranch", new { OrgId = orgActivateDeactivateViewModel.ParentCompanyID });


            }
        }

        public ActionResult ActivateBranch(int OrgID)
        {
            // OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            //organizationViewModel.organization = new Organization();
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getBranch(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32( dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("ActivateGroupCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult ActivateBranch(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.ActivateBranch(orgActivateDeactivateViewModel.CompanyID);
            if (result == true)
            {

                TempData["Success"] = "Activated successfully";
                TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                return RedirectToAction("BranchList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });

            }
            else
            {
                return RedirectToAction("ActivateBranch", new { OrgId = orgActivateDeactivateViewModel.ParentCompanyID });


            }
        }

        [HttpGet]
        public ActionResult DeleteBranch(int OrgID)
        {
            // OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            //organizationViewModel.organization = new Organization();
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getBranch(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32( dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("DeleteGroupCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeleteBranch(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel )
        {
            bool result = false;
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            result = organizationServiceClient.DeleteBranch(orgActivateDeactivateViewModel.CompanyID);
            if (result == true)
            {

                TempData["Success"] = "Deleted successfully";
                TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                return RedirectToAction("BranchList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });

            }
            else
            {
                return RedirectToAction("DeleteBranch", new { OrgId = orgActivateDeactivateViewModel.ParentCompanyID });


            }
        }


        [HttpGet]
        public ActionResult AddVendor()
        {
           // int stateID = 0;
            //int countryID = 0;
            int id = 0;
            CompanyViewModel vendorVM = new CompanyViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            vendorVM.organization = new Organization();
            vendorVM.organization.Organization_Id = 0;
            vendorVM.branch = new BranchLocation();
            vendorVM.branch.Branch_Id = 0;
            vendorVM.companydetails = new CompanyDetails();
            vendorVM.companydetails.Company_Details_ID = 0;

            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            vendorVM.GroupCompaniesList = new List<SelectListItem>();
            vendorVM.GroupCompaniesList.Add(new SelectListItem { Text = "--Select Group Company--", Value = "0" });
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    vendorVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }


            string strXMLCompanyList = organizationservice.GeSpecifictCompaniesList(id);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            vendorVM.CompaniesList = new List<SelectListItem>();
            vendorVM.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
            if (dsCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                {
                    vendorVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }


           // string strXMLCountries = organizationservice.GetCountryList();
           // string strXMLStates = organizationservice.GetStateList(countryID);
            //string strXMLCities = organizationservice.GetCityList(stateID);



           
            return View("_Vendor", vendorVM);
        }
        [HttpPost]
        public ActionResult AddVendor(CompanyViewModel vendorVM, HttpPostedFileBase file)
        {
            if(vendorVM.companydetails.Calender_EndDate == null)
            {
                vendorVM.companydetails.Calender_EndDate = DateTime.MaxValue;
            }
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    CommonController common = new CommonController();
                    vendorVM.organization.logo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                vendorVM.organization.Is_Active = true;
                vendorVM.organization.Level = 3;
                vendorVM.organization.Is_Leaf = true;
                vendorVM.organization.Is_Vendor = true;
                vendorVM.organization.Parent_Company_Id = vendorVM.CompanyID;
                vendorVM.organization.User_Id = 1;
                int id = Convert.ToInt32(organizationClient.insertVendor(vendorVM.organization, vendorVM.companydetails));
               // bool vendorresult = vendorServiceClient.insertVendorForBranch(branchVM.VendorID, id,branchVM.VendorStartDate
                 //   , branchVM.VendorEndDate, branchVM.IsVendorActive);
                if (id != 0)
                {
                   // Session["VendorName"] = vendorVM.organization.Company_Name;
                   // Session["CompanyDescription"] = branchVM.organization.Description;
                    //Session["VendorID"] = id;
                    Session["ParentCompanyID"] = vendorVM.organization.Parent_Company_Id;
                    return RedirectToAction("AboutVendor", new { id = id });
                }
                else
                {
                    return View("View");
                }
            }
            else
            {
                return View("_Vendor");
            }
        }

        [HttpGet]
        public ActionResult UpdateVendor(int OrgID)
        {
            //int OrgID = 0;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            string strxmlUpdatedData = organizationClient.getVendor(OrgID);
            DataSet dsUpdatedData = new DataSet();
            dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
            CompanyViewModel ViewModel = new CompanyViewModel();
            ViewModel.organization = new Organization();
            ViewModel.branch = new BranchLocation();
            ViewModel.companydetails = new CompanyDetails();

            ViewModel.organization.Organization_Id = OrgID;
            ViewModel.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            ViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
            ViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
            ViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
            //organizationViewModel.organization.Is_Active =(Boolean) dsUpdatedData.Tables[0].Rows[0]["Is_Active"];
            //ViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
            //branchViewModel.organization.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
            ViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
            ViewModel.companydetails.Company_Details_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Company_Details_ID"]);
            ViewModel.companydetails.Org_Hier_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
            ViewModel.companydetails.Auditing_Frequency = dsUpdatedData.Tables[0].Rows[0]["Auditing_Frequency"].ToString();
            ViewModel.companydetails.Calender_StartDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_StartDate"]);
            ViewModel.companydetails.Calender_EndDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_EndDate"]);
            ViewModel.companydetails.Company_ContactNumber1 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber1"].ToString();
            ViewModel.companydetails.Company_ContactNumber2 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber2"].ToString();
            ViewModel.companydetails.Company_EmailID = dsUpdatedData.Tables[0].Rows[0]["Company_Email_ID"].ToString();
            // ViewModel.companydetails.Formal_Name = dsUpdatedData.Tables[0].Rows[0]["Formal_Name"].ToString();

            ViewModel.organization.logo = dsUpdatedData.Tables[0].Rows[0]["logo"].ToString();




            // branchViewModel.GroupCompanyID = 36;//Convert.ToInt32((BranchViewModel) Session["GroupCompanyID"]);

            string strXMLGroupCompanyList = organizationClient.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            ViewModel.GroupCompaniesList = new List<SelectListItem>();
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    ViewModel.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            ViewModel.CompanyID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);

            string strXMLCompanyList = organizationClient.getCompanyListsforBranch(ViewModel.CompanyID);
            // string strXMLCompanyList = organizationClient.GeSpecifictCompaniesList(branchViewModel.CompanyID);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            ViewModel.CompaniesList = new List<SelectListItem>();
            if (dsCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                {
                    ViewModel.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }

            }
            Session["Logo"] = ViewModel.organization.logo;

            return View("_Vendor", ViewModel);
        }

        [HttpPost]
        public ActionResult UpdateVendor(CompanyViewModel vendorViewModel, HttpPostedFileBase file)
        {
            vendorViewModel.organization.logo = Convert.ToString("Logo");
            if (file != null)
            {
                CommonController common = new CommonController();
                vendorViewModel.organization.logo = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                string message = common.UploadFile(file, filePath);
                ModelState.AddModelError("org_hier.logo", message);
            }
            bool result = false;
            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            //BranchVM.organization.Branch_Id = BranchVM.branch.Branch_Id;
            vendorViewModel.companydetails.Org_Hier_ID = vendorViewModel.organization.Organization_Id;
            result = organizationClient.updateVendor(vendorViewModel.organization, vendorViewModel.companydetails);
            if (result != false)
            {
                return RedirectToAction("AboutVendor", new { id = vendorViewModel.organization.Organization_Id });
            }
            else
            {
                return View();
            }

        }












        //[HttpGet]
        //public ActionResult ListOfGroupCompanies()
        //{
        //    List<ListOfGroupCompanies> grouplist = new List<ListOfGroupCompanies>();
        //    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
        //    string strxmlGroupCompanies = organizationservice.GetGroupCompaniesList();


        //    DataSet dsGroupCompaniesList = new DataSet();
        //    dsGroupCompaniesList.ReadXml(new StringReader(strxmlGroupCompanies));
        //    foreach (System.Data.DataRow row in dsGroupCompaniesList.Tables[0].Rows)
        //    {
        //        ListOfGroupCompanies listOfGroup = new ListOfGroupCompanies
        //        {
        //            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
        //            CompanyName = row["Company_Name"].ToString(),
        //            IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

        //        };
        //        //GroupCompanyLogo = row["Group_Company_Logo"].ToString(), IndustryType = row["Industry_Type"].ToString() };
        //        grouplist.Add(listOfGroup);
        //    }
        //    return View(grouplist);
        //}
        //[HttpPost]
        //public ActionResult ListOfGroupCompanies(ListOfGroupCompanies ListOfGroupCompanies)
        //{
        //    return View("ListOfGroupCompanies");
        //}

        //[HttpGet]
        //public ActionResult ListOfCompanies()
        //{
        //    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

        //    //CompanyViewModel companyVM = new CompanyViewModel();
        //    //companyVM.organization = new Organization();
        //    //companyVM.organization.Organization_Id = 0;
        //    //companyVM.organization.User_Id = 1;

        //    //string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
        //    //DataSet dsGroupCompanyList = new DataSet();
        //    //dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
        //    //companyVM.GroupCompaniesList = new List<SelectListItem>();
        //    //foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
        //    //{
        //    //    companyVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
        //    //}



        //    List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();
        //    // string strxmlCompanies = organizationservice.GeSpecifictCompaniesList(companyVM.organization.Organization_Id);
        //    string strxmlC = organizationservice.GetCompaniesList();


        //    DataSet dsSpecificCompaniesList = new DataSet();
        //    dsSpecificCompaniesList.ReadXml(new StringReader(strxmlC));
        //    foreach (System.Data.DataRow row in dsSpecificCompaniesList.Tables[0].Rows)
        //    {
        //        ListOfGroupCompanies listOfCompany = new ListOfGroupCompanies
        //        {
        //            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
        //            CompanyName = row["Company_Name"].ToString(),
        //            IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

        //        };
        //        //GroupCompanyLogo = row["Group_Company_Logo"].ToString(), IndustryType = row["Industry_Type"].ToString() };
        //        companylist.Add(listOfCompany);
        //    }
        //    return View(companylist);
        //}
        //[HttpPost]
        //public ActionResult ListOfCompanies(ListOfGroupCompanies ListOfGroupCompanies)
        //{
        //    return View("ListOfCompanies");
        //}

        //[HttpGet]
        //public ActionResult ListOfBranches()
        //{
        //    List<ListOfGroupCompanies> branchList = new List<ListOfGroupCompanies>();
        //    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
        //    string strxmlBranches = organizationservice.GetBranchList();


        //    DataSet dsBranchesList = new DataSet();
        //    dsBranchesList.ReadXml(new StringReader(strxmlBranches));
        //    foreach (System.Data.DataRow row in dsBranchesList.Tables[0].Rows)
        //    {
        //        ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
        //        {
        //            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
        //            CompanyName = row["Company_Name"].ToString(),
        //            IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

        //        };
        //        //GroupCompanyLogo = row["Group_Company_Logo"].ToString(), IndustryType = row["Industry_Type"].ToString() };
        //        branchList.Add(listOfBranch);
        //    }
        //    return View(branchList);
        //}
        //[HttpPost]
        //public ActionResult ListOfBranches(ListOfGroupCompanies ListOfGroupCompanies)
        //{
        //    return View("ListOfBranches");
        //}



        [HttpGet]
        public ActionResult SelectGroupCompany()
        {
            Models.ListOfGroupCompanies model = new ListOfGroupCompanies();
            model.GroupCompaniesList=new List<SelectListItem>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            model.GroupCompaniesList.Add(new SelectListItem() { Text ="--Select GroupCompany--" , Value = "0" });
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                model.GroupCompanyID = Convert.ToInt32(dsGroupCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    model.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();            
            string strxmlCompanies = organizationservice.GeSpecifictCompaniesList(model.GroupCompanyID);

            DataSet dsSpecificCompaniesList = new DataSet();
            dsSpecificCompaniesList.ReadXml(new StringReader(strxmlCompanies));
            foreach (System.Data.DataRow row in dsSpecificCompaniesList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfCompany = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Logo = row["logo"].ToString()

                };
                companylist.Add(listOfCompany);
            }
            model.listOfGroups = companylist;
            Session["GroupCompany"] = model.GroupCompaniesList;
            return View("_CompanyList", model);
        }
        [HttpPost]
        public ActionResult SelectGroupCompany(ListOfGroupCompanies model)
        {
            List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlCompanies = organizationservice.GeSpecifictCompaniesList(model.GroupCompanyID);
            DataSet dsSpecificCompaniesList = new DataSet();
            dsSpecificCompaniesList.ReadXml(new StringReader(strxmlCompanies));
            if (dsSpecificCompaniesList.Tables.Count > 0)
            {                
                foreach (System.Data.DataRow row in dsSpecificCompaniesList.Tables[0].Rows)
                {
                    ListOfGroupCompanies listOfCompany = new ListOfGroupCompanies
                    {
                        OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                        CompanyName = row["Company_Name"].ToString(),
                        IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        Logo = row["logo"].ToString()

                    };
                    companylist.Add(listOfCompany);
                }
            }
            else { ViewBag.Message = ConfigurationManager.AppSettings["No_Companies"]; }
            model.listOfGroups = companylist;
            model.GroupCompaniesList = (List<SelectListItem>)Session["GroupCompany"];
            return View("_CompanyList", model);
            //return RedirectToAction("ListofCompany",routeValues:new { GroupCompanyid = model.GroupCompanyID });
        }

        public ActionResult ListofCompany(int GroupCompanyid)
        {
            List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlCompanies = organizationservice.GeSpecifictCompaniesList(GroupCompanyid);

            DataSet dsSpecificCompaniesList = new DataSet();
            dsSpecificCompaniesList.ReadXml(new StringReader(strxmlCompanies));
            foreach (System.Data.DataRow row in dsSpecificCompaniesList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfCompany = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Logo = row["logo"].ToString()
                    
                };
                companylist.Add(listOfCompany);

            }
            return View("_Companydashboard", companylist);
        }

        [HttpGet]
        public ActionResult SelectCompany()
        {
            int id = 0;

            Models.ListOfGroupCompanies model = new ListOfGroupCompanies();
            model.GroupCompaniesList = new List<SelectListItem>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            model.GroupCompaniesList.Add(new SelectListItem() { Text = "--Select GroupCompany--", Value = "0" });           
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                model.GroupCompanyID =Convert.ToInt32( dsGroupCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    model.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            string strXMLCompanyList = organizationservice.GeSpecifictCompaniesList(model.GroupCompanyID);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            model.CompaniesList = new List<SelectListItem>();
            model.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
            if (dsCompanyList.Tables.Count > 0)
            {
                model.CompanyID= Convert.ToInt32(dsCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                {
                    model.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            List<ListOfGroupCompanies> branchlist = new List<ListOfGroupCompanies>();            
            string strxmlCompanies = organizationservice.GeSpecifictBranchList(model.CompanyID);

            DataSet dsSpecificBranchList = new DataSet();
            dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
            foreach (System.Data.DataRow row in dsSpecificBranchList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Logo = Convert.ToString(row["logo"])
                };
                branchlist.Add(listOfBranch);

            }
            model.listOfGroups = branchlist;
            Session["GroupCompany"] = model.GroupCompaniesList;
            return View("_BranchList", model);
        }
        [HttpPost]
        public ActionResult SelectCompany(ListOfGroupCompanies model)
        {
            List<ListOfGroupCompanies> branchlist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlCompanies = organizationservice.GeSpecifictBranchList(model.CompanyID);

            DataSet dsSpecificBranchList = new DataSet();
            dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
            if (dsSpecificBranchList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsSpecificBranchList.Tables[0].Rows)
                {
                    ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
                    {
                        OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                        CompanyName = row["Company_Name"].ToString(),
                        // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                        IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        Logo = Convert.ToString(row["logo"])
                    };
                    branchlist.Add(listOfBranch);

                }
            }
            model.GroupCompaniesList = (List<SelectListItem>)Session["GroupCompany"] ;
            model.CompaniesList = (List<SelectListItem>)Session["Company"];
            model.listOfGroups = branchlist;
            return View("_BranchList", model);
            //return RedirectToAction("BranchList", routeValues: new { id = model.CompanyID });
        }



        [HttpGet]
        public ActionResult SelectCompanyForVendor()
        {
            int id = 0;

            Models.ListOfGroupCompanies model = new ListOfGroupCompanies();
            model.GroupCompaniesList = new List<SelectListItem>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            model.GroupCompaniesList.Add(new SelectListItem() { Text = "--Select GroupCompany--", Value = "0" });
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                model.GroupCompanyID = Convert.ToInt32(dsGroupCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    model.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            string strXMLCompanyList = organizationservice.GeSpecifictCompaniesList(model.GroupCompanyID);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            model.CompaniesList = new List<SelectListItem>();
            model.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
            if (dsCompanyList.Tables.Count > 0)
            {
                model.CompanyID = Convert.ToInt32(dsCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                {
                    model.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }

            List<ListOfGroupCompanies> vendorlist = new List<ListOfGroupCompanies>();            
            string strxmlVendors = organizationservice.GetVendors(model.CompanyID);
            DataSet dsSpecificVendorList = new DataSet();
            dsSpecificVendorList.ReadXml(new StringReader(strxmlVendors));
            if (dsSpecificVendorList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsSpecificVendorList.Tables[0].Rows)
                {
                    ListOfGroupCompanies listOfVendors = new ListOfGroupCompanies
                    {
                        OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                        CompanyName = row["Company_Name"].ToString(),
                        // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                        IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        Logo = Convert.ToString(row["logo"])

                    };
                    vendorlist.Add(listOfVendors);

                }
            }

            model.listOfGroups = vendorlist;
            Session["GroupCompany"] = model.GroupCompaniesList;
            return View("_vendorList", model);
        }
        [HttpPost]
        public ActionResult SelectCompanyForVendor(ListOfGroupCompanies model)
        {
            List<ListOfGroupCompanies> vendorlist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlVendors = organizationservice.GetVendors(model.CompanyID);
            DataSet dsSpecificVendorList = new DataSet();
            dsSpecificVendorList.ReadXml(new StringReader(strxmlVendors));
            if (dsSpecificVendorList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsSpecificVendorList.Tables[0].Rows)
                {
                    ListOfGroupCompanies listOfVendors = new ListOfGroupCompanies
                    {
                        OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                        CompanyName = row["Company_Name"].ToString(),
                        // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                        IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        Logo = Convert.ToString(row["logo"])

                    };
                    vendorlist.Add(listOfVendors);

                }
            }
            model.GroupCompaniesList = (List<SelectListItem>)Session["GroupCompany"];
            model.CompaniesList = (List<SelectListItem>)Session["Company"];
            model.listOfGroups = vendorlist;
            return View("_vendorList", model);
            //return RedirectToAction("CompanyVendorsList", routeValues: new {id = model.CompanyID });
        }


        //public List<AboutCompanyViewModel>  GetCompanyListUndergroupCompany(int id)
        //{
        //    AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
        //    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
           

        //    List<AboutCompanyViewModel> model = new List<AboutCompanyViewModel>();
        //   // string companyListofGroupCompany = organizationServiceClient.GeSpecifictCompaniesList(aboutCompanyViewModel.ParentCompanyID);
        //    string companyListofGroupCompany = organizationServiceClient.GeSpecifictCompaniesList(id);
        //    DataSet dscompanyList = new DataSet();
        //    dscompanyList.ReadXml(new StringReader(companyListofGroupCompany));
        //    aboutCompanyViewModel.CompaniesList = new List<SelectListItem>();
        //    if (dscompanyList.Tables.Count > 0)
        //    {
        //        foreach (System.Data.DataRow row in dscompanyList.Tables[0].Rows)
        //        {
        //            AboutCompanyViewModel listOfComp = new AboutCompanyViewModel
        //            {
        //                OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
        //                CompanyNameList = row["Company_Name"].ToString(),
        //                IndustryType = row["Industry_Type"].ToString(),
        //                CountryID = Convert.ToInt32(row["Country_ID"]),
        //                StateID = Convert.ToInt32(row["State_ID"]),
        //                CityID = Convert.ToInt32(row["City_ID"]),
        //                logo = row["logo"].ToString()
        //            };
        //            model.Add(listOfComp);

        //        }
        //    }
        //    return model;
        //}

        //public AboutCompanyViewModel aboutcreatedGroupCompany(int id)
        //{
        //    AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
        //    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
        //    string aboutcompany = organizationServiceClient.getCompanyListsforBranch(id);
        //    DataSet dsaboutCompany = new DataSet();
        //    dsaboutCompany.ReadXml(new StringReader(aboutcompany));
        //    aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
        //    aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
        //    aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
        //    aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
        //    aboutCompanyViewModel.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));
        //    // if (dsaboutCompany.Tables.Contains("logo"))
        //    {
        //        aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);
        //    }
        //    //aboutCompanyViewModel.CompanyID =Convert.ToInt32( Session["CompanyID"]);
        //    //aboutCompanyViewModel.CompanyDescription = Convert.ToString(Session["CompanyDescription"]);
        //    //aboutCompanyViewModel.CompanyName = Convert.ToString(Session["CompanyName"]);
        //    Session["ParentCompanyID"] = aboutCompanyViewModel.ParentCompanyID;
        //    return aboutCompanyViewModel;
        //}




        [HttpGet]
        public ActionResult AboutCompany(int id)
        {
            AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string aboutcompany = organizationServiceClient.getCompanyListsforBranch(id);
            DataSet dsaboutCompany = new DataSet();
            dsaboutCompany.ReadXml(new StringReader(aboutcompany));
            aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
            aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
            aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
            aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
            aboutCompanyViewModel.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));
            // if (dsaboutCompany.Tables.Contains("logo"))
            {
                aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);
            }
            //aboutCompanyViewModel.CompanyID =Convert.ToInt32( Session["CompanyID"]);
            //aboutCompanyViewModel.CompanyDescription = Convert.ToString(Session["CompanyDescription"]);
            //aboutCompanyViewModel.CompanyName = Convert.ToString(Session["CompanyName"]);
            Session["ParentCompanyID"] = aboutCompanyViewModel.ParentCompanyID;
            if (aboutCompanyViewModel.ParentCompanyID == 0)
            {
                List<AboutCompanyViewModel> companylist = new List<AboutCompanyViewModel>();
                string companyListofGroupCompany = organizationServiceClient.GeSpecifictCompaniesList(aboutCompanyViewModel.CompanyID);
                DataSet dscompanyList = new DataSet();
                dscompanyList.ReadXml(new StringReader(companyListofGroupCompany));
                aboutCompanyViewModel.CompaniesList = new List<SelectListItem>();
                if (dscompanyList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dscompanyList.Tables[0].Rows)
                    {
                        AboutCompanyViewModel listOfComp = new AboutCompanyViewModel
                        {
                            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                            CompanyNameList = row["Company_Name"].ToString(),
                            IndustryType = row["Industry_Type"].ToString(),
                            //CountryID = Convert.ToInt32(row["Country_ID"]),
                            //StateID = Convert.ToInt32(row["State_ID"]),
                            //CityID = Convert.ToInt32(row["City_ID"]),
                            logo = row["logo"].ToString()
                        };
                        companylist.Add(listOfComp);

                    }
                }

                aboutCompanyViewModel.AboutGroupCompany = companylist;
                Session["Company"] = aboutCompanyViewModel.CompaniesList;
            }
            else
            {
                
                    List<AboutCompanyViewModel> branchList = new List<AboutCompanyViewModel>();
                    string branchListofCompany = organizationServiceClient.GeSpecifictBranchList(aboutCompanyViewModel.CompanyID);
                    DataSet dsbranchList = new DataSet();
                    dsbranchList.ReadXml(new StringReader(branchListofCompany));
                    aboutCompanyViewModel.CompaniesList = new List<SelectListItem>();
                    if (dsbranchList.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsbranchList.Tables[0].Rows)
                        {
                            AboutCompanyViewModel listOfBranch = new AboutCompanyViewModel
                            {
                                OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                                CompanyNameList = row["Company_Name"].ToString(),
                                IndustryType = row["Industry_Type"].ToString(),
                                //CountryID = Convert.ToInt32(row["Country_ID"]),
                                //StateID = Convert.ToInt32(row["State_ID"]),
                                //CityID = Convert.ToInt32(row["City_ID"]),
                                logo = row["logo"].ToString()
                            };
                            branchList.Add(listOfBranch);

                        }
                    }

                    aboutCompanyViewModel.AboutGroupCompany = branchList;
                    Session["Branch"] = aboutCompanyViewModel.CompaniesList;
                }


            return View("_AboutCompany",aboutCompanyViewModel);
        }
        [HttpPost]
        public ActionResult AboutCompany(AboutCompanyViewModel aboutCompanyViewModel)
        {
                Session["CompanyID"] = aboutCompanyViewModel.CompanyID;
                Session["CompanyName"] = aboutCompanyViewModel.CompanyName;
                Session["ParentCompanyID"] = aboutCompanyViewModel.ParentCompanyID;
            int id = aboutCompanyViewModel.ParentCompanyID;


            if (aboutCompanyViewModel.ParentCompanyID == 0)
            {
                return RedirectToAction("AddCompany", new { id = id });
            }
            else
            {
                //Button btn = new Button();
               // string button = "Add Branch";
                //if (btn.Text == "Add Branch")
                {
                    return RedirectToAction("AddBranch");
                }
                //else
                //{
                  //  return RedirectToAction("AddVendor");
                //}
            }
        }
        [HttpGet]
        public ActionResult AboutBranch(int id)
        {
            AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();

            //  int id = Convert.ToInt32(TempData["ID"]);
           
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string aboutcompany = organizationServiceClient.getCompanyListsforBranch(id);
            DataSet dsaboutCompany = new DataSet();
            dsaboutCompany.ReadXml(new StringReader(aboutcompany));
            aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
            aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
            aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
            aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
            aboutCompanyViewModel.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));

            aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);
            //aboutCompanyViewModel.CompanyID = Convert.ToInt32(Session["CompanyID"]);
            //aboutCompanyViewModel.CompanyDescription = Convert.ToString(Session["CompanyDescription"]);
            //aboutCompanyViewModel.CompanyName = Convert.ToString(Session["CompanyName"]);
            //aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(Session["ParentCompanyID"]);
            return View("_AboutChildCompany", aboutCompanyViewModel);
        }

        [HttpPost]
        public ActionResult AboutBranch(AboutCompanyViewModel aboutCompanyViewModel)
        {
            //Session["BranchID"] = aboutCompanyViewModel.CompanyID;
            //Session["CompanyName"] = aboutCompanyViewModel.CompanyName;
            //Session["ParentCompanyID"] = aboutCompanyViewModel.ParentCompanyID;
            int id = aboutCompanyViewModel.ParentCompanyID;

            return RedirectToAction("BranchList", new { id = id });
        }
        
        public ActionResult BranchList(int id)
        {

            ////    OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            ////    string xmldata = client.GeSpecifictBranchList(id);
            ////    DataSet ds = new DataSet();
            ////    ds.ReadXml(new StringReader(xmldata));
            ////    List<Organization> userlist = new List<Organization>();
            ////    if (ds.Tables.Count > 0)
            ////    {
            ////        foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            ////        {
            ////            userlist.Add(new Organization
            ////            {
            ////                Organization_Id = Convert.ToInt32(row["Org_Hier_ID"]),
            ////                Company_Name = Convert.ToString(row["Company_Name"]),
            ////                logo = Convert.ToString(row["logo"])
            ////            });
            ////        }
            ////    }
            ////    return View("_BranchDashBoard", userlist);
            ////}
            /////////
            List<ListOfGroupCompanies> branchlist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlCompanies = organizationservice.GeSpecifictBranchList(id);

            DataSet dsSpecificBranchList = new DataSet();
            dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
            foreach (System.Data.DataRow row in dsSpecificBranchList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Logo=Convert.ToString(row["logo"])                 
                };
                branchlist.Add(listOfBranch);

            }


            return View("_BranchDashBoard", branchlist);
        }

    

        [HttpGet]
        public ActionResult AboutVendor(int id)
        {
            AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();


            //  int id = Convert.ToInt32(TempData["ID"]);

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string aboutcompany = organizationServiceClient.getCompanyListsforBranch(id);
            DataSet dsaboutCompany = new DataSet();
            dsaboutCompany.ReadXml(new StringReader(aboutcompany));
            aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
            aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
            aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
            aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
            aboutCompanyViewModel.Is_Active =Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));
            aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);

            return View("_AboutVendor", aboutCompanyViewModel);
        }

        [HttpPost]
        public ActionResult AboutVendor(AboutCompanyViewModel aboutCompanyViewModel)
        {
            //Session["VendorID"] = aboutCompanyViewModel.VendorID;
            //Session["VendorName"] = aboutCompanyViewModel.VendorName;
            // Session["CompanyID"] = aboutCompanyViewModel.CompanyID;

            int id = aboutCompanyViewModel.ParentCompanyID;
            return RedirectToAction("CompanyVendorsList", new { id = id });
        }

        public ActionResult CompanyVendorsList(int id)
        {

            // int BranchID = Convert.ToInt32(Session["BranchID"]);
            //  int CompanyID = Convert.ToInt32(Session["ParentCompanyID"]);
            List<ListOfGroupCompanies> vendorlist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlVendors = organizationservice.GetVendors(id);

            DataSet dsSpecificVendorList = new DataSet();
            dsSpecificVendorList.ReadXml(new StringReader(strxmlVendors));
            foreach (System.Data.DataRow row in dsSpecificVendorList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfVendors = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Logo = Convert.ToString(row["logo"])

                };
                vendorlist.Add(listOfVendors);

            }


            return View("_VendorDashBoardForCompany", vendorlist);
        }


        public ActionResult BranchVendorsList(int id)
        {

            // int BranchID = Convert.ToInt32(Session["BranchID"]);
            //  int CompanyID = Convert.ToInt32(Session["ParentCompanyID"]);
            List<ListOfGroupCompanies> branchvendorlist = new List<ListOfGroupCompanies>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlBranchVendors = organizationservice.GetVendors(id);

            DataSet dsSpecificVendorListforBranch = new DataSet();
            dsSpecificVendorListforBranch.ReadXml(new StringReader(strxmlBranchVendors));
            foreach (System.Data.DataRow row in dsSpecificVendorListforBranch.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfVendors = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Logo = Convert.ToString(row["logo"])
                    
                };
                branchvendorlist.Add(listOfVendors);

            }


            return View("_VendorDashBoardForBranch", branchvendorlist);
        }






        public ActionResult AssignVendorForBranch()
        {

            BranchViewModel branchVM = new BranchViewModel();

            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            // branchVM.organization = new Organization();
            // branchVM.organization.Organization_Id = 0;
            // branchVM.branch = new BranchLocation();
            // branchVM.branch.Branch_Id = 0;

            string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            branchVM.GroupCompaniesList = new List<SelectListItem>();
            branchVM.GroupCompaniesList.Add(new SelectListItem { Text = "--Select Group Company--", Value = "0" });
            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    branchVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }

            int groupcompid = 0;
            string strXMLCompanyList = organizationservice.GeSpecifictCompaniesList(groupcompid);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            branchVM.CompaniesList = new List<SelectListItem>();
            branchVM.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
            if (dsCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                {
                    branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }

            int compid = 0;
            string strXMLBranchList = organizationservice.GeSpecifictBranchList(compid);
            DataSet dsBranchList = new DataSet();
            dsBranchList.ReadXml(new StringReader(strXMLBranchList));
            branchVM.BranchList = new List<SelectListItem>();
            branchVM.BranchList.Add(new SelectListItem { Text = "--Select Branch--", Value = "0" });
            if (dsBranchList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsBranchList.Tables[0].Rows)
                {
                    branchVM.BranchList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }


            int CompanyID = 0;
            string xmlVndors = organizationservice.GetVendors(CompanyID);
            DataSet dsVendors = new DataSet();
            dsVendors.ReadXml(new StringReader(xmlVndors));
            branchVM.VendorList = new List<SelectListItem>();

            if (dsVendors.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsVendors.Tables[0].Rows)
                {
                    branchVM.VendorList.Add(new SelectListItem { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
            return View("_AssignVendorToBranch", branchVM);
        }


        [HttpPost]
        public ActionResult AssignVendorForBranch(BranchViewModel branchViewModel)
        {
           
            if (ModelState.IsValid)
            {
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                bool result = vendorServiceClient.insertVendorForBranch(branchViewModel.VendorID, branchViewModel.BranchID,
                    branchViewModel.VendorStartDate, Convert.ToDateTime(branchViewModel.VendorEndDate), branchViewModel.IsVendorActive);

                if (result != false)
                {
                    Session["GroupCompanyID"] = branchViewModel.GroupCompanyID;
                    Session["CompanyID"] = branchViewModel.CompanyID;
                    Session["BranchID"] = branchViewModel.BranchID;
                    Session["VendorID"] = branchViewModel.VendorID;
                    return RedirectToAction("VendorListForCompany");

                }
                //Session["GroupCompanyID"] = branchViewModel.GroupCompanyID;
                //Session["CompanyID"] = branchViewModel.CompanyID;
                //Session["BranchID"] = branchViewModel.BranchID;
                //Session["VendorID"] = branchViewModel.VendorID;

            }
            ModelState.AddModelError("", ConfigurationManager.AppSettings["ERROR"]);
            return View("_AssignVendorToBranch");

        }

        
        public ActionResult GroupCompanyList()
        { 
        List<ListOfGroupCompanies> branchlist = new List<ListOfGroupCompanies>();
        OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlCompanies = organizationservice.GetGroupCompaniesList();

        DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strxmlCompanies));
            foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
            {
                ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
                {
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Logo=Convert.ToString(row["logo"])


                };
                 branchlist.Add(listOfBranch);

            }


            return View("_DashBoardGroupCompany", branchlist);
}


        [HttpGet]
        public ActionResult DeactivateVendorUnderCompany(int OrgID)
        {
            //OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            // organizationViewModel.organization = new Organization();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string strxmlData = organizationServiceClient.getCompanyListsforBranch(OrgID);
            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32( dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("_DeactivateVendorForCompany",orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeactivateVendorUnderCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
               // OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                result = Convert.ToBoolean(vendorServiceClient.DeactivateVendorForCompany(orgActivateDeactivateViewModel.CompanyID));
                if (result == true)
                {
                    return RedirectToAction("CompanyVendorsList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                }

            }
            return View();
        }
    

    public ActionResult DeactivateVendorUnderBranch(int OrgID)
    {
        //OrganizationViewModel organizationViewModel = new OrganizationViewModel();
        OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
        // organizationViewModel.organization = new Organization();

        OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

            string strxmlData = organizationServiceClient.getCompanyListsforBranch(OrgID);
           
            DataSet dsData = new DataSet();
        dsData.ReadXml(new StringReader(strxmlData));
        orgActivateDeactivateViewModel.CompanyID = OrgID;
        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
        return View("_DeacvtivateVendorForBranch", orgActivateDeactivateViewModel);
    }
    [HttpPost]
    public ActionResult DeactivateVendorUnderBranch(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
    {
        if (ModelState.IsValid)
        {
            bool result = false;
            // OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
            result = Convert.ToBoolean(vendorServiceClient.DeactivateVendorForBranch(orgActivateDeactivateViewModel.CompanyID));
            if (result == true)
            {
                return RedirectToAction("BranchVendorsList");
            }

        }
        return View();
    }



        public ActionResult ActivateVendorUnderCompany(int OrgID)
        {
            //OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            // organizationViewModel.organization = new Organization();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

            string strxmlData = organizationServiceClient.getCompanyListsforBranch(OrgID);

            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("_ActivateVendorForCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult ActivateVendorUnderCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                // OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                result = Convert.ToBoolean(vendorServiceClient.ActivateVendorForCompany(orgActivateDeactivateViewModel.CompanyID));
                if (result == true)
                {
                    return RedirectToAction("CompanyVendorsList", new { id = orgActivateDeactivateViewModel.ParentCompanyID});
                }

            }
            return View();
        }
        public ActionResult ActivateVendorUnderBranch(int OrgID)
        {
            //OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            // organizationViewModel.organization = new Organization();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

            string strxmlData = organizationServiceClient.getCompanyListsforBranch(OrgID);

            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            return View("_AcvtivateVendorForBranch", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult ActivateVendorUnderBranch(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                // OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                result = Convert.ToBoolean(vendorServiceClient.ActivateVendorForBranch(orgActivateDeactivateViewModel.CompanyID));
                if (result == true)
                {
                    return RedirectToAction("BranchVendorsList");
                }

            }
            return View();
        }
        public ActionResult DeleteVendorUnderCompany(int OrgID)
        {
            //OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            // organizationViewModel.organization = new Organization();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

            string strxmlData = organizationServiceClient.getCompanyListsforBranch(OrgID);

            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("_DeleteVendorUnderCompany", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeleteVendorUnderCompany(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                // OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                result = Convert.ToBoolean(vendorServiceClient.DeleteVendorForCompany(orgActivateDeactivateViewModel.CompanyID));
                if (result == true)
                {
                    return RedirectToAction("CompanyVendorsList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                }

            }
            return View();
        }
        public ActionResult DeleteVendorUnderBranch(int OrgID)
        {
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

            string strxmlData = organizationServiceClient.getCompanyListsforBranch(OrgID);

            DataSet dsData = new DataSet();
            dsData.ReadXml(new StringReader(strxmlData));
            orgActivateDeactivateViewModel.CompanyID = OrgID;
            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
            orgActivateDeactivateViewModel.ParentCompanyID =Convert.ToInt32( dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
            return View("_DeleteVendorForBranch", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult DeleteVendorUnderBranch(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                result = Convert.ToBoolean(vendorServiceClient.DeactivateVendorForBranch(orgActivateDeactivateViewModel.CompanyID));
                if (result == true)
                {
                    return RedirectToAction("BranchVendorsList");
                }

            }
            return View();
        }

    }

}



