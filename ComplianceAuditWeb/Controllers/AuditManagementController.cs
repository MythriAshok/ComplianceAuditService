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

namespace ComplianceAuditWeb.Controllers
{
    public class AuditManagementController : Controller
    {
        // GET: AuditManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addComplianceAudit(string selectedcompanyid)
        {
            selectedcompanyid = "17";
            int ComplianceBrachID = 18;
            int AuditorID = 1;
            ComplianceAudit complianceAudit = new ComplianceAudit();
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            List<AuditViewModel> auditViewModelsList = new List<AuditViewModel>();

            AuditViewModel auditViewModel = new AuditViewModel();
            string strxmlCompany = auditServiceClient.getAllCompanyBrnachAssignedtoAuditor(AuditorID);
            DataSet dsCompany = new DataSet();
            dsCompany.ReadXml(new StringReader(strxmlCompany));


            auditViewModel.MappedCompany = new List<SelectListItem>();

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


            string strxmlComplianceData = auditServiceClient.getComplianceXref(ComplianceBrachID);
            DataSet dsComplianceXrefData = new DataSet();
            dsComplianceXrefData.ReadXml(new StringReader(strxmlComplianceData));
            auditViewModel.complianceXrefList = new List<ComplianceXref>();
            auditViewModel.ComplianceXrefData = new ComplianceXref();
            foreach (System.Data.DataRow row in dsComplianceXrefData.Tables[0].Rows)
            {

                auditViewModel.complianceXrefList.Add(new ComplianceXref() {
                    Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]),
                    Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]),
                    Compliance_Title = Convert.ToString(row["Compliance_Title"]),
                    Comp_Category= Convert.ToString(row["Comp_Category"])
                });

                //auditViewModel.ComplianceXrefData.Compliance_Xref_ID = Convert.ToInt32(row["Compliance_Xref_ID"]);
                //auditViewModel.ComplianceXrefData.Compliance_Parent_ID = Convert.ToInt32(row["Compliance_Parent_ID"]);
                //auditViewModel.ComplianceXrefData.Compliance_Title = Convert.ToString(row["Compliance_Title"]);
                //auditViewModel.ComplianceXrefData.Comp_Category = Convert.ToString(row["Comp_Category"];



                auditViewModelsList.Add(auditViewModel);
                ViewBag.List = auditViewModelsList;
                }
            
            return View("_complianceAuditing", ViewBag.List);

        }

        [HttpPost]
        public ActionResult addComplianceAudit(List<AuditViewModel> auditViewModelsList)
        {
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
           // auditServiceClient.insertComplianceAudit(auditViewModelsList);
            return View();
        }
    }
}