using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceService;
using Compliance.DataObject;
using ComplianceAuditWeb.Models;
using System.Data;
using System.IO;
using System.Configuration;

namespace ComplianceAuditWeb.Controllers
{
    public class AuditManagementController : Controller
    {
        // GET: AuditManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addCompanyBranch()
        {
            string selectedcompanyid = "0";

            int AuditorID = 1;
            ComplianceAudit complianceAudit = new ComplianceAudit();
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            List<AuditViewModel> auditViewModelsList = new List<AuditViewModel>();

            AuditViewModel auditViewModel = new AuditViewModel();
            string strxmlCompany = auditServiceClient.getAllCompanyBrnachAssignedtoAuditor(AuditorID);
            DataSet dsCompany = new DataSet();
            dsCompany.ReadXml(new StringReader(strxmlCompany));


            auditViewModel.MappedCompany = new List<SelectListItem>();

            auditViewModel.MappedCompany.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });

            if (dsCompany.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompany.Tables[0].Rows)
                {
                    if (row["level"].ToString() == "2")
                    {
                        auditViewModel.MappedCompany.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                    }
                }
            }

            string strxmlBranch = auditServiceClient.getAllCompanyBrnachAssignedtoAuditor(AuditorID);
            DataSet dsBranch = new DataSet();
            dsBranch.ReadXml(new StringReader(strxmlBranch));

            auditViewModel.MappedBranch = new List<SelectListItem>();
            auditViewModel.MappedBranch.Add(new SelectListItem { Text = "--Select Branch--", Value = "0" });


            if (dsCompany.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompany.Tables[0].Rows)
                {
                    if (row["Parent_Company_ID"].ToString() == selectedcompanyid)
                    {
                        auditViewModel.MappedBranch.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                    }
                }
            }
            auditViewModel.complianceAudit = new ComplianceAudit();
            auditViewModel.complianceAudit.Auditor_Id = AuditorID;
            return View(auditViewModel);
        }
        [HttpPost]
        public ActionResult addCompanyBranch(AuditViewModel auditViewModel)
        {

            Session["ComplianceBranchID"] = auditViewModel.complianceAudit.Company_ID;
            Session["AuditorID"] =auditViewModel.complianceAudit.Auditor_Id;
            Session["OrgHierID"] = auditViewModel.complianceAudit.Org_Hier_Id;
            return RedirectToAction("addComplianceAudit");
        }
        [HttpGet]
        public ActionResult addComplianceAudit()
        {
            //int ComplianceBrachID = 18;
            int ComplianceBrachID =Convert.ToInt32( Session["ComplianceBranchID"]);
            AuditViewModel auditViewModel = new AuditViewModel();
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            List<AuditViewModel> auditViewModelsList = new List<AuditViewModel>();

            auditViewModel.complianceAudit = new ComplianceAudit();

            string strxmlComplianceData = auditServiceClient.getComplianceXref(ComplianceBrachID);
            DataSet dsComplianceXrefData = new DataSet();
            dsComplianceXrefData.ReadXml(new StringReader(strxmlComplianceData));

            auditViewModel.complianceXrefList = new List<ComplianceXref>();
            auditViewModel.Section = new List<ComplianceXref>();
            auditViewModel.Rules = new List<ComplianceXref>();

            auditViewModel.ComplianceXrefData = new ComplianceXref();


            foreach (System.Data.DataRow row in dsComplianceXrefData.Tables[0].Rows)
            {
               // int id = (row["id"] == DBNull.Value) ? 0 : Convert.ToInt32(row["id"]);

                if (row["level"].ToString() == "1")
                {
                    auditViewModel.complianceXrefList.Add(new ComplianceXref()
                    {
                        Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]),
                        Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]),
                        Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                        Comp_Category = Convert.ToString(row["Comp_Category"]),
                        // Comp_Description = Convert.ToString(row["Comp_Description"]),
                        //compl_def_consequence = Convert.ToString(row["compl_def_consequence"]),
                        Country_ID = Convert.ToInt32(row["Country_ID"]),
                        City_ID = Convert.ToInt32(row["City_ID"]),
                        Effective_End_Date = Convert.ToDateTime(row["Effective_Start_Date"]),
                        Effective_Start_Date = Convert.ToDateTime(row["Effective_End_Date"]),
                        //Form = Convert.ToString(row["Form"]),
                        //level = Convert.ToInt32(row["level"]),
                        State_ID = Convert.ToInt32(row["State_ID"]),
                        Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        //Is_Best_Practice = Convert.ToBoolean(Convert.ToInt32(row["Is_Best_Practice"])),
                        Risk_Category = Convert.ToString(row["Risk_Category"]),
                        Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                        //Recurrence = Convert.ToString(row["Recurrence"]),
                        Risk_Description = Convert.ToString(row["Risk_Description"]),
                      //  Audit_Type = Convert.ToString(row["Audit_Type"]),
                        //Type = Convert.ToString(row["Type"]),
                    });
                }
                else if (row["level"].ToString() == "2")
                {
                    auditViewModel.Section.Add(new ComplianceXref()
                    {
                        Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]),
                        Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]),
                        Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                        Comp_Category = Convert.ToString(row["Comp_Category"]),
                        Comp_Description = Convert.ToString(row["Comp_Description"]),
                        compl_def_consequence = Convert.ToString(row["compl_def_consequence"]),
                        Country_ID = Convert.ToInt32(row["Country_ID"]),
                        City_ID = Convert.ToInt32(row["City_ID"]),
                        Effective_End_Date = Convert.ToDateTime(row["Effective_Start_Date"]),
                        Effective_Start_Date = Convert.ToDateTime(row["Effective_End_Date"]),
                        Form = Convert.ToString(row["Form"]),
                        level = Convert.ToInt32(row["level"]),
                        State_ID = Convert.ToInt32(row["State_ID"]),
                        Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        Is_Best_Practice = Convert.ToBoolean(Convert.ToInt32(row["Is_Best_Practice"])),
                        Risk_Category = Convert.ToString(row["Risk_Category"]),
                        Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                        Recurrence = Convert.ToString(row["Recurrence"]),
                        Risk_Description = Convert.ToString(row["Risk_Description"]),
                        Type = Convert.ToString(row["Type"]),
                       // Audit_Type = Convert.ToString(row["Audit_Type"]),



                    });
                }
                else
                {
                    auditViewModel.Rules.Add(new ComplianceXref()
                    {

                        Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]),
                        Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]),
                        Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                        Comp_Category = Convert.ToString(row["Comp_Category"]),
                        Comp_Description = Convert.ToString(row["Comp_Description"]),
                        compl_def_consequence = Convert.ToString(row["compl_def_consequence"]),
                        Country_ID = Convert.ToInt32(row["Country_ID"]),
                        City_ID = Convert.ToInt32(row["City_ID"]),
                        Effective_End_Date = Convert.ToDateTime(row["Effective_Start_Date"]),
                        Effective_Start_Date = Convert.ToDateTime(row["Effective_End_Date"]),
                        Form = Convert.ToString(row["Form"]),
                        level = Convert.ToInt32(row["level"]),
                        State_ID = Convert.ToInt32(row["State_ID"]),
                        Is_Active = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"])),
                        Is_Best_Practice = Convert.ToBoolean(Convert.ToInt32(row["Is_Best_Practice"])),
                        Risk_Category = Convert.ToString(row["Risk_Category"]),
                        Last_Updated_Date = Convert.ToDateTime(row["Last_Updated_Date"]),
                        Recurrence = Convert.ToString(row["Recurrence"]),
                        Risk_Description = Convert.ToString(row["Risk_Description"]),
                        Type = Convert.ToString(row["Type"]),
                      //  Audit_Type = Convert.ToString(row["Audit_Type"]),

                    });
                }
            }
            auditViewModel.complianceAuditList = new List<ComplianceAudit>();
            //    auditViewModelsList.Add(auditViewModel);
            // ViewBag.List = auditViewModelsList;            

            foreach (var item in auditViewModel.Rules)
            {
                auditViewModel.complianceAuditList.Add(new ComplianceAudit { Compliance_Xref_Id = item.Compliance_Xref_ID });
            }

            return View("_complianceAuditing - Copy - Copy (2)", auditViewModel);
        }
        [HttpPost]
        public ActionResult addComplianceAudit(FormCollection formCollection,HttpPostedFileBase file)
        {
          
            if (ModelState.IsValid)
            {
                //AuditViewModel auditViewModel = new AuditViewModel();
                //auditViewModel.complianceAudit = new ComplianceAudit();
                //for(int i =0; i< formCollection.Count; i++ )
                //{
                //    var str = formCollection["complianceAuditList[1].Audit_Status"];
                //}

                //DataTable dt = new DataTable();
                //DataSet ds = new DataSet();
                int counter = 0;
                List<ComplianceAudit> auditdata = new List<ComplianceAudit>();
                ComplianceAudit audit = null;
                //foreach(var item in formCollection)
                //{
                //}
                int key = formCollection.Count;
                string str = Convert.ToString(key - 1);

                int rulecount =Convert.ToInt32(Session["rulecount"]);
              //  int rulecount1 = Convert.ToInt32(formCollection[str]);
                for (int index = 1; index <= rulecount; index++)

                //{
                //    if (formCollection[index].ToString().Contains("complianceAuditList"))
                {
                    //if (formCollection[index].Contains("complianceAuditList"))
                    //{
                    audit = new ComplianceAudit();
                    AuditViewModel auditm = new AuditViewModel();
                    auditm.complianceAudit = new ComplianceAudit();
                    //string str = formCollection["complianceAuditList["+ counter + "].Audit_Status"];
                    // auditm.complianceAudit.Audit_Status = formCollection["complianceAuditList[" + counter + "].Audit_Status"];
                    audit.Audit_Status = formCollection["complianceAuditList[" + index + "].Audit_Status"];
                    audit.Audit_Date = Convert.ToDateTime(formCollection["complianceAuditList[" + index + "].Audit_Date"]);
                    audit.Audit_Remarks = formCollection["complianceAuditList[" + index + "].Audit_Remarks"];
                    audit.Penalty_nc = formCollection["complianceAuditList[" + index + "].Penalty_nc"];
                    audit.Compliance_Xref_Id = Convert.ToInt32(formCollection["complianceAuditList[" + index + "].Compliance_Xref_ID"]);
                    audit.Auditor_Id = Convert.ToInt32(Session["AuditorID"]);//1;// Convert.ToInt32(formCollection["complianceAuditList[" + counter + "].Auditor_ID"]);
                    if (file != null)
                    {
                        CommonController common = new CommonController();
                        audit.Audit_ArteFacts = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString()), Path.GetFileName(file.FileName));
                        string message = common.UploadFile(file, filePath);
                       // ModelState.AddModelError("org_hier.logo", message);
                    }

                    audit.Audit_ArteFacts = formCollection["complianceAuditList[" + index + "].Audit_ArteFacts"];
                    audit.Compliance_Audit_Id =  Convert.ToInt32(formCollection["complianceAuditList[" + index + "].Compliance_Audit_Id"]);
                   // audit.Compliance_Options_Id = 1;// Convert.ToInt32(formCollection["complianceAuditList[" + counter + "].Compliance_Options_Id"]);
                    audit.Compliance_Schedule_Instance = Convert.ToInt32(formCollection["complianceAuditList[" + index + "].Compliance_Schedule_Instance"]);
                    // audit.Is_Active = Convert.ToBoolean(formCollection["complianceAuditList[" + counter + "].Compliance_Schedule_Instance"]);
                    //audit.Last_Update_dDate = Convert.ToDateTime(formCollection["complianceAuditList[" + counter + "].Compliance_Schedule_Instance"]);
                    audit.Org_Hier_Id =Convert.ToInt32( Session["ComplianceBranchID"]);// 17; // Convert.ToInt32(formCollection["complianceAuditList[" + counter + "].Compliance_Schedule_Instance"]);
                    audit.Reviewer_Comments = formCollection["complianceAuditList[" + index + "].Compliance_Schedule_Instance"];
                    audit.Reviewer_Id = Convert.ToInt32(formCollection["complianceAuditList[" + index + "].Compliance_Schedule_Instance"]);
                    audit.User_Id = 1;// Convert.ToInt32(formCollection["complianceAuditList[" + counter + "].Compliance_Schedule_Instance"]);
                    audit.Version = Convert.ToInt32(formCollection["complianceAuditList[" + index + "].Compliance_Schedule_Instance"]);
                    audit.Is_Active = true;//formCollection["complianceAuditList[" + index + "].Is_Active"];

                    //OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    //CompanyName = row["Company_Name"].ToString(),
                    //IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

                    auditdata.Add(audit);

                    //counter++;

                    //}





                }
                bool result = false;
                AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
                string compliancedata = Convert.ToString(auditServiceClient.insertComplianceAudit(auditdata.ToArray()));
                if(compliancedata!= null)
                {
                    result = true; 
                }
                else
                {
                    result = false;
                }
              
            }
            return View();

        }
    }
}



