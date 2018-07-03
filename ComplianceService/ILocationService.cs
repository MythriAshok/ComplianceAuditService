﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILocationService" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        int insertBranchLocation(Branch branchlocation);
        int updateBranchLocation(Branch branchlocation);
    }
}
