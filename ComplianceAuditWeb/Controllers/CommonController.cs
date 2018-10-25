using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using System.Data;
using System.IO;
using System.Configuration;
using ComplianceAuditWeb.Models;

namespace ComplianceAuditWeb.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common

        public JsonResult getstate(string countryid)
            {
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            int ID = Convert.ToInt32(countryid);
            List<SelectListItem> state = new List<SelectListItem>();
            string strXMLStates = organizationservice.GetStateList(ID);
            DataSet dsstate = new DataSet();


            dsstate.ReadXml(new StringReader(strXMLStates));
            if (dsstate.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsstate.Tables[0].Rows)
                {                  
                    state.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            return Json(state, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getcity(string stateid)
            {
            List<SelectListItem> cities = new List<SelectListItem>();
            int ID = Convert.ToInt32(stateid);
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCities = organizationservice.GetCityList(ID);
            DataSet dsCities = new DataSet();
            dsCities.ReadXml(new StringReader(strXMLCities));
            if (dsCities.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {

                    cities.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getbranch(string compid)
        {
            List<SelectListItem> branch = new List<SelectListItem>();
            int ID = Convert.ToInt32(compid);
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            string xmldata = auditServiceClient.getSpecificBranchList(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            branch = new List<SelectListItem>();
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                branch.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
            }
            
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    branch.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }

            }
            return Json(branch, JsonRequestBehavior.AllowGet);
        }
       


        public JsonResult getcompany(string groupcompid)
        {
            List<SelectListItem> company = new List<SelectListItem>();
            int ID = Convert.ToInt32(groupcompid);
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata = client.GeSpecifictCompaniesList(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            company = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    company.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            Session["Company"] = company;
            return Json(company, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getcompanydropdown(string groupcompid)
        {
            List<SelectListItem> company = new List<SelectListItem>();
            int ID = Convert.ToInt32(groupcompid);
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata = client.getCompanyListDropDown(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            company = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    company.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            Session["Company"] = company;
            return Json(company, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getspecificbranch(string compid)
        {
            List<SelectListItem> company = new List<SelectListItem>();
            int ID = Convert.ToInt32(compid);
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            string xmldata = auditServiceClient.getSpecificBranchList(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            company = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    company.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            return Json(company, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getspecificbranchdropdown(string compid)
        {
            List<SelectListItem> company = new List<SelectListItem>();
            int ID = Convert.ToInt32(compid);
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string xmldata = organizationServiceClient.getSpecificBranchListDropDown(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            company = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    company.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            return Json(company, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getspecificvendors(string compid,string branchid)
        {
            List<SelectListItem> vendors = new List<SelectListItem>();
            int ID = Convert.ToInt32(compid);
            int branchID = Convert.ToInt32(branchid);
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string xmldata = organizationServiceClient.getSpecificVendorListDropDown(ID, branchID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            vendors = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    vendors.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getdefaultcountry(string compid)
        {
            BranchViewModel branchVM = new BranchViewModel();
            int ID = Convert.ToInt32(compid);
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string xmldata = organizationServiceClient.getDefaultCompanyDetails(ID);
            DataSet dsDefaultCompanyDetails = new DataSet();
            dsDefaultCompanyDetails.ReadXml(new StringReader(xmldata));
            branchVM.Country = new List<SelectListItem>();
            if (dsDefaultCompanyDetails.Tables.Count > 0)
            {                 
                branchVM.Country.Add(new SelectListItem() { Text = dsDefaultCompanyDetails.Tables[0].Rows[0]["Country_Name"].ToString(), Value = dsDefaultCompanyDetails.Tables[0].Rows[0]["Country_ID"].ToString() });
                branchVM.Country.Add(new SelectListItem() { Text = dsDefaultCompanyDetails.Tables[0].Rows[0]["State_Name"].ToString(), Value = dsDefaultCompanyDetails.Tables[0].Rows[0]["State_ID"].ToString() });
                branchVM.Country.Add(new SelectListItem() { Text = dsDefaultCompanyDetails.Tables[0].Rows[0]["City_Name"].ToString(), Value = dsDefaultCompanyDetails.Tables[0].Rows[0]["City_ID"].ToString() });
            }      
            return Json(branchVM.Country, JsonRequestBehavior.AllowGet);
        }


        public ActionResult dashboard(int pid)
        {
            List<Menus> menues = new List<Menus>();
            UserService.UserServiceClient client = new UserService.UserServiceClient();
            DataSet ds = new DataSet();
            string xmlmenu = client.getmenulist(Convert.ToInt32(Session["UserId"]), pid);
            ds.ReadXml(new StringReader(xmlmenu));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    menues.Add(new Menus { Id = Convert.ToInt32(row["Menu_ID"]), MenuName = Convert.ToString(row["Menu_Name"]), PathUrl = Convert.ToString(row["Page_URL"]), icon = Convert.ToString(row["icon"]), ParentMenuId = Convert.ToInt32(row["Parent_MenuID"]) });
                }
            }
            return View("~/Views/Shared/_Dashboard.cshtml", menues);
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View("_UploadFiles");
        }

        [HttpPost]
        public string UploadFile(HttpPostedFileBase file,string filePath)
        {
            //if (ModelState.IsValid)
            //{
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                    string fileName = Path.GetFileName(file.FileName);
                        //string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                        //string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()),fileName);
                        if (System.IO.File.Exists(filePath))
                        {                        
                            ViewBag.Message = "The File Already Exists in System";
                        }
                        else
                        {
                            file.SaveAs(filePath);
                            ViewBag.Message = "File Uploaded Successfully";

                        }
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Message = "ERROR:" + exception.Message.ToString();
                    }
                }
                else
                {
                    ViewBag.Message = "Specify the file";
                }
            //}
            //return View("View");
            return ViewBag.Message;
        }
        [HttpGet]
        public ActionResult SelectGroupCompany()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Text = "-- Select Group Company --", Value = "0" });
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLGroupCompanyList = organizationservice.getGroupCompanyListDropDown();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));

            if (dsGroupCompanyList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
                {
                    if(Convert.ToInt32(Session["GroupCompanyId"]) == Convert.ToInt32(row["Org_Hier_ID"]))
                    listItems.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString(),Selected=true });
                    else
                    listItems.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString()});
                }
            }
            return PartialView("~/Views/Shared/_SelectGroupCompany.cshtml", listItems);
        }

        [HttpPost]
        public JsonResult SelectGroupCompany(string GroupDropdown)
        {
            Session["GroupCompanyId"] = Convert.ToInt32(GroupDropdown);
            setGroupCompanyDetails(Convert.ToInt32(Session["GroupCompanyId"]));
            //return PartialView("~/Views/Shared/_SelectGroupCompany.cshtml");
            return Json(new { success = true });
        }



        public JsonResult getspecificvendorsassociatedwithbranch(string branchid)
        {
            List<SelectListItem> vendors = new List<SelectListItem>();
            int ID = Convert.ToInt32(branchid);
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();

            string xmldata = vendorServiceClient.GetAssignedVendorsforBranch(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            vendors = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    vendors.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Vendor_ID"]) });
                }
            }
            Session["A"] = vendors;

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }


        public void setGroupCompanyDetails(int groupcompanyid)
        {
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            
            string xmlData = organizationServiceClient.getParticularGroupCompaniesList(groupcompanyid);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(new StringReader(xmlData));
            if(dataSet.Tables.Count>0)
            {
              System.Web.HttpContext.Current.Session["GroupCompanyName"] = Convert.ToString( dataSet.Tables[0].Rows[0]["Company_Name"]);
               
              

            }
        }

        public JsonResult getCompliance(string countryid, string industrytypeid)
        {
            if(countryid == "")
            {
                countryid = Convert.ToString(0);
            }
            List<SelectListItem> cities = new List<SelectListItem>();
            int CID = Convert.ToInt32(countryid);
            if (industrytypeid == "")
            {
                industrytypeid = Convert.ToString(0);
            }
            int IndustryID = Convert.ToInt32(industrytypeid);
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCompliances = organizationservice.GetComplianceType(CID, IndustryID);
            DataSet dsCompliances = new DataSet();
            dsCompliances.ReadXml(new StringReader(strXMLCompliances));
            if (dsCompliances.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompliances.Tables[0].Rows)
                {

                    cities.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                }
            }
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getcompliancelistundercompany(string compid)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            int CID = Convert.ToInt32(compid);
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCompliances = organizationservice.GetAssignedComplianceTypes(CID);
            DataSet dsCompliances = new DataSet();
            dsCompliances.ReadXml(new StringReader(strXMLCompliances));
            if (dsCompliances.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompliances.Tables[0].Rows)
                {

                    cities.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                }
            }
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getdefaultindustrytype( int compid)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            VendorViewModel model = new VendorViewModel();
            model.companydetails = new CompanyDetails();
            //int CID = Convert.ToInt32(compid);
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLIndustry = organizationservice.GetIndustryType();
            DataSet dsIndustry = new DataSet();
            dsIndustry.ReadXml(new StringReader(strXMLIndustry));
            if (dsIndustry.Tables.Count > 0)
            {

                // model.companydetails.Industry_Type_ID=Convert.ToInt32(dsIndustry.Tables[0].Rows[0]["Industry_Type_ID"]);
                //cities.Add(new SelectListItem() { Text = dsIndustry.Tables[0].Rows[0]["Industry_Name"].ToString(), Value = dsIndustry.Tables[0].Rows[0]["Industry_Type_ID"].ToString() });

                foreach (System.Data.DataRow row in dsIndustry.Tables[0].Rows)
                {

                    cities.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                }
                
            }
            return Json(cities, JsonRequestBehavior.AllowGet);
        }




        public ActionResult GetComplianceToBindGrid(string countryid, string industrytypeid)
        {
            List<ComplianceIndustryViewModel> objCompList = new List<ComplianceIndustryViewModel>();
            if (countryid == "")
            {
                countryid = Convert.ToString(0);
            }
            int CID = Convert.ToInt32(countryid);

            if (industrytypeid == "")
            {
                industrytypeid = Convert.ToString(0);
            }
            int IndustryID = Convert.ToInt32(industrytypeid);
            
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCompliances = organizationservice.GetComplianceType(CID, IndustryID);
            DataSet dsCompliances = new DataSet();
            dsCompliances.ReadXml(new StringReader(strXMLCompliances));
            if (dsCompliances.Tables.Count > 0)
            {
                //ComplianceIndustryViewModel objComp1 = new ComplianceIndustryViewModel();
                //objComp1.CountryName = Convert.ToString(dsCompliances.Tables[0].Rows[0]["Country_Name"]);
                //objComp1.IndustryName = dsCompliances.Tables[0].Rows[0]["Industry_Name"].ToString();
                //objCompList.Add(objComp1);
                for (int i = 0; i < dsCompliances.Tables[0].Rows.Count; i++)
                {
                ComplianceIndustryViewModel objComp = new ComplianceIndustryViewModel();

                    objComp.CountryName = Convert.ToString(dsCompliances.Tables[0].Rows[i]["Country_Name"]);
                    objComp.IndustryName = dsCompliances.Tables[0].Rows[i]["Industry_Name"].ToString();
                    objComp.ComplianceType = new ComplianceType();
                    objComp.ComplianceType.ComplianceTypeName = dsCompliances.Tables[0].Rows[i]["Compliance_Type_Name"].ToString();
                    objComp.ComplianceType.ComplianceTypeID =Convert.ToInt32( dsCompliances.Tables[0].Rows[i]["Compliance_Type_ID"]);
                    objCompList.Add(objComp);
                }
            }
            return Json(objCompList, JsonRequestBehavior.AllowGet);
        }


            public ActionResult SetSessionVariable(string key, string value)
            {
                Session[key] = value;

                return this.Json(new { success = true });
            }

        public JsonResult getyear(int compid, int compliancetypeid)
        {
            int sd = 0;
            int ed = 0;
            List<string> years = new List<string>();
            ReportViewModel model = new ReportViewModel();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strxmlcompliancetype = organizationservice.GetParticularCompliance(compliancetypeid);
            DataSet dsComplianceType = new DataSet();
            dsComplianceType.ReadXml(new StringReader(strxmlcompliancetype));
            if(dsComplianceType.Tables.Count>0)
            {
                string ComplianceName = Convert.ToString(dsComplianceType.Tables[0].Rows[0]["Compliance_Type_Name"]);
                DateTime StartDate = Convert.ToDateTime(dsComplianceType.Tables[0].Rows[0]["Start_Date"]);
                DateTime EndDate = Convert.ToDateTime(dsComplianceType.Tables[0].Rows[0]["End_date"]);

                sd = StartDate.Year;
                ed = EndDate.Year;
            }

            string strxmlyear = organizationservice.getDefaultCompanyDetails(compid);
            DataSet dsyear = new DataSet();
            dsyear.ReadXml(new StringReader(strxmlyear));
            if (dsyear.Tables.Count > 0)
            {
                model.StartDate=Convert.ToDateTime(dsyear.Tables[0].Rows[0]["Calender_StartDate"]);
                model.yearid = model.StartDate.Year;
                if (sd == ed)
                {
                    model.years = Enumerable.Range(model.yearid, DateTime.Now.Year - (model.yearid - 1)).OrderByDescending(i=>i);
                    //model.yearid = DateTime.Now.Year;
                }
                else
                {
                    int count = DateTime.Now.Year - (model.yearid - 1);
                    for (int i = 1;i<=count; i++)
                    {
                        years.Add(model.yearid.ToString() + "-" + (model.yearid + 1));
                        ++model.yearid;
                    }
                    years.Reverse();

                    return Json(years, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(model.years, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getfrequency(int frequencyid)
        {
            List<SelectListItem> frequency = new List<SelectListItem>();
            ReportViewModel model = new ReportViewModel();
            if(frequencyid == 1)
            {

            }
            else if (frequencyid == 2)
            {
                //frequency.Add(new SelectListItem() { Text = "Select Half-Year", Value = "0" });
                frequency.Add(new SelectListItem() { Text = "First Half", Value = "1" });
                frequency.Add(new SelectListItem() { Text = "Second Half", Value = "2" });
            }
            else if (frequencyid == 3)
            {
                //frequency.Add(new SelectListItem() { Text = "Select Quarter", Value = "1" });
                frequency.Add(new SelectListItem() { Text = "Quarter 1", Value = "2" });
                frequency.Add(new SelectListItem() { Text = "Quarter 2", Value = "3" });
                frequency.Add(new SelectListItem() { Text = "Quarter 3", Value = "4" });
                frequency.Add(new SelectListItem() { Text = "Quarter 4", Value = "5" });
            }
            else if(frequencyid == 4)
            {
                //frequency.Add(new SelectListItem() { Text = "Select Month", Value = "0" });
                frequency.Add(new SelectListItem() { Text = "January", Value = "1" });
                frequency.Add(new SelectListItem() { Text = "February", Value = "2" });
                frequency.Add(new SelectListItem() { Text = "March", Value = "3" });
                frequency.Add(new SelectListItem() { Text = "April", Value = "4" });
                frequency.Add(new SelectListItem() { Text = "May", Value = "5" });
                frequency.Add(new SelectListItem() { Text = "June", Value = "6" });
                frequency.Add(new SelectListItem() { Text = "July", Value = "7" });
                frequency.Add(new SelectListItem() { Text = "August", Value = "8" });
                frequency.Add(new SelectListItem() { Text = "September", Value = "9" });
                frequency.Add(new SelectListItem() { Text = "October", Value = "10" });
                frequency.Add(new SelectListItem() { Text = "November", Value = "11" });
                frequency.Add(new SelectListItem() { Text = "December", Value = "12" });
            }
            else
            {
                //frequency.Add(new SelectListItem() { Text = "Select Month", Value = "0" });
                frequency.Add(new SelectListItem() { Text = "January", Value = "1" });
                frequency.Add(new SelectListItem() { Text = "February", Value = "2" });
                frequency.Add(new SelectListItem() { Text = "March", Value = "3" });
                frequency.Add(new SelectListItem() { Text = "April", Value = "4" });
                frequency.Add(new SelectListItem() { Text = "May", Value = "5" });
                frequency.Add(new SelectListItem() { Text = "June", Value = "6" });
                frequency.Add(new SelectListItem() { Text = "July", Value = "7" });
                frequency.Add(new SelectListItem() { Text = "August", Value = "8" });
                frequency.Add(new SelectListItem() { Text = "September", Value = "9" });
                frequency.Add(new SelectListItem() { Text = "October", Value = "10" });
                frequency.Add(new SelectListItem() { Text = "November", Value = "11" });
                frequency.Add(new SelectListItem() { Text = "December", Value = "12" });
            }
            return Json(frequency, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCompanyDetailsfroVendor(int compid, string industrytypeid)
        {
            int industryTypeID = 0;
            if (industrytypeid != "")
            {
                 industryTypeID = Convert.ToInt32(industrytypeid);
            }
            List<SelectListItem> compliance = new List<SelectListItem>();
            VendorViewModel vendorVM = new VendorViewModel();
            vendorVM.location = new BranchLocation();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCompanyDetails = organizationservice.getDefaultCompanyDetails(compid);
            DataSet dsCompanyDetails = new DataSet();
            dsCompanyDetails.ReadXml(new StringReader(strXMLCompanyDetails));
            if (dsCompanyDetails.Tables.Count > 0)
            {
                vendorVM.location.Country_Id = Convert.ToInt32(dsCompanyDetails.Tables[0].Rows[0]["Country_ID"]);
                vendorVM.location.State_Id = Convert.ToInt32(dsCompanyDetails.Tables[0].Rows[0]["State_ID"]);
                vendorVM.location.City_Id = Convert.ToInt32(dsCompanyDetails.Tables[0].Rows[0]["City_ID"]);
                vendorVM.location.Postal_Code = Convert.ToString(dsCompanyDetails.Tables[0].Rows[0]["Postal_Code"]);
               string strCompliances= organizationservice.GetComplianceType(industryTypeID, vendorVM.location.Country_Id);
                DataSet dsCompliances = new DataSet();
                dsCompliances.ReadXml(new StringReader(strCompliances));
                if (dsCompliances.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsCompliances.Tables[0].Rows)
                    {

                        compliance.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                    }
                }

            }
            return Json(compliance, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getYearTextBox(int complianceid)
        //{
        //    int date = 0;
        //    int month = 0;
        //    List<string> years = new List<string>();

        //    CompanyViewModel model = new CompanyViewModel();
        //    model.companydetails = new CompanyDetails();
        //    OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
        //    string strxmlcompliancetype = organizationservice.GetParticularCompliance(complianceid);
        //    DataSet dsComplianceType = new DataSet();
        //    dsComplianceType.ReadXml(new StringReader(strxmlcompliancetype));
        //    if (dsComplianceType.Tables.Count > 0)
        //    {
        //        string ComplianceName = Convert.ToString(dsComplianceType.Tables[0].Rows[0]["Compliance_Type_Name"]);
        //        DateTime StartDate = Convert.ToDateTime(dsComplianceType.Tables[0].Rows[0]["Start_Date"]);
        //        DateTime EndDate = Convert.ToDateTime(dsComplianceType.Tables[0].Rows[0]["End_date"]);

        //        date = StartDate.Day;
        //        month = StartDate.Month;

        //        years.Add(Convert.ToString(date));
        //        years.Add(Convert.ToString(month));


        //    }
        //    return Json(years, JsonRequestBehavior.AllowGet);
        //}
    }
}