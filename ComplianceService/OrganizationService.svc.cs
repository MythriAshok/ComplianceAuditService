using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrganizationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrganizationService.svc or OrganizationService.svc.cs at the Solution Explorer and start debugging.
    public class OrganizationService : IOrganizationService
    {
        public void DoWork()
        {
        }
        public int insertOrganization(Organization org)
        {
            int insertorgid = 0;
            try
            {
                OrganizationHelper helper = new OrganizationHelper();
                insertorgid = helper.insertupdateOrganizationHier(org, 'I');
            }
            catch
            {
                throw;
            }
            return insertorgid;
        }
        public int updateOrganization(Organization org)
        {
            int insertorgid = 0;
            try
            {
                OrganizationHelper helper = new OrganizationHelper();
                insertorgid = helper.insertupdateOrganizationHier(org, 'I');
            }
            catch
            {
                throw;
            }
            return insertorgid;
        }
    }
}
