using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccessService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccessService.svc or AccessService.svc.cs at the Solution Explorer and start debugging.
    public class AccessService : IAccessService
    {
        public void DoWork()
        {
        }
        public int GetLoginData(User user)
        {
            UserHelper userHelper = new UserHelper();
            int LoginId = Convert.ToInt32(userHelper.getLoginData(user));
            return LoginId;
        }

        //public string updatePasswordData(User user)
        //{
        //    string result = null;
        //    UserHelper userHelper = new UserHelper();
        //    result = userHelper.updatePassword(user);
        //    return result;
        //}

        public string getmenulist(int groupid)
        {
            MenusHelper helper = new MenusHelper();
            DataSet ds=helper.getMenus(groupid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
    }
}
