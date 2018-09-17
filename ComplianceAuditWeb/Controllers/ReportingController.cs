using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using ComplianceAuditWeb.Models;
using Rotativa;
using System.Collections;
using Rotativa.Options;

namespace ComplianceAuditWeb.Controllers
{
    public class ReportingController : Controller
    {
        // GET: Reporting
      

        [HttpGet]
        public ActionResult selectbranch()
        {
            ReportViewModel model = new ReportViewModel();
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
        public ActionResult selectbranch(ReportViewModel model)
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
            {
                TempData["Message"] = "No Vendors assigned for the selected branch.";
            }

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



































        public ActionResult GetBranchReports( )
        {
            var id = Request.QueryString["x"];
            int branchid =Convert.ToInt32( id);
            ReportingService.ReportingServiceClient clientBranch = new ReportingService.ReportingServiceClient();
            //List<ReportViewModel> reportList = new List<ReportViewModel>();
            List<ComplianceAudit> reportList = new List<ComplianceAudit>();
            string xmlbranchdata = clientBranch.getBranchReport(branchid);
            DataSet dsBranchReport = new DataSet();
            dsBranchReport.ReadXml(new StringReader(xmlbranchdata));
            if (dsBranchReport.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsBranchReport.Tables[0].Rows)
                {
                    ComplianceAudit BranchReportList = new ComplianceAudit
                    {
                        Risk_Category = Convert.ToString(row["Risk_Category"]),
                        Audit_Remarks = Convert.ToString(row["Audit_Remarks"]),
                        Audit_Status = Convert.ToString(row["Compliance_Status"]),
                        Evidences = Convert.ToString(row["Evidences"]),
                        Company_Name = Convert.ToString(row["Company_Name"]),
                        Title = Convert.ToString(row["Compliance_Title"]),
                        

                        //Applicability = Convert.ToString(row["Applicability"]),
                        // Audit_Date= Convert.ToDateTime(row["Audit_Date"]),
                        // Audit_Followup_Date = Convert.ToDateTime(row["Audit_Followup_Date"]),
                        // Compliance_Audit_Id= Convert.ToInt32(row["Compliance_Audit_ID"]),
                        // End_Date= Convert.ToDateTime(row["End_Date"]),
                        //Is_Active= Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        // Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                        //Org_Hier_Id= Convert.ToInt32(row["Org_Hier_ID"]),
                        // Start_Date = Convert.ToDateTime(row["Start_Date"]),
                        // Vendor_Id= Convert.ToInt32(row["Vendor_ID"]),
                        // Version= Convert.ToInt32(row["Version"]),
                        // Xref_Comp_Type_Map_ID= Convert.ToInt32(row["Xref_Comp_Type_Map_ID"]),
                        // Auditor_Id = Convert.ToInt32(row["Auditor_ID"])
                    };
                    reportList.Add(BranchReportList);

                    Session["Name"] = BranchReportList.Company_Name;
                }
            }
            Session["BranchList"] = reportList;
            return View("_Report", reportList);

        }

        public ActionResult BranchReport()
        {
         
            List<ComplianceAudit> model = new List<ComplianceAudit>();
            model = (List<ComplianceAudit>)Session["BranchList"];
            ViewBag.Name = Session["Name"];


            string footer =
               "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of " +
               "[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.ViewAsPdf("_PDF", model)
            {
                FileName = "firstpdf.pdf",
                CustomSwitches = footer
            };


        }

        [HttpGet]
        public ActionResult GoToChart()
        {
            var id = Request.QueryString["branchid"];
            if (id != null)
            {
                TempData["ID"] = id;
            }
           
            return View("_Chart");
        }
        public ActionResult GetData()
        {
            int id =Convert.ToInt32( TempData["ID"]);
            ComplianceAudit audit = new ComplianceAudit();
            List<ComplianceAudit> auditList = new List<ComplianceAudit>();
            ReportingService.ReportingServiceClient client = new ReportingService.ReportingServiceClient();
            Ratio objratio = new Ratio();
           // int OrgID =Convert.ToInt32( Session["GroupCompanyId"]);
          int  BranchID = id;
            string xmlbranchdata = client.getBranchReport(BranchID);
            DataSet dsBranchReport = new DataSet();
            dsBranchReport.ReadXml(new StringReader(xmlbranchdata));
            if (dsBranchReport.Tables.Count > 0)
            {
                objratio.Name =Convert.ToString( dsBranchReport.Tables[0].Rows[0]["Company_Name"]);
                objratio.ID = Convert.ToInt32(dsBranchReport.Tables[0].Rows[0]["Org_Hier_ID"]);
               DateTime d = Convert.ToDateTime(dsBranchReport.Tables[0].Rows[0]["Audit_Date"]);
                objratio.Date =Convert.ToString( d.Year);
                foreach (System.Data.DataRow row in dsBranchReport.Tables[0].Rows)
                {
                    ComplianceAudit BranchReportList = new ComplianceAudit
                    {
                        Audit_Status = Convert.ToString(row["Compliance_Status"])
                    };
                    auditList.Add(BranchReportList);

                    int complianced = auditList.Count(x => x.Audit_Status == "complianced");
                    int non_complianced = auditList.Where(x => x.Audit_Status == "non_complianced").Count();
                    int partially_complianced = auditList.Where(x => x.Audit_Status == "partially_complianced").Count();
                    objratio.Complianced = complianced;
                    objratio.Non_Complianced = non_complianced;
                    objratio.Partially_Complianced = partially_complianced;
                   
                    

                }
            }
            return Json(objratio, JsonRequestBehavior.AllowGet);

        }
        public class Ratio
        {
            public int Complianced { get; set; }
            public int Non_Complianced { get; set; }
            public int Partially_Complianced { get; set; }
            public int ID { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
        }

    }
}