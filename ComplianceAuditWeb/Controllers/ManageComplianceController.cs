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
            //model.ActType.Add(new SelectListItem { Text = "City Level", Value = "3" });

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

        public ActionResult ListofCompliance()
        {
            ViewBag.Message = TempData["Message"];
            ListofComplianceViewModel model = new ListofComplianceViewModel();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata = client.GetActs(0);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                model.Actslist = bindCompliancelist(ds.Tables[0], model.Actslist);

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
                }
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
                    return RedirectToAction("ListofCompliance");
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
            return RedirectToAction("ListofCompliance");
        }

        [HttpGet]
        public ActionResult ComplianceActMapping()
        {
            complianceActmappingViewModel model = new complianceActmappingViewModel();
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata = client.GetComplainceType();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.ComplianceType = new List<SelectListItem>();
            if (ds.Tables.Count > 0)
            {
                model.compliancetypeid = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Type_ID"]);

                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.ComplianceType.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
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
            xmldata = client.GetXrefComplainceTypemapping(compliancetypeid);
            dsact.ReadXml(new StringReader(xmldata));
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
        public ActionResult ComplianceActMapping(string selectedItems)
        {
            if (selectedItems != string.Empty)
            {
                List<treenode> ruleslist = (new JavaScriptSerializer()).Deserialize<List<treenode>>(selectedItems);
                int[] rules = new int[ruleslist.Count];
                int i = 0;
                int compliancetyid = Convert.ToInt32(Session["compliancetypeid"]);
                foreach (var item in ruleslist)
                {
                    rules[i++] = Convert.ToInt32(item.id);
                }
                ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
                client.deletexreftypemapping(compliancetyid);
                client.insertxreftypemapping(rules, compliancetyid);
            }
            return RedirectToAction("ComplianceActMapping");
        }
    }
}