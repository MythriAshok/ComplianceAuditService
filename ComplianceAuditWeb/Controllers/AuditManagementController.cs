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

        public ActionResult addComplianceAudit (string selectedcompanyid)
        {
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
            foreach (System.Data.DataRow row in dsComplianceXrefData.Tables[0].Rows)
            {
                AuditViewModel auditViewModelforacts = new AuditViewModel

                {
                     
                
                    OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
                    CompanyName = row["Company_Name"].ToString(),
                    IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

               


            };
                auditViewModelsList.Add(auditViewModelforacts   );
                auditViewModelsList.Add(auditViewModel);
            }
            return View(auditViewModelsList);
        }

        [HttpPost]
        public ActionResult addComplianceAudit(List<AuditViewModel> auditViewModelsList)
        {
            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
            auditServiceClient.insertComplianceAudit(auditViewModelsList);
            return View();
        }
    }
}