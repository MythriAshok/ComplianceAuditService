using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceService;
using ComplianceAuditWeb.Models;
using Compliance.DataObject;
using System.Data;
using System.IO;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageComplianceAuditController : Controller
    {
        // GET: ManageComplianceAudit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult insertComplianceAudit()
        {
            int AuditorID = 0;
            ComplianceAuditViewModel complianceAuditViewModel = new ComplianceAuditViewModel();
            List<ComplianceAuditViewModel> complianceAuditViewModelsList = new List<ComplianceAuditViewModel>();
            List<CompanyViewModel> companyViewModelsList = new List<CompanyViewModel>();
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            string strxmlCompany= auditServiceClient.getCompanyAllocatedToAuditor(AuditorID);
            DataSet dsCompany = new DataSet();
            dsCompany.ReadXml(new StringReader(strxmlCompany));
            complianceAuditViewModel.complianceBranchMap = new List<SelectListItem>();
            if (dsCompany.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompany.Tables[0].Rows)
                {
                    complianceAuditViewModel.complianceCompanyMap.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }


            string strxmlBranch = auditServiceClient.getBranchAllocatedToAuditor(AuditorID);
            DataSet dsBranch = new DataSet();
            dsBranch.ReadXml(new StringReader(strxmlBranch));
            complianceAuditViewModel.complianceBranchMap = new List<SelectListItem>();
            if (dsBranch.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsBranch.Tables[0].Rows)
                {
                    complianceAuditViewModel.complianceBranchMap.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }



            string strxmlComplianceXref = auditServiceClient.getComplianceXref(AuditorID);
            DataSet dsComplianceXref = new DataSet();
            dsComplianceXref.ReadXml(new StringReader(strxmlComplianceXref));
            complianceAuditViewModel.complianceXrefList = new List<SelectListItem>();
            if (dsComplianceXref.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsComplianceXref.Tables[0].Rows)
                {
                    complianceAuditViewModel.complianceXrefList.Add(new SelectListItem() { Text = row["Comp_Category"].ToString(), Value = row["Compliance_Xref_ID"].ToString() });
                }
            }


            return View(complianceAuditViewModelsList);
        }

        [HttpPost]
        public ActionResult insertComplianceAudit(ComplianceAuditViewModel complianceAuditViewModel)
        {

            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
           // auditServiceClient.insertComplianceAudit(complianceAuditViewModel.ComplianceAuditLists);
            return View();
        }

        public ActionResult getComplianceAudit()
        {
            ComplianceAuditViewModel complianceAuditViewModel = new ComplianceAuditViewModel();
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            return View();
        }

        [HttpPost]

        public ActionResult getComplianceAudit(ComplianceViewModel complianceViewModel)
        {
            int ComplianceAuditID = 0;
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            auditServiceClient.getComplianceAudit(ComplianceAuditID);
            return View();
        }

        public ActionResult updateComplianceAudit(int ComplianceAuditID)
        {
            ComplianceAuditViewModel complianceAuditViewModel = new ComplianceAuditViewModel();
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            string strxmlolddata = auditServiceClient.getComplianceAudit(ComplianceAuditID).ToString();
            DataSet dsOldData = new DataSet();
            dsOldData.ReadXml(new StreamReader(strxmlolddata));
            complianceAuditViewModel.ComplianceAuditLists = new List<ComplianceAudit>();
            complianceAuditViewModel.complianceAudit = new ComplianceAudit();
            complianceAuditViewModel.complianceAudit.Compliance_Audit_Id = ComplianceAuditID;
            complianceAuditViewModel.complianceAudit.Auditor_Id = ds
        }
    }
}