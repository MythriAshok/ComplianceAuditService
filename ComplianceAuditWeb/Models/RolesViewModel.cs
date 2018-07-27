using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compliance.DataObject;

namespace ComplianceAuditWeb.Models
{
    public class RolesViewModel
    {
        public Roles roles { get; set; }

        public List<SelectListItem> Privilege { get; set; }

        public int[] PrivilegeId { get; set; }

    }
}