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
            model.frequency = new List<SelectListItem>();
            model.ComplianceTypeList = new List<SelectListItem>();


            model.yearid = 0;
            model.frequencyid = 0;
            model.frequency.Add(new SelectListItem() { Text = "Select Frequency", Value = "0" });
            model.frequency.Add(new SelectListItem() { Text = "Yearly", Value = "1" });
            model.frequency.Add(new SelectListItem() { Text = "Half-Yearly", Value = "2" });
            model.frequency.Add(new SelectListItem() { Text = "Quarterly", Value = "3" });
            model.frequency.Add(new SelectListItem() { Text = "Monthly", Value = "4" });
            model.frequency.Add(new SelectListItem() { Text = "Periodic", Value = "5" });


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
              
                model.ComplianceTypeList = new List<SelectListItem>();
                xmldata = client.GetAssignedComplianceTypes(model.companyid);
                ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    model.branchid = Convert.ToInt32(ds.Tables[0].Rows[0]["Org_Hier_ID"]);
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.ComplianceTypeList.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
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
                {
                    TempData["Message"] = "No Vendors assigned for the selected branch.";
                }

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
            model.frequency = new List<SelectListItem>();


           
            model.frequency.Add(new SelectListItem() { Text = "Select Frequency", Value = "0" });
            model.frequency.Add(new SelectListItem() { Text = "Yearly", Value = "1" });
            model.frequency.Add(new SelectListItem() { Text = "Half-Yearly", Value = "2" });
            model.frequency.Add(new SelectListItem() { Text = "Quarterly", Value = "3" });
            model.frequency.Add(new SelectListItem() { Text = "Monthly", Value = "4" });
            model.frequency.Add(new SelectListItem() { Text = "Periodic", Value = "5" });


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

            model.ComplianceTypeList = new List<SelectListItem>();
            xmldata = client.GetAssignedComplianceTypes(model.companyid);
            ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                {
                    model.ComplianceTypeList.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
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

        public ActionResult selectauditfrequency(int branchid, int vendorid, string vendorname, int frequencyid, int yearid, int compliancid)
        {
            // Compliancetype_view_model model1 = new Compliancetype_view_model();
            ReportViewModel model = new ReportViewModel();

            model.ComplianceAudit = new ComplianceAudit();
            model.compliance_Types = new List<compliance_type>();
            model.branchid = branchid;
            model.Vendorid = vendorid;
            model.frequencyid = frequencyid;
            model.yearid = yearid;
            model.Vendorname = vendorname;
            model.complianceTypeid = compliancid;
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
                xmldata = serviceClient.GetComplainceType(compliancid);
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
                    ViewBag.ComplianceName = ds.Tables[0].Rows[0]["Compliance_Type_Name"];
            }
              foreach (var item in model.compliance_Types)
                {
                    List<StartEndDates> dates = new List<StartEndDates>();
                    model.startEndDates = new List<StartEndDates>();
                    DateTime sdate = new DateTime(yearid, item.startdate.Month, 01);

                    int inc = 1;
                    if (model.frequencyid == 1)
                    {
                        inc = 11;
                        DateTime edate = new DateTime(yearid, item.startdate.Month + inc, 31);
                        for (int k = 0, j = 0; j < model.frequencyid; k = k + inc, j++)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate= edate.AddMonths(k)
                            });
                        }

                    }
                    else if (model.frequencyid == 2)
                    {
                        model.frequencyvalue = 2;

                        inc = 6;

                        DateTime edate = new DateTime(yearid, item.startdate.Month + 5, 30);
                        for (int k = 0, j = 0; j < model.frequencyid; k = k + inc, j++)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k)

                            });
                        }
                       
                    }
                    else if (model.frequencyid == 3)
                    {
                        model.frequencyvalue = 4;
                        inc = 3;
                        DateTime edate = new DateTime(yearid, item.startdate.Month + 2, 31);
                        for (int k = 0, j = 0; j < model.frequencyvalue; k = k + inc, j++)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k)

                            });
                        }
                    }
                    else if (model.frequencyid == 4)
                    {
                        inc = 1;
                        model.frequencyvalue = 12;
                        
                        DateTime edate = new DateTime(yearid, item.startdate.Month, 31);
                        for (int k = 0, j = 0; j < model.frequencyvalue; k = k + inc, j++)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k)

                            });
                        }
                    }
                    else
                    {


                    }

                }

                if (branchid == vendorid)
                {
                    if (branchid != 0)
                    {
                        TempData["ID"] = branchid;
                    }
                }
                else
                {
                    if (vendorid != 0)
                    {
                        TempData["ID"] = vendorid;
                    }
                }
                Session["model"] = model;

            }
            return View("_SelectFrequency", model);

        }
        public ActionResult GetBranchReports( )

        {
            int branchid = 0;
            var id = Request.QueryString["x"];
            var vendorid = Request.QueryString["y"];
            var compliancetypeid = Request.QueryString["compliancetypeid"];
           
            if (id == vendorid)
            {

                branchid = Convert.ToInt32(id);

            }
            else
            {

                branchid = Convert.ToInt32(vendorid);

            }


            string status = Request.QueryString["n"];
            string sdate = Request.QueryString["sdate"];
            string edate = Request.QueryString["edate"];
            
            ReportViewModel model = new ReportViewModel();
            model.ComplianceAudit = new ComplianceAudit();
            model.ComplianceAudit.Start_Date =Convert.ToDateTime( sdate);
            model.ComplianceAudit.End_Date = Convert.ToDateTime(edate);

            model.complianceTypeid =Convert.ToInt32( compliancetypeid);
            

            ReportingService.ReportingServiceClient clientBranch = new ReportingService.ReportingServiceClient();
            string xmlbranchdata ="";
            string xmlbranchACTdata ="";
            if (status == null)
            {
                 xmlbranchdata = clientBranch.getBranchReport(branchid, model.ComplianceAudit.Start_Date, model.ComplianceAudit.End_Date, model.complianceTypeid);
                xmlbranchACTdata = clientBranch.getBranchRACTeport(branchid);
                DataSet dsBranchACTReport = new DataSet();
                dsBranchACTReport.ReadXml(new StringReader(xmlbranchACTdata));
                if (dsBranchACTReport.Tables.Count > 0)
                {
                    model.ActList = bindCompliancelist(dsBranchACTReport);
                }
            }
            //else if(status!= null)
            else 
            {
                xmlbranchdata = clientBranch.getBranchStatusReport(branchid,status, model.ComplianceAudit.Start_Date, model.ComplianceAudit.End_Date, model.complianceTypeid);
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
                //foreach(System.Data.DataRow rowstatus in dsBranchReport.Tables[0].Rows)
                //{
                //    model.ComplianceAudit.Audit_Status =Convert.ToString( dsBranchReport.Tables[0].Rows[0]["Compliance_Status"]);
                //}
                Session["Name"] = Convert.ToString(dsBranchReport.Tables[0].Rows[0]["Company_Name"]);
                DataView dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Complianced'";
                DataTable dtComplianced = dv.ToTable();
                if (dtComplianced.Rows.Count > 0)
                {
                    model.ComplianceStatus = Convert.ToString(dtComplianced.Rows[0]["Compliance_Status"]);
                }
                dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Non_Complianced'";
                DataTable dtNonComplianced = dv.ToTable();
                if (dtNonComplianced.Rows.Count > 0)
                {
                    model.NonComplianceStatus = Convert.ToString(dtNonComplianced.Rows[0]["Compliance_Status"]);
                }


                dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Partially_Complianced'";
                DataTable dtPartiallyComplianced = dv.ToTable();
                if (dtPartiallyComplianced.Rows.Count > 0)
                {
                    model.PartiallyComplianceStatus = Convert.ToString(dtPartiallyComplianced.Rows[0]["Compliance_Status"]);

                }
                if (dtComplianced.Rows.Count>0)
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
            ViewBag.Name = Session["Name"];
            model.branchid =Convert.ToInt32( id);
            return View("_Report", model);

        }

        public ActionResult BranchReport()
        {
            ReportViewModel model = new ReportViewModel();
           
            model.ActList=(List<ComplianceXref>) Session["BranchActList"];
            model.CompliancedRuleList = (List<ComplianceAudit>) Session["BranchCompliancedRuleList"];
            model.NonCompliancedRuleListHighRisk = (List<ComplianceAudit>) Session["BranchNonCompliancedRuleListHighRisk"];
            model.NonCompliancedRuleListMediumRisk = (List<ComplianceAudit>) Session["BranchNonCompliancedRuleListMediumRisk"];
            model.NonCompliancedRuleListLowRisk = (List<ComplianceAudit>)Session["BranchNonCompliancedRuleListLowRisk"];
            model.PartiallyCompliancedRuleList = (List<ComplianceAudit>)Session["BranchPartiallyCompliancedRuleList"];
            ViewBag.Name = Session["Name"];
            model.branchid =Convert.ToInt32( Request.QueryString["bid"]);
            string footer =
               "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of " +
               "[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.ViewAsPdf("_PDF", model)
            {
                FileName = "firstpdf.pdf",
                CustomSwitches = footer
            };


        }

        //[HttpGet]
        //public ActionResult GoToChart(DateTime sdate, DateTime edate)
        //{
        //    ReportViewModel model = new ReportViewModel();
        //    model.ComplianceAudit = new ComplianceAudit();

        //    var branchid = Request.QueryString["branchid"];
        //    var vendorid = Request.QueryString["vendorid"];

        //    model.ComplianceAudit.Start_Date = sdate;
        //    model.ComplianceAudit.End_Date = edate;

        //    model.branchid = Convert.ToInt32(branchid);
        //    model.Vendorid = Convert.ToInt32(vendorid);
        //    if (branchid == vendorid)
        //    {
        //        if (branchid != null)
        //        {
        //            TempData["ID"] = branchid;
        //        }
        //    }
        //    else
        //    {
        //        if (vendorid != null)
        //        {
        //            TempData["ID"] = vendorid;
        //        }
        //    }
        //    Session["model"] = model;
        //    return View("_Chart",model);
        //}
        public ActionResult GetData()
        {
            int id = 0;
            int BranchID = 0;
            var bid = "";
            if (TempData["ID"] != null)
            {
                id = Convert.ToInt32(TempData["ID"]);
                BranchID = id;
            }
            else
            {
                 bid = (Request.QueryString["id"]);
                BranchID = Convert.ToInt32(bid);
            }
            ReportViewModel audit = new ReportViewModel();
            audit.ComplianceAudit = new ComplianceAudit();
            if (Session["model"] != null)
            {
                audit = (ReportViewModel)Session["model"];
            }
            ReportingService.ReportingServiceClient client = new ReportingService.ReportingServiceClient();
            List<Ratio> objratiolist = new List<Ratio>();
            if (id > 0)
            {
                foreach (var date in audit.startEndDates)
                {
                    Ratio objratio = new Ratio();
                    string xmlbranchdata = client.getBranchReport(BranchID,date.StartDate, date.EndDate,audit.complianceTypeid);
                    DataSet dsBranchReport = new DataSet();
                    dsBranchReport.ReadXml(new StringReader(xmlbranchdata));
                    objratio.compliancetypeid = audit.complianceTypeid;
                    if (dsBranchReport.Tables.Count > 0)
                    {
                        objratio.Name = Convert.ToString(dsBranchReport.Tables[0].Rows[0]["Company_Name"]);
                        objratio.ID = Convert.ToInt32(dsBranchReport.Tables[0].Rows[0]["Org_Hier_ID"]);
                        objratio.VendorID = Convert.ToInt32(dsBranchReport.Tables[0].Rows[0]["Vendor_ID"]);
                        DateTime d = Convert.ToDateTime(dsBranchReport.Tables[0].Rows[0]["Audit_Date"]);
                        objratio.Date = Convert.ToString(d.Year);
                        List<ComplianceAudit> auditList = new List<ComplianceAudit>();
                        foreach (System.Data.DataRow row in dsBranchReport.Tables[0].Rows)
                        {
                            auditList.Add(new ComplianceAudit
                            {
                                Audit_Status = Convert.ToString(row["Compliance_Status"]),
                            });
                            //objratio.auditList = auditList;
                            int complianced = auditList.Count(x => x.Audit_Status == "Complianced");
                            int non_complianced = auditList.Where(x => x.Audit_Status == "Non_Complianced").Count();
                            int partially_complianced = auditList.Where(x => x.Audit_Status == "Partially_Complianced").Count();
                            objratio.Complianced = complianced;
                            objratio.Non_Complianced = non_complianced;
                            objratio.Partially_Complianced = partially_complianced;
                        }
                        objratio.StartDate = date.StartDate.ToString("MMM/dd/yyyy");
                        objratio.EndDate = date.EndDate.ToString("MMM/dd/yyyy");
                    }
                    objratiolist.Add(objratio);
                }

                //string xmlSecbranchdata = client.getBranchReport(BranchID, audit.StartDateSecond, audit.EndDateSecond);
                //DataSet dsSecBranchReport = new DataSet();
                //dsSecBranchReport.ReadXml(new StringReader(xmlSecbranchdata));
                //if (dsSecBranchReport.Tables.Count > 0)
                //{
                //    objratio.Name = Convert.ToString(dsSecBranchReport.Tables[0].Rows[0]["Company_Name"]);
                //    objratio.ID = Convert.ToInt32(dsSecBranchReport.Tables[0].Rows[0]["Org_Hier_ID"]);
                //    objratio.VendorID = Convert.ToInt32(dsSecBranchReport.Tables[0].Rows[0]["Vendor_ID"]);
                //    DateTime d = Convert.ToDateTime(dsSecBranchReport.Tables[0].Rows[0]["Audit_Date"]);
                //    objratio.Date = Convert.ToString(d.Year);
                //    List<ComplianceAudit> auditListSecond = new List<ComplianceAudit>();
                //    foreach (System.Data.DataRow row in dsSecBranchReport.Tables[0].Rows)
                //    {
                //        auditListSecond.Add(new ComplianceAudit
                //        {
                //            Audit_Status = Convert.ToString(row["Compliance_Status"]),
                //        });
                //        objratio.auditListSecond = auditListSecond;
                //        int complianced = auditListSecond.Count(x => x.Audit_Status == "Complianced");
                //        int non_complianced = auditListSecond.Where(x => x.Audit_Status == "Non_Complianced").Count();
                //        int partially_complianced = auditListSecond.Where(x => x.Audit_Status == "Partially_Complianced").Count();
                //        objratio.SecComplianced = complianced;
                //        objratio.SecNon_Complianced = non_complianced;
                //        objratio.SecPartially_Complianced = partially_complianced;
                //    }
                //    objratio.SecStartDate = audit.StartDateSecond.ToString("MMM/dd/yyyy");
                //    objratio.SecEndDate = audit.EndDateSecond.ToString("MMM/dd/yyyy");
                //}

            }
            //if (bid!= null)
            //{
            //    string xmlpiebranchdata = client.getBranchpieReport(BranchID);
            //    DataSet dspieBranchReport = new DataSet();
            //    dspieBranchReport.ReadXml(new StringReader(xmlpiebranchdata));
            //    if (dspieBranchReport.Tables.Count > 0)
            //    {
            //        List<ComplianceAudit> auditListpie = new List<ComplianceAudit>();
            //        foreach (System.Data.DataRow row in dspieBranchReport.Tables[0].Rows)
            //        {
            //            auditListpie.Add(new ComplianceAudit
            //            {
            //                Audit_Status = Convert.ToString(row["Compliance_Status"]),
            //            });

            //            objratio.auditList = auditListpie;
            //            int complianced = auditListpie.Count(x => x.Audit_Status == "Complianced");
            //            int non_complianced = auditListpie.Where(x => x.Audit_Status == "Non_Complianced").Count();
            //            int partially_complianced = auditListpie.Where(x => x.Audit_Status == "Partially_Complianced").Count();
            //            objratio.Complianced = complianced;
            //            objratio.Non_Complianced = non_complianced;
            //            objratio.Partially_Complianced = partially_complianced;
            //        }
            //    }
            //}
            return Json(objratiolist, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int Complianced { get; set; }
            public int Non_Complianced { get; set; }
            public int Partially_Complianced { get; set; }
        public int compliancetypeid { get; set; }
            public int ID { get; set; }
            public int VendorID { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
         
            public List<ComplianceAudit> auditList { get; set; }
            public List<ComplianceAudit> auditListSecond { get; set; }
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
                    ParentID = Convert.ToInt32(row["Compliance_Parent_ID"]),

                    Applicability = Convert.ToString(row["Applicability"]),
                    Audit_Date = Convert.ToDateTime(row["Audit_Date"]),
                    Audit_Followup_Date = Convert.ToDateTime(row["Audit_Followup_Date"]),
                    Compliance_Audit_Id = Convert.ToInt32(row["Compliance_Audit_ID"]),
                    End_Date = Convert.ToDateTime(row["End_Date"]),
                    Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                    Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                    Org_Hier_Id = Convert.ToInt32(row["Org_Hier_ID"]),
                    Start_Date = Convert.ToDateTime(row["Start_Date"]),
                    Vendor_Id = Convert.ToInt32(row["Vendor_ID"]),
                    Version = Convert.ToInt32(row["Version"]),
                    Xref_Comp_Type_Map_ID = Convert.ToInt32(row["Xref_Comp_Type_Map_ID"]),
                    Auditor_Id = Convert.ToInt32(row["Auditor_ID"]),
                    Description = Convert.ToString(row["Comp_Description"])

                });
            }
            return model;
        }

        //public ActionResult frequencytype()
        //{
        //    int branchid = 0; int vendorid = 0; string vendorname = "";
        //    Compliancetype_view_model model = new Compliancetype_view_model();
        //    model.compliance_Types = new List<compliance_type>();
        //    model.branchid = branchid;
        //    model.vendorid = vendorid;
        //    model.vendorname = vendorname;
        //    OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
        //    string xmldata = client.GetAssignedComplianceTypes(vendorid);
        //    DataSet ds = new DataSet();
        //    DataSet dscompliancetype = new DataSet();
        //    ds.ReadXml(new StringReader(xmldata));
        //    int[] compliancetypeid = new int[ds.Tables[0].Rows.Count];
        //    int i = 0;

        //    if (ds.Tables.Count > 0)
        //    {
        //        foreach (System.Data.DataRow row in ds.Tables[0].Rows)
        //        {
        //            compliancetypeid[i++] = Convert.ToInt32(row["Compliance_Type_ID"]);
        //        }
        //    }

        //    ComplianceXrefService.ComplianceXrefServiceClient serviceClient = new ComplianceXrefService.ComplianceXrefServiceClient();
        //    for (i = 0; i < compliancetypeid.Length; i++)
        //    {
        //        xmldata = serviceClient.GetComplainceType(compliancetypeid[i]);
        //        ds = new DataSet();
        //        ds.ReadXml(new StringReader(xmldata));
        //        if (ds.Tables.Count > 0)
        //        {
        //            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
        //            {
        //                model.compliance_Types.Add(new compliance_type
        //                {
        //                    complianceid = Convert.ToInt32(row["Compliance_Type_ID"]),
        //                    auditfrequency = Convert.ToInt32(row["Audit_Frequency"]),
        //                    Name = Convert.ToString(row["Compliance_Type_Name"]),
        //                    startdate = Convert.ToDateTime(row["Start_Date"]),
        //                    enddate = Convert.ToDateTime(row["End_Date"])
        //                });
        //            }
        //        }
        //    }
        //    int OrgID = 70;
        //    //ReportViewModel model = new ReportViewModel();
        //    //model.yearlists = new List<SelectListItem>();

        //    //ReportingService.ReportingServiceClient client = new ReportingService.ReportingServiceClient();
        //    //string xmldata = client.getYearForAuditReport(OrgID);
        //    //DataSet ds = new DataSet();
        //    //ds = new DataSet();
        //    //ds.ReadXml(new StringReader(xmldata));
        //    //if (ds.Tables.Count > 0)
        //    //{
        //    //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
        //    //    {
        //    //        model.yearlist.Add(new SelectListItem
        //    //        {
        //    //            Text = Convert.ToString(row["Start_Date"]), Value = Convert.ToString(row["Org_Hier_ID"]) });


        //    //    }
        //    //}
        //    model.yearid = 0;
        //    model.frequency = new List<SelectListItem>();
        //    model.frequency.Add(new SelectListItem { Text = "Select Frequency", Value = "0" });
        //    model.frequency.Add(new SelectListItem { Text = "Yearly", Value = "1" });
        //    model.frequency.Add(new SelectListItem { Text = "Half-Yearly", Value = "2" });
        //    model.frequency.Add(new SelectListItem { Text = "Quarterly", Value = "3" });
        //    model.frequency.Add(new SelectListItem { Text = "Monthly", Value = "4" });
        //    model.frequency.Add(new SelectListItem { Text = "Periodic", Value = "5"});
            
       
           


        //    return View("_FrequencyType", model);
        //}

        //public JsonResult getLinks(string frequencyid, string yearid)
        //{
        //    Compliancetype_view_model model = new Compliancetype_view_model();
        //    int FID = Convert.ToInt32(frequencyid);

        //    int inc = 1;
        //    if (FID == 1)
        //    {
        //        inc = 11;
               
        //    }
        //    else if (FID == 2)
        //    {
        //        inc = 5;
              

        //    }
        //    else if (FID == 3)
        //    {
        //        inc = 3;
            

        //    }
        //    else
        //    {
        //        inc = 1;

        //    }
        //    for (int i = 0, j = 0; j < FID; i = i + inc, j++)
        //    {
                
        //    }

        //        return Json(model, JsonRequestBehavior.AllowGet);
        //}


    }
}