using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using System.Data;
using System.IO;
using System.Configuration;

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

        public JsonResult getspecificbranch(string compid)
        {
            List<SelectListItem> company = new List<SelectListItem>();
            int ID = Convert.ToInt32(compid);
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            string xmldata = auditServiceClient.getSpecificBranchList(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            company = new List<SelectListItem>();
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                company.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
            }
            return Json(company, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getspecificvendors(string compid)
        {
            List<SelectListItem> vendors = new List<SelectListItem>();
            int ID = Convert.ToInt32(compid);
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            string xmldata = organizationServiceClient.GetVendors(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            vendors = new List<SelectListItem>();
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                vendors.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
            }
            return Json(vendors, JsonRequestBehavior.AllowGet);
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
                    menues.Add(new Menus { MenuName = Convert.ToString(row["Menu_Name"]), PathUrl = Convert.ToString(row["Page_URL"]), icon = Convert.ToString(row["icon"]), ParentMenuId = Convert.ToInt32(row["Parent_MenuID"]) });
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
    }
}