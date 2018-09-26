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
    public class ManageAuditController : Controller
    {
        // GET: ManageAudit
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult selectbranch()
        {
            AuditorpageViewModel model = new AuditorpageViewModel();
            model.companyList = new List<SelectListItem>();
            model.BranchList = new List<SelectListItem>();
            model.VendorList = new List<Organization>();

            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            int groupid = Convert.ToInt32(Session["GroupCompanyId"]);
            if (groupid != 0)
            {
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
                model.BranchList = new List<SelectListItem>();
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
                ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);

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

        public ActionResult selectauditfrequency(int branchid, int vendorid, string vendorname)
        {
            Compliancetype_view_model model = new Compliancetype_view_model();
            model.compliance_Types = new List<compliance_type>();
            model.branchid = branchid;
            model.vendorid = vendorid;
            model.vendorname = vendorname;
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata = client.GetAssignedComplianceTypes(vendorid);
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

            ComplianceXrefService.ComplianceXrefServiceClient serviceClient = new ComplianceXrefService.ComplianceXrefServiceClient();
            for (i = 0; i < compliancetypeid.Length; i++)
            {
                xmldata = serviceClient.GetComplainceType(compliancetypeid[i]);
                ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.compliance_Types.Add(new compliance_type
                        {
                            complianceid = Convert.ToInt32(row["Compliance_Type_ID"]),
                            auditfrequency = Convert.ToInt32(row["Audit_Frequency"]),
                            Name = Convert.ToString(row["Compliance_Type_Name"]),
                            startdate = Convert.ToDateTime(row["Start_Date"]),
                            enddate = Convert.ToDateTime(row["End_Date"])
                        });
                    }
                }                                  
            }
            


            return View("_SelectFrequency", model);
        }

        public ActionResult Auditentry(int compliancetypeid,int branchid, int vendorid,DateTime sdate,DateTime edate)
        {
            AuditentryViewModel model = new AuditentryViewModel();
            model.ActList = new List<SelectListItem>();
            AuditService.AuditServiceClient client = new AuditService.AuditServiceClient();
            string xmldata=client.getComplianceActList(branchid, vendorid, compliancetypeid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));

            if (ds.Tables.Count > 0)
            {
                model.actid = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Xref_ID"]);
                Session["Actid"]= Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Xref_ID"]);
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.ActList.Add(new SelectListItem() { Text = Convert.ToString(row["Compliance_Title"]), Value = Convert.ToString(row["Compliance_Xref_ID"]) });
                }
            }
            Session["CompliancetypeID"] = compliancetypeid;
            Session["BranchID"] = branchid;
            Session["Vendorid"] = vendorid;
            Session["Sdate"] = sdate;
            Session["Edate"] = edate;
            return View("_auuditentry",model);
        }

        public JsonResult GetAuditData(string sidx, string sord, int page, int rows,int actid)
        {
            actid=Convert.ToInt32(Session["Actid"]);
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            AuditentryViewModel model = new AuditentryViewModel();
            model.auditentries = new List<Auditentry>();
            AuditService.AuditServiceClient client = new AuditService.AuditServiceClient();
            // int actid = Convert.ToInt32(form["actid"]);
            int branchid = Convert.ToInt32(Session["BranchID"]);
            int vendorid = Convert.ToInt32(Session["Vendorid"]);
            int compliancetypeid = Convert.ToInt32(Session["CompliancetypeID"]);
            string xmldata  = client.getComplianceXref(branchid,vendorid ,compliancetypeid , actid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            xmldata = client.getcomplianceonorg(branchid, vendorid, 0,Convert.ToDateTime(Session["Sdate"]),Convert.ToDateTime(Session["Edate"]));
            model.SDate = Convert.ToDateTime(Session["Sdate"]);
            model.EDate = Convert.ToDateTime(Session["Edate"]);
            DataSet dsaudit = new DataSet();
            dsaudit.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count>0)
            {                
                foreach(System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    Auditentry auditentry = new Auditentry
                    {
                        Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                        Description = Convert.ToString(row["Comp_Description"]),
                        Non_compliance = Convert.ToString(row["Consequence"]),
                        Periodicity = Convert.ToString(row["Periodicity"]),
                       // Details=Convert.ToString(row["Details"]),
                       compliance_Xref_id = Convert.ToInt32(row["Compliance_Xref_ID"]),
                        Start_Date = Convert.ToDateTime(Session["Sdate"]),
                        End_Date = Convert.ToDateTime(Session["Edate"]),
                        Compliance_Audit_Id = 0

                    };
                    ComplianceAudit audit = new ComplianceAudit();
                    if (dsaudit.Tables.Count > 0)
                    {
                        for(int i=0;i<dsaudit.Tables[0].Rows.Count;i++)
                        //foreach (System.Data.DataRow item in dsaudit.Tables[0].Rows)
                        {
                            if (auditentry.compliance_Xref_id == Convert.ToInt32(dsaudit.Tables[1].Rows[i]["Compliance_Xref_ID"]))
                            {
                                auditentry.Applicability = Convert.ToString(dsaudit.Tables[0].Rows[i]["Applicability"]);
                                auditentry.Audit_Followup_Date = Convert.ToDateTime(dsaudit.Tables[0].Rows[i]["Audit_Followup_Date"]);
                                auditentry.Risk_Category = Convert.ToString(dsaudit.Tables[0].Rows[i]["Risk_Category"]);
                                auditentry.Audit_Remarks = Convert.ToString(dsaudit.Tables[0].Rows[i]["Audit_Remarks"]);
                                auditentry.Audit_Status = Convert.ToString(dsaudit.Tables[0].Rows[i]["Compliance_Status"]);
                                auditentry.Compliance_Audit_Id = Convert.ToInt32(dsaudit.Tables[0].Rows[i]["Compliance_Audit_ID"]);
                                audit.Xref_Comp_Type_Map_ID = Convert.ToInt32(dsaudit.Tables[0].Rows[i]["Xref_Comp_Type_Map_ID"]);
                                break;
                            }
                        }
                    }

                    auditentry.audits = audit;
                    model.auditentries.Add(auditentry);
                   
                }
            }

            var Results=model.auditentries;
            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            //if (sord.ToUpper() == "DESC")
            //{
            //    Results = Results.OrderByDescending(s => s.Id);
            //    Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            //else
            //{
            //    Results = Results.OrderBy(s => s.Id);
            //    Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult EditAuditdata(FormCollection form, HttpPostedFileBase file)
        {
            ComplianceAudit model = new ComplianceAudit();
            string evidinces = form["Evidences"];
            HttpPostedFileBase httpfile = Request.Files["Evidences"];
            // HttpFileCollection httpFile = Request.Files["Evidences"];
            model.Auditor_Id = Convert.ToInt32(form["auditid"]);
            model.Applicability = form["Applicability"];
            model.Start_Date = Convert.ToDateTime(form["Start_Date"]);
            model.End_Date = Convert.ToDateTime(form["End_Date"]);
            model.Audit_Followup_Date = Convert.ToDateTime(form["Audit_Followup_Date"]);
            model.Audit_Status = form["Audit_Status"];
            model.Audit_Remarks = form["Audit_Remarks"];
            model.Auditor_Id = Convert.ToInt32(Session["UserId"]);
            model.Org_Hier_Id = Convert.ToInt32(Session["BranchID"]);
            model.Vendor_Id = Convert.ToInt32(Session["Vendorid"]);
            model.Periodicity = form["Periodicity"];
            model.Version = 0;

            string option = form["oper"];
            AuditService.AuditServiceClient client = new AuditService.AuditServiceClient();

            if (option=="add")
            {
                
                ComplianceXref custom = new ComplianceXref();
                custom.Compliance_Parent_ID = Convert.ToInt32(Session["Actid"]);
                custom.Compliance_Title = form["Compliance_Title"];
                custom.Compliance_Type_ID = Convert.ToInt32(Session["CompliancetypeID"]);
                custom.compl_def_consequence = form["Non_compliance"];
                custom.Comp_Category = "Rule";
                custom.Comp_Description = form["Description"];
                custom.Risk_Description = form["Details"];
                //custom.Country_ID = Convert.ToInt32(form[""]);
                //custom.State_ID = Convert.ToInt32(form[""]);
                //custom.City_ID = Convert.ToInt32(form[""]);
                custom.Country_ID =0;
                custom.State_ID = 0;
                custom.City_ID = 0;
                custom.Effective_Start_Date = Convert.ToDateTime(form["Start_Date"]);
                custom.Effective_End_Date = Convert.ToDateTime(form["End_Date"]);
                custom.Periodicity = form["Periodicity"];
                custom.Is_Header = false;
                custom.Comp_Order = 2;
                custom.level = 2;
                custom.Is_Best_Practice = true;
                custom.Version = 1;
                ComplianceXrefService.ComplianceXrefServiceClient service = new ComplianceXrefService.ComplianceXrefServiceClient();
                model.Xref_Comp_Type_Map_ID = service.insertCustomxref(custom);
                client.insertCustomAuditEntries(model);

            }
            else 
            {
                int compliancexrefid = Convert.ToInt32(form["compliance_Xref_id"]);
                ComplianceXrefService.ComplianceXrefServiceClient serviceClient = new ComplianceXrefService.ComplianceXrefServiceClient();
                string xmldata = serviceClient.GetXrefComplainceTypemapping(Convert.ToInt32(Session["CompliancetypeID"]), compliancexrefid);
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                model.Xref_Comp_Type_Map_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Xref_Comp_Type_Map_ID"]);
                            
                client.insertAuditEntries(model);
            }
         

            //int compliancexrefid=Convert.ToInt32(form["ID"]);          
          
            var result = "Success";
            return Json(result);
        }

        public ActionResult SubmitAudit(AuditentryViewModel model,string sdate, string edate )
        {
            AuditService.AuditServiceClient client = new AuditService.AuditServiceClient();
            ComplianceAudit audit = new ComplianceAudit();
            audit.Audit_Remarks = model.Overallremarks;
            audit.Auditor_Id = Convert.ToInt32(Session["UserId"]);
            audit.Org_Hier_Id = Convert.ToInt32(Session["BranchID"]);
            audit.Vendor_Id = Convert.ToInt32(Session["Vendorid"]);
            audit.Version = 1;
            audit.Start_Date =Convert.ToDateTime(sdate);
            audit.End_Date =Convert.ToDateTime(edate);
            client.UpdatetAuditEntries(audit);
            return View();
        }
    }
}