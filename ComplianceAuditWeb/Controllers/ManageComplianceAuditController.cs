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
            string strxmlCompany= auditServiceClient.getCompany(AuditorID);
            DataSet dsCompany = new DataSet();
            dsCompany.ReadXml(new StringReader(strxmlCompany));
            complianceAuditViewModel.complianceBranchMap = new List<SelectListItem>();
            if (dsCompany.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCompany.Tables[0].Rows)
                {
                    complianceAuditViewModel.complianceBranchMap.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
                }
            }
          
            return View(complianceAuditViewModel);
        }

        [HttpPost]
        public ActionResult insertComplianceAudit(ComplianceAuditViewModel complianceAuditViewModel)
        {

            AuditService.AuditServiceClient auditServiceClient = new AuditService.AuditServiceClient();
           // auditServiceClient.insertComplianceAudit(complianceAuditViewModel.ComplianceAuditLists);
            return View();
        }
    }
}