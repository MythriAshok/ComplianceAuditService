using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using ComplianceAuditWeb.Models;
using System.Data;
using System.IO;

namespace ComplianceAuditWeb.Controllers
{
    public class ComplianceManagementController : Controller
    {
        // GET: ComplianceManagement
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateActs()
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Countrylist = new List<SelectListItem>();
            model.Countrylist.Add(new SelectListItem() { Text = "--Select Country--", Value = "0" });
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationservice.GetCountryList();           
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));       
            
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                model.Countrylist.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
            string strXMLStates = organizationservice.GetStateList(0);
            string strXMLCities = organizationservice.GetCityList(0);
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
            if (dsStates.Tables.Count > 0)
            {
                model.Statelist = new List<SelectListItem>();
                model.Statelist.Add(new SelectListItem() { Text = "--Select State--", Value = "0" });
                foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
                {
                    model.Statelist.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            if (dsCities.Tables.Count > 0)
            {
                model.Citylist = new List<SelectListItem>();
                model.Citylist.Add(new SelectListItem() { Text = "--Select City--", Value = "0" });
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    model.Citylist.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            return View("_AddActs",model);
        }
        [HttpPost]
        public ActionResult CreateActs(ComplianceViewModel model)
        {
             if (ModelState.IsValid)
            {
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                model.Compliance.User_ID = 1;
                 model.Compliance.Compliance_Parent_ID = client.insertActs(model.Compliance);
                if (model.Compliance.Compliance_Parent_ID>0)
                {
                    model.Compliance.Compliance_Title = "RuleSection";
                    client.insertSection(model.Compliance);
                   return RedirectToAction("ListofCompliance");
                }
            }
            return View("_AddActs",model);
        }

        [HttpGet]
        public ActionResult GetActs()
        {
            List<ComplianceXref> model = new List<ComplianceXref>();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata=client.GetActs();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            foreach(System.Data.DataRow row in ds.Tables[0].Rows)
            {
                model.Add(new ComplianceXref
                {
                    Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]),
                    Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]),
                    Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                    Comp_Category = Convert.ToString(row["Comp_Category"]),
                    Comp_Description = Convert.ToString(row["Comp_Description"]),
                    compl_def_consequence = Convert.ToString(row["compl_def_consequence"]),
                    Comp_Order = Convert.ToInt32(row["Comp_Order"]),
                    Country_ID = Convert.ToInt32(row["Country_ID"]),
                    City_ID = Convert.ToInt32(row["City_ID"]),
                    Effective_End_Date = Convert.ToDateTime(row["Effective_Start_Date"]),
                    Effective_Start_Date = Convert.ToDateTime(row["Effective_End_Date"]),
                    Form = Convert.ToString(row["Form"]),
                    Is_Header = Convert.ToBoolean(row["Is_Header"]),
                    level = Convert.ToInt32(row["level"]),
                    State_ID = Convert.ToInt32(row["State_ID"]),
                    User_ID = Convert.ToInt32(row["User_ID"]),
                    Is_Active = Convert.ToBoolean(row["Is_Active"]),
                    Is_Best_Practice = Convert.ToBoolean(row["Is_Best_Practice"]),
                    Risk_Category = Convert.ToString(row["Risk_Category"]),
                    Option_ID = Convert.ToInt32(row["Option_ID"]),
                    Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                    Recurrence = Convert.ToString(row["Recurrence"]),
                    Risk_Description = Convert.ToString(row["Risk_Description"]),
                    Type = Convert.ToString(row["Type"]),
                    Version = Convert.ToInt32(row["Version"])
                });
            }           
            return View();
        }

        [HttpGet]
        public ActionResult CreateSection(int parentid)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Compliance = new ComplianceXref();
            model.Compliance.Compliance_Parent_ID = parentid;
            model.Countrylist = new List<SelectListItem>();
            model.Countrylist.Add(new SelectListItem() { Text = "--Select Country--", Value = "0" });
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationservice.GetCountryList();
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                model.Countrylist.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
            string strXMLStates = organizationservice.GetStateList(0);
            string strXMLCities = organizationservice.GetCityList(0);
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
            if (dsStates.Tables.Count > 0)
            {
                model.Statelist = new List<SelectListItem>();
                model.Statelist.Add(new SelectListItem() { Text = "--Select State--", Value = "0" });
                foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
                {
                    model.Statelist.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            if (dsCities.Tables.Count > 0)
            {
                model.Citylist = new List<SelectListItem>();
                model.Citylist.Add(new SelectListItem() { Text = "--Select City--", Value = "0" });
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    model.Citylist.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            return View("_AddSection", model);
        }
        [HttpPost]
        public ActionResult CreateSection(ComplianceViewModel model)
        {
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            model.Compliance.User_ID = 1;
            client.insertSection(model.Compliance);
            return RedirectToAction("ListofCompliance");
        }

        [HttpGet]
        public ActionResult CreateRules(int Parentid)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Compliance = new ComplianceXref();
            model.Compliance.Compliance_Parent_ID = Parentid;
            model.Countrylist = new List<SelectListItem>();
            model.Countrylist.Add(new SelectListItem() { Text = "--Select Country--", Value = "0" });
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationservice.GetCountryList();
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                model.Countrylist.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
            string strXMLStates = organizationservice.GetStateList(0);
            string strXMLCities = organizationservice.GetCityList(0);
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
            if (dsStates.Tables.Count > 0)
            {
                model.Statelist = new List<SelectListItem>();
                model.Statelist.Add(new SelectListItem() { Text = "--Select State--", Value = "0" });
                foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
                {
                    model.Statelist.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            if (dsCities.Tables.Count > 0)
            {
                model.Citylist = new List<SelectListItem>();
                model.Citylist.Add(new SelectListItem() { Text = "--Select City--", Value = "0" });
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    model.Citylist.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            return PartialView("_AddRules",model);
        }

        [HttpPost]
        public ActionResult CreateRules(ComplianceViewModel model)
        {
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            model.Compliance.User_ID = 1;
            client.insertRules(model.Compliance);
            return RedirectToAction("ListofCompliance");
        }
        public ActionResult ListofCompliance()
        {
            ListofComplianceViewModel model = new ListofComplianceViewModel();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata = client.GetActs();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Actslist = bindCompliancelist(ds.Tables[0], model.Actslist);
            xmldata = client.GetSections(0);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Sectionlist = bindCompliancelist(ds.Tables[0], model.Sectionlist);
            xmldata = client.GetRules(0);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                model.Rulelist.Add(new ComplianceXref
                {
                    Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]),
                    Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]),
                    Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                    Comp_Category = Convert.ToString(row["Comp_Category"]),
                    Comp_Description = Convert.ToString(row["Comp_Description"]),
                    compl_def_consequence = Convert.ToString(row["compl_def_consequence"]),
                    Comp_Order = Convert.ToInt32(row["Comp_Order"]),
                    Country_ID = Convert.ToInt32(row["Country_ID"]),
                    City_ID = Convert.ToInt32(row["City_ID"]),
                    Effective_End_Date = Convert.ToDateTime(row["Effective_Start_Date"]),
                    Effective_Start_Date = Convert.ToDateTime(row["Effective_End_Date"]),
                    Form = Convert.ToString(row["Form"]),
                    Is_Header = Convert.ToBoolean(Convert.ToInt32(row["Is_Header"])),
                    level = Convert.ToInt32(row["level"]),
                    State_ID = Convert.ToInt32(row["State_ID"]),
                    User_ID = Convert.ToInt32(row["User_ID"]),
                    Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Is_Best_Practice = Convert.ToBoolean(Convert.ToInt32(row["Is_Best_Practice"])),
                    Risk_Category = Convert.ToString(row["Risk_Category"]),
                    Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                    Recurrence = Convert.ToString(row["Recurrence"]),
                    Risk_Description = Convert.ToString(row["Risk_Description"]),
                    Type = Convert.ToString(row["Type"]),
                    Version = Convert.ToInt32(row["Version"])
                });
            }
                return View("_ListofCompliance", model);            
        }
        private List<ComplianceXref> bindCompliancelist(DataTable dt,List<ComplianceXref> model)
        {
            foreach (System.Data.DataRow row in dt.Rows)
            {
                model.Add(new ComplianceXref
                {
                    Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]),
                    Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]),
                    Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                    Comp_Category = Convert.ToString(row["Comp_Category"]),
                    Comp_Description = Convert.ToString(row["Comp_Description"]),
                    Comp_Order = Convert.ToInt32(row["Comp_Order"]),
                    Country_ID = Convert.ToInt32(row["Country_ID"]),
                    City_ID = Convert.ToInt32(row["City_ID"]),
                    Effective_End_Date = Convert.ToDateTime(row["Effective_Start_Date"]),
                    Effective_Start_Date = Convert.ToDateTime(row["Effective_End_Date"]),
                    Is_Header = Convert.ToBoolean(Convert.ToInt32(row["Is_Header"])),
                    level = Convert.ToInt32(row["level"]),
                    State_ID = Convert.ToInt32(row["State_ID"]),
                    User_ID = Convert.ToInt32(row["User_ID"]),
                    Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Risk_Category = Convert.ToString(row["Risk_Category"]),
                    Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                    Risk_Description = Convert.ToString(row["Risk_Description"]),
                    Version = Convert.ToInt32(row["Version"])                   
                });
            }
                return model;
        }
        [HttpGet]
        public ActionResult AllocateActandRule()
        {
            AllocateActandRuleViewModel model = new AllocateActandRuleViewModel();
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata=client.GetCompaniesList();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Companylist = new List<SelectListItem>() { new SelectListItem { Text = "--Select Company--", Value = "0" } };
            foreach(System.Data.DataRow row in ds.Tables[0].Rows)
            {
                model.Companylist.Add(new SelectListItem { Text =Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
            }
           xmldata=client.GetBranchList();
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.BranchList = new List<SelectListItem>() { new SelectListItem { Text = "--Select Branch--", Value = "0" } };
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                model.BranchList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
            }
            ComplianceXrefService.ComplianceXrefServiceClient xrefServiceClient = new ComplianceXrefService.ComplianceXrefServiceClient();
            xmldata = xrefServiceClient.GetActs();
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Actdropdownlist = new List<SelectListItem>() { new SelectListItem { Text = "--Select Act--", Value = "0" } };
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                model.Actdropdownlist.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Title"]), Value = Convert.ToString(row["Compliance_Xref_ID"]) });
            }           
            //xmldata=  xrefServiceClient.GetSections(0);
            //ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
            model.Sectionlist = new List<SelectListItem>() { new SelectListItem { Text = "--Select Section--", Value = "0" } };
            ////model.Sectionlist = bindCompliancelist(ds.Tables[0], model.Sectionlist);
            //foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //{
            //    model.Sectionlist.Add(new SelectListItem
            //    {
            //        Text = Convert.ToString(row["Compliance_Title"]),
            //        Value = Convert.ToString(row["Compliance_Xref_ID"])
            //    });
            //}
            //xmldata = xrefServiceClient.GetRules(0);
            //ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
            model.Rulelist = new List<SelectListItem>();
            //foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //{
            //    model.Rulelist.Add(new SelectListItem
            //    {
            //        Text = Convert.ToString(row["Compliance_Title"]),
            //        Value = Convert.ToString(row["Compliance_Xref_ID"])
            //    });
            //}
            model.Selectedrule = new List<SelectListItem>();
            return View("_AllocateActsandRules", model);
        }

        [HttpPost]
        public ActionResult AllocateActandRule(AllocateActandRuleViewModel model)
        {
            ComplianceAudit audit = new ComplianceAudit();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            audit.Auditor_Id=client.GetAuditorId(model.BranchId);
            audit.Org_Hier_Id = model.BranchId;
            audit.User_Id = 1;
            client.inseretActandRuleforBranch(audit, model.selectedid);
            return View();
        }
        
        public JsonResult getSection(string actid)
        {
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            int ID = Convert.ToInt32(actid);
            string xmldata = client.GetSections(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            List<SelectListItem> sectionlist = new List<SelectListItem>();
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                sectionlist.Add(new SelectListItem
                {
                    Text = Convert.ToString(row["Compliance_Title"]),
                    Value = Convert.ToString(row["Compliance_Xref_ID"])
                });
            }
            return Json(sectionlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getRules(string secid)
        {
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            int ID = Convert.ToInt32(secid);
            string xmldata = client.GetRules(ID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            List<SelectListItem> Rulelist = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    Rulelist.Add(new SelectListItem
                    {
                        Text = Convert.ToString(row["Compliance_Title"]),
                        Value = Convert.ToString(row["Compliance_Xref_ID"])
                    });
                }
            }
            return Json(Rulelist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getallocatedrules(string secid,string branchid)
        {
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            int sectionID = Convert.ToInt32(secid);
            int BranchID = Convert.ToInt32(branchid);
            string xmldata = client.getRuleforBranch(sectionID, BranchID);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            List<SelectListItem> Rulelist = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    Rulelist.Add(new SelectListItem
                    {
                        Text = Convert.ToString(row["Compliance_Title"]),
                        Value = Convert.ToString(row["Compliance_Xref_ID"])
                    });
                }
            }
            return Json(Rulelist, JsonRequestBehavior.AllowGet);
        }

    }
}