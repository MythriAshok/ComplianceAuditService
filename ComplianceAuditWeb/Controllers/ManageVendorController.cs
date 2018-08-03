using ComplianceAuditWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using System.Data;
using System.IO;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageVendorController : Controller
    {
        // GET: ManageVendor
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddVendor()
        {
            
            int id = 0;
            VendorViewModel vendorViewModel = new VendorViewModel();
            VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
            OrgService.OrganizationServiceClient organizationServiceClient = new OrgService.OrganizationServiceClient();
            vendorViewModel.vendor = new VendorMaster();
            vendorViewModel.vendor.VendorID = 0;

            string strXMLGroupCompanyList = organizationServiceClient.GetGroupCompaniesList();
            DataSet dsGroupCompanyList = new DataSet();
            dsGroupCompanyList.ReadXml(new StringReader(strXMLGroupCompanyList));
            vendorViewModel.GroupCompaniesList = new List<SelectListItem>();
            vendorViewModel.GroupCompaniesList.Add(new SelectListItem { Text = "--Select Group Company--", Value = "0" });

            foreach (System.Data.DataRow row in dsGroupCompanyList.Tables[0].Rows)
            {
                vendorViewModel.GroupCompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            }


            string strXMLCompanyList = organizationServiceClient.GeSpecifictCompaniesList(id);
            DataSet dsCompanyList = new DataSet();
            dsCompanyList.ReadXml(new StringReader(strXMLCompanyList));
            vendorViewModel.CompaniesList = new List<SelectListItem>();
            vendorViewModel.CompaniesList.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });

            foreach (System.Data.DataRow row in dsCompanyList.Tables[0].Rows)
            {
                vendorViewModel.CompaniesList.Add(new SelectListItem() { Text = row["Company_Name"].ToString(), Value = row["Org_Hier_ID"].ToString() });
            }


            



           
            return View("_Vendor", vendorViewModel);
        }
        [HttpPost]
        public ActionResult AddVendor(VendorViewModel vendorViewModel)
        {
            if (ModelState.IsValid)
            {
                VendorService.VendorServiceClient vendorServiceClient = new VendorService.VendorServiceClient();
                OrgService.OrganizationServiceClient organizationClient = new OrgService.OrganizationServiceClient();
                vendorViewModel.vendor.OrgCompanyID= vendorViewModel.CompanyID;
                vendorViewModel.vendor.IsActive = true;
                vendorViewModel.vendor.IsDelete = false;
                vendorViewModel.vendor.UserID = 1;
                int id = Convert.ToInt32(vendorServiceClient.insertVendor(vendorViewModel.vendor));
                vendorViewModel.vendor.VendorID = id;
                if (id != 0)
                {
                    Session["VendorName"] = vendorViewModel.vendor.VendorName;
                    Session["VendorID"] = vendorViewModel.vendor.VendorID;
                    Session["CompanyID"] = vendorViewModel.vendor.OrgCompanyID;
                    return RedirectToAction("AboutVendor");
                }
                else
                {
                    return View("View");
                }
            }
            else
            {
                return View("_Vendor");
            }
        }

    //    [HttpGet]
    //    public ActionResult AboutVendor()
    //    {
    //        AboutCompanyViewModel aboutCompanyViewModel = new AboutCompanyViewModel();
    //        aboutCompanyViewModel.CompanyID = Convert.ToInt32(Session["CompanyID"]);
    //        aboutCompanyViewModel.CompanyDescription = Convert.ToString(Session["CompanyDescription"]);
    //        aboutCompanyViewModel.CompanyName = Convert.ToString(Session["CompanyName"]);
    //        aboutCompanyViewModel.ParentCompanyID = Convert.ToInt32(Session["ParentCompanyID"]);
    //        return View("_AboutChildCompany", aboutCompanyViewModel);
    //    }

    //    [HttpPost]
    //    public ActionResult AboutBranch(AboutCompanyViewModel aboutCompanyViewModel)
    //    {
    //        Session["CompanyID"] = aboutCompanyViewModel.CompanyID;
    //        Session["CompanyName"] = aboutCompanyViewModel.CompanyName;
    //        Session["ParentCompanyID"] = aboutCompanyViewModel.ParentCompanyID;
    //        return RedirectToAction("BranchList");
    //    }

    //    public ActionResult BranchList()
    //    {
    //        int CompanyID = Convert.ToInt32(Session["ParentCompanyID"]);
    //        List<ListOfGroupCompanies> branchlist = new List<ListOfGroupCompanies>();
    //        OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
    //        string strxmlCompanies = organizationservice.GeSpecifictBranchList(CompanyID);

    //        DataSet dsSpecificBranchList = new DataSet();
    //        dsSpecificBranchList.ReadXml(new StringReader(strxmlCompanies));
    //        foreach (System.Data.DataRow row in dsSpecificBranchList.Tables[0].Rows)
    //        {
    //            ListOfGroupCompanies listOfBranch = new ListOfGroupCompanies
    //            {
    //                OrganizationID = Convert.ToInt32(row["Org_Hier_ID"]),
    //                CompanyName = row["Company_Name"].ToString(),
    //                // ParentCompanyID = Convert.ToInt32(row["Parent_Company_ID"])
    //                IsActive = Convert.ToBoolean(Convert.ToInt32(row["Is_Active"]))

    //            };
    //            branchlist.Add(listOfBranch);

    //        }
    //        return View("_Branchdashboard", branchlist);
    //    }

    }



}
