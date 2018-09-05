using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Compliance.DataObject;

namespace ComplianceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IComplianceXrefService" in both code and config file together.
    [ServiceContract]
    public interface IComplianceXrefService
    {
        [OperationContract]
        int insertActs(ComplianceXref compliance);
        [OperationContract]
        int insertRules(ComplianceXref compliance);
        //[OperationContract]
        //int insertSection(ComplianceXref compliance);
        [OperationContract]
        int UpdateActs(ComplianceXref compliance);
        [OperationContract]
        int UpdateRules(ComplianceXref compliance);
        [OperationContract]
        string GetActs(int complianceid);
        [OperationContract]
        string Getlineitems(int parentid);
        [OperationContract]
        string GetSpecificsection(int complianceid);
        [OperationContract]
        string GetRules(int parentid);
        [OperationContract]
        int GetAuditorId(int Branchid);
        [OperationContract]
        string getRuleforBranch(int branchid,int vendorid);
        [OperationContract]
        bool inseretActandRuleforBranch(int orgid, int[] ruleid, int userid,int vendorid,int compliancetypeid);
        [OperationContract]
        bool DeleteRuleforBranch(int orgid,int vendorid);
        [OperationContract]
        string GetComplaince(int Audit_Type_ID);
        [OperationContract]
        string GetcomplianceonType(int Audit_Type_Id,int countryId,int StateId,int cityId,int flag);
        [OperationContract]
        string GetSpecificComplaince(int complianceId);
        [OperationContract]
        string GetComplainceType(int compliancetypeid);

        [OperationContract]
        bool insertxreftypemapping(int[] xrefid, int compliancetypeid);
        [OperationContract]
        bool deletexreftypemapping(int compliancetypeid, int[] xrefid);
        [OperationContract]
        string GetXrefComplainceTypemapping(int compliancetypeid,int complianceid);
    }
}
