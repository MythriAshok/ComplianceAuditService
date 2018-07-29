using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataAccess;
using Compliance.DataObject;
using System.Data;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ComplianceXrefService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ComplianceXrefService.svc or ComplianceXrefService.svc.cs at the Solution Explorer and start debugging.
    public class ComplianceXrefService : IComplianceXrefService
    {
        public int insertActs(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = true;
            compliance.level = 1;
            compliance.Comp_Category = "Act";
            compliance.Comp_Order = 1;
            compliance.Version = 1;
            compliance.Compliance_Parent_ID = 0;
            return helper.insertupdateComplianceXref(compliance,'I');
        }
        public int insertSection(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = true;
            compliance.level = 2;
            compliance.Comp_Category = "Section";
            compliance.Comp_Order = 2;
            compliance.Version = 1;
            return helper.insertupdateComplianceXref(compliance,'I');
        }
        public int insertRules(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = false;
            compliance.level = 3;
            compliance.Comp_Category = "Rule";
            compliance.Version = 1;
            return helper.insertupdateComplianceXref(compliance,'I');
        }
        public int UpdateActs(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            return helper.insertupdateComplianceXref(compliance,'U');
        }
        public int UpdateRules(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            return helper.insertupdateComplianceXref(compliance,'U');
        }
        public string GetActs()
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds=helper.getAct();
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();            
        }
        public string GetSections(int parentid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getSection(parentid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
        public string GetRules(int parentid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getRules(parentid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }

       public int GetAuditorId(int Branchid)
       {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            return helper.getAuditorforBranch(Branchid);
       }

       public bool inseretActandRuleforBranch(int orgid,int[] ruleid,int userid)
        {
            bool res = false;
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            foreach(int id in ruleid)
            {                
                res=helper.insertActAndRuleforBranch(orgid,id,'I', userid);
            }
            return res;
            
        }

        public string getRuleforBranch(int sectionid,int branchid)
        {
            return Bindruleforbranch(sectionid, branchid);
        }

        private string Bindruleforbranch(int sectionid, int branchid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getRuleforBranch(sectionid,branchid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
    }
}
