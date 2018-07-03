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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LocationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LocationService.svc or LocationService.svc.cs at the Solution Explorer and start debugging.
    public class LocationService : ILocationService
    {
        public void DoWork()
        {
        }
        public int insertBranchLocation(Branch branchlocation)
        {
            int insertresultbranchid = 0;
            try
            {
                BranchHelper helper = new BranchHelper();
                insertresultbranchid = helper.insertupdateBranchLocation(branchlocation, 'I');
            }
            catch
            {
                throw;
            }
            return insertresultbranchid;
        }

        public int updateBranchLocation(Branch branchlocation)
        {
            int updateresultbranchid = 0;
            try
            {
                BranchHelper helper = new BranchHelper();
                updateresultbranchid = helper.insertupdateBranchLocation(branchlocation, 'U');
            }
            catch
            {
                throw;
            }
            return updateresultbranchid;
        }

       
       

    
    }
}
