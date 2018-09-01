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
        //public int insertSection(ComplianceXref compliance)
        //{
        //    ComplianceXrefHelper helper = new ComplianceXrefHelper();
        //    compliance.Is_Active = true;
        //    compliance.Is_Header = true;
        //    compliance.level = 2;
        //    compliance.Comp_Category = "Section";
        //    compliance.Comp_Order = 2;
        //    compliance.Version = 1;
        //    return helper.insertupdateComplianceXref(compliance,'I');
        //}
        public int insertRules(ComplianceXref compliance)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            compliance.Is_Active = true;
            compliance.Is_Header = false;
            compliance.level = 2;
            //compliance.Comp_Category = "Rule";
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
        public string GetActs(int complianceid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds=helper.getAct(complianceid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();            
        }
        public string Getlineitems(int parentid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getlineitems(parentid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
        public string GetSpecificsection(int complianceid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getSpecifiySection(complianceid);
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

       public bool inseretActandRuleforBranch(int orgid,int[] ruleid,int userid,int vendorid)
        {
            bool res = false;
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            foreach(int id in ruleid)
            {                
                res=helper.insertActAndRuleforBranch(orgid,id, userid,vendorid,DateTime.Now.Year,DateTime.Now,DateTime.Now);
            }
            return res;
            
        }

        public string getRuleforBranch(int branchid,int vendorid)
        {
            return Bindruleforbranch( branchid,vendorid);
        }

        private string Bindruleforbranch( int branchid,int vendorid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getRuleforBranch(branchid,vendorid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }

        public bool DeleteRuleforBranch(int orgid)
        {
            bool res = false;
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
           
                res = helper.deleteComlianceXref(orgid);
            
            return res;
        }

        public string GetComplaince(int Audit_Type_ID)
        {
            return BindCompaliance(Audit_Type_ID);
        }
        private string BindCompaliance(int Audit_Type_ID)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getComlianceXref(Audit_Type_ID);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();

        }

       public string GetcomplianceonType(int Audit_Type_Id, int countryId, int StateId, int cityId,int flag)
        {
            return BindCompalianceType(Audit_Type_Id,countryId,StateId,cityId,flag);
        }

        private string BindCompalianceType(int Audit_Type_Id, int countryId, int StateId, int cityId,int flag)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getComlianceXrefonType(Audit_Type_Id, countryId, StateId, cityId,flag);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();

        }
        public string GetSpecificComplaince(int complianceId)
        {
            return bindspecificcompliance(complianceId);
        }
        private string bindspecificcompliance(int complianceid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.getSpecificcompliance(complianceid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }
        public string GetComplainceType()
        {
            return bindcompliancetype();
        }
        private string bindcompliancetype()
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.GetComplianceType();
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }

      public  bool insertxreftypemapping(int[] xrefid, int compliancetypeid)
        {
            bool res = false;
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            foreach (var id in xrefid)
            {
                 res = helper.insertxreftypemapping(id, compliancetypeid,"Act");
            }
            return res;
        }

       public bool deletexreftypemapping(int compliancetypeid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            bool res = helper.deletexreftypemapping(compliancetypeid);
            return res;
        }

        public string GetXrefComplainceTypemapping(int compliancetypeid)
        {
            return bindXrefComplainceTypemapping(compliancetypeid);
        }
        private string bindXrefComplainceTypemapping(int compliancetypeid)
        {
            ComplianceXrefHelper helper = new ComplianceXrefHelper();
            DataSet ds = helper.GetxrefComplianceMapping(compliancetypeid);
            UtilityHelper utilityHelper = new UtilityHelper();
            ds = utilityHelper.ConvertNullsToEmptyString(ds);
            return ds.GetXml();
        }

    }
}
