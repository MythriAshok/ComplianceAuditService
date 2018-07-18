using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;
using ComplianceAuditWeb.Models;
using System.Data;
using System.IO;

namespace ComplianceAuditWeb.Controllers
{
    public class ComplianceManagementController : Controller
    {
        // GET: ComplianceManagement
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateActs()
        {
            ComplianceViewModel model = new ComplianceViewModel();
            model.Countrylist = new List<SelectListItem>();
            model.Countrylist.Add(new SelectListItem() { Text = "--Select Country--", Value = "0" });
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCountries = organizationservice.GetCountryList();           
            DataSet dsCountries = new DataSet();
            dsCountries.ReadXml(new StringReader(strXMLCountries));                
            foreach (System.Data.DataRow row in dsCountries.Tables[0].Rows)
            {
                model.Countrylist.Add(new SelectListItem() { Text = row["Country_Name"].ToString(), Value = row["Country_ID"].ToString() });
            }
            string strXMLStates = organizationservice.GetStateList(0);
            string strXMLCities = organizationservice.GetCityList(0);
            DataSet dsStates = new DataSet();
            DataSet dsCities = new DataSet();
            dsStates.ReadXml(new StringReader(strXMLStates));
            dsCities.ReadXml(new StringReader(strXMLCities));
            if (dsStates.Tables.Count > 0)
            {
                model.Statelist = new List<SelectListItem>();
                model.Statelist.Add(new SelectListItem() { Text = "--Select State--", Value = "0" });
                foreach (System.Data.DataRow row in dsStates.Tables[0].Rows)
                {
                    model.Statelist.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            if (dsCities.Tables.Count > 0)
            {
                model.Citylist = new List<SelectListItem>();
                model.Citylist.Add(new SelectListItem() { Text = "--Select City--", Value = "0" });
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    model.Citylist.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            return View("_insertActs",model);
        }
        [HttpPost]
        public ActionResult CreateActs(ComplianceViewModel model)
        {
            ComplianceXrefService.ComplianceXrefServiceClient client = new ComplianceXrefService.ComplianceXrefServiceClient();
            client.insertActs(model.Compliance);
            return View();
        }
    }
}