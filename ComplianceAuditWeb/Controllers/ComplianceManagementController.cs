﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using ComplianceAuditWeb.Models;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;

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
            if (dsCountries.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    model.Countrylist.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }
      
            model.Statelist = new List<SelectListItem>();
            model.Statelist.Add(new SelectListItem() { Text = "--Select State--", Value = "0" });
          
            model.Citylist = new List<SelectListItem>();
            model.Citylist.Add(new SelectListItem() { Text = "--Select City--", Value = "0" });
          
            model.ActType = new List<SelectListItem>();
            model.ActType.Add(new SelectListItem { Text = "--Select Act Type--", Value = "0" });
            model.ActType.Add(new SelectListItem { Text = "Union Level", Value = "1" });
            model.ActType.Add(new SelectListItem { Text = "State Level", Value = "2" });
            //model.ActType.Add(new SelectListItem { Text = "City Level", Value = "3" });

           // ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();

            //model.ComplianceType = new List<SelectListItem>();
            //string xmldata=client.GetComplainceType();
            //DataSet ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
            //model.ComplianceType.Add(new SelectListItem { Text = "-- Select Compliance --", Value = "0" });
            //if (ds.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //    {
            //        model.ComplianceType.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
            //    }
            //}
            model.Compliance = new ComplianceXref();
            model.Compliance.Compliance_Xref_ID = 0;
            Session["Actmodel"] = model;
            return View("_AddActs", model);
        }
        [HttpPost]
        public ActionResult CreateActs(ComplianceViewModel model)
        {
            //if(model.Compliance.Effective_Start_Date==null)
            //{
            //    model.Compliance.Effective_Start_Date = DateTime.MinValue.Date;
            //}
            if (ModelState.IsValid)
            {
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                model.Compliance.User_ID = Convert.ToInt32(Session["UserId"]);
                model.Compliance.Effective_End_Date = DateTime.MaxValue.Date;
                model.Compliance.Compliance_Parent_ID = client.insertActs(model.Compliance);
                if (model.Compliance.Compliance_Parent_ID > 0)
                {
                    TempData["Message"] = "Successfuly Created " + model.Compliance.Compliance_Title + " Act.";
                    //model.Compliance.Compliance_Title = "RuleSection";
                    //client.insertSection(model.Compliance);                   
                    return RedirectToAction("ListofCompliance");
                }
                else
                    TempData["Message"] = "Not able to create the " + model.Compliance.Compliance_Title + "Act.";
            }
            else
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            model = (ComplianceViewModel)Session["Actmodel"];
            Session.Remove("Actmodel");
            return View("_AddActs", model);
        }

        [HttpGet]
        public ActionResult GetActs()
        {
            List<ComplianceXref> model = new List<ComplianceXref>();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata = client.GetActs(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
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
                    Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                    Recurrence = Convert.ToString(row["Recurrence"]),
                    Risk_Description = Convert.ToString(row["Risk_Description"]),
                    Type = Convert.ToString(row["Type"]),
                    Version = Convert.ToInt32(row["Version"])
                });
            }
            return View();
        }

        //[HttpGet]
        //public ActionResult CreateSection(int Parentid,string type)
        //{
        //    ComplianceViewModel model = new ComplianceViewModel();
        //    model.Compliance = new ComplianceXref();
        //    ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
        //    model.Compliance.Compliance_Parent_ID = Parentid;
        //    string xmldata;
        //    if (type == "SubSection")
        //    {  xmldata = client.GetSpecificsection(Parentid); }
        //    else { xmldata = client.GetActs(Parentid); }
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new StringReader(xmldata));
        //    model.Compliance.Effective_Start_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_Start_Date"]);
        //    model.Compliance.Compliance_Type_ID=Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Type_ID"]);
        //    model.Compliance.Country_ID= Convert.ToInt32(ds.Tables[0].Rows[0]["Country_ID"]);
        //    model.Compliance.State_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["State_ID"]);
        //    model.Compliance.City_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["City_ID"]);
        //    model.Compliance.Effective_End_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_End_Date"]);
        //    return View("_AddSection", model);
        //}
        //[HttpPost]
        //public ActionResult CreateSection(ComplianceViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
        //        model.Compliance.User_ID = Convert.ToInt32(Session["UserId"]);
        //        int id = client.insertSection(model.Compliance);
        //        if (id > 0)
        //        {
        //            TempData["Message"] = "Successfuly Created " + model.Compliance.Compliance_Title + " Section";

        //        }
        //        else
        //            TempData["Error"] = "Not able to create successfully";
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
        //        TempData["Error"] = "Not able to Create " + model.Compliance.Compliance_Title + " Rule.";
        //    }
        //        //return View("ListofCompliance");
        //        return RedirectToAction("ListofCompliance");
        //}

        [HttpGet]
        public ActionResult CreateRules(int Parentid)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Compliance = new ComplianceXref();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            model.Compliance.Compliance_Parent_ID = Parentid;
            string xmldata = client.GetActs(Parentid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Compliance.Effective_Start_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_Start_Date"]);
            //model.Compliance.Compliance_Type_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Type_ID"]);
            model.Compliance.Country_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Country_ID"]);
            model.Compliance.State_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["State_ID"]);
            model.Compliance.City_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["City_ID"]);
            model.Compliance.Effective_End_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_End_Date"]);
           // Session["Rulemodel"] = model;
            return PartialView("_AddRules", model);
        }

        [HttpPost]
        public ActionResult CreateRules(ComplianceViewModel model)
        {
            if (ModelState.IsValid)
            {
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                model.Compliance.User_ID = Convert.ToInt32(Session["UserId"]);
                int id = client.insertRules(model.Compliance);
                if (id > 0)
                {
                    TempData["Message"] = "Successfuly Created " + model.Compliance.Compliance_Title + " Rule";
                    return RedirectToAction("ListofCompliance");
                }
                else
                    TempData["Error"] = "Not able to Create " + model.Compliance.Compliance_Title + " Rule.";
            }
            else
            {
                TempData["Error"] = "Not able to Create " + model.Compliance.Compliance_Title + " Rule.";
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            }
         //   return PartialView("_AddRules", model);
           return RedirectToAction("ListofCompliance");

        }
        public ActionResult ListofCompliance()
        {
            ViewBag.Message = TempData["Message"];
            ListofComplianceViewModel model = new ListofComplianceViewModel();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            //string xmlcompliance = client.GetComplaince(0);
            //DataSet dscompliance = new DataSet();
            //dscompliance.ReadXml(new StringReader (xmlcompliance));
            //DataView dv = new DataView(dscompliance.Tables[0]);
            //dv.RowFilter = "level=1";
            string xmldata = client.GetActs(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                model.Actslist = bindCompliancelist(ds.Tables[0], model.Actslist);
            
            //xmldata = client.GetSections(0);
            //ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
                //if (ds.Tables.Count > 0)
                //{
                  //  model.Sectionlist = bindCompliancelist(ds.Tables[0], model.Sectionlist);

                    xmldata = client.GetRules(0);
                    ds = new DataSet();
                    ds.ReadXml(new StringReader(xmldata));
                    if (ds.Tables.Count > 0)
                    {
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
                    }
                //}
            }
            return View("_ListofCompliance", model);
        }
        private List<ComplianceXref> bindCompliancelist(DataTable dt, List<ComplianceXref> model)
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
                    Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),                    
                    Version = Convert.ToInt32(row["Version"]),
                    Compliance_Type_ID=Convert.ToInt32(row["Compliance_Type_ID"])
                });
            }
            return model;
        }
        
        //public JsonResult getallocatedrules(string secid, string branchid)
        //{
        //    ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
        //    int sectionID = Convert.ToInt32(secid);
        //    int BranchID = Convert.ToInt32(branchid);
        //    string xmldata = client.getRuleforBranch(BranchID,vendorid);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(new StringReader(xmldata));
        //    List<SelectListItem> Rulelist = new List<SelectListItem>();
        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (System.Data.DataRow row in ds.Tables[0].Rows)
        //        {
        //            Rulelist.Add(new SelectListItem
        //            {
        //                Text = Convert.ToString(row["Compliance_Title"]),
        //                Value = Convert.ToString(row["Compliance_Xref_ID"])
        //            });
        //        }
        //    }
        //    return Json(Rulelist, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult AssignRules(string Branchid,string Vendorid, string Branchname)
        {
            AllocateActandRuleViewModel model = new AllocateActandRuleViewModel();
            model.ActType = new List<SelectListItem>();
            model.ActType.Add(new SelectListItem { Text = "Union and State Level", Value = "0" });
            model.ActType.Add(new SelectListItem { Text = "Union Level", Value = "1" });
            model.ActType.Add(new SelectListItem { Text = "State Level", Value = "2" });


            model.AuditType = new List<SelectListItem>();
            model.AuditType.Add(new SelectListItem { Text = "Labour Compliance", Value = "1" });

            model.BranchId = Convert.ToInt32(Branchid);
            model.Name = Branchname;
            Session["Branch_Id"] = Branchid;
            Session["BranchName"] = Branchname;
            Session["VendorId"] = Vendorid;
            return View("_AssignRules",model);
        }
        public JsonResult GetJsTree3Data(string audittypeid,string  acttype)
        {
            int auditid = Convert.ToInt32(audittypeid);
            
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata=client.getorglocation(Convert.ToInt32(Session["Branch_Id"]));
            DataSet loc = new DataSet();
            loc.ReadXml(new StringReader(xmldata));
            int countryid = Convert.ToInt32(loc.Tables[0].Rows[0]["Country_ID"]);
            int stateid=0;
            int cityid=0;
            int flag = Convert.ToInt32(acttype);
            if (acttype== "1")
            {
                countryid = Convert.ToInt32(loc.Tables[0].Rows[0]["Country_ID"]);
                stateid = 0;
                cityid = 0;                
            }
            else if(acttype == "2")
            {
                stateid = Convert.ToInt32(loc.Tables[0].Rows[0]["State_ID"]);
                cityid = 0;
            }
            else
            {
                cityid= Convert.ToInt32(loc.Tables[0].Rows[0]["City_ID"]);
            }
           
            var root = new treenode() //Create our root node and ensure it is opened
            {
                id = Guid.NewGuid().ToString(),
                text = "Select All",
                state = new Models.State(true, false, false)
            };
            int orgid = Convert.ToInt32(Session["Branch_Id"]);
            int vendorid = Convert.ToInt32(Session["VendorId"]);
            //Create a basic structure of nodes
            var children = new List<treenode>();
            ComplianceXrefService.ComplianceXrefServiceClient xrefclient = new ComplianceXrefService.ComplianceXrefServiceClient();

            xmldata = xrefclient.GetcomplianceonType(auditid, countryid, stateid, cityid,flag);
            DataSet dscomp = new DataSet();
            dscomp.ReadXml(new StringReader(xmldata));
            DataTable ds = new DataTable();
            DataTable dsection = new DataTable();
            DataTable dsrules = new DataTable();

            if (dscomp.Tables.Count > 0)
            {
                DataView dv = new DataView(dscomp.Tables[0]);
                dv.RowFilter = "level=1";
                ds = dv.ToTable();

                dv.Table = dscomp.Tables[0];
                dv.RowFilter = "level=3";
                 dsrules = dv.ToTable();

                dv.Table = dscomp.Tables[0];
                dv.RowFilter = "level=2";
                dsection = dv.ToTable();

            }
            else
            TempData["Error"] = "No Rules for the state level";
            xmldata = xrefclient.getRuleforBranch(orgid,vendorid);
            DataSet dsassigenedrule = new DataSet();
            dsassigenedrule.ReadXml(new StringReader(xmldata));

            treenode act = new treenode();
            if (ds.Rows.Count > 0)
            {                 
                foreach (System.Data.DataRow row in ds.Rows)
                {
                    bool isrule = false;
                    act = new treenode { id = row["Compliance_Xref_ID"].ToString(), text = row["Compliance_Title"].ToString(), icon = "fa fa-legal", state = new Models.State(true, false, false), categorytype = "Act", children = new List<treenode>() };
                    if (dsection.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow section in dsection.Rows)
                        {
                            if (row["Compliance_Xref_ID"].ToString() == section["Compliance_Parent_ID"].ToString())
                            {
                                isrule = false;
                                var sec = new treenode { id = section["Compliance_Xref_ID"].ToString(), text = section["Compliance_Title"].ToString(), icon = "fa fa-book", state = new Models.State(false, false, false), categorytype = "Section", children = new List<treenode>() };

                                if (dsrules.Rows.Count > 0)
                                {
                                    foreach(System.Data.DataRow rules in dsrules.Rows)
                                    {
                                        if (section["Compliance_Xref_ID"].ToString() == rules["Compliance_Parent_ID"].ToString())
                                        {
                                            isrule = true;
                                            var rule=new treenode { id = rules["Compliance_Xref_ID"].ToString(), text = rules["Compliance_Title"].ToString(), icon = "fa fa-leanpub", state = new Models.State(false, false, false), categorytype = "Rule", children = new List<treenode>() };
                                            if (dsassigenedrule.Tables.Count > 0)
                                            {
                                                foreach (System.Data.DataRow assignrules in dsassigenedrule.Tables[0].Rows)
                                                {
                                                    if (assignrules["Compliance_Xref_ID"].ToString() == rules["Compliance_Xref_ID"].ToString())
                                                    {
                                                        rule.state = new Models.State(false, false, true);
                                                        break;
                                                    }
                                                }
                                            }
                                            if (isrule == true)
                                            {
                                                sec.children.Add(rule);
                                            }
                                        }
                                        
                                    }
                                }
                                if (isrule == true)
                                {
                                    act.children.Add(sec);
                                }
                            }
                        }
                    }
                    if (isrule == true)
                    {
                        children.Add(act);
                    }
                }
            }
            // Add the sturcture to the root nodes children property
            root.children = children;

            // Return the object as JSON
            return Json(root, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AssignRules(string selectedItems)
        {
            if (selectedItems != string.Empty)
            {
                List<treenode> ruleslist = (new JavaScriptSerializer()).Deserialize<List<treenode>>(selectedItems);
                int[] rules = new int[ruleslist.Count];
                int i = 0;
                int userid = Convert.ToInt32(Session["UserId"]);
                int orgid = Convert.ToInt32(Session["Branch_Id"]);
                int vendorid = Convert.ToInt32(Session["VendorId"]);
                foreach (var item in ruleslist)
                {
                    rules[i++] = Convert.ToInt32(item.id);
                }
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                client.DeleteRuleforBranch(orgid);
                client.inseretActandRuleforBranch(orgid, rules, userid,vendorid);
                TempData["Message"] = "Successfully assigned " + ruleslist.Count + "Rules to" + Session["BranchName"];
                return RedirectToAction("AssignRules", new { Branchid = Convert.ToString(Session["Branch_Id"]), Vendorid= Convert.ToInt32(Session["VendorId"]), Branchname = Convert.ToString(Session["BranchName"]) });
            }
            else
               TempData["Error"]="Please select atleast one rule";
            return RedirectToAction("AssignRules", new { Branchid = Convert.ToString(Session["Branch_Id"]), Vendorid= Convert.ToInt32(Session["VendorId"]), Branchname = Convert.ToString(Session["BranchName"]) });
        }
        [HttpGet]
        public ActionResult SMEdashboard()
        {

            AllocateActandRuleViewModel model = new AllocateActandRuleViewModel();
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            int groupid= Convert.ToInt32(Session["GroupCompanyId"]);
            string xmldata = client.getCompanyListDropDown(groupid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Companylist = new List<SelectListItem>() { new SelectListItem { Text = "--Select Company--", Value = "0" } };
            if (ds.Tables.Count > 0)
            {
                model.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["Org_Hier_ID"]);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.Companylist.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
        
            model.Branch = new List<Organization>();
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();

            string strxmlCompanies = organizationservice.GeSpecifictBranchList(model.CompanyId);

            DataSet dsSpecificBranchList = new DataSet();
            dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
            if (dsSpecificBranchList.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsSpecificBranchList.Tables[0].Rows)
                {
                    model.Branch.Add(new Organization { Company_Name = Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Org_Hier_ID"]) });
                }
            }
            else { ViewBag.Message = ConfigurationManager.AppSettings["No_Branches"]; }                                             
           
            //model.Vendor = new List<Organization>();        
            //xmldata = organizationservice.GetVendors(model.CompanyId);
            //ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
            //if (ds.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //    {
            //        model.Vendor.Add(new Organization { Company_Name = Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Org_Hier_ID"]) });
            //    }
            //}
            return View("_SMEDashboard", model);
        }

        [HttpPost]
        public ActionResult SMEdashboard(AllocateActandRuleViewModel model)
        {
            model.Companylist =new  List<SelectListItem>();
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            int groupid = Convert.ToInt32(Session["GroupCompanyId"]);
            string xmldata = client.getCompanyListDropDown(groupid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Companylist = new List<SelectListItem>() { new SelectListItem { Text = "--Select Company--", Value = "0" } };
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.Companylist.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            model.Branch = new List<Organization>();    
            xmldata = client.GeSpecifictBranchList(model.CompanyId);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.Branch.Add(new Organization { Company_Name= Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Org_Hier_ID"] ) });
                    //model.BranchList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            
           
            return View("_SMEDashboard", model);
        }

        public ActionResult Listofvendors(int branchid,string branchname)
        {
            AllocateActandRuleViewModel model = new AllocateActandRuleViewModel();
            model.BranchId = branchid;
            model.Branch = new List<Organization>();
            OrgService.OrganizationServiceClient service = new OrgService.OrganizationServiceClient();
            string xmldata = service.getBranch(model.BranchId);
            DataSet data = new DataSet();
            data.ReadXml(new StringReader(xmldata));
            model.Branch.Add(new Organization
            {
                Company_Id = Convert.ToInt32(data.Tables[0].Rows[0]["Org_Hier_ID"]),
                Company_Name = Convert.ToString(data.Tables[0].Rows[0]["Company_Name"]),
                Parent_Company_Id = Convert.ToInt32(data.Tables[0].Rows[0]["Parent_Company_ID"]),
                logo = Convert.ToString(data.Tables[0].Rows[0]["logo"])
            });
            model.Vendor = new List<Organization>();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
            xmldata = vendorServiceClient.GetAssignedVendorsforBranch(branchid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.Vendor.Add(new Organization { Company_Name = Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Vendor_ID"]),logo=Convert.ToString(row["logo"]) });
                }
            }
            else
                TempData["Message"] = "No Vendors assigned for the selected branch.";
            return View("_ListofVendors",model);
        }

        [HttpGet]
        public ActionResult UpdateAct(int id)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Countrylist = new List<SelectListItem>();
            model.Countrylist.Add(new SelectListItem() { Text = "--Select Country--", Value = "0" });
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationservice.GetCountryList();
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            if (dsCountries.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    model.Countrylist.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }

            model.Statelist = new List<SelectListItem>();
            model.Statelist.Add(new SelectListItem() { Text = "--Select State--", Value = "0" });

            model.Citylist = new List<SelectListItem>();
            model.Citylist.Add(new SelectListItem() { Text = "--Select City--", Value = "0" });

            model.ActType = new List<SelectListItem>();
            model.ActType.Add(new SelectListItem { Text = "--Select Act Type--", Value = "0" });
            model.ActType.Add(new SelectListItem { Text = "Union Level", Value = "1" });
            model.ActType.Add(new SelectListItem { Text = "State Level", Value = "2" });
            //model.ActType.Add(new SelectListItem { Text = "City Level", Value = "3" });

            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            model.ComplianceType = new List<SelectListItem>();
            string xmldata = client.GetComplainceType();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.ComplianceType.Add(new SelectListItem { Text = "-- Select Compliance --", Value = "0" });
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.ComplianceType.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
                }
            }
            model.Compliance = new ComplianceXref();

            
            xmldata=client.GetActs(id);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Compliance.Compliance_Xref_ID = id;
            model.Compliance.Compliance_Type_ID=Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Type_ID"]);
            model.Compliance.Compliance_Parent_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Parent_ID"]);
            model.Compliance.Compliance_Title = Convert.ToString(ds.Tables[0].Rows[0]["Compliance_Title"]);
            model.Compliance.compl_def_consequence = Convert.ToString(ds.Tables[0].Rows[0]["compl_def_consequence"]);
            model.Compliance.City_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["City_ID"]);
            model.Compliance.Comp_Description = Convert.ToString(ds.Tables[0].Rows[0]["Comp_Description"]);
            model.Compliance.Effective_End_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_End_Date"]);
            model.Compliance.Effective_Start_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_Start_Date"]);
            model.Compliance.State_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["State_ID"]);
            model.Compliance.Country_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Country_ID"]);
            if (model.Compliance.State_ID == 0)
            {
                model.ActTypeID = 1;
            }
            else
                model.ActTypeID = 2;
            return PartialView("~/Views/ComplianceManagement/UpdateAct.cshtml", model); 
        }

        public ActionResult Updatesection(int id)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.ComplianceType = new List<SelectListItem>();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata = client.GetComplainceType();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.ComplianceType.Add(new SelectListItem { Text = "-- Select Compliance --", Value = "0" });
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.ComplianceType.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
                }
            }
            model.Compliance = new ComplianceXref();

           
            xmldata = client.GetSpecificComplaince(id);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Compliance.Compliance_Xref_ID = id;           
            model.Compliance.Compliance_Parent_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Parent_ID"]);
            model.Compliance.Compliance_Title = Convert.ToString(ds.Tables[0].Rows[0]["Compliance_Title"]);            
            model.Compliance.Comp_Description = Convert.ToString(ds.Tables[0].Rows[0]["Comp_Description"]);
            model.Compliance.Effective_End_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_End_Date"]);
            model.Compliance.Effective_Start_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_Start_Date"]);

            return PartialView("~/Views/ComplianceManagement/_AddSection.cshtml", model);
        }

        public ActionResult UpdateRule(int id)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.ComplianceType = new List<SelectListItem>();
            model.ComplianceType.Add(new SelectListItem { Text = "Labour Compliance", Value = "1" });
            model.Compliance = new ComplianceXref();

            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata = client.GetSpecificComplaince(id);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Compliance.Compliance_Xref_ID = id;
            model.Compliance.Compliance_Parent_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Parent_ID"]);
            model.Compliance.Compliance_Title = Convert.ToString(ds.Tables[0].Rows[0]["Compliance_Title"]);
            model.Compliance.compl_def_consequence = Convert.ToString(ds.Tables[0].Rows[0]["compl_def_consequence"]);
            model.Compliance.Comp_Description = Convert.ToString(ds.Tables[0].Rows[0]["Comp_Description"]);
            model.Compliance.Effective_End_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_End_Date"]);
            model.Compliance.Effective_Start_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_Start_Date"]);
            model.Compliance.Risk_Category = Convert.ToString(ds.Tables[0].Rows[0]["Risk_Category"]);
            model.Compliance.compl_def_consequence = Convert.ToString(ds.Tables[0].Rows[0]["Risk_Description"]);


            return PartialView("~/Views/ComplianceManagement/_AddRules.cshtml", model);
        }
    }
}