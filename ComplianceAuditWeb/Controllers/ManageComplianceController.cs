using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using ComplianceAuditWeb.Models;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageComplianceController : Controller
    {
        // GET: ManageCompliance
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
            model.ActType.Add(new SelectListItem { Text = "City Level", Value = "3" });

            model.Compliance = new ComplianceXref();
            model.Compliance.Compliance_Xref_ID = 0;
            Session["Actmodel"] = model;
            return View("_AddActs", model);
        }

        [HttpPost]
        public ActionResult CreateActs(ComplianceViewModel model)
        {
            if (ModelState.IsValid)
            {
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                model.Compliance.User_ID = Convert.ToInt32(Session["UserId"]);
                model.Compliance.Effective_End_Date = DateTime.MaxValue.Date;
                model.Compliance.Compliance_Parent_ID = client.insertActs(model.Compliance);
                if (model.Compliance.Compliance_Parent_ID > 0)
                {
                    TempData["Message"] = "Successfuly Created " + model.Compliance.Compliance_Title + " Act.";
                    return RedirectToAction("ListofCompliance",new { countryid =model.Compliance.Country_ID});
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

        public ActionResult ListofCompliance()
        {
            ViewBag.Message = TempData["Message"];
            ListofComplianceViewModel model = new ListofComplianceViewModel();
            int flag=0;
            OrgService.OrganizationServiceClient organizationService = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationService.GetCountryList();
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            model.CountryList = new List<SelectListItem>();
            if (dsCountries.Tables.Count > 0)
            {
                model.countryid = Convert.ToInt32(dsCountries.Tables[0].Rows[0]["Country_ID"]);
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    model.CountryList.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }
            string strXMLIndustryType = organizationService.GetIndustryType();
            DataSet dsIndustryType = new DataSet();
            dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
            model.IndustryTypeList = new List<SelectListItem>();
            model.IndustryTypeList.Add(new SelectListItem { Text = "-- Select Industry Type --", Value = "0" });
            if (dsIndustryType.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                {
                    model.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                }
            }

            model.ComplianceTypeList = new List<SelectListItem>();
            model.ComplianceTypeList.Add(new SelectListItem { Text = "-- Select ComplianceType List --", Value = "0" });
            var  countryid= Request.QueryString["countryid"];
            if (countryid != null)
            {
                model.countryid = Convert.ToInt32(countryid);
            }
            else
                model.countryid = 1;
            var audit = Request.QueryString["compliancetypeid"];
            if (audit != null)
            {
                model.compliancetypeid = Convert.ToInt32(audit);
            }
            else
                model.compliancetypeid = 0;

            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();

            string xmldata = client.GetcomplianceonType(model.compliancetypeid, model.countryid, 0, 0, flag);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "level=1";
            dv.Sort = "Last_Updated_Date desc";
            DataTable dt = dv.ToTable();

            dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "level=2";
            DataTable dtrules = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                model.Actslist = bindCompliancelist(dt);
                if (dtrules.Rows.Count > 0)
                {
                    model.Rulelist = bindCompliancelist(dtrules);          
                }
            }
            return View("_ListofCompliance", model);
        }

        private List<ComplianceXref> bindCompliancelist(DataTable dt)
        {
            List<ComplianceXref> model = new List<ComplianceXref>();
            foreach (System.Data.DataRow row in dt.Rows)
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
                    Is_Header = Convert.ToBoolean(Convert.ToInt32(row["Is_Header"])),
                    level = Convert.ToInt32(row["level"]),
                    State_ID = Convert.ToInt32(row["State_ID"]),
                    User_ID = Convert.ToInt32(row["User_ID"]),
                    Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Is_Best_Practice = Convert.ToBoolean(Convert.ToInt32(row["Is_Best_Practice"])),
                    Risk_Category = Convert.ToString(row["Risk_Category"]),
                    Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                    Periodicity = Convert.ToString(row["Periodicity"]),
                    Risk_Description = Convert.ToString(row["Risk_Description"]),
                    Version = Convert.ToInt32(row["Version"])
                });
            }
            return model;
        }

        [HttpGet]
        public ActionResult Createlineitems(int Parentid, string type)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Compliance = new ComplianceXref();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            model.Compliance.Compliance_Parent_ID = Parentid;
            model.Compliance.Comp_Category = type;
            string xmldata = client.GetActs(Parentid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Compliance.Effective_Start_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_Start_Date"]);
            model.Compliance.Country_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Country_ID"]);
            model.Compliance.State_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["State_ID"]);
            model.Compliance.City_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["City_ID"]);
            model.Compliance.Effective_End_Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["Effective_End_Date"]);
            // Session["Rulemodel"] = model;
            return PartialView("_AddLineitems", model);
        }

        [HttpPost]
        public ActionResult Createlineitems(ComplianceViewModel model)
        {
            if (ModelState.IsValid)
            {
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                model.Compliance.User_ID = Convert.ToInt32(Session["UserId"]);
                int id = client.insertRules(model.Compliance);                
                if (id > 0)
                {
                    TempData["Message"] = "Successfuly Created " + model.Compliance.Compliance_Title + model.Compliance.Comp_Category;
                    return RedirectToAction("ListofCompliance", new { countryid = model.Compliance.Country_ID });
                }
                else
                    TempData["Error"] = "Not able to Create " + model.Compliance.Compliance_Title + model.Compliance.Comp_Category + ".";
            }
            else
            {
                TempData["Error"] = "Not able to Create " + model.Compliance.Compliance_Title + " Rule.";
                ModelState.AddModelError("", ConfigurationManager.AppSettings["Requried"]);
            }
            //   return PartialView("_AddRules", model);
            return RedirectToAction("ListofCompliance", new { countryid = model.Compliance.Country_ID });
        }

        [HttpGet]
        public ActionResult ComplianceActMapping()
        {
            complianceActmappingViewModel model = new complianceActmappingViewModel();
            OrgService.OrganizationServiceClient organizationService = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationService.GetCountryList();
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            model.Country = new List<SelectListItem>();
           // model.Country.Add(new SelectListItem { Text = "-- Select Country --", Value = "" });
            if (dsCountries.Tables.Count > 0)
            {
                model.countryid = Convert.ToInt32(dsCountries.Tables[0].Rows[0]["Country_ID"]);
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    model.Country.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }
          
            //ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            //string xmldata = client.GetComplainceType(0);
            //DataSet ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
            model.ComplianceType = new List<SelectListItem>();
            //if (ds.Tables.Count > 0)
            //{
            //    model.compliancetypeid = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Type_ID"]);

            //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //    {
            //        model.ComplianceType.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
            //    }
            //}  

            string strXMLIndustryType = organizationService.GetIndustryType();
            DataSet dsIndustryType = new DataSet();
            dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
            model.IndustryType = new List<SelectListItem>();
           // model.IndustryType.Add(new SelectListItem { Text = "-- Industry Type --", Value = "" });
            if (dsIndustryType.Tables.Count > 0)
            {
                model.industryid = Convert.ToInt32(dsIndustryType.Tables[0].Rows[0]["Industry_Type_ID"]);
                foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                {
                    model.IndustryType.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                }
            }

            string strXMLCompliances = organizationService.GetComplianceType(model.countryid, model.industryid);
            DataSet dsCompliances = new DataSet();
            dsCompliances.ReadXml(new StringReader(strXMLCompliances));
            if (dsCompliances.Tables.Count > 0)
            {
                model.compliancetypeid = Convert.ToInt32(dsCompliances.Tables[0].Rows[0]["Compliance_Type_ID"]);
                foreach (System.Data.DataRow row in dsCompliances.Tables[0].Rows)
                {
                    model.ComplianceType.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
                }
            }
            return View("_ComplianceActMapping", model);
        }

        public JsonResult getcompliancetypeact(int compliancetypeid)
        {
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            Session["compliancetypeid"] = compliancetypeid;

            var root = new treenode() //Create our root node and ensure it is opened
            {
                id = Guid.NewGuid().ToString(),
                text = "Select All",
                state = new Models.State(true, false, false)
            };
            var children = new List<treenode>();
            string xmldata = client.GetActs(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            DataSet dsact = new DataSet();
            xmldata = client.GetXrefComplainceTypemapping(compliancetypeid,0);
            dsact.ReadXml(new StringReader(xmldata));
            Session["AssignedActs"] = dsact;
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    var list = new treenode() { id= Convert.ToString(row["Compliance_Xref_ID"]), text = Convert.ToString(row["Compliance_Title"]),children=null,categorytype="Act",state=new Models.State(false,false,false) };
                   
                    if (dsact.Tables.Count > 0)
                    {
                        foreach (System.Data.DataRow item in dsact.Tables[0].Rows)
                        {
                            if (Convert.ToString(item["Compliance_Xref_ID"]) == list.id)
                            {
                                list.state = new Models.State(false, false, true);
                            }
                        }
                      
                    }
                    children.Add(list);
                }
            }
            root.children = children;
            return Json(root, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ComplianceActMapping(string selectedItems,int compliancetypeid)
        {
            if (selectedItems != string.Empty)
            {
                List<treenode> ruleslist = (new JavaScriptSerializer()).Deserialize<List<treenode>>(selectedItems);
                int[] rules = new int[ruleslist.Count];
                
                int i = 0;
                DataSet data = (DataSet)Session["AssignedActs"];
                int[] Deleterules = new int[data.Tables[0].Rows.Count];
                //int compliancetyid = Convert.ToInt32(Session["compliancetypeid"]);
                foreach (var item in ruleslist)
                {
                    rules[i++] = Convert.ToInt32(item.id);
                }
                i = 0;
                foreach(System.Data.DataRow row in data.Tables[0].Rows)
                {
                    foreach (var item in rules)
                    {
                        if (Convert.ToInt32(row["Compliance_Xref_ID"]) == item)
                        {
                            break;
                        }
                        else if (Convert.ToInt32(row["Compliance_Xref_ID"])!=item)
                        {
                            Deleterules[i++] = Convert.ToInt32(row["Compliance_Xref_ID"]);
                        }
                    }
                }
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                client.deletexreftypemapping(compliancetypeid,Deleterules);
                client.insertxreftypemapping(rules, compliancetypeid);
            }
            return RedirectToAction("ComplianceActMapping");
        }

        [HttpGet]
        public ActionResult selectbranch()
        {
            AuditorpageViewModel model = new AuditorpageViewModel();

            int groupid = Convert.ToInt32(Session["GroupCompanyId"]);
            if (groupid != 0)
            {
                
                model.companyList = new List<SelectListItem>();
                model.BranchList = new List<SelectListItem>();
                model.VendorList = new List<Organization>();
                OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
                string xmldata = client.getCompanyListDropDown(groupid);
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                model.companyList = new List<SelectListItem>();

                if (ds.Tables.Count > 0)
                {
                    model.companyid = Convert.ToInt32(ds.Tables[0].Rows[0]["Org_Hier_ID"]);
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.companyList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                    }
                }

                xmldata = client.GeSpecifictBranchList(model.companyid);
                ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    model.branchid = Convert.ToInt32(ds.Tables[0].Rows[0]["Org_Hier_ID"]);
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.BranchList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                    }
                }

                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                xmldata = vendorServiceClient.GetAssignedVendorsforBranch(model.branchid);
                ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.VendorList.Add(new Organization { Company_Name = Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Vendor_ID"]), logo = Convert.ToString(row["logo"]) });
                    }
                }
                else
                    TempData["Message"] = "No Vendors assigned for the selected branch.";
            }
            else
            //ViewBag.Message = ConfigurationManager.AppSettings["SelectGroupCompany"];
            ModelState.AddModelError("",ConfigurationManager.AppSettings["SelectGroupCompany"]);

            return View("_SelectBranch", model);
        }

        [HttpPost]
        public ActionResult selectbranch(AuditorpageViewModel model)
        {
            model.companyList = new List<SelectListItem>();
            model.BranchList = new List<SelectListItem>();
            model.VendorList = new List<Organization>();

            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            int groupid = Convert.ToInt32(Session["GroupCompanyId"]);
            string xmldata = client.getCompanyListDropDown(groupid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.companyList = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.companyList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }
            model.BranchList = new List<SelectListItem>();
            xmldata = client.GeSpecifictBranchList(model.companyid);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.BranchList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
            }

            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
            xmldata = vendorServiceClient.GetAssignedVendorsforBranch(model.branchid);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.VendorList.Add(new Organization { Company_Name = Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Vendor_ID"]), logo = Convert.ToString(row["logo"]) });
                }
            }
            else
                TempData["Message"] = "No Vendors assigned for the selected branch.";

            return View("_SelectBranch", model);
        }

        [HttpGet]
        public ActionResult AssignRules(string branchid, string vendorid, string vendorname)
        {
            AllocateActandRuleViewModel model = new AllocateActandRuleViewModel();
            model.ActType = new List<SelectListItem>();
            model.ActType.Add(new SelectListItem { Text = "Union and State Level", Value = "0" });
            model.ActType.Add(new SelectListItem { Text = "Union Level", Value = "1" });
            model.ActType.Add(new SelectListItem { Text = "State Level", Value = "2" });

            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata = client.GetAssignedComplianceTypes(Convert.ToInt32(vendorid));
            DataSet ds = new DataSet();
            DataSet dscompliancetype = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            int[] compliancetypeid = new int[ds.Tables[0].Rows.Count];
            int i = 0;
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    compliancetypeid[i++] = Convert.ToInt32(row["Compliance_Type_ID"]);
                }
            }
            model.AuditType = new List<SelectListItem>();
            ComplianceXrefService.ComplianceXrefServiceClient serviceClient = new ComplianceXrefService.ComplianceXrefServiceClient();
            for (i = 0; i < compliancetypeid.Length; i++)
            {
                xmldata = serviceClient.GetComplainceType(compliancetypeid[i]);
                ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    model.AuditTypeID = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Type_ID"]);
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.AuditType.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
                    }
                }
            }           

            model.BranchId = Convert.ToInt32(branchid);
            model.Name = vendorname;
            Session["Branch_Id"] = branchid;
            Session["BranchName"] = vendorname;
            Session["VendorId"] = vendorid;
            return View("_AssignRules", model);
        }

        public JsonResult GetJsTree3Data(string audittypeid, string acttype)
        {
            int auditid = Convert.ToInt32(audittypeid);

            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata = client.getorglocation(Convert.ToInt32(Session["Branch_Id"]));
            DataSet loc = new DataSet();
            loc.ReadXml(new StringReader(xmldata));
            int countryid = Convert.ToInt32(loc.Tables[0].Rows[0]["Country_ID"]);
            int stateid = 0;
            int cityid = 0;
            int flag = Convert.ToInt32(acttype);
            if (acttype == "1")
            {
                countryid = Convert.ToInt32(loc.Tables[0].Rows[0]["Country_ID"]);
                stateid = 0;
                cityid = 0;
            }
            else if (acttype == "2")
            {
                stateid = Convert.ToInt32(loc.Tables[0].Rows[0]["State_ID"]);
                cityid = 0;
            }
            else
            {
                cityid = Convert.ToInt32(loc.Tables[0].Rows[0]["City_ID"]);
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

            xmldata = xrefclient.GetcomplianceonType(auditid, countryid, stateid, cityid, flag);
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
                dv.RowFilter = "level=2";
                dsrules = dv.ToTable();              
            }
            else
                TempData["Error"] = "No Rules for the state level";
            xmldata = xrefclient.getRuleforBranch(orgid, vendorid);
            DataSet dsassigenedrule = new DataSet();
            dsassigenedrule.ReadXml(new StringReader(xmldata));

            treenode act = new treenode();
            if (ds.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Rows)
                {
                    //bool isrule = false;
                    act = new treenode { id = row["Compliance_Xref_ID"].ToString(), text = row["Compliance_Title"].ToString(), icon = "fa fa-legal", state = new Models.State(true, false, false), categorytype = "Act", children = new List<treenode>() };
                   
                                if (dsrules.Rows.Count > 0)
                                {
                                    foreach (System.Data.DataRow rules in dsrules.Rows)
                                    {
                                        if (row["Compliance_Xref_ID"].ToString() == rules["Compliance_Parent_ID"].ToString())
                                        {
                                           // isrule = true;
                                            var rule = new treenode { id = rules["Compliance_Xref_ID"].ToString(), text = rules["Compliance_Title"].ToString(), icon = "fa fa-leanpub", state = new Models.State(false, false, false), categorytype = "Rule", children = new List<treenode>() };
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
                                act.children.Add(rule);
                            }

                                    }
                                }

                    children.Add(act);

                }                        
                    }                                                                                           
            // Add the sturcture to the root nodes children property
            root.children = children;

            // Return the object as JSON
            return Json(root, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AssignRules(string selectedItems,int AuditTypeID)
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
                client.DeleteRuleforBranch(orgid,vendorid);
                client.inseretActandRuleforBranch(orgid, rules, userid, vendorid,AuditTypeID);
                TempData["Message"] = "Successfully assigned " + ruleslist.Count + "Rules to" + Session["BranchName"];
                return RedirectToAction("AssignRules", new { Branchid = Convert.ToString(Session["Branch_Id"]), Vendorid = Convert.ToInt32(Session["VendorId"]), Branchname = Convert.ToString(Session["BranchName"]) });
            }
            else
                TempData["Error"] = "Please select atleast one rule";
            return RedirectToAction("AssignRules", new { Branchid = Convert.ToString(Session["Branch_Id"]), Vendorid = Convert.ToInt32(Session["VendorId"]), Branchname = Convert.ToString(Session["BranchName"]) });
        }

        public ActionResult UpdateAct(int id)
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Compliance = new ComplianceXref();
            model.ActType = new List<SelectListItem>();
            model.ActType.Add(new SelectListItem { Text = "--Select Act Type--", Value = "0" });
            model.ActType.Add(new SelectListItem { Text = "Union Level", Value = "1" });
            model.ActType.Add(new SelectListItem { Text = "State Level", Value = "2" });
            model.ActType.Add(new SelectListItem { Text = "City Level", Value = "3" });

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

            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
        
            string xmldata = client.GetSpecificComplaince(id);
             DataSet  ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Compliance.Compliance_Xref_ID = id;
            model.Compliance.Compliance_Parent_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Parent_ID"]);
            model.Compliance.Compliance_Title = Convert.ToString(ds.Tables[0].Rows[0]["Compliance_Title"]);
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
            return View("_UpdateAct",model);
        }

        public ActionResult UpdateLineitems(int id)
        {
            ComplianceViewModel model = new ComplianceViewModel();
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
            model.Compliance.Periodicity = Convert.ToString(ds.Tables[0].Rows[0]["Periodicity"]);
             model.Compliance.Risk_Description = Convert.ToString(ds.Tables[0].Rows[0]["Risk_Description"]);
            //model.Compliance.compl_def_consequence = Convert.ToString(ds.Tables[0].Rows[0]["Risk_Description"]);
            return View("_AddLineitems",model);
        }

    }
}