using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplianceAuditWeb.Models;
using ComplianceService;
using System.Data;
using System.IO;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageIndustryComplianceTypesController : Controller
    {
        // GET: ManageIndustryComplianceTypes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndustryTypeMapwithCompliance()
        {
            ComplianceIndustryViewModel model = new ComplianceIndustryViewModel();
            OrgService.OrganizationServiceClient organizationService = new OrgService.OrganizationServiceClient();
            model.IndustryType = new Compliance.DataObject.IndustryType();
            model.ComplianceType = new Compliance.DataObject.ComplianceType();
            string strXMLCountries = organizationService.GetCountryList();
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));
            model.CountryList = new List<SelectListItem>();
            model.CountryList.Add(new SelectListItem { Text = "-- Select Country --", Value = "" });
            if (dsCountries.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
                {
                    model.CountryList.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
                }
            }

            string strXMLIndustryType = organizationService.GetIndustryType();
            DataSet dsIndustryType = new DataSet();
            dsIndustryType.ReadXml(new StringReader(strXMLIndustryType));
            model.IndustryTypeList = new List<SelectListItem>();
            model.IndustryTypeList.Add(new SelectListItem { Text = "-- Industry Type --", Value = "" });
            if (dsIndustryType.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsIndustryType.Tables[0].Rows)
                {
                    model.IndustryTypeList.Add(new SelectListItem() { Text = row["Industry_Name"].ToString(), Value = row["Industry_Type_ID"].ToString() });
                }
            }

            //string strXMLComplianceTypes = organizationService.getAllComplianceTypes();
            //DataSet dsComplianceTypes = new DataSet();
            //dsComplianceTypes.ReadXml(new StringReader(strXMLIndustryType));
            //model.ComplianceTypeList = new List<SelectListItem>();
            //model.ComplianceTypeList.Add(new SelectListItem { Text = "-- Industry Type --", Value = "" });
            //if (dsComplianceTypes.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow row in dsComplianceTypes.Tables[0].Rows)
            //    {
            //        model.ComplianceTypeList.Add(new SelectListItem() { Text = row["Compliance_Type_Name"].ToString(), Value = row["Compliance_Type_ID"].ToString() });
            //    }
            //}

            return View("_MapComplianceTypes", model);
        }

        public ActionResult IndustryTypeMapwithCompliance(ComplianceIndustryViewModel model)
        {
            OrgService.OrganizationServiceClient organizationService = new OrgService.OrganizationServiceClient();
            int id = organizationService.insertComplianceTypesMappedWithIndustryType(model.ComplianceType, model.IndustryType);
            return View("");
        }
    }
}