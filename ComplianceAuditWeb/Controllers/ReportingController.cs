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
            model.halfYear = new List<SelectListItem>();
            model.quarter = new List<SelectListItem>();
            model.month = new List<SelectListItem>();
            model.ComplianceTypeList = new List<SelectListItem>();


            model.yearid = 0;
            model.frequencyid = 0;
            model.frequency.Add(new SelectListItem() { Text = "Select Frequency", Value = "0" });
            model.frequency.Add(new SelectListItem() { Text = "Yearly", Value = "1" });
            model.frequency.Add(new SelectListItem() { Text = "Half-Yearly", Value = "2" });
            model.frequency.Add(new SelectListItem() { Text = "Quarterly", Value = "3" });
            model.frequency.Add(new SelectListItem() { Text = "Monthly", Value = "4" });
            model.frequency.Add(new SelectListItem() { Text = "Periodic", Value = "5" });


            model.HalfYearFrequencyid = 0;
            model.halfYear.Add(new SelectListItem() { Text = "-- Select --", Value = "0" });



            model.QuarterFrequencyid = 0;
            model.quarter.Add(new SelectListItem() { Text = "-- Select --", Value = "0" });



            model.MonthFrequencyid = 0;
            model.month.Add(new SelectListItem() { Text = "-- Select --", Value = "0" });


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
                    model.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Calender_StartDate"]);
                    var companyid = Request.QueryString["companyid"];
                    if(companyid!= null)
                    {
                        model.companyid = Convert.ToInt32(companyid);
                    }
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.companyList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                    }

                    model.yearid = model.StartDate.Year;

                    model.years = Enumerable.Range(model.yearid, DateTime.Now.Year - (model.yearid - 1)).OrderByDescending(i => i);
                    model.yearid = DateTime.Now.Year;

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

                model.BranchList.Add(new SelectListItem { Text = "-- Select Branch --", Value = "0" });
                model.branchid = Convert.ToInt32(Request.QueryString["branchid"]);
                if (ds.Tables.Count > 0)
                {
                    //model.branchid = Convert.ToInt32(ds.Tables[0].Rows[0]["Org_Hier_ID"]);
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
            model.halfYear = new List<SelectListItem>();
            model.quarter = new List<SelectListItem>();
            model.month = new List<SelectListItem>();



            model.frequency.Add(new SelectListItem() { Text = "Select Frequency", Value = "0" });
            model.frequency.Add(new SelectListItem() { Text = "Yearly", Value = "1" });
            model.frequency.Add(new SelectListItem() { Text = "Half-Yearly", Value = "2" });
            model.frequency.Add(new SelectListItem() { Text = "Quarterly", Value = "3" });
            model.frequency.Add(new SelectListItem() { Text = "Monthly", Value = "4" });
            model.frequency.Add(new SelectListItem() { Text = "Periodic", Value = "5" });

            model.halfYear.Add(new SelectListItem() { Text = "Select Half-Year", Value = "0" });
            model.halfYear.Add(new SelectListItem() { Text = "First Half", Value = "1" });
            model.halfYear.Add(new SelectListItem() { Text = "Second Half", Value = "2" });


            model.quarter.Add(new SelectListItem() { Text = "Select Quarter", Value = "0" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 1", Value = "2" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 2", Value = "3" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 3", Value = "4" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 4", Value = "5" });


            model.month.Add(new SelectListItem() { Text = "Select Month", Value = "0" });
            model.month.Add(new SelectListItem() { Text = "January", Value = "1" });
            model.month.Add(new SelectListItem() { Text = "February", Value = "2" });
            model.month.Add(new SelectListItem() { Text = "March", Value = "3" });
            model.month.Add(new SelectListItem() { Text = "April", Value = "4" });
            model.month.Add(new SelectListItem() { Text = "May", Value = "5" });
            model.month.Add(new SelectListItem() { Text = "June", Value = "6" });
            model.month.Add(new SelectListItem() { Text = "July", Value = "7" });
            model.month.Add(new SelectListItem() { Text = "August", Value = "8" });
            model.month.Add(new SelectListItem() { Text = "September", Value = "9" });
            model.month.Add(new SelectListItem() { Text = "October", Value = "10" });
            model.month.Add(new SelectListItem() { Text = "November", Value = "11" });
            model.month.Add(new SelectListItem() { Text = "December", Value = "12" });


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
                    model.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Calender_StartDate"]);

                    model.companyList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
                //model.yearid = model.StartDate.Year;
                model.years = Enumerable.Range(model.yearid, DateTime.Now.Year - (model.yearid - 1));
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

        public ActionResult selectauditfrequency(int branchid, int vendorid, string vendorname, int frequencyid, int yearid, int compliancid, int companyid,
            int halfyearid, int monthid)
        {
            // Compliancetype_view_model model1 = new Compliancetype_view_model();
            ReportViewModel model = new ReportViewModel();

            model.ComplianceAudit = new ComplianceAudit();
            model.compliance_Types = new List<compliance_type>();
            model.companyid = companyid;
            model.branchid = branchid;
            model.Vendorid = vendorid;
            model.frequencyid = frequencyid;
            model.yearid = yearid;
            model.Vendorname = vendorname;
            model.complianceTypeid = compliancid;
            model.HalfYearFrequencyid = halfyearid;

            model.MonthFrequencyid = monthid;
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
                    DateTime sdate = new DateTime(yearid, item.startdate.Month, item.startdate.Day);
                    DateTime edate = new DateTime(yearid, item.enddate.Month, item.enddate.Day);
                    //DateTime sdate = item.startdate;
                    //DateTime edate = item.enddate;
                    int k = 0;
                    if (model.frequencyid == 1)
                    {
                        model.startEndDates.Add(new StartEndDates
                        {
                            StartDate = sdate,
                            EndDate = edate
                        });
                    }
                    else if (model.frequencyid == 2)
                    {
                        if (model.HalfYearFrequencyid == 1)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k - 6)
                            });
                        }
                        else
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 6),
                                EndDate = edate
                            });
                        }
                    }
                    else if (model.frequencyid == 3)
                    {
                        if (model.QuarterFrequencyid == 1)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k + 3)
                            });
                        }
                        else if (model.QuarterFrequencyid == 2)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 3),
                                EndDate = edate.AddMonths(k + 6)
                            });
                        }
                        else if (model.QuarterFrequencyid == 3)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 6),
                                EndDate = edate.AddMonths(k + 9)
                            });
                        }
                        else
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 9),
                                EndDate = edate
                            });
                        }
                    }
                    else if (model.frequencyid == 4)
                    {
                        if (model.MonthFrequencyid == 1)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k + 1)
                            });
                        }
                        else if (model.MonthFrequencyid == 2)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 2),
                                EndDate = edate.AddMonths(k + 3)
                            });
                        }
                        else if (model.MonthFrequencyid == 3)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 3),
                                EndDate = edate.AddMonths(k + 4)
                            });
                        }
                        else if (model.MonthFrequencyid == 4)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 4),
                                EndDate = edate.AddMonths(k + 5)
                            });
                        }
                        else if (model.MonthFrequencyid == 5)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 5),
                                EndDate = edate.AddMonths(k + 6)
                            });
                        }
                        else if (model.MonthFrequencyid == 6)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 6),
                                EndDate = edate.AddMonths(k + 7)
                            });
                        }
                        else if (model.MonthFrequencyid == 7)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 7),
                                EndDate = edate.AddMonths(k + 8)
                            });
                        }
                        else if (model.MonthFrequencyid == 8)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 8),
                                EndDate = edate.AddMonths(k + 9)
                            });
                        }
                        else if (model.MonthFrequencyid == 9)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 9),
                                EndDate = edate.AddMonths(k + 10)
                            });
                        }
                        else if (model.MonthFrequencyid == 10)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 10),
                                EndDate = edate.AddMonths(k + 11)
                            });
                        }
                        else if (model.MonthFrequencyid == 11)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 11),
                                EndDate = edate.AddMonths(k + 12)
                            });
                        }
                        else if (model.MonthFrequencyid == 12)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 12),
                                EndDate = edate
                            });
                        }
                    }
                    else
                    {

                        model.startEndDates.Add(new StartEndDates
                        {
                            StartDate = sdate.AddMonths(model.HalfYearFrequencyid - 1),
                            EndDate = edate.AddMonths(model.MonthFrequencyid).AddYears(-1)
                        });


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
                OrgService.OrganizationServiceClient orgclient = new OrgService.OrganizationServiceClient();
                string xmlcompdata = orgclient.getDefaultCompanyDetails(model.companyid);
                DataSet dsCompanyName = new DataSet();
                dsCompanyName.ReadXml(new StringReader(xmlcompdata));
                if (dsCompanyName.Tables.Count > 0)
                {
                    ViewBag.CompanyName = dsCompanyName.Tables[0].Rows[0]["Company_Name"];
                    model.CompanyName = Convert.ToString(dsCompanyName.Tables[0].Rows[0]["Company_Name"]);
                }
                Session["model"] = model;

            }
            return View("_SelectFrequency", model);

        }
        public ActionResult GetBranchReports()
        {
            int BranchID = 0;
            int VendorID = 0;
            var branchid = Request.QueryString["x"];
            var companyname = Request.QueryString["companyname"];
            var vendorid = Request.QueryString["y"];
            var frequencyid = Request.QueryString["frequencyid"];
            var compliancetypeid = Request.QueryString["compliancetypeid"];
            string status = Request.QueryString["n"];
            string sdate = Request.QueryString["sdate"];
            string edate = Request.QueryString["edate"];
            ReportViewModel model = new ReportViewModel();
            model.ComplianceAudit = new ComplianceAudit();
            model.ComplianceAudit.Start_Date = Convert.ToDateTime(sdate);
            model.ComplianceAudit.End_Date = Convert.ToDateTime(edate);
            model.Vendorid = Convert.ToInt32(vendorid);
            model.branchid = Convert.ToInt32(branchid);
            model.frequencyid = Convert.ToInt32(frequencyid);
            model.CompanyName = companyname;
            model.complianceTypeid = Convert.ToInt32(compliancetypeid);
            ReportingService.ReportingServiceClient clientBranch = new ReportingService.ReportingServiceClient();
            string xmlbranchdata = "";
            string xmlbranchACTdata = "";
            if (status == null)
            {
                xmlbranchdata = clientBranch.getBranchReport(BranchID, model.ComplianceAudit.Start_Date,
                    model.ComplianceAudit.End_Date, model.complianceTypeid, model.Vendorid);
                if (BranchID == VendorID)
                {
                    xmlbranchACTdata = clientBranch.getBranchRACTeport(model.branchid, model.Vendorid);
                }
                else
                {
                    xmlbranchACTdata = clientBranch.getBranchRACTeport(model.branchid, model.Vendorid);
                }
                DataSet dsBranchACTReport = new DataSet();
                dsBranchACTReport.ReadXml(new StringReader(xmlbranchACTdata));
                if (dsBranchACTReport.Tables.Count > 0)
                {
                    model.ActList = bindCompliancelist(dsBranchACTReport);
                }
            }
            else
            {
                xmlbranchdata = clientBranch.getBranchStatusReport(model.branchid, status, model.ComplianceAudit.Start_Date,
                    model.ComplianceAudit.End_Date, model.complianceTypeid, model.Vendorid);
                if (model.branchid == model.Vendorid)
                {
                    xmlbranchACTdata = clientBranch.getBranchStatusACTReport(model.branchid, status, model.Vendorid);
                }
                else
                {
                    xmlbranchACTdata = clientBranch.getBranchStatusACTReport(model.branchid, status, model.Vendorid);
                }
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
                dv.RowFilter = "Compliance_Status = 'Compliant'";
                DataTable dtComplianced = dv.ToTable();
                if (dtComplianced.Rows.Count > 0)
                {
                    model.ComplianceStatus = Convert.ToString(dtComplianced.Rows[0]["Compliance_Status"]);
                }
                dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Non Compliant'";
                DataTable dtNonComplianced = dv.ToTable();
                if (dtNonComplianced.Rows.Count > 0)
                {
                    model.NonComplianceStatus = Convert.ToString(dtNonComplianced.Rows[0]["Compliance_Status"]);
                }
                dv = new DataView(dsBranchReport.Tables[0]);
                dv.RowFilter = "Compliance_Status = 'Partially Compliant'";
                DataTable dtPartiallyComplianced = dv.ToTable();
                if (dtPartiallyComplianced.Rows.Count > 0)
                {
                    model.PartiallyComplianceStatus = Convert.ToString(dtPartiallyComplianced.Rows[0]["Compliance_Status"]);
                }
                if (dtComplianced.Rows.Count > 0)
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
            model.Vendorname = Convert.ToString(Session["Name"]);
            //model.branchid =Convert.ToInt32( id);
            model.StartDate = model.ComplianceAudit.Start_Date;
            model.EndDate = model.ComplianceAudit.End_Date;
            ViewBag.StartDate = sdate;
            ViewBag.EndDate = edate;
            ViewBag.CompanyName = model.CompanyName;
            return View("_Report", model);

        }

        public ActionResult BranchReport()
        {
            var companyname = Request.QueryString["companyname"];
            var sdate = Request.QueryString["sdate"];
            var edate = Request.QueryString["edate"];
            ReportViewModel model = new ReportViewModel();

            model.ActList = (List<ComplianceXref>)Session["BranchActList"];
            model.CompliancedRuleList = (List<ComplianceAudit>)Session["BranchCompliancedRuleList"];
            model.NonCompliancedRuleListHighRisk = (List<ComplianceAudit>)Session["BranchNonCompliancedRuleListHighRisk"];
            model.NonCompliancedRuleListMediumRisk = (List<ComplianceAudit>)Session["BranchNonCompliancedRuleListMediumRisk"];
            model.NonCompliancedRuleListLowRisk = (List<ComplianceAudit>)Session["BranchNonCompliancedRuleListLowRisk"];
            model.PartiallyCompliancedRuleList = (List<ComplianceAudit>)Session["BranchPartiallyCompliancedRuleList"];
            ViewBag.Name = Session["Name"];
            ViewBag.CompanyNamePDF = companyname;
            ViewBag.sdate = sdate;
            ViewBag.edate = edate;
            model.branchid = Convert.ToInt32(Request.QueryString["bid"]);
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

            ReportViewModel audit = new ReportViewModel();
            audit.ComplianceAudit = new ComplianceAudit();
            if (Session["model"] != null)
            {
                audit = (ReportViewModel)Session["model"];
            }

            ReportingService.ReportingServiceClient client = new ReportingService.ReportingServiceClient();
            List<Ratio> objratiolist = new List<Ratio>();
            if (audit.branchid > 0 && audit.Vendorid > 0)
            {
                foreach (var date in audit.startEndDates)
                {
                    Ratio objratio = new Ratio();
                    string xmlbranchdata = client.getBranchReport(audit.branchid, date.StartDate, date.EndDate, audit.complianceTypeid, audit.Vendorid);
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
                            int complianced = auditList.Count(x => x.Audit_Status == "Compliant");
                            int non_complianced = auditList.Where(x => x.Audit_Status == "Non Compliant").Count();
                            int partially_complianced = auditList.Where(x => x.Audit_Status == "Partially Compliant").Count();
                            objratio.Complianced = complianced;
                            objratio.Non_Complianced = non_complianced;
                            objratio.Partially_Complianced = partially_complianced;
                        }
                        objratio.StartDate = date.StartDate.ToString("dd/MMM/yyyy");
                        objratio.EndDate = date.EndDate.ToString("dd/MMM/yyyy");
                        objratio.frequencyid = audit.frequencyid;
                        objratio.CompanyName = audit.CompanyName;
                    }
                    objratiolist.Add(objratio);
                }



            }

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
            public string CompanyName { get; set; }
            public int CompanyID { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public int frequencyid { get; set; }
            public List<ComplianceAudit> auditList { get; set; }
            public int monthid { get; set; }
            public int halfyearid { get; set; }
            public int yearid { get; set; }
            public int typeid { get; set; }

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

        [HttpGet]
        public ActionResult getCompany()
        {
            ReportViewModel model = new ReportViewModel();
            model.companyList = new List<SelectListItem>();
            //model.BranchList = new List<SelectListItem>();
            //model.VendorList = new List<Organization>();
            model.frequency = new List<SelectListItem>();
            model.halfYear = new List<SelectListItem>();
            model.quarter = new List<SelectListItem>();
            model.month = new List<SelectListItem>();
            model.yearlist = new List<SelectListItem>();
            model.ComplianceTypeList = new List<SelectListItem>();

            model.yearid = 0;
            model.frequencyid = 0;
            //model.frequency.Add(new SelectListItem() { Text = "Select Frequency", Value = "0" });
            model.frequency.Add(new SelectListItem() { Text = "Yearly", Value = "1" });
            model.frequency.Add(new SelectListItem() { Text = "Half-Yearly", Value = "2" });
            model.frequency.Add(new SelectListItem() { Text = "Quarterly", Value = "3" });
            model.frequency.Add(new SelectListItem() { Text = "Monthly", Value = "4" });
            model.frequency.Add(new SelectListItem() { Text = "Periodic", Value = "5" });

            model.HalfYearFrequencyid = 0;
            model.halfYear.Add(new SelectListItem() { Text = "-- Select --", Value = "0" });

            model.QuarterFrequencyid = 0;
            model.quarter.Add(new SelectListItem() { Text = "-- Select --", Value = "0" });

            model.MonthFrequencyid = 0;
            model.month.Add(new SelectListItem() { Text = "-- Select --", Value = "0" });

            model.ID = 0;
            model.yearlist.Add(new SelectListItem() { Text = "Select Report Type", Value = "0" });
            model.yearlist.Add(new SelectListItem() { Text = "Consolidated", Value = "1" });
            model.yearlist.Add(new SelectListItem() { Text = "Self", Value = "2" });
            model.yearlist.Add(new SelectListItem() { Text = "Vendor", Value = "3" });

            model.yearid = 0;
            model.years = Enumerable.Range(model.yearid, DateTime.Now.Year - (model.yearid - 1)).OrderByDescending(i => i);

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
                    model.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Calender_StartDate"]);
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.companyList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                    }
                    model.yearid = model.StartDate.Year;
                    model.years = Enumerable.Range(model.yearid, DateTime.Now.Year - (model.yearid - 1)).OrderByDescending(i => i);
                    model.yearid = DateTime.Now.Year;
                }

                model.ComplianceTypeList = new List<SelectListItem>();
                xmldata = client.GetAssignedComplianceTypes(model.companyid);
                ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    //model.branchid = Convert.ToInt32(ds.Tables[0].Rows[0]["Org_Hier_ID"]);
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        //model.complianceTypeid = Convert.ToInt32(ds.Tables[0].Rows[0]["Compliance_Type_ID"]);
                        model.ComplianceTypeList.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
                    }
                }

                //model.BranchList = new List<SelectListItem>();
                //xmldata = client.GeSpecifictBranchList(model.companyid);
                //ds = new DataSet();
                //ds.ReadXml(new StringReader(xmldata));
                //if (ds.Tables.Count > 0)
                //{
                //    model.branchid = Convert.ToInt32(ds.Tables[0].Rows[0]["Org_Hier_ID"]);
                //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                //    {
                //        model.BranchList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                //    }
                //}

                //VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                //xmldata = vendorServiceClient.GetAssignedVendorsforBranch(model.branchid);
                //ds = new DataSet();
                //ds.ReadXml(new StringReader(xmldata));
                //if (ds.Tables.Count > 0)
                //{
                //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                //    {
                //        model.VendorList.Add(new Organization { Company_Name = Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Vendor_ID"]), logo = Convert.ToString(row["logo"]) });
                //    }
                //}
                //else
                //{
                //    TempData["Message"] = "No Vendors assigned for the selected branch.";
                //}

            }
            else
                ModelState.AddModelError("", ConfigurationManager.AppSettings["SelectGroupCompany"]);

            return View("_getCompany", model);
        }

        [HttpPost]
        public ActionResult getCompany(ReportViewModel model)
        {

            model.companyList = new List<SelectListItem>();
            //model.BranchList = new List<SelectListItem>();
            //model.VendorList = new List<Organization>();
            model.frequency = new List<SelectListItem>();
            model.halfYear = new List<SelectListItem>();
            model.quarter = new List<SelectListItem>();
            model.month = new List<SelectListItem>();
            model.yearlist = new List<SelectListItem>();

            model.frequency.Add(new SelectListItem() { Text = "Select Frequency", Value = "0" });
            model.frequency.Add(new SelectListItem() { Text = "Yearly", Value = "1" });
            model.frequency.Add(new SelectListItem() { Text = "Half-Yearly", Value = "2" });
            model.frequency.Add(new SelectListItem() { Text = "Quarterly", Value = "3" });
            model.frequency.Add(new SelectListItem() { Text = "Monthly", Value = "4" });
            model.frequency.Add(new SelectListItem() { Text = "Periodic", Value = "5" });

            model.halfYear.Add(new SelectListItem() { Text = "Select Half-Year", Value = "0" });
            model.halfYear.Add(new SelectListItem() { Text = "First Half", Value = "1" });
            model.halfYear.Add(new SelectListItem() { Text = "Second Half", Value = "2" });

            model.quarter.Add(new SelectListItem() { Text = "Select Quarter", Value = "0" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 1", Value = "2" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 2", Value = "3" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 3", Value = "4" });
            model.quarter.Add(new SelectListItem() { Text = "Quarter 4", Value = "5" });

            model.month.Add(new SelectListItem() { Text = "Select Month", Value = "0" });
            model.month.Add(new SelectListItem() { Text = "January", Value = "1" });
            model.month.Add(new SelectListItem() { Text = "February", Value = "2" });
            model.month.Add(new SelectListItem() { Text = "March", Value = "3" });
            model.month.Add(new SelectListItem() { Text = "April", Value = "4" });
            model.month.Add(new SelectListItem() { Text = "May", Value = "5" });
            model.month.Add(new SelectListItem() { Text = "June", Value = "6" });
            model.month.Add(new SelectListItem() { Text = "July", Value = "7" });
            model.month.Add(new SelectListItem() { Text = "August", Value = "8" });
            model.month.Add(new SelectListItem() { Text = "September", Value = "9" });
            model.month.Add(new SelectListItem() { Text = "October", Value = "10" });
            model.month.Add(new SelectListItem() { Text = "November", Value = "11" });
            model.month.Add(new SelectListItem() { Text = "December", Value = "12" });

            model.yearlist.Add(new SelectListItem() { Text = "Select Report Type", Value = "0" });
            model.yearlist.Add(new SelectListItem() { Text = "Consolidated", Value = "1" });
            model.yearlist.Add(new SelectListItem() { Text = "Self", Value = "2" });
            model.yearlist.Add(new SelectListItem() { Text = "Vendor", Value = "3" });

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
                    model.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Calender_StartDate"]);

                    model.companyList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
                }
                //model.yearid = model.StartDate.Year;
                model.years = Enumerable.Range(model.yearid, DateTime.Now.Year - (model.yearid - 1));
            }
            //model.BranchList = new List<SelectListItem>();
            //xmldata = client.GeSpecifictBranchList(model.companyid);
            //ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
            //if (ds.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //    {
            //        model.BranchList.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
            //    }
            //}

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
            //VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
            //xmldata = vendorServiceClient.GetAssignedVendorsforBranch(model.branchid);
            //ds = new DataSet();
            //ds.ReadXml(new StringReader(xmldata));
            //if (ds.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            //    {
            //        model.VendorList.Add(new Organization { Company_Name = Convert.ToString(row["Company_Name"]), Company_Id = Convert.ToInt32(row["Vendor_ID"]), logo = Convert.ToString(row["logo"]) });
            //    }
            //}
            //else
            //{
            //    TempData["Message"] = "No Vendors assigned for the selected branch.";
            //}

            return RedirectToAction("SelectAuditfrequencyForCompany", new { frequencyid = model.frequencyid,
                yearid = model.yearid,
                complianceid = model.complianceTypeid, compid = model.companyid, halfyearid = model.HalfYearFrequencyid,
                monthid = model.MonthFrequencyid, typeid = model.ID
            });
        }

        public ActionResult SelectAuditfrequencyForCompany(int frequencyid, int yearid, int complianceid, int compid,
         int halfyearid, int monthid, int typeid)
        {
            ReportViewModel model = new ReportViewModel();
            model.ComplianceAudit = new ComplianceAudit();
            model.compliance_Types = new List<compliance_type>();
            model.companyid = compid;
            model.frequencyid = frequencyid;
            model.yearid = yearid;
            model.complianceTypeid = complianceid;
            model.HalfYearFrequencyid = halfyearid;
            model.MonthFrequencyid = monthid;
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata = client.GetAssignedComplianceTypes(compid);
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
                xmldata = serviceClient.GetComplainceType(complianceid);
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
                    DateTime sdate = new DateTime(yearid, item.startdate.Month, item.startdate.Day);
                    DateTime edate = new DateTime(yearid, item.enddate.Month, item.enddate.Day);
                    //DateTime sdate = item.startdate;
                    //DateTime edate = item.enddate;
                    int k = 0;
                    if (model.frequencyid == 1)
                    {
                        model.startEndDates.Add(new StartEndDates
                        {
                            StartDate = sdate,
                            EndDate = edate
                        });
                    }
                    else if (model.frequencyid == 2)
                    {
                        if (model.HalfYearFrequencyid == 1)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k - 6)
                            });
                        }
                        else
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 6),
                                EndDate = edate
                            });
                        }
                    }
                    else if (model.frequencyid == 3)
                    {
                        if (model.QuarterFrequencyid == 1)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k + 3)
                            });
                        }
                        else if (model.QuarterFrequencyid == 2)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 3),
                                EndDate = edate.AddMonths(k + 6)
                            });
                        }
                        else if (model.QuarterFrequencyid == 3)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 6),
                                EndDate = edate.AddMonths(k + 9)
                            });
                        }
                        else
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 9),
                                EndDate = edate
                            });
                        }
                    }
                    else if (model.frequencyid == 4)
                    {
                        if (model.MonthFrequencyid == 1)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k),
                                EndDate = edate.AddMonths(k + 1)
                            });
                        }
                        else if (model.MonthFrequencyid == 2)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 2),
                                EndDate = edate.AddMonths(k + 3)
                            });
                        }
                        else if (model.MonthFrequencyid == 3)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 3),
                                EndDate = edate.AddMonths(k + 4)
                            });
                        }
                        else if (model.MonthFrequencyid == 4)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 4),
                                EndDate = edate.AddMonths(k + 5)
                            });
                        }
                        else if (model.MonthFrequencyid == 5)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 5),
                                EndDate = edate.AddMonths(k + 6)
                            });
                        }
                        else if (model.MonthFrequencyid == 6)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 6),
                                EndDate = edate.AddMonths(k + 7)
                            });
                        }
                        else if (model.MonthFrequencyid == 7)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 7),
                                EndDate = edate.AddMonths(k + 8)
                            });
                        }
                        else if (model.MonthFrequencyid == 8)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 8),
                                EndDate = edate.AddMonths(k + 9)
                            });
                        }
                        else if (model.MonthFrequencyid == 9)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 9),
                                EndDate = edate.AddMonths(k + 10)
                            });
                        }
                        else if (model.MonthFrequencyid == 10)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 10),
                                EndDate = edate.AddMonths(k + 11)
                            });
                        }
                        else if (model.MonthFrequencyid == 11)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 11),
                                EndDate = edate.AddMonths(k + 12)
                            });
                        }
                        else if (model.MonthFrequencyid == 12)
                        {
                            model.startEndDates.Add(new StartEndDates
                            {
                                StartDate = sdate.AddMonths(k + 12),
                                EndDate = edate
                            });
                        }
                    }
                    else
                    {

                        model.startEndDates.Add(new StartEndDates
                        {
                            StartDate = sdate.AddMonths(model.HalfYearFrequencyid - 1),
                            EndDate = edate.AddMonths(model.MonthFrequencyid).AddYears(-1)
                        });
                    }
                }

                OrgService.OrganizationServiceClient orgclient = new OrgService.OrganizationServiceClient();
                string xmlcompdata = orgclient.getDefaultCompanyDetails(model.companyid);
                DataSet dsCompanyName = new DataSet();
                dsCompanyName.ReadXml(new StringReader(xmlcompdata));
                if (dsCompanyName.Tables.Count > 0)
                {
                    ViewBag.CompanyName = dsCompanyName.Tables[0].Rows[0]["Company_Name"];
                    model.CompanyName = Convert.ToString(dsCompanyName.Tables[0].Rows[0]["Company_Name"]);
                }
                model.ID = typeid;
                Session["model"] = model;
            }
            return View("_SelectFrequencyForCompany", model);
        }
        public ActionResult GetCompanyData()
        {
            int TotalBranchCount = 0;
            int TotalCompliantBranchCount = 0;
            int TotalNonCompliantBranchCount = 0;
            ReportViewModel audit = new ReportViewModel();
            audit.ComplianceAudit = new ComplianceAudit();
            if (Session["model"] != null)
            {
                audit = (ReportViewModel)Session["model"];
            }
            ReportingService.ReportingServiceClient client = new ReportingService.ReportingServiceClient();
            List<Ratio> objratiolist = new List<Ratio>();
            if (audit.companyid>0)
            {
                if (audit.ID == 1)
                {

                }
                else if (audit.ID == 2)
                {
                    foreach (var date in audit.startEndDates)
                    {
                        Ratio objratio = new Ratio();
                        string xmlbranchcount = client.getBranchCount(audit.companyid);
                        DataSet dsBranchCount = new DataSet();
                        dsBranchCount.ReadXml(new StringReader(xmlbranchcount));
                        if (dsBranchCount.Tables.Count > 0)
                        {
                            TotalBranchCount = dsBranchCount.Tables[0].Rows.Count;
                        }
                        string xmlCompliantbranchcount = client.getCompliantBranchCount(audit.companyid, date.StartDate, date.EndDate, audit.complianceTypeid);
                        DataSet dsCompliantBranchCount = new DataSet();
                        dsCompliantBranchCount.ReadXml(new StringReader(xmlCompliantbranchcount));
                        if (dsCompliantBranchCount.Tables.Count > 0)
                        {
                            TotalCompliantBranchCount = dsCompliantBranchCount.Tables[0].Rows.Count;
                        }
                        string xmlNonCompliantbranchcount = client.getNonCompliantBranchCount(audit.companyid, date.StartDate, date.EndDate, audit.complianceTypeid);
                        DataSet dsNonCompliantBranchCount = new DataSet();
                        dsNonCompliantBranchCount.ReadXml(new StringReader(xmlNonCompliantbranchcount));
                        if (dsNonCompliantBranchCount.Tables.Count > 0)
                        {
                            TotalNonCompliantBranchCount = dsNonCompliantBranchCount.Tables[0].Rows.Count;
                        }


                        int complianced = TotalCompliantBranchCount;
                        int non_complianced = TotalNonCompliantBranchCount;
                        //complianced = 20;
                        //non_complianced = 80;
                        objratio.Complianced = complianced;
                        objratio.Non_Complianced = non_complianced;
                        objratio.StartDate = date.StartDate.ToString("dd/MMM/yyyy");
                        objratio.EndDate = date.EndDate.ToString("dd/MMM/yyyy");
                        objratio.frequencyid = audit.frequencyid;
                        objratio.CompanyName = audit.CompanyName;
                        objratio.CompanyID = audit.companyid;
                        objratio.compliancetypeid = audit.complianceTypeid;
                        objratio.yearid = audit.yearid;
                        objratio.monthid = audit.MonthFrequencyid;
                        objratio.halfyearid = audit.HalfYearFrequencyid;
                        objratio.typeid = audit.ID;
                        objratiolist.Add(objratio);
                    }
                }
                else
                {

                }
            }
            return Json(objratiolist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCompanyReports()
        
        {
            var companyname = Request.QueryString["companyname"];
            var companyid = Request.QueryString["companyid"];
            var frequencyid = Request.QueryString["frequencyid"];
            var compliancetypeid = Request.QueryString["compliancetypeid"];
            string status = Request.QueryString["n"];
            string sdate = Request.QueryString["sdate"];
            string edate = Request.QueryString["edate"];
            string yearid = Request.QueryString["yearid"];
            string monthid = Request.QueryString["monthid"];
            string halfyearid = Request.QueryString["halfyearid"];
            int typeid =Convert.ToInt32(Request.QueryString["typeid"]);
            ReportViewModel model = new ReportViewModel();
            model.CompliantBranchList = new List<SelectListItem>();
            model.NonCompliantBranchList = new List<SelectListItem>();
            if (typeid == 1)
            {

            }
            else if (typeid == 2)
            {
                ReportingService.ReportingServiceClient client = new ReportingService.ReportingServiceClient();
                string xmlbranchcount = client.getBranchCount(Convert.ToInt32(companyid));
                DataSet dsBranchCount = new DataSet();
                dsBranchCount.ReadXml(new StringReader(xmlbranchcount));

                string xmlcompliantbranchcount = client.getCompliantBranchCount
                    (Convert.ToInt32(companyid), Convert.ToDateTime(sdate), Convert.ToDateTime(edate), Convert.ToInt32(compliancetypeid));
                DataSet dsCompliantBranchCount = new DataSet();
                dsCompliantBranchCount.ReadXml(new StringReader(xmlcompliantbranchcount));
                if (dsCompliantBranchCount.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsCompliantBranchCount.Tables[0].Rows)
                    {
                        model.CompliantBranchList.Add
                            (new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                    }
                    ViewBag.ComplianceBranchCount = dsCompliantBranchCount.Tables[0].Rows.Count;
                }

                string xmlNoncompliantbranchcount = client.getNonCompliantBranchCount
                   (Convert.ToInt32(companyid), Convert.ToDateTime(sdate), Convert.ToDateTime(edate), Convert.ToInt32(compliancetypeid));
                DataSet dsNonCompliantBranchCount = new DataSet();
                dsNonCompliantBranchCount.ReadXml(new StringReader(xmlNoncompliantbranchcount));
                if (dsNonCompliantBranchCount.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in dsNonCompliantBranchCount.Tables[0].Rows)
                    {
                        model.NonCompliantBranchList.Add
                            (new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                    }
                    ViewBag.NonComplianceBranchCount = dsNonCompliantBranchCount.Tables[0].Rows.Count;
                }
                Session["CompliantBranchList"] = model.CompliantBranchList;
                Session["NonCompliantBranchList"] = model.NonCompliantBranchList;
            }
            else
            {

            }
            ViewBag.StartDate = sdate;
            ViewBag.EndDate = edate;
            ViewBag.CompanyName = companyname;
            model.StartDate = Convert.ToDateTime(sdate);
            model.EndDate = Convert.ToDateTime(edate);
            model.frequencyid =Convert.ToInt32( frequencyid);
            model.companyid = Convert.ToInt32(companyid);
            model.complianceTypeid = Convert.ToInt32(compliancetypeid);
            model.MonthFrequencyid= Convert.ToInt32(monthid);
            model.HalfYearFrequencyid= Convert.ToInt32(halfyearid);
            model.yearid= Convert.ToInt32(yearid);

           
            return View("_CompanyReport", model);
        }


        public ActionResult CompanyReport()
        {
            var companyname = Request.QueryString["companyname"];
            var sdate = Request.QueryString["sdate"];
            var edate = Request.QueryString["edate"];
            ReportViewModel model = new ReportViewModel();

            model.CompliantBranchList =( List<SelectListItem>) Session["CompliantBranchList"];
            model.NonCompliantBranchList =( List<SelectListItem>)Session["NonCompliantBranchList"];

            ViewBag.CompanyNamePDF = companyname;
            ViewBag.sdate = sdate;
            ViewBag.edate = edate;
            string footer =
               "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of " +
               "[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.ViewAsPdf("_CompanyPDF", model)
            {
                FileName = "firstpdf.pdf",
                CustomSwitches = footer
            };


        }
    }
}