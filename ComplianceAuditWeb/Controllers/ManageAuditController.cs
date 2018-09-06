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
                            auditfrequency = Convert.ToString(row["Audit_Frequency"]),
                            Name = Convert.ToString(row["Compliance_Type_Name"]),
                            startdate = Convert.ToDateTime(row["Start_Date"]),
                            enddate = Convert.ToDateTime(row["End_Date"])
                        });
                    }
                }
            }
            
            return View("_SelectFrequency", model);
        }

        public ActionResult Auditentry(int compliancetypeid,int branchid, int vendorid)
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
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.ActList.Add(new SelectListItem() { Text = Convert.ToString(row["Compliance_Title"]), Value = Convert.ToString(row["Compliance_Xref_ID"]) });
                }
            }
            Session["ComplianceID"] = compliancetypeid;
            Session["BranchID"] = branchid;
            Session["Vendorid"] = vendorid;
            return View("_auuditentry",model);
        }

        public JsonResult GetAuditData(string sidx, string sord, int page, int rows,int actid)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            AuditentryViewModel model = new AuditentryViewModel();
            model.auditentries = new List<Auditentry>();
            AuditService.AuditServiceClient client = new AuditService.AuditServiceClient();
           // int actid = Convert.ToInt32(form["actid"]);
            string xmldata  = client.getComplianceXref(Convert.ToInt32(Session["BranchID"]), Convert.ToInt32(Session["Vendorid"]), Convert.ToInt32(Session["ComplianceID"]),1);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));

            if(ds.Tables.Count>0)
            {
                foreach(System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.auditentries.Add(new Auditentry
                    {
                        Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                        Description = Convert.ToString(row["Comp_Description"]),
                        Non_compliance = Convert.ToString(row["compl_def_consequence"]),
                        Periodicity = Convert.ToString(row["Periodicity"]),
                        compliance_Xref_id=Convert.ToInt32(row["Compliance_Xref_ID"]),
                        audits = new List<ComplianceAudit>()
                    });
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

        public JsonResult EditAuditdata(FormCollection form)
        {
            ComplianceAudit model = new ComplianceAudit();
            model.Applicability = form["Applicability"];
            model.Start_Date = Convert.ToDateTime(form["Start_Date"]);
            model.End_Date = Convert.ToDateTime(form["End_Date"]);                       
            model.Audit_Followup_Date = Convert.ToDateTime(form["Audit_Followup_Date"]);
            model.Audit_Status= form["Audit_Status"];
            model.Audit_Remarks= form["Audit_Remarks"];
            string ope = form["oper"];

            //int compliancexrefid=Convert.ToInt32(form["ID"]);          
            int compliancexrefid = Convert.ToInt32(form["compliance_Xref_id"]);
            ComplianceXrefService.ComplianceXrefServiceClient serviceClient = new ComplianceXrefService.ComplianceXrefServiceClient();
            string xmldata= serviceClient.GetXrefComplainceTypemapping(Convert.ToInt32(Session["ComplianceID"]), compliancexrefid);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            model.Xref_Comp_Type_Map_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Xref_Comp_Type_Map_ID"]);

            model.Org_Hier_Id = Convert.ToInt32(Session["BranchID"]);
            model.Vendor_Id= Convert.ToInt32(Session["Vendorid"]);
            model.Auditor_Id=Convert.ToInt32(Session["UserId"]);
            AuditService.AuditServiceClient client = new AuditService.AuditServiceClient();
            
            client.insertAuditEntries(model);
            var result = "Success";
            return Json(result);
        }
    }
}