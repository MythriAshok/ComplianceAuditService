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
        public ActionResult GetBranchReports( )
        {
            var id = Request.QueryString["x"];
            int branchid =Convert.ToInt32( id);
            string status = Request.QueryString["n"];
            string sdate = Request.QueryString["sd"];
            //DateTime StartDate = DateTime.ParseExact(sdate,"dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //sdate = string.Format("dd-mm-yyyy",sdate);
            DateTime StartDate = Convert.ToDateTime(sdate);
            //string edate = Request.QueryString["ed"];
            //DateTime EndDate =Convert.ToDateTime( Request.QueryString["ed"]);

            ReportViewModel model = new ReportViewModel();
            ReportingService.ReportingServiceClient clientBranch = new ReportingService.ReportingServiceClient();
            string xmlbranchdata ="";
            string xmlbranchACTdata ="";
            if (status == null)
            {
                 xmlbranchdata = clientBranch.getBranchReport(branchid);
                xmlbranchACTdata = clientBranch.getBranchRACTeport(branchid);
                DataSet dsBranchACTReport = new DataSet();
                dsBranchACTReport.ReadXml(new StringReader(xmlbranchACTdata));
                if (dsBranchACTReport.Tables.Count > 0)
                {
                    model.ActList = bindCompliancelist(dsBranchACTReport);
                }
            }
            else if(status!= null)
            {
                xmlbranchdata = clientBranch.getBranchStatusReport(branchid,status);
                xmlbranchACTdata = clientBranch.getBranchStatusACTReport(branchid, status);
                DataSet dsBranchACTReport = new DataSet();
                dsBranchACTReport.ReadXml(new StringReader(xmlbranchACTdata));
                if (dsBranchACTReport.Tables.Count > 0)
                {
                    model.ActList = bindCompliancelist(dsBranchACTReport);
                }
            }
            DataSet dsBranchReport = new DataSet();
            dsBranchReport.ReadXml(new StringReader(xmlbranchdata));

            if (dsBranchReport.Tables.Count > 0)
            {
                Session["Name"] = Convert.ToString(dsBranchReport.Tables[0].Rows[0]["Company_Name"]);
                DataView dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Complianced'";
                DataTable dtComplianced = dv.ToTable();

                dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Non_Complianced'";
                DataTable dtNonComplianced = dv.ToTable();


                dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Partially_Complianced'";
                DataTable dtPartiallyComplianced = dv.ToTable();
                if(dtComplianced.Rows.Count>0)
                {
                    model.CompliancedRuleList = bindComplianceAuditRuleList(dtComplianced);
                }
                if (dtNonComplianced.Rows.Count > 0)
                {
                    dv = new DataView(dtNonComplianced);
                    dv.RowFilter = "Risk_Category = 'high'";
                    DataTable dtNonCompliancedhighRisk = dv.ToTable();

                    dv = new DataView(dtNonComplianced);
                    dv.RowFilter = "Risk_Category = 'medium'";
                    DataTable dtNonCompliancedmediumRisk = dv.ToTable();

                    dv = new DataView(dtNonComplianced);
                    dv.RowFilter = "Risk_Category = 'low'";
                    DataTable dtNonCompliancedlowRisk = dv.ToTable();

                    if (dtNonCompliancedhighRisk.Rows.Count > 0)
                    {
                        model.NonCompliancedRuleListHighRisk = bindComplianceAuditRuleList(dtNonCompliancedhighRisk);
                    }
                    if (dtNonCompliancedmediumRisk.Rows.Count > 0)
                    {
                        model.NonCompliancedRuleListMediumRisk = bindComplianceAuditRuleList(dtNonCompliancedmediumRisk);
                    }
                    if (dtNonCompliancedlowRisk.Rows.Count > 0)
                    {
                        model.NonCompliancedRuleListHighRisk = bindComplianceAuditRuleList(dtNonCompliancedlowRisk);
                    }
                }
                if (dtPartiallyComplianced.Rows.Count > 0)
                {
                    model.PartiallyCompliancedRuleList = bindComplianceAuditRuleList(dtPartiallyComplianced);
                }

            }
            Session["BranchCompliancedRuleList"] = model.CompliancedRuleList;
            Session["BranchNonCompliancedRuleListHighRisk"] = model.NonCompliancedRuleListHighRisk;
            Session["BranchNonCompliancedRuleListMediumRisk"] = model.NonCompliancedRuleListMediumRisk;
            Session["BranchNonCompliancedRuleListLowRisk"] = model.NonCompliancedRuleListLowRisk;
            Session["BranchPartiallyCompliancedRuleList"] = model.PartiallyCompliancedRuleList;
            Session["BranchActList"] = model.ActList;
            return View("_Report", model);

        }

        public ActionResult BranchReport()
        {
            ReportViewModel model = new ReportViewModel();
            //List<ReportViewModel> model = new List<ReportViewModel>();
            //List<ComplianceAudit> model = new List<ComplianceAudit>();
            model.ActList=(List<ComplianceXref>) Session["BranchActList"];
            model.CompliancedRuleList = (List<ComplianceAudit>) Session["BranchCompliancedRuleList"];
            model.NonCompliancedRuleListHighRisk = (List<ComplianceAudit>) Session["BranchNonCompliancedRuleListHighRisk"];
            model.NonCompliancedRuleListMediumRisk = (List<ComplianceAudit>) Session["BranchNonCompliancedRuleListMediumRisk"];
            model.NonCompliancedRuleListLowRisk = (List<ComplianceAudit>)Session["BranchNonCompliancedRuleListLowRisk"];
            model.PartiallyCompliancedRuleList = (List<ComplianceAudit>)Session["BranchPartiallyCompliancedRuleList"];
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
            ReportViewModel model = new ReportViewModel();
            model.ComplianceAudit = new ComplianceAudit();

            var id = Request.QueryString["branchid"];
            DateTime sdate =Convert.ToDateTime( Request.QueryString["sd"]);
            if(sdate!= null)
            {
                model.ComplianceAudit.Start_Date = sdate;
            }
              string edate =Request.QueryString["ed"];
            if (edate != null)
            {
                model.ComplianceAudit.End_Date = Convert.ToDateTime(edate);
            }
            //model.ComplianceAudit.End_Date.ToString(edate);
            //model.dtime= DateTime.ParseExact(edate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

          
            model.branchid = Convert.ToInt32(id);
            if (id != null)
            {
                TempData["ID"] = id;
            }
            Session["model"] = model;
            return View("_Chart",model);
        }
        public ActionResult GetData()
        {
            int id =Convert.ToInt32( TempData["ID"]);
            
            ReportViewModel audit = new ReportViewModel();
            audit.ComplianceAudit = new ComplianceAudit();
            audit =(ReportViewModel) Session["model"];
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
                List<ComplianceAudit> auditList = new List<ComplianceAudit>();

                foreach (System.Data.DataRow row in dsBranchReport.Tables[0].Rows)
                {
                    auditList.Add(new ComplianceAudit
                    {
                        Audit_Status = Convert.ToString(row["Compliance_Status"]),
                        Start_Date = Convert.ToDateTime(dsBranchReport.Tables[0].Rows[0]["Start_Date"]),
                     End_Date= Convert.ToDateTime(dsBranchReport.Tables[0].Rows[0]["End_Date"])

                });
                    objratio.auditList = auditList;

                    int complianced = auditList.Count(x => x.Audit_Status == "Complianced");
                    int non_complianced = auditList.Where(x => x.Audit_Status == "Non_Complianced").Count();
                    int partially_complianced = auditList.Where(x => x.Audit_Status == "Partially_Complianced").Count();
                    objratio.Complianced = complianced;
                    objratio.Non_Complianced = non_complianced;
                    objratio.Partially_Complianced = partially_complianced;
                }
                objratio.StartDate = audit.ComplianceAudit.Start_Date;
                objratio.EndDate = audit.ComplianceAudit.End_Date;
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
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public List<ComplianceAudit> auditList { get; set; }
        }









        private List<ComplianceXref> bindCompliancelist(DataSet ds)
        {
            List<ComplianceXref> model = new List<ComplianceXref>();
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
        private List<ComplianceAudit> bindComplianceAuditRuleList(DataTable dt)
        {
            List<ComplianceAudit> model = new List<ComplianceAudit>();
            foreach (DataRow row in dt.Rows)
            {
                model.Add(new ComplianceAudit
                {
                    Risk_Category = Convert.ToString(row["Risk_Category"]),
                    Audit_Remarks = Convert.ToString(row["Audit_Remarks"]),
                    Audit_Status = Convert.ToString(row["Compliance_Status"]),
                    Evidences = Convert.ToString(row["Evidences"]),
                    Company_Name = Convert.ToString(row["Company_Name"]),
                    Title = Convert.ToString(row["Compliance_Title"]),
                    ParentID = Convert.ToInt32(row["Compliance_Parent_ID"])

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

                });
            }
            return model;
        }



    }
}