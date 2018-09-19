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
using System.Configuration;
using System.Web.UI.WebControls;
using System.Dynamic;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageOrganizationController : Controller
    {
        // GET: ManageOrganization

        [HttpGet]
        public ActionResult AddGroupCompany()
        {
            OrganizationViewModel organizationVM = new OrganizationViewModel();
            try
            {
                organizationVM.organization = new Organization();
                organizationVM.organization.Organization_Id = 0;
            }
            catch (Exception ex)
            {
                return View("ErrorPage");
            }
            return View("_Organization", organizationVM);
        }

        [HttpPost]
        public ActionResult AddGroupCompany(OrganizationViewModel organizationVM, HttpPostedFileBase file)
        {
            try
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
                    else
                    {
                        CommonController common = new CommonController();
                        organizationVM.organization.logo = Path.GetFileName("noimage.png");
                        string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName("noimage.png"));
                        string message = common.UploadFile(file, filePath);
                        ModelState.AddModelError("org_hier.logo", message);
                    }
                    OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                    int GroupCompid = 0;
                    organizationVM.organization.User_Id = Convert.ToInt32(Session["UserID"]);
                    organizationVM.organization.Is_Leaf = false;
                    organizationVM.organization.Level = 1;
                    organizationVM.organization.Is_Active = true;
                    organizationVM.organization.Is_Delete = false;
                    organizationVM.organization.Is_Vendor = false;
                    organizationVM.organization.Parent_Company_Id = 0;
                    GroupCompid = organizationClient.insertOrganization(organizationVM.organization);
                    if (GroupCompid != 0)
                    {
                        TempData["ParentCompanyID"] = organizationVM.organization.Organization_Id;
                        string appkey = String.Join("", "group", GroupCompid);
                        string path = string.Join("/", ConfigurationManager.AppSettings["FilePath"], appkey);
                        Directory.CreateDirectory(Path.Combine(Server.MapPath(path)));
                        // ConfigurationManager.AppSettings[appkey] = path;
                        TempData["Success"] = "Group Company created successfully!!!";
                        return RedirectToAction("AboutGroupCompany", new { id = GroupCompid });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Group Company creation was not successfull. Please try again!!!";
                    }
                }
                else
                {
                    ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
                    organizationVM.organization.Organization_Id = 0;
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View("_Organization", organizationVM);
        }

        [HttpGet]
        public ActionResult UpdateGroupCompany(int OrgID)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            try
            {
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                string strxmlUpdatedData = organizationClient.getGroupOrganization(OrgID);
                DataSet dsUpdatedData = new DataSet();
                dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
                organizationViewModel.organization = new Organization();
                if (dsUpdatedData.Tables.Count > 0)
                {
                    organizationViewModel.organization.Organization_Id = OrgID;
                    organizationViewModel.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
                    organizationViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
                    organizationViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
                    organizationViewModel.organization.Type = dsUpdatedData.Tables[0].Rows[0]["Type"].ToString();
                    organizationViewModel.organization.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Active"]));
                    organizationViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
                    organizationViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
                    organizationViewModel.organization.logo = dsUpdatedData.Tables[0].Rows[0]["logo"].ToString();
                    Session["Logo"] = organizationViewModel.organization.logo;
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View("_Organization", organizationViewModel);
        }

        [HttpPost]
        public ActionResult UpdateGroupCompany(OrganizationViewModel organizationVM, HttpPostedFileBase file)
        {
            try
            {
                organizationVM.organization.logo = Convert.ToString(Session["Logo"]);
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
                    else
                    {
                        CommonController common = new CommonController();
                        organizationVM.organization.logo = Path.GetFileName("noimage.png");
                        string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName("noimage.png"));
                        string message = common.UploadFile(file, filePath);
                        ModelState.AddModelError("org_hier.logo", message);
                    }
                    bool result = false;
                    OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                    result = organizationClient.updateOrganization(organizationVM.organization);
                    if (result != false)
                    {
                        TempData["Success"] = "Updated successfully!!!";
                        return RedirectToAction("AboutGroupCompany", new { id = organizationVM.organization.Organization_Id });
                    }
                }
                ModelState.AddModelError("Company_Name", "Error occurred while updating");
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return RedirectToAction("UpdateGroupCompany", new { OrgID = organizationVM.organization.Organization_Id });
        }


        public ActionResult DeactivateGroupCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = false;
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

                    string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                    }
                    result = organizationServiceClient.DeactivateGroupCompany(orgActivateDeactivateViewModel.CompanyID);

                    if (result == true)
                    {
                        TempData["Success"] = "deactivated successfully!!!";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        return RedirectToAction("GroupCompanyList");
                    }
                }
            }

            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View("ErrorPage");
        }
        public ActionResult ActivateGroupCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool result = false;
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

                    string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                    }
                    result = organizationServiceClient.ActivateGroupCompany(orgActivateDeactivateViewModel.CompanyID);

                    if (result == true)
                    {
                        TempData["Success"] = "activated successfully!!!";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        return RedirectToAction("GroupCompanyList");
                    }
                }
            }

            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View("ErrorPage");
        }

        public ActionResult DeleteGroupCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool result = false;
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

                    string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                    }
                    result = organizationServiceClient.DeleteGroupCompany(orgActivateDeactivateViewModel.CompanyID);

                    if (result == true)
                    {
                        TempData["Success"] = "deleted successfully!!!";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        return RedirectToAction("GroupCompanyList");
                    }
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View("ErrorPage");
        }

        [HttpGet]
        public ActionResult AboutGroupCompany(int id)
        {
            AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
            try
            {
                aboutCompanyViewModel.AboutGroupCompany = new List<AboutCompanyViewModel>();
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();

                string aboutcompany = organizationServiceClient.getGroupOrganization(id);
                DataSet dsaboutCompany = new DataSet();
                dsaboutCompany.ReadXml(new StringReader(aboutcompany));
                if (dsaboutCompany.Tables.Count > 0)
                {
                    aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
                    aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
                    aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
                    aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
                    aboutCompanyViewModel.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));
                    {
                        aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);
                    }
                    Session["GroupCompanyName"] = aboutCompanyViewModel.CompanyName;
                    Session["GroupCompanyID"] = aboutCompanyViewModel.CompanyID;
                }


                if (aboutCompanyViewModel.ParentCompanyID == 0)
                {
                    List<AboutCompanyViewModel> companylist = new List<AboutCompanyViewModel>();
                    string companyListofGroupCompany = organizationServiceClient.GeSpecifictCompaniesList(aboutCompanyViewModel.CompanyID);
                    DataSet dscompanyList = new DataSet();
                    dscompanyList.ReadXml(new StringReader(companyListofGroupCompany));
                    aboutCompanyViewModel.CompaniesList = new List<SelectListItem>();

                    if (dscompanyList.Tables.Count > 0)
                    {
                        DataView dv = new DataView(dscompanyList.Tables[0]);
                        dv.Sort = "Is_Active desc";
                        DataTable dtdeactive = dv.ToTable();



                        foreach (System.Data.DataRow row in dtdeactive.Rows)
                        {
                            AboutCompanyViewModel listOfComp = new AboutCompanyViewModel
                            {
                                OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                                CompanyNameList = row["Company_Name"].ToString(),
                                IndustryType = row["Type"].ToString(),
                                Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                                logo = row["logo"].ToString()
                            };
                            companylist.Add(listOfComp);
                        }
                    }


                    aboutCompanyViewModel.AboutGroupCompany = companylist;
                    Session["Company"] = aboutCompanyViewModel.AboutGroupCompany;
                    aboutCompanyViewModel.AboutCompany = new List<AboutCompanyViewModel>();
                    if (TempData["Success"] != null)
                    {
                        return View("_AboutGroupCompany", aboutCompanyViewModel);
                    }
                    else if (aboutCompanyViewModel.AboutGroupCompany.Count == 0)
                    {
                        ViewBag.Message = "No companies found";
                    }
                    else
                    {
                        return View("_AboutGroupCompany", aboutCompanyViewModel);
                    }
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }

            return View("_AboutGroupCompany", aboutCompanyViewModel);
        }



        [HttpGet]
        public ActionResult AddCompany()
        {
            CompanyViewModel companyVM = new CompanyViewModel();
            companyVM.ComplianceList = new List<SelectListItem>();
            companyVM.organization = new Organization();
            companyVM.branch = new BranchLocation();
            companyVM.companydetails = new CompanyDetails();
            companyVM.GroupCompaniesList = new List<SelectListItem>();
            companyVM.Country = new List<SelectListItem>();
            companyVM.State = new List<SelectListItem>();
            companyVM.City = new List<SelectListItem>();
            companyVM.IndustryTypeList = new List<SelectListItem>();



            if (Convert.ToInt32(Session["GroupCompanyId"]) != 0)
            {
                try
                {
                    companyVM.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                    {
                        var groupcopmpanyid = Request.QueryString["Orgid"];
                        if (groupcopmpanyid != null)
                        {
                            companyVM.GroupCompanyID = Convert.ToInt32(groupcopmpanyid);
                        }
                    }
                    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                    companyVM.organization.Organization_Id = 0;
                    companyVM.branch.Branch_Id = 0;
                    companyVM.companydetails.Company_Details_ID = 0;


                    string strXMLGroupCompanyList = organizationservice.getParticularGroupCompaniesList(companyVM.GroupCompanyID);
                    DataSet dsGroupCompanyList = new DataSet();
                    dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));

                    if (dsGroupCompanyList.Tables.Count > 0)
                    {
                        companyVM.GroupCompanyName = Convert.ToString(dsGroupCompanyList.Tables[0].Rows[0]["Company_Name"]);
                    }
                    string strXMLCountries = organizationservice.GetCountryList();
                    DataSet dsCountries = new DataSet();
                    dsCountries.ReadXml(new StringReader(strXMLCountries));
                    companyVM.Country.Add(new SelectListItem { Text = "-- Select Country --", Value = "" });
                    if (dsCountries.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                        {
                            companyVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                        }
                    }
                    companyVM.State.Add(new SelectListItem { Text = "-- Select State --", Value = "" });
                    companyVM.City.Add(new SelectListItem { Text = "-- Select City --", Value = "" });

                    string strXMLIndustryType = organizationservice.GetIndustryType();
                    DataSet dsIndustryType = new DataSet();
                    dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
                    companyVM.IndustryTypeList.Add(new SelectListItem { Text = "-- Industry Type --", Value = "" });
                    if (dsIndustryType.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                        {
                            companyVM.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                        }
                    }

                    int CountryID = 0;
                    int IndustryTypeID = 0;
                    
                    string xmlComplianceType = organizationservice.GetComplianceType(CountryID, IndustryTypeID);
                    DataSet dsCompliance = new DataSet();
                    dsCompliance.ReadXml(new StringReader(xmlComplianceType));


                    TempData["CompleteCompanyDetails"] = companyVM;

                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }


            }
            else
            {
                ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);

            }

            return View("_Company", companyVM);
        }


        [HttpPost]
        public ActionResult AddCompany(CompanyViewModel companyVM, HttpPostedFileBase file)
        {
            int id = 0;
            try
            {
                if (companyVM.companydetails.Calender_StartDate != DateTime.MinValue)
                {
                    int year = DateTime.Now.Year;
                    if (companyVM.companydetails.Calender_StartDate.Year <= year || companyVM.companydetails.Calender_StartDate.Year > year)
                    {
                        DateTime date = new DateTime();
                        date.AddDays(31);
                        if (companyVM.companydetails.Calender_StartDate.Month == 04)
                        {
                            date.AddMonths(03);
                            date.AddYears(companyVM.companydetails.Calender_StartDate.Year + 1);
                        }
                        else
                        {
                            date.AddMonths(12);
                        }
                        companyVM.companydetails.Calender_EndDate = date;
                    }
                    else
                        ModelState.AddModelError("Startdate", "Please enter the vaild Calender start date");
                }
                else
                {
                    DateTime date = new DateTime();
                    date.AddDays(01);
                    date.AddMonths(04);
                    date.AddYears(DateTime.Now.Year);
                    companyVM.companydetails.Calender_StartDate = date;
                    date.AddDays(31);
                    date.AddMonths(03);
                    date.AddYears(DateTime.Now.Year + 1);
                    companyVM.companydetails.Calender_EndDate = date;
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
                    else
                    {
                        CommonController common = new CommonController();
                        companyVM.organization.logo = Path.GetFileName("noimage.png");
                        string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName("noimage.png"));
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
                    TempData["CName"] = companyVM.organization.Company_Name;
                    id = organizationClient.insertCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);
                    int resultID = organizationClient.insertcomplianceTypes(companyVM.ComplianceID, id);
                    if (id != 0)
                    {
                        TempData["ParentCompany_ID"] = companyVM.organization.Parent_Company_Id;
                        TempData["Success"] = "created successfully!!!";
                        //BranchViewModel branchViewModel = new BranchViewModel();
                        //branchViewModel.organization = new Organization();
                        //branchViewModel.branch = new BranchLocation();
                        //branchViewModel.organization.Is_Active = true;
                        //branchViewModel.organization.Level = 3;
                        //branchViewModel.organization.Is_Leaf = true;
                        //branchViewModel.organization.Is_Vendor = false;
                        //branchViewModel.organization.Parent_Company_Id = id;
                        //branchViewModel.organization.User_Id = Convert.ToInt32(Session["UserID"]);
                        //branchViewModel.branch.Country_Id = companyVM.branch.Country_Id;
                        //branchViewModel.branch.State_Id = companyVM.branch.State_Id;
                        //branchViewModel.branch.City_Id = companyVM.branch.City_Id;
                        //branchViewModel.organization.Company_Name = "HeadQuarter" + companyVM.organization.Company_Name;
                        //branchViewModel.organization.Type = "Head Quarter";
                        //branchViewModel.branch.Postal_Code = companyVM.branch.Postal_Code;
                        //string strXMLComplianceTyp = organizationClient.GetAssignedComplianceTypes(id);
                        //DataSet dsComplianceTyp = new DataSet();
                        //dsComplianceTyp.ReadXml(new StringReader(strXMLComplianceTyp));
                        //branchViewModel.ComplianceList = new List<SelectListItem>();
                        //foreach (System.Data.DataRow row in dsComplianceTyp.Tables[0].Rows)
                        //{
                        //    branchViewModel.ComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                        //}
                        //int headQuarterid = Convert.ToInt32(organizationClient.insertBranch(branchViewModel.organization, branchViewModel.branch));
                        //int resid = organizationClient.insertcomplianceTypes(companyVM.ComplianceID, headQuarterid);

                        //return RedirectToAction("AboutCompany", new { id = id });
                        return RedirectToAction("SelectGroupCompany", new { id = id });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = " Company creation was not successfull. Please try again!!!";
                    }
                }
                else
                {
                    ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
                    companyVM.organization.Organization_Id = 0;
                    companyVM = (CompanyViewModel)TempData["CompleteCompanyDetails"];
                }
            }
            catch (Exception)

            {
                return View("ErrorPage");
            }
            return View("_Company", companyVM);
        }


        [HttpGet]
        public ActionResult UpdateCompany(int OrgID)
        {
            CompanyViewModel companyVM = new CompanyViewModel();

            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            try
            {

                string strxmlUpdatedData = organizationClient.getGroupCompany(OrgID);
                DataSet dsUpdatedData = new DataSet();
                dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));



                if (dsUpdatedData.Tables.Count > 0)
                {
                    companyVM.organization = new Organization();
                    companyVM.companydetails = new CompanyDetails();
                    companyVM.branch = new BranchLocation();




                    companyVM.organization.Organization_Id = OrgID;
                    companyVM.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
                    companyVM.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
                    companyVM.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
                    companyVM.organization.Type = dsUpdatedData.Tables[0].Rows[0]["Type"].ToString();
                    companyVM.organization.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Active"]));
                    companyVM.organization.Is_Delete = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Delete"]));
                    companyVM.organization.Is_Leaf = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Leaf"]));
                    companyVM.organization.Is_Vendor = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Vendor"]));
                    companyVM.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
                    companyVM.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
                    companyVM.organization.Parent_Company_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);
                    companyVM.organization.Level = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Level"]);
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
                    companyVM.companydetails.Industry_Type_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Industry_Type_ID"]);

                    string strxmlData = organizationClient.getParticularGroupCompaniesList(companyVM.organization.Parent_Company_Id);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        companyVM.GroupCompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                    }

                    string strXMLIndustryType = organizationClient.GetIndustryType();
                    DataSet dsIndustryType = new DataSet();
                    dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
                    companyVM.IndustryTypeList = new List<SelectListItem>();
                    companyVM.IndustryTypeList.Add(new SelectListItem { Text = "-- Industry Type --", Value = "0" });
                    if (dsIndustryType.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                        {

                            companyVM.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });

                        }
                    }


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


                    string strXMLGroupCompanyList = organizationClient.getParticularGroupCompaniesList(companyVM.GroupCompanyID);
                    DataSet dsGroupCompanyList = new DataSet();
                    dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                    if (dsGroupCompanyList.Tables.Count > 0)
                    {
                        companyVM.GroupCompaniesList = new List<SelectListItem>();
                        if (dsGroupCompanyList.Tables.Count > 0)
                        {
                            foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                            {

                                companyVM.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });

                            }
                        }

                    }



                    string xmldata = organizationClient.GetComplianceType(companyVM.branch.Country_Id, companyVM.companydetails.Industry_Type_ID);
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xmldata));
                    companyVM.ComplianceList = new List<SelectListItem>();
                    DataSet dsrole = new DataSet();
                    xmldata = organizationClient.GetAssignedComplianceTypes(companyVM.organization.Organization_Id);
                    dsrole.ReadXml(new StringReader(xmldata));
                    if (ds.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                        {
                            bool selected = false;
                            if (dsrole.Tables.Count > 0)
                            {
                                foreach (System.Data.DataRow id in dsrole.Tables[0].Rows)
                                {
                                    if (Convert.ToInt32(id["Compliance_Type_ID"]) == Convert.ToInt32(row["Compliance_Type_ID"]))
                                    {
                                        selected = true;
                                        break;
                                    }
                                }
                            }
                            companyVM.ComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString(), Selected = selected });
                        }
                    }

                }
            }

            catch (Exception)
            {
                return View("ErrorPage");

            }








            return View("_Company", companyVM);
        }

        [HttpPost]
        public ActionResult UpdateCompany(CompanyViewModel companyVM, HttpPostedFileBase file)
        {
            try
            {
                if (companyVM.companydetails.Calender_StartDate == null)
                {
                    companyVM.companydetails.Calender_StartDate = Convert.ToDateTime(DateTime.MinValue.ToString("dd-MM-yyyy"));
                }
                if (companyVM.companydetails.Calender_EndDate == null)
                {
                    companyVM.companydetails.Calender_EndDate = Convert.ToDateTime(DateTime.MaxValue.ToString("dd-MM-yyyy"));
                }
                bool result = false;


                companyVM.organization.logo = Convert.ToString(Session["Logo"]);
                if (file != null)
                {
                    CommonController common = new CommonController();
                    companyVM.organization.logo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }
                else
                {
                    CommonController common = new CommonController();
                    companyVM.organization.logo = Path.GetFileName("noimage.png");
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName("noimage.png"));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }

                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                companyVM.companydetails.Org_Hier_ID = companyVM.organization.Organization_Id;
                companyVM.branch.Org_Hier_ID = companyVM.organization.Organization_Id;
                companyVM.organization.Level = 2;
                result = organizationClient.updateCompany(companyVM.organization, companyVM.companydetails, companyVM.branch);
                organizationClient.DeleteCompliance(companyVM.organization.Organization_Id);
                organizationClient.insertcomplianceTypes(companyVM.ComplianceID, companyVM.organization.Organization_Id);


                if (result != false)
                {

                    TempData["Success"] = "Updated successfully!!!";
                    return RedirectToAction("AboutCompany", new { id = companyVM.organization.Organization_Id });

                }
                else
                {

                    return RedirectToAction("UpdateCompany", new { OrgID = companyVM.organization.Organization_Id });

                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
        }
        public ActionResult DeactivateCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool result = false;
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

                    string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {

                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                        orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);


                    }
                    result = organizationServiceClient.DeactivateCompany(orgActivateDeactivateViewModel.CompanyID);
                    if (result == true)
                    {

                        TempData["Success"] = "deactivated successfully!!!";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        //return RedirectToAction("ListofCompany", new { GroupCompanyid = orgActivateDeactivateViewModel.ParentCompanyID });
                        return RedirectToAction("SelectGroupCompany");


                    }

                }
            }

            catch (Exception)
            {
                return View("ErrorPage");
            }


            return View("ErrorPage");
        }
        public ActionResult ActivateCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool result = false;
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

                    string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {

                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                        orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);


                    }
                    result = organizationServiceClient.ActivateCompany(orgActivateDeactivateViewModel.CompanyID);
                    if (result == true)
                    {
                        TempData["Success"] = "activated successfully!!!";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        return RedirectToAction("SelectGroupCompany");

                    }
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");

            }

            return View("ErrorPage");

        }
        public ActionResult DeleteCompany(int Orgid)
        {
            try {
                if (ModelState.IsValid)
                {

                    bool result = false;
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();

                    string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {

                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                        orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);


                    }
                    result = organizationServiceClient.DeleteCompany(orgActivateDeactivateViewModel.CompanyID);
                    if (result == true)
                    {

                        TempData["Success"] = "deleted successfully!!!";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        return RedirectToAction("SelectGroupCompany");


                    }
                }
            }


            catch (Exception)
            {
                return View("ErrorPage");

            }

            return View("ErrorPage");
        }
        [HttpGet]
        public ActionResult AboutCompany(int id)
        {
            AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
            try
            {
                aboutCompanyViewModel.AboutBranch = new List<AboutCompanyViewModel>();
                aboutCompanyViewModel.AboutCompany = new List<AboutCompanyViewModel>();
                aboutCompanyViewModel.AboutGroupCompany = new List<AboutCompanyViewModel>();
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();

                string aboutcompany = organizationServiceClient.getCompanyListsforBranch(id);
                DataSet dsaboutCompany = new DataSet();
                dsaboutCompany.ReadXml(new StringReader(aboutcompany));
                if (dsaboutCompany.Tables.Count > 0)
                {

                    aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
                    aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
                    aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
                    aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
                    aboutCompanyViewModel.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));

                    aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);

                }
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
                            IndustryType = row["Type"].ToString(),
                            Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            logo = row["logo"].ToString()
                        };
                        branchList.Add(listOfBranch);
                    }
                }
                aboutCompanyViewModel.AboutGroupCompany = new List<AboutCompanyViewModel>();
                aboutCompanyViewModel.AboutCompany = branchList;
                Session["Branch"] = aboutCompanyViewModel.AboutCompany;
                List<AboutCompanyViewModel> vendorList = new List<AboutCompanyViewModel>();
                string vendorListofCompany = organizationServiceClient.GetVendors(aboutCompanyViewModel.CompanyID);
                DataSet dsvendorList = new DataSet();
                dsvendorList.ReadXml(new StringReader(vendorListofCompany));
                aboutCompanyViewModel.CompaniesList = new List<SelectListItem>();
                if (dsvendorList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsvendorList.Tables[0].Rows)
                    {
                        AboutCompanyViewModel listOfVendors = new AboutCompanyViewModel
                        {
                            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                            CompanyNameList = row["Company_Name"].ToString(),
                            IndustryType = row["Type"].ToString(),
                            Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            logo = row["logo"].ToString()
                        };
                        vendorList.Add(listOfVendors);
                    }
                }
                aboutCompanyViewModel.AboutBranch = vendorList;
                Session["Branch"] = aboutCompanyViewModel.AboutGroupCompany;
                if (TempData["Success"] != null)
                {
                    return View("_AboutCompany", aboutCompanyViewModel);

                }
                else if (aboutCompanyViewModel.AboutCompany.Count == 0 && aboutCompanyViewModel.AboutBranch.Count == 0)
                {
                    ViewBag.Message = "No branches found";
                    ViewBag.MessageV = "No vendors found";
                }
                else if (aboutCompanyViewModel.AboutCompany.Count == 0)
                {
                    ViewBag.Message = "No branches found";
                }
                else if (aboutCompanyViewModel.AboutBranch.Count == 0)
                {
                    ViewBag.MessageV = "No vendors found";
                }
                else
                {
                    return View("_AboutCompany", aboutCompanyViewModel);

                }
            }

            catch (Exception)
            {
                return View("ErrorPage");

            }

            return View("_AboutCompany", aboutCompanyViewModel);
        }
        [HttpPost]
        public ActionResult AboutCompany(AboutCompanyViewModel aboutCompanyViewModel)
        {
            try
            {
                TempData["CompanyID"] = aboutCompanyViewModel.CompanyID;
                TempData["CompanyName"] = aboutCompanyViewModel.CompanyName;
                TempData["ParentCompany_ID"] = aboutCompanyViewModel.CompanyID;
                int id = aboutCompanyViewModel.CompanyID;
                {
                    return RedirectToAction("AddBranch");
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");

            }
        }



        [HttpGet]
        public ActionResult AddBranch()
        {
            BranchViewModel branchVM = new BranchViewModel();
            branchVM.GroupCompaniesList = new List<SelectListItem>();
            branchVM.CompaniesList = new List<SelectListItem>();
            branchVM.Country = new List<SelectListItem>();
            branchVM.State = new List<SelectListItem>();
            branchVM.City = new List<SelectListItem>();
            branchVM.ComplianceList = new List<SelectListItem>();
                branchVM.branch = new BranchLocation();
            branchVM.organization = new Organization();



            if (Convert.ToInt32(Session["GroupCompanyId"])!= 0)
            { 
            int id = 0;// session
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            try
            {
                var copmpanyid = Request.QueryString["Orgid"];
                if (copmpanyid != null)
                {
                    branchVM.CompanyID = Convert.ToInt32(copmpanyid);
                    branchVM.organization.Company_Id = branchVM.CompanyID;
                }

                branchVM.organization.Organization_Id = 0;
                branchVM.branch.Branch_Id = 0;
                string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
                DataSet dsGroupCompanyList = new DataSet();
                dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                string strXMLCompanyList = organizationservice.getCompanyListDropDown(Convert.ToInt32(Session["GroupCompanyId"]));
                DataSet dsCompanyList = new DataSet();
                dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                branchVM.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "" });
                if (dsCompanyList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                    {
                        branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                    }
                }

                if (TempData["CompanyID"] != null)
                {
                    int compid = Convert.ToInt32(TempData["CompanyID"]);
                    string strXMLDefaultCompanyDetails = organizationservice.getDefaultCompanyDetails(compid);
                    DataSet dsDefaultCompanyDetails = new DataSet();
                    dsDefaultCompanyDetails.ReadXml(new StringReader(strXMLDefaultCompanyDetails));
                  
                    branchVM.CompaniesList.Add(new SelectListItem { Text = "--Select Country--", Value = "" });
                    if (dsDefaultCompanyDetails.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsDefaultCompanyDetails.Tables[0].Rows)
                        {
                            branchVM.branch.Country_Id = Convert.ToInt32(dsDefaultCompanyDetails.Tables[0].Rows[0]["Country_ID"].ToString());
                            branchVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                            branchVM.branch.State_Id = Convert.ToInt32(dsDefaultCompanyDetails.Tables[0].Rows[0]["State_ID"].ToString());
                            branchVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });

                            branchVM.branch.City_Id = Convert.ToInt32(dsDefaultCompanyDetails.Tables[0].Rows[0]["City_ID"].ToString());
                            branchVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                        }
                    }
                    branchVM.State = new List<SelectListItem>();
                    branchVM.State.Add(new SelectListItem { Text = "--Select State--", Value = "" });
                    string strXMLDefaultStates = organizationservice.GetStateList(branchVM.branch.Country_Id);
                    DataSet dsDefaultStates = new DataSet();
                    dsDefaultStates.ReadXml(new StringReader(strXMLDefaultStates));
                    if (dsDefaultStates.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsDefaultStates.Tables[0].Rows)
                        {
                            branchVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                        }
                    }
                    branchVM.City = new List<SelectListItem>();
                    branchVM.City.Add(new SelectListItem { Text = "--Select City--", Value = "" });
                    string strXMLDefaulyCities = organizationservice.GetCityList(branchVM.branch.State_Id);
                    DataSet dsDefaultCities = new DataSet();
                    dsDefaultCities.ReadXml(new StringReader(strXMLDefaulyCities));
                    if (dsDefaultCities.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsDefaultCities.Tables[0].Rows)
                        {
                            branchVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                        }
                    }

                    string strXMLComplianceType = organizationservice.GetAssignedComplianceTypes((Convert.ToInt32(TempData["CompanyID"])));
                    DataSet dsComplianceType = new DataSet();
                    dsComplianceType.ReadXml(new StringReader(strXMLComplianceType));
                    foreach (System.Data.DataRow row in dsComplianceType.Tables[0].Rows)
                    {

                        branchVM.ComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                    }
                    return View("_Branch", branchVM);
                }
                if (branchVM.CompanyID != 0)
                {
                    string strXMLDefaultCompanyDetails = organizationservice.getDefaultCompanyDetails(branchVM.CompanyID);
                    DataSet dsDefaultCompanyDetails = new DataSet();
                    dsDefaultCompanyDetails.ReadXml(new StringReader(strXMLDefaultCompanyDetails));
                    branchVM.Country = new List<SelectListItem>();
                    branchVM.State = new List<SelectListItem>();
                    branchVM.City = new List<SelectListItem>();
                    branchVM.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
                    if (dsDefaultCompanyDetails.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsDefaultCompanyDetails.Tables[0].Rows)
                        {
                            branchVM.branch.Country_Id = Convert.ToInt32(dsDefaultCompanyDetails.Tables[0].Rows[0]["Country_ID"].ToString());
                            branchVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                            branchVM.branch.State_Id = Convert.ToInt32(dsDefaultCompanyDetails.Tables[0].Rows[0]["State_ID"].ToString());
                            branchVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });

                            branchVM.branch.City_Id = Convert.ToInt32(dsDefaultCompanyDetails.Tables[0].Rows[0]["City_ID"].ToString());
                            branchVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                        }
                        TempData["DefaultCompanyName"] = dsDefaultCompanyDetails.Tables[0].Rows[0]["Company_Name"].ToString();
                    }
                    branchVM.State = new List<SelectListItem>();
                    branchVM.State.Add(new SelectListItem { Text = "--Select State--", Value = "" });
                    string strXMLDefaultStates = organizationservice.GetStateList(branchVM.branch.Country_Id);
                    DataSet dsDefaultStates = new DataSet();
                    dsDefaultStates.ReadXml(new StringReader(strXMLDefaultStates));
                    if (dsDefaultStates.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsDefaultStates.Tables[0].Rows)
                        {
                            branchVM.State.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                        }
                    }
                    branchVM.City = new List<SelectListItem>();
                    branchVM.City.Add(new SelectListItem { Text = "--Select City--", Value = "" });
                    string strXMLDefaulyCities = organizationservice.GetCityList(branchVM.branch.State_Id);
                    DataSet dsDefaultCities = new DataSet();
                    dsDefaultCities.ReadXml(new StringReader(strXMLDefaulyCities));
                    if (dsDefaultCities.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsDefaultCities.Tables[0].Rows)
                        {
                            branchVM.City.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                        }
                    }

                    string strXMLComplianceTyp = organizationservice.GetAssignedComplianceTypes(branchVM.CompanyID);
                    DataSet dsComplianceTyp = new DataSet();
                    dsComplianceTyp.ReadXml(new StringReader(strXMLComplianceTyp));
                    branchVM.ComplianceList = new List<SelectListItem>();
                    foreach (System.Data.DataRow row in dsComplianceTyp.Tables[0].Rows)
                    {

                        branchVM.ComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                    }

                    return View("_Branch", branchVM);
                }

                string strXMLCountries = organizationservice.GetCountryList();
                DataSet dsCountries = new DataSet();
                dsCountries.ReadXml(new StringReader(strXMLCountries));
                branchVM.Country = new List<SelectListItem>();
                branchVM.Country.Add(new SelectListItem { Text = "--Select Country--", Value = "" });
                if (dsCompanyList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                    {
                        branchVM.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                    }
                }


                branchVM.State = new List<SelectListItem>();
                branchVM.State.Add(new SelectListItem { Text = "--Select State--", Value = "" });



                branchVM.City = new List<SelectListItem>();
                branchVM.City.Add(new SelectListItem { Text = "--Select City--", Value = "" });



                string strXMLComplianceTypes = organizationservice.GetAssignedComplianceTypes(branchVM.CompanyID);
                DataSet dsComplianceTypes = new DataSet();
                dsComplianceTypes.ReadXml(new StringReader(strXMLComplianceTypes));

            }
            catch (Exception ex)
            {

                return View("ErrorPage");
            }
            Session["CompleteBranchDetails"] = branchVM;
        }
        else
        {
            ModelState.AddModelError("",ConfigurationManager.AppSettings["SelectGroupCompany"]);
        }
            return View("_Branch", branchVM);

        }


        [HttpPost]
        public ActionResult AddBranch(BranchViewModel branchVM)//, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                try
                {
                    branchVM.organization.Is_Active = true;
                    branchVM.organization.Level = 3;
                    branchVM.organization.Is_Leaf = true;
                    branchVM.organization.Is_Vendor = false;
                    branchVM.organization.Parent_Company_Id = branchVM.CompanyID;
                    branchVM.organization.User_Id = Convert.ToInt32(Session["UserID"]);
                    int id = Convert.ToInt32(organizationClient.insertBranch(branchVM.organization, branchVM.branch));
                    int resultID = organizationClient.insertcomplianceTypes(branchVM.ComplianceID, id);
                    branchVM.organization.Organization_Id = id;

                    if (id != 0)
                    {
                        TempData["Success"] = "Branch created successfully for";
                        string appkey = String.Join("", "group", branchVM.GroupCompanyID);
                        string appcompany = String.Join("", "Company", branchVM.organization.Parent_Company_Id);
                        string appstring = String.Join("", "Branch", id);
                        string path = string.Join("/", ConfigurationManager.AppSettings["FilePath"], appkey, appcompany, appstring);
                        Directory.CreateDirectory(Path.Combine(Server.MapPath(path)));
                        //ConfigurationManager.AppSettings[appstring] = path;
                        if (branchVM.ChildCompanyName == null)
                        {
                            string data = organizationClient.getCompanyListsforBranch(branchVM.CompanyID);
                            DataSet dataSet = new DataSet();
                            dataSet.ReadXml(new StringReader(data));
                            branchVM.ChildCompanyName = dataSet.Tables[0].Rows[0]["Company_Name"].ToString();
                            branchVM.GroupCompanyName = Session["GroupCompanyName"].ToString();
                        }
                        Session["CompleteBranchDetails"] = null;
                        TempData["PID"] = branchVM.CompanyID;
                        TempData["ChildCompanyName"] = branchVM.ChildCompanyName;
                        //return RedirectToAction("AboutBranch", new { id = id });
                        return RedirectToAction("SelectCompany");
 
                    }
                    else


                    {
                        TempData["ErrorMessage"] = " Branch creation was not successfull. Please try again!!!";

                    }
                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }
            }
            else
            {
                try
                {
                    ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
                    branchVM.organization.Organization_Id = 0;
                    branchVM = (BranchViewModel)Session["CompleteBranchDetails"];
                }
                catch(Exception)
                {
                    return View("ErrorPage");
                }
            }
            return View("_Branch", branchVM);

        }


        [HttpGet]
        public ActionResult UpdateBranch(int OrgID)
        {
            BranchViewModel branchViewModel = new BranchViewModel();

            OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            try
            {
                string strxmlUpdatedData = organizationClient.getBranch(OrgID);
                DataSet dsUpdatedData = new DataSet();
                dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
                if (dsUpdatedData.Tables.Count > 0)
                {
                    branchViewModel.organization = new Organization();
                    branchViewModel.branch = new BranchLocation();

                    branchViewModel.organization.Organization_Id = OrgID;
                    branchViewModel.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
                    branchViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
                    branchViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
                    branchViewModel.organization.Type = dsUpdatedData.Tables[0].Rows[0]["Type"].ToString();
                    if (branchViewModel.organization.Type == "Head Quarter")
                    {
                        TempData["HeadQuarter"] = branchViewModel.organization.Type;
                    }

                    branchViewModel.organization.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Active"]));
                    branchViewModel.organization.Is_Delete = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Vendor"]));
                    branchViewModel.organization.Is_Leaf = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Leaf"]));
                    branchViewModel.organization.Level = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Level"]);
                    branchViewModel.organization.Last_Updated_Date = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Last_Updated_Date"]);
                    branchViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
                    branchViewModel.branch.Branch_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Location_ID"]);
                    branchViewModel.branch.Address = dsUpdatedData.Tables[0].Rows[0]["Address"].ToString();
                    branchViewModel.branch.Branch_Coordinates1 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates1"].ToString();
                    branchViewModel.branch.Branch_Coordinates2 = dsUpdatedData.Tables[0].Rows[0]["Branch_Coordinates2"].ToString();
                    branchViewModel.branch.Branch_CoordinatesURL = dsUpdatedData.Tables[0].Rows[0]["Branch_CoordinateURL"].ToString();
                    branchViewModel.branch.Branch_Name = dsUpdatedData.Tables[0].Rows[0]["Location_Name"].ToString();
                    branchViewModel.branch.Postal_Code = dsUpdatedData.Tables[0].Rows[0]["Postal_Code"].ToString();
                    branchViewModel.organization.Parent_Company_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);
                    branchViewModel.organization.logo = dsUpdatedData.Tables[0].Rows[0]["logo"].ToString();
                    branchViewModel.CompanyID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);




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
                }



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


                string strXMLCompanyList = organizationClient.getCompanyListsforBranch(branchViewModel.CompanyID);
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

                
                string xmldata = organizationClient.GetAssignedComplianceTypes(branchViewModel.CompanyID);
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                branchViewModel.ComplianceList = new List<SelectListItem>();
                DataSet dsrole = new DataSet();
                xmldata = organizationClient.GetAssignedComplianceTypes(branchViewModel.organization.Organization_Id);
                dsrole.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        bool selected = false;
                        if (dsrole.Tables.Count > 0)
                        {
                            foreach (System.Data.DataRow id in dsrole.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(id["Compliance_Type_ID"]) == Convert.ToInt32(row["Compliance_Type_ID"]))
                                {
                                    selected = true;
                                    break;
                                }
                            }
                        }
                        branchViewModel.ComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString(), Selected = selected });
                    }
                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }


            return View("_Branch", branchViewModel);
        }

        [HttpPost]
        public ActionResult UpdateBranch(BranchViewModel BranchVM, HttpPostedFileBase file)
        {
            try
            {
                BranchVM.organization.logo = Convert.ToString(Session["Logo"]);
            }
            catch(Exception)
            {
                return View("ErrorPage");

            }
            if (file != null)
            {
                CommonController common = new CommonController();
                BranchVM.organization.logo = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                string message = common.UploadFile(file, filePath);
                ModelState.AddModelError("org_hier.logo", message);
            }
            else
            {
                CommonController common = new CommonController();
                BranchVM.organization.logo = Path.GetFileName("noimage.png");
                string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName("noimage.png"));
                string message = common.UploadFile(file, filePath);
                ModelState.AddModelError("org_hier.logo", message);
            }
            
                bool result = false;
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
            try
            {
                BranchVM.branch.Org_Hier_ID = BranchVM.organization.Organization_Id;
                BranchVM.organization.Parent_Company_Id = BranchVM.CompanyID;
                result = organizationClient.updateBranch(BranchVM.organization, BranchVM.branch);
                organizationClient.DeleteCompliance(BranchVM.organization.Organization_Id);
                organizationClient.insertcomplianceTypes(BranchVM.ComplianceID, BranchVM.organization.Organization_Id);
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            try
            {

                if (result != false)
                {


                    TempData["Success"] = BranchVM.organization.Company_Name+ ""+" updated successfully";
                   

                    //return RedirectToAction("AboutBranch", new { id = BranchVM.organization.Organization_Id });
                    return RedirectToAction("SelectCompany");
                }




                else
                {
                    return RedirectToAction("UpdateBranch", new { OrgID = BranchVM.organization.Organization_Id });

                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }

        }



        public ActionResult ActivateBranch(int Orgid)
        {
           
                try
                {
                    if (ModelState.IsValid)
                    {

                        bool result = false;
                        OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                        OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
                        string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                        DataSet dsData = new DataSet();
                        dsData.ReadXml(new StringReader(strxmlData));
                        if (dsData.Tables.Count > 0)
                        {
                            orgActivateDeactivateViewModel.CompanyID = Orgid;
                            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                            orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);

                        }
                        
                        result = organizationServiceClient.ActivateBranch(orgActivateDeactivateViewModel.CompanyID);
                        if (result == true)
                        {
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        TempData["Success"] = "activated successfully!!!";

                            //return RedirectToAction("BranchList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                            BranchViewModel branchVM = new BranchViewModel();
                            branchVM.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                            branchVM.GroupCompaniesList = new List<SelectListItem>();
                            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                            string strXMLGroupCompanyList = organizationservice.getParticularGroupCompaniesList(branchVM.GroupCompanyID);//
                            DataSet dsGroupCompanyList = new DataSet();
                            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                        branchVM.GroupCompanyName = dsGroupCompanyList.Tables[0].Rows[0]["Company_Name"].ToString();
                           
                            string strXMLCompanyList = organizationservice.getCompanyListDropDown(branchVM.GroupCompanyID);
                            DataSet dsCompanyList = new DataSet();
                            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                            branchVM.CompaniesList = new List<SelectListItem>();
                            branchVM.CompaniesList.Add(new SelectListItem { Text = "-- Select Company --", Value = "0" });
                            if (dsCompanyList.Tables.Count > 0)
                            {
                                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                                {
                                    branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                                }
                            }


                            List<BranchViewModel> branchlist = new List<BranchViewModel>();
                        branchVM.CompanyID = orgActivateDeactivateViewModel.ParentCompanyID;
                            string strxmlCompanies = organizationservice.GeSpecifictBranchList(branchVM.CompanyID);

                            DataSet dsSpecificBranchList = new DataSet();
                            dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
                            if (dsSpecificBranchList.Tables.Count > 0)
                            {
                            DataView dv = new DataView(dsSpecificBranchList.Tables[0]);
                            dv.Sort = "Is_Active desc";
                            DataTable dtdeactive = dv.ToTable();
                            foreach (System.Data.DataRow row in dtdeactive.Rows)
                                {
                                    BranchViewModel listOfBranch = new BranchViewModel
                                    {
                                        BranchID = Convert.ToInt32(row["Org_Hier_ID"]),
                                        BranchName = row["Company_Name"].ToString(),
                                        // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                                        Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                                        Logo = Convert.ToString(row["logo"])
                                    };
                                    branchlist.Add(listOfBranch);

                                }
                            }
                        branchVM.viewmodel = branchlist;
                        branchVM.organization = new Organization();
                            branchVM.organization.Parent_Company_Id = orgActivateDeactivateViewModel.ParentCompanyID;
                            return View("_BranchList", branchVM);


                        }
                    }
                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }
                return View();
            }
        public ActionResult DeactivateBranch(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool result = false;
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
                    string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                        orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);

                    }
                    result = organizationServiceClient.DeactivateBranch(orgActivateDeactivateViewModel.CompanyID);
                    if (result == true)
                    {
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        TempData["Success"] = "deactivated successfully!!!";

                        //return RedirectToAction("BranchList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                        BranchViewModel branchVM = new BranchViewModel();
                        branchVM.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                        branchVM.GroupCompaniesList = new List<SelectListItem>();
                        OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                        string strXMLGroupCompanyList = organizationservice.getParticularGroupCompaniesList(branchVM.GroupCompanyID);//
                        DataSet dsGroupCompanyList = new DataSet();
                        dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                        branchVM.GroupCompanyName = dsGroupCompanyList.Tables[0].Rows[0]["Company_Name"].ToString();
                      
                        string strXMLCompanyList = organizationservice.getCompanyListDropDown(branchVM.GroupCompanyID);
                        DataSet dsCompanyList = new DataSet();
                        dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                        branchVM.CompaniesList = new List<SelectListItem>();
                        branchVM.CompaniesList.Add(new SelectListItem { Text = "-- Select Company --", Value = "0" });
                        if (dsCompanyList.Tables.Count > 0)
                        {
                            foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                            {
                                branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                            }
                        }


                        List<BranchViewModel> branchlist = new List<BranchViewModel>();
                        branchVM.CompanyID = orgActivateDeactivateViewModel.ParentCompanyID;
                        string strxmlCompanies = organizationservice.GeSpecifictBranchList(branchVM.CompanyID);

                        DataSet dsSpecificBranchList = new DataSet();
                        dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
                        if (dsSpecificBranchList.Tables.Count > 0)
                        {
                            DataView dv = new DataView(dsSpecificBranchList.Tables[0]);
                            dv.Sort = "Is_Active desc";
                            DataTable dtdeactive = dv.ToTable();
                            foreach (System.Data.DataRow row in dtdeactive.Rows)
                            {
                                BranchViewModel listOfBranch = new BranchViewModel
                                {
                                    BranchID = Convert.ToInt32(row["Org_Hier_ID"]),
                                    BranchName = row["Company_Name"].ToString(),
                                    // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                                    Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                                    Logo = Convert.ToString(row["logo"])
                                };
                                branchlist.Add(listOfBranch);

                            }
                        }
                        branchVM.viewmodel = branchlist;
                        branchVM.organization = new Organization();
                        branchVM.organization.Parent_Company_Id = orgActivateDeactivateViewModel.ParentCompanyID;
                        return View("_BranchList", branchVM);


                    }
                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            return View();
        }
        public ActionResult DeleteBranch(int Orgid)
        {
          
                try
                {
                    if (ModelState.IsValid)
                    {

                        bool result = false;
                        OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                        OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
                        string strxmlData = organizationServiceClient.getGroupOrganization(Orgid);
                        DataSet dsData = new DataSet();
                        dsData.ReadXml(new StringReader(strxmlData));
                        if (dsData.Tables.Count > 0)
                        {
                            orgActivateDeactivateViewModel.CompanyID = Orgid;
                            orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                            orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);

                        }
                        result = organizationServiceClient.DeleteBranch(orgActivateDeactivateViewModel.CompanyID);
                        if (result == true)
                        {
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;

                        TempData["Success"] = "deleted successfully!!!";

                            //return RedirectToAction("BranchList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                            BranchViewModel branchVM = new BranchViewModel();
                            branchVM.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                            branchVM.GroupCompaniesList = new List<SelectListItem>();
                            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                            string strXMLGroupCompanyList = organizationservice.getParticularGroupCompaniesList(branchVM.GroupCompanyID);//
                            DataSet dsGroupCompanyList = new DataSet();
                            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                        branchVM.GroupCompanyName = dsGroupCompanyList.Tables[0].Rows[0]["Company_Name"].ToString();
                           
                            string strXMLCompanyList = organizationservice.getCompanyListDropDown(branchVM.GroupCompanyID);
                            DataSet dsCompanyList = new DataSet();
                            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                            branchVM.CompaniesList = new List<SelectListItem>();
                            branchVM.CompaniesList.Add(new SelectListItem { Text = "-- Select Company --", Value = "0" });
                            if (dsCompanyList.Tables.Count > 0)
                            {
                                foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                                {
                                    branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                                }
                            }


                            List<BranchViewModel> branchlist = new List<BranchViewModel>();
                        branchVM.CompanyID = orgActivateDeactivateViewModel.ParentCompanyID;
                            string strxmlCompanies = organizationservice.GeSpecifictBranchList(branchVM.CompanyID);

                            DataSet dsSpecificBranchList = new DataSet();
                            dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
                            if (dsSpecificBranchList.Tables.Count > 0)
                            {
                            DataView dv = new DataView(dsSpecificBranchList.Tables[0]);
                            dv.Sort = "Is_Active desc";
                            DataTable dtdeactive = dv.ToTable();
                            foreach (System.Data.DataRow row in dtdeactive.Rows)
                                {
                                    BranchViewModel listOfBranch = new BranchViewModel
                                    {
                                        BranchID = Convert.ToInt32(row["Org_Hier_ID"]),
                                        BranchName = row["Company_Name"].ToString(),
                                        // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
                                        Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                                        Logo = Convert.ToString(row["logo"])
                                    };
                                    branchlist.Add(listOfBranch);

                                }
                            }
                        branchVM.viewmodel = branchlist;
                        branchVM.organization = new Organization();
                            branchVM.organization.Parent_Company_Id = orgActivateDeactivateViewModel.ParentCompanyID;
                            return View("_BranchList", branchVM);


                        }
                    }
                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }
                return View();
            }

        [HttpGet]
        public ActionResult AddVendor()
        {
           int id = 0;
            VendorViewModel vendorVM = new VendorViewModel();
            vendorVM.organization = new Organization();
            vendorVM.companydetails = new CompanyDetails();
            vendorVM.CompaniesList = new List<SelectListItem>();
            vendorVM.ComplianceList = new List<SelectListItem>();
            vendorVM.IndustryTypeList = new List<SelectListItem>();
            if (Convert.ToInt32(Session["GroupCompanyId"]) != 0)
                {

                try
                {
                    var copmpanyid = Request.QueryString["Orgid"];
                    if (copmpanyid != null)
                    {
                        vendorVM.CompanyID = Convert.ToInt32(copmpanyid);
                    }
                    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                    vendorVM.organization.Organization_Id = 0;
                    vendorVM.companydetails.Company_Details_ID = 0;
                    vendorVM.organization.Parent_Company_Id = Convert.ToInt32(TempData["CompanyID"]);

                    {
                        string strXMLCompanyList = "";
                        if (Session["G"] == null)
                        {
                            strXMLCompanyList = organizationservice.getCompanyListDropDown(Convert.ToInt32(Session["GroupCompanyId"]));

                        }
                        else
                        {
                            strXMLCompanyList = organizationservice.getCompanyListDropDown(Convert.ToInt32(Session["G"]));

                        }
                        DataSet dsCompanyList = new DataSet();
                        dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                        vendorVM.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "" });
                        if (dsCompanyList.Tables.Count > 0)
                        {
                            foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                            {
                                vendorVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                            }
                        }
                    }

                    string strXMLComplianceTypes = organizationservice.GetAssignedComplianceTypes(vendorVM.CompanyID);
                    DataSet dsComplianceTypes = new DataSet();
                    dsComplianceTypes.ReadXml(new StringReader(strXMLComplianceTypes));
                    //vendorVM.ComplianceList.Add(new SelectListItem { Text = "-- List of Compliances --", Value = "0" });

                    if (vendorVM.CompanyID > 0)
                    {
                        if (dsComplianceTypes.Tables.Count > 0)
                        {
                            foreach (System.Data.DataRow row in dsComplianceTypes.Tables[0].Rows)
                            {

                                vendorVM.ComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                            }
                        }
                    }

                    string strXMLIndustryType = organizationservice.GetIndustryType();
                    DataSet dsIndustryType = new DataSet();
                    dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
                    vendorVM.IndustryTypeList.Add(new SelectListItem { Text = "-- Vendor Type --", Value = "" });
                    //if (vendorVM.CompanyID > 0)
                    {
                        if (dsIndustryType.Tables.Count > 0)
                        {
                            foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                            {
                                vendorVM.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }

                Session["CompleteVendorDetails"] = vendorVM;
                vendorVM.companydetails.Calender_StartDate = DateTime.Now;
            }
            else
            {
                ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);

            }
            return View("_Vendor", vendorVM);
        }
        [HttpPost]
        public ActionResult AddVendor(VendorViewModel vendorVM, HttpPostedFileBase file)
        {
            try
            {
                if (vendorVM.companydetails.Calender_EndDate == DateTime.MinValue)
                {
                    vendorVM.companydetails.Calender_EndDate = Convert.ToDateTime(DateTime.MaxValue.ToString("dd-MM-yyyy"));
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
                    else
                    {
                        CommonController common = new CommonController();
                        vendorVM.organization.logo = Path.GetFileName("noimage.png");
                        string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName("noimage.png"));
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
                    //int resultID = organizationClient.insertcomplianceTypes(vendorVM.ComplianceID, id);


                    if (id != 0)
                    {
                        TempData["ParentCompanyID"] = vendorVM.organization.Company_Id;
                        TempData["CompanyName"] = vendorVM.organization.Company_Name;

                        TempData["Success"] = "Vendor created successfully!!!";
                        return RedirectToAction("AboutVendor", new { id = id });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Not able to create vendor. Please try again!!!";
                    }
                }
                else
                {

                    ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
                    vendorVM.organization.Organization_Id = 0;

                    vendorVM = (VendorViewModel)Session["CompleteVendorDetails"];
                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            return View("_Vendor", vendorVM);

        }

        [HttpGet]
        public ActionResult UpdateVendor(int OrgID)
        {
            
                VendorViewModel ViewModel = new VendorViewModel();
            try
            {
                ViewModel.organization = new Organization();
                ViewModel.companydetails = new CompanyDetails();

                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();

                string strxmlUpdatedData = organizationClient.getVendor(OrgID);
                DataSet dsUpdatedData = new DataSet();
                dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));
                if (dsUpdatedData.Tables.Count > 0)
                {

                    ViewModel.organization.Organization_Id = OrgID;
                    ViewModel.organization.Organization_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
                    ViewModel.organization.Company_Name = dsUpdatedData.Tables[0].Rows[0]["Company_Name"].ToString();
                    ViewModel.organization.Description = dsUpdatedData.Tables[0].Rows[0]["Description"].ToString();
                    // ViewModel.organization.Industry_Type = dsUpdatedData.Tables[0].Rows[0]["Industry_Type"].ToString();
                    ViewModel.organization.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Active"]));
                    ViewModel.organization.Is_Leaf = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Leaf"]));
                    ViewModel.organization.Is_Vendor = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Vendor"]));
                    ViewModel.organization.Is_Delete = Convert.ToBoolean(Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Is_Delete"]));
                    ViewModel.organization.User_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["User_ID"]);
                    ViewModel.organization.Level = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Level"]);
                    ViewModel.companydetails.Company_Details_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Company_Details_ID"]);
                    ViewModel.companydetails.Org_Hier_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Org_Hier_ID"]);
                    ViewModel.companydetails.Auditing_Frequency = dsUpdatedData.Tables[0].Rows[0]["Auditing_Frequency"].ToString();
                    ViewModel.companydetails.Calender_StartDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_StartDate"]);
                    ViewModel.companydetails.Calender_EndDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Calender_EndDate"]);
                    ViewModel.companydetails.Company_ContactNumber1 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber1"].ToString();
                    ViewModel.companydetails.Company_ContactNumber2 = dsUpdatedData.Tables[0].Rows[0]["Company_ContactNumber2"].ToString();
                    ViewModel.companydetails.Company_EmailID = dsUpdatedData.Tables[0].Rows[0]["Company_Email_ID"].ToString();
                    ViewModel.companydetails.Website = dsUpdatedData.Tables[0].Rows[0]["Website"].ToString();
                    ViewModel.organization.Parent_Company_Id = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);

                    ViewModel.organization.logo = dsUpdatedData.Tables[0].Rows[0]["logo"].ToString();
                    ViewModel.CompanyID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Parent_Company_ID"]);
                    ViewModel.companydetails.Industry_Type_ID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Industry_Type_ID"]);

                }
                string strXMLIndustryType = organizationClient.GetIndustryType();
                //string strXMLIndustryType = organizationClient.getDefaultIndustryType(ViewModel.CompanyID);
                DataSet dsIndustryType = new DataSet();
                dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
                ViewModel.IndustryTypeList = new List<SelectListItem>();
                ViewModel.IndustryTypeList.Add(new SelectListItem { Text = "-- Vendor Type --", Value = "0" });
                if (ViewModel.CompanyID > 0)
                {
                    if (dsIndustryType.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                        {
                            ViewModel.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                        }
                    }
                }



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

                string strXMLCompanyList = organizationClient.getCompanyListsforBranch(ViewModel.CompanyID);

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



                //string xmldata = organizationClient.GetAssignedComplianceTypes(ViewModel.CompanyID);
                //DataSet ds = new DataSet();
                //ds.ReadXml(new StringReader(xmldata));
                //ViewModel.ComplianceList = new List<SelectListItem>();
                //DataSet dsrole = new DataSet();
                //xmldata = organizationClient.GetAssignedComplianceTypes(ViewModel.organization.Organization_Id);
                //dsrole.ReadXml(new StringReader(xmldata));
                //if (ds.Tables.Count > 0)
                //{
                //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                //    {
                //        bool selected = false;
                //        if (dsrole.Tables.Count > 0)
                //        {
                //            foreach (System.Data.DataRow id in dsrole.Tables[0].Rows)
                //            {
                //                if (Convert.ToInt32(id["Compliance_Type_ID"]) == Convert.ToInt32(row["Compliance_Type_ID"]))
                //                {
                //                    selected = true;
                //                    break;
                //                }
                //            }
                //        }
                //        ViewModel.ComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString(), Selected = selected });
                //    }
                //}


            }
            catch(Exception)
            {
                return View("ErrorPage");
            }

            return View("_Vendor", ViewModel);
        }

        [HttpPost]
        public ActionResult UpdateVendor(CompanyViewModel vendorViewModel, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    CommonController common = new CommonController();
                    vendorViewModel.organization.logo = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }
                else
                {
                    CommonController common = new CommonController();
                    vendorViewModel.organization.logo = Path.GetFileName("noimage.png");
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName("noimage.png"));
                    string message = common.UploadFile(file, filePath);
                    ModelState.AddModelError("org_hier.logo", message);
                }

                bool result = false;
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                vendorViewModel.companydetails.Org_Hier_ID = vendorViewModel.organization.Organization_Id;
                result = organizationClient.updateVendor(vendorViewModel.organization, vendorViewModel.companydetails);
                //organizationClient.DeleteCompliance(vendorViewModel.organization.Organization_Id);
               // organizationClient.insertcomplianceTypes(vendorViewModel.ComplianceID, vendorViewModel.organization.Organization_Id);
                if (result != false)
                {
                    TempData["Success"] = "Vendor details updated succesfully!!!";
                    return RedirectToAction("AboutVendor", new { id = vendorViewModel.organization.Organization_Id });
                }
                else
                {
                    return View();
                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }

        }

        [HttpGet]
        public ActionResult SelectGroupCompany()
        {
            Models.ListOfGroupCompanies model = new ListOfGroupCompanies();

            try
            {
                model.GroupCompaniesList = new List<SelectListItem>();
                List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();




                if (Convert.ToInt32(Session["GroupCompanyID"]) != 0)
                {


                    var id = Request.QueryString["Orgid"];
                    if (id != null)
                    {
                        model.GroupCompanyID = Convert.ToInt32(id);

                    }
                    model.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyID"]);
                    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                    string strXMLGroupCompanyList = organizationservice.getParticularGroupCompaniesList(Convert.ToInt32(Session["GroupCompanyID"]));//
                    DataSet dsGroupCompanyList = new DataSet();
                    dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                    model.GroupCompanyName = Convert.ToString(dsGroupCompanyList.Tables[0].Rows[0]["Company_Name"]);

                    string strxmlCompanies = organizationservice.GeSpecifictCompaniesList(Convert.ToInt32(Session["GroupCompanyID"]));
                    DataSet dsSpecificCompaniesList = new DataSet();
                    dsSpecificCompaniesList.ReadXml(new StringReader(strxmlCompanies));
                    if (Convert.ToInt32(Session["GroupCompanyID"]) != 0)
                    {
                        if (dsSpecificCompaniesList.Tables.Count > 0)
                        {
                            DataView dv = new DataView(dsSpecificCompaniesList.Tables[0]);
                            dv.Sort = "Is_Active desc";
                            DataTable dtdeactive = dv.ToTable();

                            foreach (System.Data.DataRow row in dtdeactive.Rows)
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
                    }
                    model.listOfGroups = companylist;
                    Session["GroupCompany"] = model.GroupCompaniesList;


                    model.CompanyName = Convert.ToString(TempData["CName"]);
                    ViewBag.MessageCompany = model.CompanyName;

                    ViewBag.Success = Convert.ToString(TempData["Success"]);
                    if (model.listOfGroups.Count == 0)
                    {
                        ViewBag.NotFound = "No companies found";
                    }
                }
                else
                {
                    ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);
                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            return View("_CompanyList", model);
        }
        [HttpPost]
        public ActionResult SelectGroupCompany(ListOfGroupCompanies model)
        {
            List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();
            try
            {
                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                string strxmlCompanies = organizationservice.GeSpecifictCompaniesList(model.GroupCompanyID);
                DataSet dsSpecificCompaniesList = new DataSet();
                dsSpecificCompaniesList.ReadXml(new StringReader(strxmlCompanies));
                if (dsSpecificCompaniesList.Tables.Count > 0)
                {
                    DataView dv = new DataView(dsSpecificCompaniesList.Tables[0]);
                    dv.Sort = "Is_Active desc";
                    DataTable dtdeactive = dv.ToTable();
                    foreach (System.Data.DataRow row in dtdeactive.Rows)
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
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View("_CompanyList", model);
        }

        public ActionResult ListofCompany()
        {
            List<ListOfGroupCompanies> companylist = new List<ListOfGroupCompanies>();
          
                int ID = 0;
                var groupcopmpanyid = Session["GroupCompanyId"];//Request.QueryString["GroupCompanyid"];
                if (groupcopmpanyid != null)
                {
                    ID = Convert.ToInt32(groupcopmpanyid);
                }
                ID = Convert.ToInt32(Session["GroupCompanyId"]);
                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                string strxmlCompanies = organizationservice.GeSpecifictCompaniesList(ID);

                DataSet dsSpecificCompaniesList = new DataSet();
                dsSpecificCompaniesList.ReadXml(new StringReader(strxmlCompanies));
                if (dsSpecificCompaniesList.Tables.Count > 0)
                {
                DataView dv = new DataView(dsSpecificCompaniesList.Tables[0]);
                dv.Sort = "Is_Active desc";
                DataTable dtdeactive = dv.ToTable();

                foreach (System.Data.DataRow row in dtdeactive.Rows)
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

                 
                    
                    // dv = new DataView(dsSpecificCompaniesList.Tables[0]);
                    // dv.RowFilter = "IsActive=0";
                    // DataTable dtdeactive = dv.ToTable();

                }
                else
                {
                    TempData["Success"] = "No companies found";
                }
            
          
            return View("_Companydashboard", companylist);
        }

        [HttpGet]
        public ActionResult SelectCompany()
        {
             
            Models.BranchViewModel model = new BranchViewModel();
            model.organization = new Organization();
                    model.CompaniesList = new List<SelectListItem>();
            model.viewmodel = new List<BranchViewModel>();
            List<BranchViewModel> branchlist = new List<BranchViewModel>();

            
            if (Convert.ToInt32(Session["GroupCompanyID"]) != 0)
            {
                try
                {

                    var compid = Request.QueryString["compid"];
                    if (compid != null)
                    {
                        model.CompanyID = Convert.ToInt32(compid);
                    }

                    model.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                    model.GroupCompanyName = Convert.ToString(Session["GroupCompanyName"]);
                    if (TempData["PID"] != null)
                    {
                        model.CompanyID = Convert.ToInt32(TempData["PID"]);
                    }

                    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();


                    string strXMLCompanyList = organizationservice.getCompanyListDropDown(model.GroupCompanyID);
                    DataSet dsCompanyList = new DataSet();
                    dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                    model.CompaniesList.Add(new SelectListItem { Text = "-- Select Company --", Value = "0" });
                    if (dsCompanyList.Tables.Count > 0)
                    {
                        if (model.CompanyID == 0)
                        {
                            model.CompanyID = Convert.ToInt32(dsCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                        }
                        // model.ChildCompanyName = Convert.ToString(dsCompanyList.Tables[0].Rows[0]["Company_Name"]);
                        foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                        {
                            model.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                        }
                    }
                    string strxmlCompanies = organizationservice.GeSpecifictBranchList(model.CompanyID);

                    DataSet dsSpecificBranchList = new DataSet();
                    dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
                    if (dsSpecificBranchList.Tables.Count > 0)
                    {
                        DataView dv = new DataView(dsSpecificBranchList.Tables[0]);
                        dv.Sort = "Is_Active desc";
                        DataTable dtdeactive = dv.ToTable();
                        foreach (System.Data.DataRow row in dtdeactive.Rows)
                        {
                            BranchViewModel listOfBranch = new BranchViewModel
                            {
                                BranchID = Convert.ToInt32(row["Org_Hier_ID"]),
                                BranchName = row["Company_Name"].ToString(),
                                Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                                Logo = Convert.ToString(row["logo"])
                            };
                            branchlist.Add(listOfBranch);

                        }

                    }
                    else
                    {
                        ViewBag.Message = ConfigurationManager.AppSettings["No_Branches"];
                    }

                    model.viewmodel = branchlist;
                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }
                model.organization = new Organization();
                model.organization.Parent_Company_Id = model.CompanyID;
                ViewBag.Success = Convert.ToString(TempData["Success"]);
                ViewBag.MessageBranch = Convert.ToString(TempData["ChildCompanyName"]);
                if (model.viewmodel.Count == 0)
                {
                    ViewBag.NotFound = "No branches found";
                }
            }
            else
            {
                ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);

            }
            return View("_BranchList", model);
        }
        [HttpPost]
        public ActionResult SelectCompany(BranchViewModel model)
        {
            try
            {
                if(model.GroupCompanyName != Convert.ToString(Session["GroupCompanyName"]))
                {
                    model.CompanyID = 0;
                }



                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

                model.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                model.GroupCompanyName = Convert.ToString(Session["GroupCompanyName"]);
              
                string strXMLCompanyList = organizationservice.getCompanyListDropDown(model.GroupCompanyID);
                DataSet dsCompanyList = new DataSet();
                dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                model.CompaniesList = new List<SelectListItem>();
                model.CompaniesList.Add(new SelectListItem { Text = "-- Select Company --", Value = "0" });
                if (dsCompanyList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                    {
                        if (model.CompanyID == 0)
                        {
                            model.CompanyID = Convert.ToInt32(dsCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                            model.ChildCompanyName = Convert.ToString(dsCompanyList.Tables[0].Rows[0]["Company_Name"]);
                        }
                        model.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });

                    }



                    List<BranchViewModel> branchlist = new List<BranchViewModel>();
                    string strxmlCompanies = organizationservice.GeSpecifictBranchList(model.CompanyID);

                    DataSet dsSpecificBranchList = new DataSet();
                    dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
                    if (dsSpecificBranchList.Tables.Count > 0)
                    {
                        DataView dv = new DataView(dsSpecificBranchList.Tables[0]);
                        dv.Sort = "Is_Active desc";
                        DataTable dtdeactive = dv.ToTable();

                        foreach (System.Data.DataRow row in dtdeactive.Rows)
                        {
                            BranchViewModel listOfBranch = new BranchViewModel
                            {
                                BranchID = Convert.ToInt32(row["Org_Hier_ID"]),
                                BranchName = row["Company_Name"].ToString(),
                                Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                                Logo = Convert.ToString(row["logo"])
                            };
                            branchlist.Add(listOfBranch);

                        }
                    }
                    else
                    {
                        ViewBag.Message = ConfigurationManager.AppSettings["No_Branches"];
                    }
                    model.viewmodel = branchlist;
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            model.organization = new Organization();
            model.organization.Parent_Company_Id = model.CompanyID;

            return View("_BranchList", model);
        }



        [HttpGet]
        public ActionResult SelectCompanyForVendor()
        {
            Models.ListOfGroupCompanies model = new ListOfGroupCompanies();
            model.GroupCompaniesList = new List<SelectListItem>();
            model.CompaniesList = new List<SelectListItem>();
            List<ListOfGroupCompanies> vendorlist = new List<ListOfGroupCompanies>();



            if (Convert.ToInt32(Session["GroupCompanyId"]) != 0)
            {
                try
                {
                    model.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                    string strXMLGroupCompanyList = organizationservice.getParticularGroupCompaniesList(model.GroupCompanyID);
                    DataSet dsGroupCompanyList = new DataSet();
                    dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                    if (dsGroupCompanyList.Tables.Count > 0)
                    {
                        model.GroupCompanyID = Convert.ToInt32(dsGroupCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                        foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                        {
                            model.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                        }
                    }
                    string strXMLCompanyList = organizationservice.getCompanyListDropDown(model.GroupCompanyID);
                    DataSet dsCompanyList = new DataSet();
                    dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                    model.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
                    if (dsCompanyList.Tables.Count > 0)
                    {
                        model.CompanyID = Convert.ToInt32(dsCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                        foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                        {
                            model.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                        }
                    }

                    string strxmlVendors = organizationservice.GetVendors(model.CompanyID);
                    DataSet dsSpecificVendorList = new DataSet();
                    dsSpecificVendorList.ReadXml(new StringReader(strxmlVendors));
                    if (dsSpecificVendorList.Tables.Count > 0)
                    {
                        DataView dv = new DataView(dsSpecificVendorList.Tables[0]);
                        dv.Sort = "Is_Active desc";
                        DataTable dtdeactive = dv.ToTable();
                        foreach (System.Data.DataRow row in dtdeactive.Rows)
                        {
                            ListOfGroupCompanies listOfVendors = new ListOfGroupCompanies
                            {
                                OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                                CompanyName = row["Company_Name"].ToString(),
                                IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                                Logo = Convert.ToString(row["logo"])

                            };
                            vendorlist.Add(listOfVendors);

                        }
                    }
                    else { ViewBag.Message = ConfigurationManager.AppSettings["No_Vendors"]; }

                    model.listOfGroups = vendorlist;
                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }
            }
            else
            {
                ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);

            }
            return View("_vendorList", model);
        }
        [HttpPost]
        public ActionResult SelectCompanyForVendor(ListOfGroupCompanies model)
        {
            try
            {
                model.GroupCompaniesList = new List<SelectListItem>();
                model.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyId"]);
                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                string strXMLGroupCompanyList = organizationservice.getParticularGroupCompaniesList(model.GroupCompanyID);
                DataSet dsGroupCompanyList = new DataSet();
                dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
                if (dsGroupCompanyList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                    {
                        model.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                    }
                }
                string strXMLCompanyList = organizationservice.getCompanyListDropDown(model.GroupCompanyID);
                DataSet dsCompanyList = new DataSet();
                dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                model.CompaniesList = new List<SelectListItem>();
                model.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
                if (dsCompanyList.Tables.Count > 0)
                {
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
                    DataView dv = new DataView(dsSpecificVendorList.Tables[0]);
                    dv.Sort = "Is_Active desc";
                    DataTable dtdeactive = dv.ToTable();
                    foreach (System.Data.DataRow row in dtdeactive.Rows)
                    {
                        ListOfGroupCompanies listOfVendors = new ListOfGroupCompanies
                        {
                            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                            CompanyName = row["Company_Name"].ToString(),
                            IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            Logo = Convert.ToString(row["logo"])

                        };
                        vendorlist.Add(listOfVendors);

                    }
                }
                else { ViewBag.Message = ConfigurationManager.AppSettings["No_Vendors"]; }
                model.listOfGroups = vendorlist;
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            return View("_vendorList", model);
        }


       
   
        [HttpGet]
        public ActionResult AboutBranch(int id)
        {
            AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
            try
            {
                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();

                string aboutcompany = organizationServiceClient.getCompanyListsforBranch(id);
                DataSet dsaboutCompany = new DataSet();
                dsaboutCompany.ReadXml(new StringReader(aboutcompany));
                if (dsaboutCompany.Tables.Count > 0)
                {
                    aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
                    aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
                    aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
                    aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
                    aboutCompanyViewModel.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));
                    aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);
                }
                string data = organizationServiceClient.getCompanyListsforBranch(aboutCompanyViewModel.ParentCompanyID);
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(new StringReader(data));

                aboutCompanyViewModel.CompanyNameList = dataSet.Tables[0].Rows[0]["Company_Name"].ToString();
                aboutCompanyViewModel.GroupCompanyName = Session["GroupCompanyName"].ToString();
                List<AboutCompanyViewModel> branchList = new List<AboutCompanyViewModel>();
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                string branchListofCompany = vendorServiceClient.GetAssignedVendorsforBranch(aboutCompanyViewModel.CompanyID);
                DataSet dsbranchList = new DataSet();
                dsbranchList.ReadXml(new StringReader(branchListofCompany));
                aboutCompanyViewModel.CompaniesList = new List<SelectListItem>();
                if (dsbranchList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsbranchList.Tables[0].Rows)
                    {
                        AboutCompanyViewModel listOfBranch = new AboutCompanyViewModel
                        {
                            OrganizationID = Convert.ToInt32(row["Vendor_ID"]),
                            CompanyNameList = row["Company_Name"].ToString(),
                            IndustryType = row["Type"].ToString(),
                            Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            logo = row["logo"].ToString()
                        };
                        branchList.Add(listOfBranch);

                    }
                }

                aboutCompanyViewModel.AboutGroupCompany = branchList;
                Session["Branch"] = aboutCompanyViewModel.AboutGroupCompany;
                if (TempData["Success"] != null)
                {
                    return View("_AboutBranch", aboutCompanyViewModel);
                }
                if (aboutCompanyViewModel.AboutGroupCompany.Count == 0)
                {
                    ViewBag.Message = "No vendors assigned";
                }
            }
            catch(Exception)
            {
                throw;
                return View("ErrorPage");
            }
            
            return View("_AboutBranch", aboutCompanyViewModel);
        }

        [HttpPost]
        public ActionResult AboutBranch(AboutCompanyViewModel aboutCompanyViewModel)
        {
            int id = 0;
            try
            {
                TempData["BranchName"] = aboutCompanyViewModel.CompanyName;
                 id = aboutCompanyViewModel.ParentCompanyID;
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }


            return RedirectToAction("BranchList", new { id = id });
        }
        
        public ActionResult BranchList(int id)
        {
            List<ListOfGroupCompanies> branchlist = new List<ListOfGroupCompanies>();
            try
            {
                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                string strxmlCompanies = organizationservice.GeSpecifictBranchList(id);

                DataSet dsSpecificBranchList = new DataSet();
                dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
                if (dsSpecificBranchList.Tables.Count > 0)
                {
                    DataView dv = new DataView(dsSpecificBranchList.Tables[0]);
                    dv.Sort = "Is_Active desc";
                    DataTable dtdeactive = dv.ToTable();
                    foreach (System.Data.DataRow row in dtdeactive.Rows)
                    {
                        ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
                        {
                            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                            CompanyName = row["Company_Name"].ToString(),
                            IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            Logo = Convert.ToString(row["logo"])
                        };
                        branchlist.Add(listOfBranch);

                    }
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }


            return View("_BranchDashBoard", branchlist);
        }

    

        [HttpGet]
        public ActionResult AboutVendor(int id)
        {
            AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
            try
            {


                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                string aboutcompany = organizationServiceClient.getCompanyListsforBranch(id);
                DataSet dsaboutCompany = new DataSet();
                dsaboutCompany.ReadXml(new StringReader(aboutcompany));
                if (dsaboutCompany.Tables.Count > 0)
                {
                    aboutCompanyViewModel.CompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Org_Hier_ID"]);
                    aboutCompanyViewModel.CompanyDescription = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Description"]);
                    aboutCompanyViewModel.CompanyName = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["Company_Name"]);
                    aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Parent_Company_ID"]);
                    aboutCompanyViewModel.Is_Active = Convert.ToBoolean(Convert.ToInt32(dsaboutCompany.Tables[0].Rows[0]["Is_Active"]));
                    aboutCompanyViewModel.CompanyLogo = Convert.ToString(dsaboutCompany.Tables[0].Rows[0]["logo"]);
                }
                string data = organizationServiceClient.getCompanyListsforBranch(aboutCompanyViewModel.ParentCompanyID);
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(new StringReader(data));

                aboutCompanyViewModel.CompanyNameList = dataSet.Tables[0].Rows[0]["Company_Name"].ToString();
                aboutCompanyViewModel.GroupCompanyName = Session["GroupCompanyName"].ToString();
                List<AboutCompanyViewModel> branchList = new List<AboutCompanyViewModel>();
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

                string branchListofCompany = vendorServiceClient.GetBranchesAssociatedWithVendors(aboutCompanyViewModel.CompanyID);

                DataSet dsbranchList = new DataSet();
                dsbranchList.ReadXml(new StringReader(branchListofCompany));
                aboutCompanyViewModel.CompaniesList = new List<SelectListItem>();
                if (dsbranchList.Tables.Count > 0)
                {
                    DataView dv = new DataView(dsbranchList.Tables[0]);
                    dv.Sort = "Is_Active desc";
                    DataTable dtdeactive = dv.ToTable();
                    foreach (System.Data.DataRow row in dtdeactive.Rows)
                    {
                        AboutCompanyViewModel listOfBranch = new AboutCompanyViewModel
                        {
                            OrganizationID = Convert.ToInt32(row["Branch_ID"]),
                            CompanyNameList = row["Company_Name"].ToString(),
                            //IndustryType = row["Industry_Type"].ToString(),
                            Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            logo = row["logo"].ToString()
                        };
                        branchList.Add(listOfBranch);

                    }

                }

                aboutCompanyViewModel.AboutGroupCompany = branchList;
                if (TempData["Success"] != null)
                {
                    return View("_AboutVendor", aboutCompanyViewModel);
                }
                if (aboutCompanyViewModel.AboutGroupCompany.Count == 0)
                {
                    ViewBag.Message = "No branch associated with";
                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            
          

            return View("_AboutVendor", aboutCompanyViewModel);
        }

        [HttpPost]
        public ActionResult AboutVendor(AboutCompanyViewModel aboutCompanyViewModel)
        {
            int id = 0;
            try
            {
                id = aboutCompanyViewModel.ParentCompanyID;
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            //return RedirectToAction("CompanyVendorsList", new { id = id });
            return RedirectToAction("SelectCompanyForVendor");
        }

        public ActionResult CompanyVendorsList(int id)
        {

            List<ListOfGroupCompanies> vendorlist = new List<ListOfGroupCompanies>();
            try
            {
                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                string strxmlVendors = organizationservice.GetVendors(id);

                DataSet dsSpecificVendorList = new DataSet();
                dsSpecificVendorList.ReadXml(new StringReader(strxmlVendors));
                if (dsSpecificVendorList.Tables.Count > 0)

                {
                    DataView dv = new DataView(dsSpecificVendorList.Tables[0]);
                    dv.Sort = "Is_Active desc";
                    DataTable dtdeactive = dv.ToTable();
                    foreach (System.Data.DataRow row in dtdeactive.Rows)
                    {
                        ListOfGroupCompanies listOfVendors = new ListOfGroupCompanies
                        {
                            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                            CompanyName = row["Company_Name"].ToString(),
                            IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            Logo = Convert.ToString(row["logo"])

                        };
                        vendorlist.Add(listOfVendors);

                    }
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }


            return View("_VendorDashBoardForCompany", vendorlist);
        }


        public ActionResult BranchVendorsList(int id)
        {

            List<ListOfGroupCompanies> branchvendorlist = new List<ListOfGroupCompanies>();
            try
            {
                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                string strxmlBranchVendors = organizationservice.GetVendors(id);

                DataSet dsSpecificVendorListforBranch = new DataSet();
                dsSpecificVendorListforBranch.ReadXml(new StringReader(strxmlBranchVendors));
                if (dsSpecificVendorListforBranch.Tables.Count > 0)
                {
                    DataView dv = new DataView(dsSpecificVendorListforBranch.Tables[0]);
                    dv.Sort = "Is_Active desc";
                    DataTable dtdeactive = dv.ToTable();
                    foreach (System.Data.DataRow row in dtdeactive.Rows)
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
                }

            }
            catch (Exception)
            {
                return View("ErrorPage");
            }

            return View("_VendorDashBoardForBranch", branchvendorlist);
        }




        
        public ActionResult GroupCompanyList()
        { 
        List<ListOfGroupCompanies> branchlist = new List<ListOfGroupCompanies>();
            try
            {
                OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
                string strxmlCompanies = organizationservice.GetGroupCompaniesList();

                DataSet dsGroupCompanyList = new DataSet();
                dsGroupCompanyList.ReadXml(new StringReader(strxmlCompanies));
                if (dsGroupCompanyList.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                    {
                        ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
                        {
                            OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                            CompanyName = row["Company_Name"].ToString(),
                            IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                            Logo = Convert.ToString(row["logo"])


                        };
                        branchlist.Add(listOfBranch);

                    }
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }

            return View("_DashBoardGroupCompany", branchlist);
}


        public ActionResult DeactivateVendorUnderCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = false;
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    string strxmlData = organizationServiceClient.getCompanyListsforBranch(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                        orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
                    }
                    VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                    result = Convert.ToBoolean(vendorServiceClient.DeactivateVendorForCompany(orgActivateDeactivateViewModel.CompanyID));
                    if (result == true)
                    {
                        TempData["Success"] = "deactivated successfully";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                        return RedirectToAction("CompanyVendorsList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                    }

                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View();
        }

        




        public ActionResult ActivateVendorUnderCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = false;
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    string strxmlData = organizationServiceClient.getCompanyListsforBranch(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                        orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
                    }
                    VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                    result = Convert.ToBoolean(vendorServiceClient.ActivateVendorForCompany(orgActivateDeactivateViewModel.CompanyID));
                    if (result == true)
                    {
                        TempData["Success"] = "activated successfully";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                        return RedirectToAction("CompanyVendorsList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                    }

                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View();
        }
        public ActionResult ActivateVendorUnderBranch(int OrgID)
        {
            OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
            try
            {

                OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

                string strxmlData = organizationServiceClient.getCompanyListsforBranch(OrgID);

                DataSet dsData = new DataSet();
                dsData.ReadXml(new StringReader(strxmlData));
                if (dsData.Tables.Count > 0)
                {
                    orgActivateDeactivateViewModel.CompanyID = OrgID;
                    orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View("_AcvtivateVendorForBranch", orgActivateDeactivateViewModel);
        }
        [HttpPost]
        public ActionResult ActivateVendorUnderBranch(OrgActivateDeactivateViewModel orgActivateDeactivateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = false;
                    VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                    result = Convert.ToBoolean(vendorServiceClient.ActivateVendorForBranch(orgActivateDeactivateViewModel.CompanyID));
                    if (result == true)
                    {
                        TempData["Success"] = "Activated successfully";
                        TempData["Compant"] = orgActivateDeactivateViewModel.CompanyName;
                        return RedirectToAction("BranchVendorsList");
                    }

                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View();
        }

        public ActionResult DeleteVendorUnderCompany(int Orgid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = false;
                    OrgActivateDeactivateViewModel orgActivateDeactivateViewModel = new OrgActivateDeactivateViewModel();
                    OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
                    string strxmlData = organizationServiceClient.getCompanyListsforBranch(Orgid);
                    DataSet dsData = new DataSet();
                    dsData.ReadXml(new StringReader(strxmlData));
                    if (dsData.Tables.Count > 0)
                    {
                        orgActivateDeactivateViewModel.CompanyID = Orgid;
                        orgActivateDeactivateViewModel.CompanyName = dsData.Tables[0].Rows[0]["Company_Name"].ToString();
                        orgActivateDeactivateViewModel.ParentCompanyID = Convert.ToInt32(dsData.Tables[0].Rows[0]["Parent_Company_ID"]);
                    }
                    VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                    result = Convert.ToBoolean(vendorServiceClient.DeleteVendorForCompany(orgActivateDeactivateViewModel.CompanyID));
                    if (result == true)
                    {
                        TempData["Success"] = "deleted successfully";
                        TempData["Company"] = orgActivateDeactivateViewModel.CompanyName;
                        return RedirectToAction("CompanyVendorsList", new { id = orgActivateDeactivateViewModel.ParentCompanyID });
                    }

                }
            }
            catch (Exception)
            {
                return View("ErrorPage");
            }
            return View();
        }

        [HttpGet]
        public ActionResult AssignDessignVendors()
        {
            int compid = 0;
            BranchViewModel branchVM = new BranchViewModel();
            branchVM.GroupCompaniesList = new List<SelectListItem>();
            branchVM.CompaniesList = new List<SelectListItem>();
            branchVM.AssignedList = new List<SelectListItem>();
            branchVM.BranchList = new List<SelectListItem>();
            branchVM.VendorList = new List<SelectListItem>();



            if (Convert.ToInt32(Session["GroupCompanyId"]) != 0)
            {
                try
                {
                    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

                    string strXMLGroupCompanyList = organizationservice.GetGroupCompaniesList();
                    DataSet dsGroupCompanyList = new DataSet();
                    dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));

                    string strXMLCompanyList = "";

                    {
                        strXMLCompanyList = organizationservice.getCompanyListDropDown(Convert.ToInt32(Session["GroupCompanyId"]));
                    }

                    DataSet dsCompanyList = new DataSet();
                    dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
                    branchVM.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
                    if (dsCompanyList.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
                        {
                            //branchVM.CompanyID = Convert.ToInt32(dsCompanyList.Tables[0].Rows[0]["Org_Hier_ID"]);
                            branchVM.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                        }
                    }

                    string strXMLBranchList = organizationservice.getSpecificBranchListDropDown(compid);
                    DataSet dsBranchList = new DataSet();
                    dsBranchList.ReadXml(new StringReader(strXMLBranchList));
                    branchVM.BranchList.Add(new SelectListItem { Text = "--Select Branch--", Value = "0" });

                    int BranchID = 0;
                    VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                    string xmlAssignedVndors = vendorServiceClient.GetAssignedVendorsforBranch(BranchID);
                    DataSet dsAssignedVendors = new DataSet();
                    dsAssignedVendors.ReadXml(new StringReader(xmlAssignedVndors));
                    branchVM.AssignedList.Add(new SelectListItem { Text = "--Select Vendor--", Value = "0" });



                    int CompanyID = 0;
                    string xmlVndors = organizationservice.GetVendors(CompanyID);
                    DataSet dsVendors = new DataSet();
                    dsVendors.ReadXml(new StringReader(xmlVndors));
                    branchVM.VendorList.Add(new SelectListItem { Text = "--Select Vendor--", Value = "0" });


                    //branchVM.currentList = new List<SelectListItem>();
                    //branchVM.currentList.Add(new SelectListItem { Text = "Drag and Drop here", Value = "0" });

                    Session["CompleteAssigningDetails"] = branchVM;
                }
                catch (Exception)
                {
                    return View("ErrorPage");
                }
                branchVM.VendorStartDate = DateTime.Now;
            }
            else
            {
                ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);

            }
            return View("_AssignVendorToBranch", branchVM);

        }

        [HttpPost]
        public ActionResult AssignDessignVendors(BranchViewModel branchViewModel)
        {
            try
            {
                branchViewModel.GroupCompanyID = Convert.ToInt32(Session["GroupCompanyID"]);
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

                bool result = false;
                branchViewModel.organization = new Organization();
                branchViewModel.BranchList = new List<SelectListItem>();
                branchViewModel.CompaniesList = new List<SelectListItem>();
                branchViewModel.GroupCompaniesList = new List<SelectListItem>();
                if (ModelState.IsValid)
                {

                    branchViewModel.IsVendorActive = true;
                    branchViewModel.AssignedList = Session["A"] as List<SelectListItem>;



                    foreach (var Citem in branchViewModel.assignedID)
                    {
                        if (branchViewModel.AssignedList.Count == 0 && Citem > 0)
                        {
                            branchViewModel.VendorsID = Citem;
                            result = vendorServiceClient.insertVendorForBranch(branchViewModel.VendorsID, branchViewModel.BranchID,
                               branchViewModel.VendorStartDate, Convert.ToDateTime(branchViewModel.VendorEndDate), branchViewModel.IsVendorActive);
                           TempData["Assigned"] = "Assigned successfully";

                        }
                        else

                        {
                            if (branchViewModel.AssignedList.Count != 0)
                            {
                                foreach (var item in branchViewModel.AssignedList)
                                {
                                    if (item.Value == Citem.ToString())
                                    {

                                    }
                                    else if (item.Value != Citem.ToString())
                                    {
                                        if (Citem > 0)
                                        {
                                            branchViewModel.VendorsID = Citem;
                                            result = vendorServiceClient.insertVendorForBranch(branchViewModel.VendorsID, branchViewModel.BranchID,
                                               branchViewModel.VendorStartDate, Convert.ToDateTime(branchViewModel.VendorEndDate), branchViewModel.IsVendorActive);
                                            TempData["Assigned"] = "Assigned successfully";

                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (var item in branchViewModel.AssignedList)
                    {
                        foreach (var Vitem in branchViewModel.VendorID)
                        {
                            if (item.Value == Vitem.ToString())
                            {
                                if (Vitem > 0)
                                {
                                    int[] Orgid = branchViewModel.VendorID;
                                    result = Convert.ToBoolean(vendorServiceClient.DeactivateVendorForBranch((Orgid), branchViewModel.BranchID));
                                    TempData["Deleted"] = "de-allocated successfully.";
                                }
                            }

                        }
                    }
                    if (result != false)
                    {
                        Session["GroupCompanyID"] = branchViewModel.GroupCompanyID;
                        Session["CompanyID"] = branchViewModel.CompanyID;
                        Session["BranchID"] = branchViewModel.BranchID;
                        Session["VendorID"] = branchViewModel.VendorID;
                        //TempData["Assigned"] = "Assigned Successfully";
                        TempData["BranchName"] = branchViewModel.organization.Company_Name;
                        foreach (var item in branchViewModel.VendorID)
                        {
                            TempData["VendorName"] = branchViewModel.VendorName;
                        }
                        return RedirectToAction("AssignDessignVendors");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error while assiging";
                    }
                }
                else
                {
                    ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
                    branchViewModel.organization.Organization_Id = 0;

                    branchViewModel = (BranchViewModel)Session["CompleteAssigningDetails"];
                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            return View("_AssignVendorToBranch", branchViewModel);

        }

        [HttpGet]
        public ActionResult IndustryTypeMapCompliance()
        {
            ComplianceIndustryViewModel model = new ComplianceIndustryViewModel();
            try
            {
                OrgService.OrganizationServiceClient organizationService = new OrgService.OrganizationServiceClient();
               // model.MappedComplianceList = new List<SelectListItem>();
                // model.IndustryType = new Compliance.DataObject.IndustryType();
                model.ComplianceType = new Compliance.DataObject.ComplianceType();
                string strXMLCountries = organizationService.GetCountryList();
                DataSet dsCountries = new DataSet();
                dsCountries.ReadXml(new StringReader(strXMLCountries));
                model.CountryList = new List<SelectListItem>();
                model.CountryList.Add(new SelectListItem { Text = "-- Select Country --", Value = "" });
                if (dsCountries.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                    {
                        model.CountryList.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                    }
                }

                string strXMLIndustryType = organizationService.GetIndustryType();
                DataSet dsIndustryType = new DataSet();
                dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
                model.IndustryTypeList = new List<SelectListItem>();
                model.IndustryTypeList.Add(new SelectListItem { Text = "-- Industry Type --", Value = "" });
                if (dsIndustryType.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                    {
                        model.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                    }
                }

                model.AuditfrequencyList = new List<SelectListItem>();
                model.AuditfrequencyList.Add(new SelectListItem { Text = "-- Audit Frequency --", Value = "" });
                model.ComplianceType.StartDate = DateTime.Now;
                model.ComplianceType.EndDate = model.ComplianceType.StartDate.AddYears(1);



                // List<ComplianceIndustryViewModel> compmodel = new List<ComplianceIndustryViewModel>();

                //string strXMLMappedCompliance = organizationService.GetComplianceType(model.ComplianceType.IndustryTypeID, model.ComplianceType.CountryID);
                int CountryID = 0;
                int IndustryTypeID = 0;
                string strXMLMappedCompliance = organizationService.GetComplianceType(CountryID, IndustryTypeID);
                DataSet dsMappedCompliance = new DataSet();
                dsMappedCompliance.ReadXml(new StringReader(strXMLMappedCompliance));
                
              

                model.ComplianceTypeList = new List<SelectListItem>();
                //if (model.ComplianceType.IndustryTypeID > 0 && model.ComplianceType.CountryID > 0)
                //{
                //    if (dsMappedCompliance.Tables.Count > 0)
                //    {
                //        //foreach (System.Data.DataRow row in dsMappedCompliance.Tables[0].Rows)
                //        //{
                //        //    model.MappedComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                //        //}

                //        foreach (System.Data.DataRow row in dsMappedCompliance.Tables[0].Rows)
                //        {
                //            model = new ComplianceIndustryViewModel();
                //            model.ComplianceType = new ComplianceType();
                //            model.MappedComplianceList = new List<SelectListItem>();

                //            model.IndustryName = Convert.ToString(Convert.ToInt32(row["Industry_Name"]));
                //            model.CountryName = Convert.ToString(Convert.ToInt32(row["Country_Name"]));


                //            //model.ComplianceType.ComplianceTypeID = Convert.ToInt32(row["compliance_Type_ID"]);
                //            //model.ComplianceType.ComplianceTypeName = Convert.ToString(row["Compliance_Type_Name"]);

                //            model.MappedComplianceList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });

                //        }
                //    }
                //}
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
           






            return View("_MapComplianceTypes", model);
        }
        [HttpPost]
        public ActionResult IndustryTypeMapCompliance(ComplianceIndustryViewModel model)
        {
            try
            {
                OrgService.OrganizationServiceClient organizationService = new OrgService.OrganizationServiceClient();
                int id = organizationService.insertComplianceTypesMappedWithIndustryType(model.ComplianceType);
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            return RedirectToAction("dashboard", "common", new { pid = 0 });

        }

        [HttpGet]
        public ActionResult UpdateIndustryTypeMapCompliance(int ComplianceID)
        {
            ComplianceIndustryViewModel model = new ComplianceIndustryViewModel();
            try
            {
                model.ComplianceType = new Compliance.DataObject.ComplianceType();

                OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
                string strxmlUpdatedData = client.GetMappedCompliance(ComplianceID);
                DataSet dsUpdatedData = new DataSet();
                dsUpdatedData.ReadXml(new StringReader(strxmlUpdatedData));

                model.ComplianceType.ComplianceTypeID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Compliance_Type_ID"]);
                model.ComplianceType.ComplianceTypeName = Convert.ToString(dsUpdatedData.Tables[0].Rows[0]["Compliance_Type_Name"]);
                model.ComplianceType.EndDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["End_Date"]);
                model.ComplianceType.StartDate = Convert.ToDateTime(dsUpdatedData.Tables[0].Rows[0]["Start_Date"]);

                string strXMLIndustryType = client.GetIndustryType();
                DataSet dsIndustryType = new DataSet();
                dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
                model.IndustryTypeList = new List<SelectListItem>();
                model.IndustryTypeList.Add(new SelectListItem { Text = "-- Industry Type --", Value = "" });
                if (dsIndustryType.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                    {
                        model.ComplianceType.IndustryTypeID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Industry_Type_ID"]);
                        model.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                    }
                }



                string strXMLCountries = client.GetCountryList();
                DataSet dsCountries = new DataSet();
                dsCountries.ReadXml(new StringReader(strXMLCountries));
                model.CountryList = new List<SelectListItem>();
                model.CountryList.Add(new SelectListItem { Text = "-- Select Country --", Value = "" });
                if (dsCountries.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                    {
                        model.ComplianceType.CountryID = Convert.ToInt32(dsUpdatedData.Tables[0].Rows[0]["Country_ID"]);
                        model.CountryList.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                    }
                }
                model.AuditfrequencyList = new List<SelectListItem>();
                model.AuditfrequencyList.Add(new SelectListItem { Text = "-- Audit Frequency --", Value = "" });
                model.ComplianceType.AuditingFrequency = Convert.ToString(dsUpdatedData.Tables[0].Rows[0]["Audit_Frequency"]);

                model.ComplianceType.StartDate = DateTime.Now;
                model.ComplianceType.EndDate = model.ComplianceType.StartDate.AddYears(1);
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }

            return View("_MapComplianceTypes", model);

        }

        [HttpPost]
        public ActionResult UpdateIndustryTypeMapCompliance(ComplianceIndustryViewModel model)
        {
            try
            {
                OrgService.OrganizationServiceClient organizationService = new OrgService.OrganizationServiceClient();
                int id = organizationService.updateComplianceTypesMappedWithIndustryType(model.ComplianceType);
                if (id != 0)
                {
                    TempData["Success"] = "Updated successfully";
                }
                else
                {
                    TempData["Success"] = "Could not update successfully";


                }
            }
            catch(Exception)
            {
                return View("ErrorPage");
            }
            return RedirectToAction("dashboard", "common", new { pid = 0 });

        }

    
    }
}





