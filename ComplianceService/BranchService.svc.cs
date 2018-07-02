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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BranchService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BranchService.svc or BranchService.svc.cs at the Solution Explorer and start debugging.
    public class BranchService : IBranchService
    {
        public void DoWork()
        {
        }

        public int insertBranchLocation(BranchHelper helper, char Flag)
        {
            int insertresultbranchid = 0;
            try
            {

                if (helper != null)
                {
                    Branch branch = new Branch();
                    Flag = 'I';
                    insertresultbranchid =  helper.insertupdateBranchLocation(branch, Flag);
                }
            }
            catch
            {
                throw;
            }
            return insertresultbranchid;
        }

        public int updateBranchLocation(BranchHelper helper, char Flag)
        {
            int updateresultbranchid = 0;
            try
            {

                if (helper != null)
                {
                    Branch branch = new Branch();
                    Flag = 'U';
                    updateresultbranchid = helper.insertupdateBranchLocation(branch, Flag);
                }
            }
            catch
            {
                throw;
            }
            return updateresultbranchid;
        }
    }
}

        

    

