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
    public class CommonController : Controller
    {
        // GET: Common

        public JsonResult getstate(string countryid)
        {
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            int ID = Convert.ToInt32(countryid);
            List<SelectListItem> state = new List<SelectListItem>();
            string strXMLStates = organizationservice.GetStateList(ID);
            DataSet dsstate = new DataSet();
            dsstate.ReadXml(new StringReader(strXMLStates));
            if (dsstate.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsstate.Tables[0].Rows)
                {
                    state.Add(new SelectListItem() { Text = row["State_Name"].ToString(), Value = row["State_ID"].ToString() });
                }
            }
            return Json(state, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getcity(string stateid)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            int ID = Convert.ToInt32(stateid);
            OrgService.OrganizationServiceClient organizationservice = new OrgService.OrganizationServiceClient();
            string strXMLCities = organizationservice.GetCityList(ID);
            DataSet dsCities = new DataSet();
            dsCities.ReadXml(new StringReader(strXMLCities));
            if (dsCities.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in dsCities.Tables[0].Rows)
                {
                    cities.Add(new SelectListItem() { Text = row["City_Name"].ToString(), Value = row["City_ID"].ToString() });
                }
            }
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getbranch(string compid)
        {
            List<SelectListItem> branch = new List<SelectListItem>();
            int ID = Convert.ToInt32(compid);
            OrgService.OrganizationServiceClient client = new OrgService.OrganizationServiceClient();
            string xmldata = client.GetBranchList();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            branch = new List<SelectListItem>();
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                branch.Add(new SelectListItem { Text = Convert.ToString(row["Company_Name"]), Value = Convert.ToString(row["Org_Hier_ID"]) });
            }

            return Json(branch, JsonRequestBehavior.AllowGet);
        }

    }
}