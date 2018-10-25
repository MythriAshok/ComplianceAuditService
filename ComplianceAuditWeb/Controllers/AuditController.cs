using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceAuditWeb.Models;
using Compliance.DataObject;
using System.Data;
using System.Configuration;
using System.IO;

namespace ComplianceAuditWeb.Controllers
{
    public class AuditController : Controller
    {
        // GET: Audit
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
            model.CompliancceType = new List<SelectListItem>();

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

                xmldata=client.GetAssignedComplianceTypes(model.companyid);
                ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds.Tables.Count > 0)
                {
                    model.ComplianceTypeid = Convert.ToInt32(ds.Tables[0].Rows[0][""]);
                    foreach (System.Data.DataRow row in ds.Tables[0].Rows)
                    {
                        model.CompliancceType.Add(new SelectListItem { Text = Convert.ToString(row["Compliance_Type_Name"]), Value = Convert.ToString(row["Compliance_Type_ID"]) });
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
    }
}