using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Controllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GeneratePDF(List<ComplianceAudit> model)
        {

            string footer =
                "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of " +
                "[toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new Rotativa.ViewAsPdf(model)
            {
                FileName = "firstpdf.pdf",
                CustomSwitches = footer
            };
        }
    }
}