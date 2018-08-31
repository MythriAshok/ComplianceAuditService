DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getspecificcompliance`(p_Compliance_Xref_ID int)
BEGIN
select * from tbl_compliance_xref where Compliance_Xref_ID=p_Compliance_Xref_ID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ActivateOrgHier`(
p_Org_Hier_ID int
)
begin
update tbl_org_hier set Is_Active = 1 where Org_Hier_ID=p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ActivateVendorForBranch`(
p_Vendor_Branch_ID int
)
begin
update tbl_vendor_branch_mapping set 
Is_Active = 1,
Effective_Start_Date= now()
 where Vendor_Branch_ID = p_Vendor_Branch_ID ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ActivateVendorForCompany`(
p_Org_Hier_ID int
)
begin
update tbl_org_hier  

inner join tbl_company_details on tbl_company_details.Org_Hier_ID = tbl_org_hier.Org_Hier_ID  
set
Is_Active = 1 ,
Calender_StartDate = now() 
where tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeactivateOrgHier`(
p_Org_Hier_ID int
)
begin
update tbl_org_hier set Is_Active = 0 where Org_Hier_ID=p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeactivateVendorForBranch`(
p_Branch_ID int,
p_Vendor_ID int

)
begin
update tbl_vendor_branch_mapping set 
Is_Active = 0,
End_Date= now()
 where Branch_ID = p_Branch_ID and Vendor_ID = p_Vendor_ID ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeactivateVendorForCompany`(
p_Org_Hier_ID int
)
begin
update tbl_org_hier  
set
Is_Active = 0 
where tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID;
update tbl_company_details
set
Calender_EndDate = now() 
where tbl_company_details.Org_Hier_ID=p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteBranchAuditorMapping`(
p_Branch_Allocation_ID int 
)
begin
update tbl_compliance_branch_mapping set Is_Active = 0 where Branch_Allocation_ID=p_Branch_Allocation_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteBranchLocation`(
p_Location_ID int
)
begin
delete from tbl_branch_location where Location_ID=p_Location_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceAudit`(
p_Compliance_Audit_ID int
)
begin
update tbl_compliance_audit set Is_Active  =0 where Compliance_Audit_ID=p_Compliance_Audit_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceAuditTrail`(
p_Compliance_Audit_ID int
)
begin
update tbl_Compliance_Audit_AuditTrail set Is_Actice = 0 where Compliance_Audit_ID=p_Compliance_Audit_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteComplianceBranchMapping`(
p_Org_Hier_ID int 
)
begin
DELETE FROM `compliancedb`.`tbl_compliance_branch_mapping`
WHERE Org_Hier_ID=p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceOptionsXref`(
p_Compliance_Opt_Xerf_ID int
)
begin
delete from tbl_compliance_xref where Compliance_Opt_Xerf_ID=p_Compliance_Opt_Xerf_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteComplianceTypes`(p_Org_Hier_ID int)
begin
DELETE FROM `compliancedb`.`tbl_compliance_type_mapping`
WHERE Org_Hier_ID= p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceXrefAuditTrail`(
p_Compliance_Xref_ID int
)
begin
update tbl_compliance_xref_audittrail set Is_Active = 0 where Compliance_Xref_ID=p_Compliance_Xref_ID;

end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteOrganizationHier`(
p_Org_Hier_ID int
)
begin
update tbl_org_hier set
Is_Delete=1
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteRole`(p_Role_ID int)
begin
UPDATE `compliancedb`.`tbl_role`
SET
`Is_Active` = 0
WHERE `Role_ID` = p_Role_ID ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteRolePrivilege`(p_Role_ID int)
begin
DELETE FROM `compliancedb`.`tbl_role_priv_map`
WHERE Role_ID= p_Role_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteUser`(p_User_ID int)
begin
update `compliancedb`.`tbl_user` set Is_Active=0 
where User_ID = p_User_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUsergroup`(p_User_Group_ID int)
begin
DELETE FROM `compliancedb`.`tbl_user_group`
WHERE User_Group_ID= p_User_Group_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUserGroupMembers`(p_User_ID int)
begin
DELETE FROM `compliancedb`.`tbl_user_group_members`
WHERE User_ID= p_User_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUserRole`(p_User_ID int)
begin
DELETE FROM `compliancedb`.`tbl_user_role_map`
WHERE User_ID=p_User_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteVendorForCompany`(
p_Org_Hier_ID int

)
begin
update tbl_org_hier set 
Is_Delete = 1
where tbl_org_hier.Org_Hier_ID = p_Org_Hier_ID;
update tbl_company_details set
tbl_company_details.Calender_EndDate= now()
 where tbl_company_details.Org_Hier_ID = p_Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_fetchchangePassword`(
p_User_ID int,
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
select User_ID from tbl_user where Email_ID= p_Email_ID and User_Password=p_User_Password;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getActs`(
p_Compliance_Xref_ID int
)
begin
if(p_Compliance_Xref_ID=0)
then
SELECT * FROM `compliancedb`.`tbl_compliance_xref`
where Comp_Category='Act' and `tbl_compliance_xref`.`level`=1;
else
SELECT *
FROM `compliancedb`.`tbl_compliance_xref`
where Comp_Category='Act' and `tbl_compliance_xref`.`level`=1 and Compliance_Xref_ID=p_Compliance_Xref_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllCompanyBrnachAssignedtoAuditor`(
p_Auditor_ID int 
)
begin  
select * from tbl_org_hier where 
Org_Hier_ID IN(Select Org_Hier_ID from tbl_org_hier where Is_Active = 1 and Is_Vendor= 0 and level =3 ) 
Union
select * from tbl_org_hier where 
Org_Hier_ID IN(
Select distinct Parent_Company_ID from tbl_org_hier where 
Org_Hier_ID in (Select Org_Hier_ID from tbl_org_hier)) and Is_Active = 1 and Is_Vendor= 0 and level =2;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllUser`(P_Company_ID int)
begin
SELECT `tbl_user`.`User_ID`,
    `tbl_user`.`User_Password`,
    `tbl_user`.`First_Name`,
    `tbl_user`.`Middle_Name`,
    `tbl_user`.`Last_Name`,
    `tbl_user`.`Email_ID`,
    `tbl_user`.`Contact_Number`,
    `tbl_user`.`Company_ID`,
    `tbl_user`.`Gender`,
    `tbl_user`.`Is_Active`,
    `tbl_user`.`Last_Login`,
    `tbl_user`.`Photo`
FROM `compliancedb`.`tbl_user` where `tbl_user`.`Company_ID`=P_Company_ID and Is_Active=1;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAssignedCompliances`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from tbl_compliance_type_mapping;
else
select Org_Hier_ID, compliance_type_map_ID,tbl_compliance_type_mapping.Compliance_Type_ID,
Compliance_Type_Name
from tbl_compliance_type_mapping
inner join tbl_compliance_type on tbl_compliance_type.Compliance_Type_ID = tbl_compliance_type_mapping.Compliance_Type_ID

where Org_Hier_ID= p_Org_Hier_ID ;
 end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAuditorforBranch`(p_Branch_Id int)
begin
SELECT `tbl_branch_auditor_mapping`.`Branch_Allocation_ID`,
    `tbl_branch_auditor_mapping`.`Auditor_ID`
FROM `compliancedb`.`tbl_branch_auditor_mapping`
where  `tbl_branch_auditor_mapping`.`Org_Hier_ID`=p_Branch_Id ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchAssociatedWithVendors`(p_Vendor_ID int)
begin 
if(p_Vendor_ID=0)
then
select * from tbl_vendor_branch_mapping ;
else
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,tbl_vendor_branch_mapping.Is_Active ,
Company_Name,Type,logo 
 from tbl_vendor_branch_mapping
inner join tbl_org_hier on tbl_org_hier.Org_Hier_ID = tbl_vendor_branch_mapping.Branch_ID
where Vendor_ID= p_Vendor_ID ;
 end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchAuditorMapping`(
p_Branch_Allocation_ID int 
)
begin
if(p_Branch_Allocation_ID=0)
then
select
Org_Hier_ID,
Auditor_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date
from tbl_compliance_branch_mapping; 
else
select 
Org_Hier_ID,
Auditor_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date
from tbl_compliance_branch_mapping 
where Branch_Allocation_ID=p_Branch_Allocation_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchesAssignedforAuditor`(p_Auditor_ID int)
begin
select * from tbl_org_hier where 
Org_Hier_ID IN(Select Org_Hier_ID from tbl_branch_auditor_mapping where Auditor_ID=p_Auditor_ID) ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchJoin`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select
Company_Name,
Company_Code, 
Parent_Company_ID, 
Description, 
level,
Is_Leaf, 
Type, 
Last_Updated_Date,
logo,
User_ID, 
Is_Active ,
Is_Vendor
from 
tbl_org_hier;
else 
select 
tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_Code, 
Parent_Company_ID, 
Description, 
level,
Is_Leaf, 
Type, 
Last_Updated_Date,
 logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor,
tbl_branch_location.Org_Hier_ID,
Location_ID,
Location_Name,
Address,
Country_ID,
State_ID,
City_ID,
Postal_Code,
Branch_Coordinates1,
Branch_Coordinates2,
Branch_CoordinateURL
from tbl_org_hier 
inner join tbl_branch_location on tbl_branch_location.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchList`()
begin  
select Company_Name, Org_Hier_ID,Type,Is_Active from tbl_org_hier where level=3 and Is_Delete = 0;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchLocation`(
 p_Org_Hier_ID int
)
begin
SELECT `Location_ID`,
    `Location_Name`,
    `Address`,
    `Country_ID`,
    `State_ID`,
    `City_ID`,
    `Postal_Code`,
    `Branch_Coordinates1`,
    `Branch_Coordinates2`,
    `Branch_CoordinateURL`,
    `Org_Hier_ID`
FROM `tbl_branch_location`
where p_Org_Hier_ID=Org_Hier_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCity`(
p_State_ID int
)
begin
if(p_State_ID=0)
then
select *  from tbl_city;
else
select *  from tbl_city where State_ID = p_State_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompaniesList`(p_Parent_Company_ID int)
begin  
select Company_Name, Org_Hier_ID,Type,Is_Active from tbl_org_hier where level= 2 and Parent_Company_ID=p_Parent_Company_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompaniesListDropDown`(
p_Parent_Company_ID int
)
begin
select Org_Hier_ID,Company_Name  from tbl_org_hier where Parent_Company_ID= p_Parent_Company_ID and Is_Active=1 and Is_Delete = 0 and level =2;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanieyList`()
begin  
select Company_Name, Org_Hier_ID,Industry_Type,Is_Active from tbl_org_hier where level= 2 and Is_Delete=0 ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanyLists`(p_Parent_Company_ID int)
begin 
if( p_Parent_Company_ID=0)
then
select * from tbl_org_hier where level = 2;
else
select * from tbl_org_hier where Parent_Company_ID=p_Parent_Company_ID  ;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanyListsforBranch`(p_Org_Hier_ID int)
begin 
if( p_Org_Hier_ID=0)
then
select * from tbl_org_hier where level = 2;
else
select * from tbl_org_hier where Org_Hier_ID=p_Org_Hier_ID ;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceAudit`(
p_Compliance_Audit_ID int 
)
begin
if(p_Compliance_Audit_ID=0)
then
select
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,
User_ID,
Is_Active 
from tbl_compliance_audit ;
else
select
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,
User_ID,
Is_Active 
from tbl_compliance_audit 
where Compliance_Audit_ID=p_Compliance_Audit_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceAuditTrail`(
p_Compliance_Audit_ID int
)
begin
if(p_Compliance_Audit_ID =0)
then
select 
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date ,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,
User_ID,
Is_Active,
Action_Type
from tbl_Compliance_Audit_AuditTrail;
else
select 
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date ,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,
User_ID,
Is_Active,
Action_Type
from tbl_Compliance_Audit_AuditTrail
where Compliance_Audit_ID=p_Compliance_Audit_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceBranchMapping`(
p_Branch_Mapping_ID int 
)
begin
if(p_Branch_Mapping_ID=0)
then
select
Org_Hier_ID,
Compliance_Xref_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date
from tbl_compliance_branch_mapping ;
else
select 
Org_Hier_ID,
Compliance_Xref_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date
from tbl_compliance_branch_mapping 
where Branch_Mapping_ID=p_Branch_Mapping_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceList`(p_Country_ID int, p_Industry_Type_ID int)
begin 
if(p_Country_ID=0 && p_Industry_Type_ID=0)
then
select * from tbl_Compliance_Type ;
else
select Compliance_Type_ID,
Compliance_Type_Name,
Industry_Type_ID,Country_ID,Audit_Frequency,End_Date, Start_Date 

from tbl_Compliance_Type
 

where Country_ID= p_Country_ID and Industry_Type_ID=p_Industry_Type_ID ;
 end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceType`()
begin
Select * from compliance_type;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceXref`(
p_Audit_Type_ID int 
)
begin
if(p_Audit_Type_ID=0)
then
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
    `tbl_compliance_xref`.`Comp_Category`,
    `tbl_compliance_xref`.`Comp_Description`,
    `tbl_compliance_xref`.`Is_Header`,
    `tbl_compliance_xref`.`level`,
    `tbl_compliance_xref`.`Comp_Order`,
    `tbl_compliance_xref`.`Risk_Category`,
    `tbl_compliance_xref`.`Risk_Description`,
    `tbl_compliance_xref`.`Recurrence`,
    `tbl_compliance_xref`.`Form`,
    `tbl_compliance_xref`.`Type`,
    `tbl_compliance_xref`.`Is_Best_Practice`,
    `tbl_compliance_xref`.`Version`,
    `tbl_compliance_xref`.`Effective_Start_Date`,
    `tbl_compliance_xref`.`Effective_End_Date`,
    `tbl_compliance_xref`.`Country_ID`,
    `tbl_compliance_xref`.`State_ID`,
    `tbl_compliance_xref`.`City_ID`,
    `tbl_compliance_xref`.`Last_Updated_Date`,
    `tbl_compliance_xref`.`User_ID`,
    `tbl_compliance_xref`.`Is_Active`,
    `tbl_compliance_xref`.`Compliance_Title`,
    `tbl_compliance_xref`.`Compliance_Parent_ID`,
    `tbl_compliance_xref`.`compl_def_consequence`,
    `tbl_compliance_xref`.`Audit_Type_ID`
FROM `compliancedb`.`tbl_compliance_xref`;
else
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
    `tbl_compliance_xref`.`Comp_Category`,
    `tbl_compliance_xref`.`Comp_Description`,
    `tbl_compliance_xref`.`Is_Header`,
    `tbl_compliance_xref`.`level`,
    `tbl_compliance_xref`.`Comp_Order`,
    `tbl_compliance_xref`.`Risk_Category`,
    `tbl_compliance_xref`.`Risk_Description`,
    `tbl_compliance_xref`.`Recurrence`,
    `tbl_compliance_xref`.`Form`,
    `tbl_compliance_xref`.`Type`,
    `tbl_compliance_xref`.`Is_Best_Practice`,
    `tbl_compliance_xref`.`Version`,
    `tbl_compliance_xref`.`Effective_Start_Date`,
    `tbl_compliance_xref`.`Effective_End_Date`,
    `tbl_compliance_xref`.`Country_ID`,
    `tbl_compliance_xref`.`State_ID`,
    `tbl_compliance_xref`.`City_ID`,
    `tbl_compliance_xref`.`Last_Updated_Date`,
    `tbl_compliance_xref`.`User_ID`,
    `tbl_compliance_xref`.`Is_Active`,
    `tbl_compliance_xref`.`Compliance_Title`,
    `tbl_compliance_xref`.`Compliance_Parent_ID`,
    `tbl_compliance_xref`.`compl_def_consequence`,
    `tbl_compliance_xref`.`Audit_Type_ID`
FROM `compliancedb`.`tbl_compliance_xref`
where Audit_Type_ID= p_Audit_Type_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceXrefAuditTrail`(
p_Compliance_Xref_ID int
)
begin
if(p_Compliance_Xref_ID=0)
then
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID, Action_Type,Is_Active from tbl_compliance_xref_audittrail;
else
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID, Action_Type,Is_Active from tbl_compliance_xref_audittrail
where Compliance_Xref_ID = p_Compliance_Xref_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceXrefData`(
p_Org_Hier_ID int 
)
begin
select tbl_compliance_xref.*,tbl_compliance_branch_mapping.Org_Hier_ID  from tbl_compliance_xref 
inner join tbl_compliance_branch_mapping on tbl_compliance_xref.Compliance_Xref_ID = tbl_compliance_branch_mapping.Compliance_Xref_ID
where 
tbl_compliance_xref.Compliance_Xref_ID IN(Select Compliance_Xref_ID from tbl_compliance_branch_mapping where Org_Hier_ID=p_Org_Hier_ID) 
and tbl_compliance_xref.Effective_Start_Date <=Now() and tbl_compliance_xref.Effective_End_Date >= Now()

Union
select tbl_compliance_xref.*,tbl_compliance_branch_mapping.Org_Hier_ID from tbl_compliance_xref 
left join tbl_compliance_branch_mapping on tbl_compliance_xref.Compliance_Xref_ID = tbl_compliance_branch_mapping.Compliance_Xref_ID
where 
tbl_compliance_xref.Compliance_Xref_ID IN(
Select distinct  Compliance_Parent_ID from tbl_compliance_xref where 
tbl_compliance_xref.Compliance_Xref_ID in (Select Compliance_Xref_ID from tbl_compliance_branch_mapping  
where Org_Hier_ID=p_Org_Hier_ID))
and tbl_compliance_xref.Effective_Start_Date <=Now() and tbl_compliance_xref.Effective_End_Date >= Now() 


union 
select tbl_compliance_xref.*,tbl_compliance_branch_mapping.Org_Hier_ID from tbl_compliance_xref 
left join tbl_compliance_branch_mapping on tbl_compliance_xref.Compliance_Xref_ID = tbl_compliance_branch_mapping.Compliance_Xref_ID
where 
tbl_compliance_xref.Compliance_Xref_ID IN(
select tbl_compliance_xref.Compliance_Parent_ID from tbl_compliance_xref where tbl_compliance_xref.Compliance_Xref_ID IN(
Select distinct  Compliance_Parent_ID from tbl_compliance_xref where 
tbl_compliance_xref.Compliance_Xref_ID in (Select Compliance_Xref_ID from tbl_compliance_branch_mapping  
where Org_Hier_ID=p_Org_Hier_ID)))
and tbl_compliance_xref.Effective_Start_Date <=Now() and tbl_compliance_xref.Effective_End_Date >= Now();


end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceXreftype`(
p_Audit_Type_ID int,
p_Country_ID int,
p_State_ID int,
p_City_ID int,
flag int
)
begin
if(flag=0)
then
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
    `tbl_compliance_xref`.`Comp_Category`,
    `tbl_compliance_xref`.`Comp_Description`,
    `tbl_compliance_xref`.`Is_Header`,
    `tbl_compliance_xref`.`level`,
    `tbl_compliance_xref`.`Comp_Order`,
    `tbl_compliance_xref`.`Risk_Category`,
    `tbl_compliance_xref`.`Risk_Description`,
    `tbl_compliance_xref`.`Recurrence`,
    `tbl_compliance_xref`.`Form`,
    `tbl_compliance_xref`.`Type`,
    `tbl_compliance_xref`.`Is_Best_Practice`,
    `tbl_compliance_xref`.`Version`,
    `tbl_compliance_xref`.`Effective_Start_Date`,
    `tbl_compliance_xref`.`Effective_End_Date`,
    `tbl_compliance_xref`.`Country_ID`,
    `tbl_compliance_xref`.`State_ID`,
    `tbl_compliance_xref`.`City_ID`,
    `tbl_compliance_xref`.`Last_Updated_Date`,
    `tbl_compliance_xref`.`User_ID`,
    `tbl_compliance_xref`.`Is_Active`,
    `tbl_compliance_xref`.`Compliance_Title`,
    `tbl_compliance_xref`.`Compliance_Parent_ID`,
    `tbl_compliance_xref`.`compl_def_consequence`,
    `tbl_compliance_xref`.`Compliance_Type_ID`
FROM `compliancedb`.`tbl_compliance_xref`
where Compliance_Type_ID= p_Audit_Type_ID and Country_ID=p_Country_ID;
else
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
    `tbl_compliance_xref`.`Comp_Category`,
    `tbl_compliance_xref`.`Comp_Description`,
    `tbl_compliance_xref`.`Is_Header`,
    `tbl_compliance_xref`.`level`,
    `tbl_compliance_xref`.`Comp_Order`,
    `tbl_compliance_xref`.`Risk_Category`,
    `tbl_compliance_xref`.`Risk_Description`,
    `tbl_compliance_xref`.`Recurrence`,
    `tbl_compliance_xref`.`Form`,
    `tbl_compliance_xref`.`Type`,
    `tbl_compliance_xref`.`Is_Best_Practice`,
    `tbl_compliance_xref`.`Version`,
    `tbl_compliance_xref`.`Effective_Start_Date`,
    `tbl_compliance_xref`.`Effective_End_Date`,
    `tbl_compliance_xref`.`Country_ID`,
    `tbl_compliance_xref`.`State_ID`,
    `tbl_compliance_xref`.`City_ID`,
    `tbl_compliance_xref`.`Last_Updated_Date`,
    `tbl_compliance_xref`.`User_ID`,
    `tbl_compliance_xref`.`Is_Active`,
    `tbl_compliance_xref`.`Compliance_Title`,
    `tbl_compliance_xref`.`Compliance_Parent_ID`,
    `tbl_compliance_xref`.`compl_def_consequence`,
    `tbl_compliance_xref`.`Compliance_Type_ID`
FROM `compliancedb`.`tbl_compliance_xref`
where Compliance_Type_ID= p_Audit_Type_ID and Country_ID=p_Country_ID and State_ID=p_State_ID and City_ID=p_City_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCountry`()
begin
select *  from tbl_Country;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultCompanyLists`(p_Org_Hier_ID int)
begin 
if( p_Org_Hier_ID=0)
then
select * from tbl_org_hier where level = 2;
else
select tbl_org_hier.Org_Hier_ID,Company_Name,tbl_branch_location.Country_ID,tbl_branch_location.State_ID,
tbl_branch_location.City_ID, Country_Name, State_Name, City_Name from tbl_org_hier
inner join tbl_branch_location on tbl_branch_location.Org_Hier_ID = tbl_org_hier.Org_Hier_ID 
inner join tbl_Country on  tbl_Country.Country_ID= tbl_branch_location.Country_ID 
inner join tbl_State on  tbl_State.State_ID= tbl_branch_location.State_ID 
inner join tbl_city on  tbl_city.City_ID= tbl_branch_location.City_ID 


where tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultIndustryType`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select Org_Hier_ID,tbl_company_details.Industry_Type_ID, Industry_Name
from tbl_company_details

inner join tbl_industry_type_master on tbl_industry_type_master.Industry_Type_ID = tbl_company_details.Industry_Type_ID;
else
select Org_Hier_ID,tbl_company_details.Industry_Type_ID, Industry_Name
from tbl_company_details
inner join tbl_industry_type_master on tbl_industry_type_master.Industry_Type_ID = tbl_company_details.Industry_Type_ID

where Org_Hier_ID= p_Org_Hier_ID ;
 end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompaniesList`()
begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1;

end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompaniesListDropDown`()
begin
select Org_Hier_ID, Company_Name  from tbl_org_hier where Parent_Company_ID=0;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListActiveDeactive`()
begin
select Company_Name, Org_Hier_ID,Type,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListDropDown`()
begin  

select Company_Name, Org_Hier_ID,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 ;


end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupHierJoin`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select
Company_Name,
Company_Code, 
Parent_Company_ID, 
Description,
level,
Is_Leaf,
Type,
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Vendor 
from tbl_org_hier;
else 
select tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_COde, 
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Type,
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor
from tbl_org_hier 
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getIndustryTypeList`()
begin  

select *from tbl_industry_type_master ;

end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getLoginData`(
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
select * from tbl_user where Email_ID= p_Email_ID and User_Password = p_User_Password;
update tbl_user set Last_Login=now() where User_ID in(Select User_ID where Email_ID= p_Email_ID);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getMenulist`()
BEGIN
SELECT Menu_ID,Parent_MenuID,Menu_Name,Page_URL,Is_Active,icon 
FROM `tbl_menus`
where Parent_MenuID>0;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getMenus`(p_User_ID int,p_Parent_MenuID int)
begin
SELECT distinct `tbl_menus`.`Menu_ID`,`Parent_MenuID`,Menu_Name,Page_URL,Is_Active,icon
FROM `tbl_menus` left join tbl_usergroup_menu_map on tbl_menus.Menu_ID=tbl_usergroup_menu_map.Menu_ID
WHERE `User_Group_ID` in (select User_Group_ID from tbl_user_group_members where User_ID = p_User_ID) and `Parent_MenuID`=p_Parent_MenuID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getOrganizationHier`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from tbl_org_hier;
else 

select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from tbl_org_hier
where Org_Hier_ID = p_Org_Hier_ID;
End If;

end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getOrganizationHierJoin`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select
Company_Name,
Company_Code, 
Parent_Company_ID, 
Description,
level,
Is_Leaf,
Type,
Last_Updated_Date,
 logo,
User_ID, 
Is_Active ,
Is_Vendor
from tbl_org_hier;
else 
select tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_COde, 
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Type, 
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor,
tbl_company_details.Company_Details_ID,
tbl_company_details.Org_Hier_ID, 
Formal_Name, 
Calender_StartDate,
Calender_EndDate,
Auditing_Frequency,
Website,
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
tbl_branch_location.Location_ID,
tbl_branch_location.Org_Hier_ID,
Location_Name,
Address,
Country_ID,
State_ID,
City_ID,
Postal_Code,
Branch_Coordinates1,
Branch_Coordinates2,
Branch_CoordinateURL,
Industry_Type_ID
from tbl_org_hier 
inner join  tbl_company_Details  on tbl_company_details.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
inner join tbl_branch_location on tbl_branch_location.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getParticularGroupCompaniesList`(p_Org_Hier_ID int)
begin  

select Company_Name, Org_Hier_ID,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 and
Org_Hier_ID = p_Org_Hier_ID;


end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getPrivilege`()
begin
select Privilege_ID,Privilege_Name,Privilege_Type,Is_Active from  tbl_privilege where Is_Active=1;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRole`(p_Role_ID int)
begin
if(p_Role_ID=0)
then
SELECT `tbl_role`.`Role_ID`,
    `tbl_role`.`Role_Name`,
    `tbl_role`.`Is_Active`,
    `tbl_role`.`Is_Group_Role`
FROM `compliancedb`.`tbl_role` where Is_Active=1;
else
SELECT `tbl_role`.`Role_ID`,
    `tbl_role`.`Role_Name`,
    `tbl_role`.`Is_Active`,
    `tbl_role`.`Is_Group_Role`
FROM `compliancedb`.`tbl_role`
WHERE `Role_ID` = p_Role_ID ;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRoleList`(p_flag int)
begin
if(p_flag=0)
then
select Role_ID,Role_Name from tbl_role where Is_Group_Role=0 and Is_Active=1;
else
select Role_ID,Role_Name from tbl_role where Is_Group_Role=1 and Is_Active=1;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRolePrivilege`(p_Role_ID int)
begin
select a.Privilege_ID,Privilege_Name,Privilege_Type,b.Is_Active from tbl_role_priv_map a left join tbl_privilege b 
on a.Privilege_ID=b.Privilege_ID where a.Role_ID=p_Role_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRuleforBranch`(p_Org_ID int,p_vendor_ID int)
begin
select Compliance_Xref_ID,Compliance_Title from tbl_compliance_xref where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3 and
Compliance_Xref_ID in (select Compliance_Xref_ID from tbl_compliance_branch_mapping where Org_Hier_ID= p_Org_ID and Vendor_ID=p_vendor_ID);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRules`(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
then
SELECT * FROM `compliancedb`.`tbl_compliance_xref`
where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3;
else
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
  `tbl_compliance_xref`.`Compliance_Title`
  FROM `compliancedb`.`tbl_compliance_xref`
  where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3 and Compliance_Parent_ID=p_Compliance_Parent_ID;
  end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSections`(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
then
SELECT * FROM `compliancedb`.`tbl_compliance_xref`
where Comp_Category='Section' and `tbl_compliance_xref`.`level`=2;
else
SELECT *  FROM `compliancedb`.`tbl_compliance_xref`
  where Comp_Category='Section' and `tbl_compliance_xref`.`level`=2 and Compliance_Parent_ID=p_Compliance_Parent_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificBranchList`(p_Parent_Company_ID int)
begin 
if(p_Parent_Company_ID=0)
then
select Company_Name, Org_Hier_ID,Type,Is_Active,logo from tbl_org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0;
else
 
select Company_Name, Org_Hier_ID,Type,Is_Active ,logo from tbl_org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0
 and Parent_Company_ID= p_Parent_Company_ID ;
 end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificBranchListDropDown`(p_Parent_Company_ID int)
begin 
select Company_Name, Org_Hier_ID,Type,Is_Active ,logo from tbl_org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0 and Is_Active=1
 and Parent_Company_ID= p_Parent_Company_ID ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificVendorList`(p_Parent_Company_ID int)
begin 
if(p_Parent_Company_ID=0)
then
select Company_Name, Org_Hier_ID,Type,Is_Active,logo, Is_Vendor from tbl_org_hier where level=3 and Is_Vendor=1 and Is_Delete = 0;
else
 
select Company_Name, Org_Hier_ID,Type,Is_Active,logo, Is_Vendor from tbl_org_hier where level=3 and Is_Vendor=1  and Is_Delete = 0
 and Parent_Company_ID= p_Parent_Company_ID ;
 end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecifiySection`(p_Compliance_Xref_ID int)
BEGIN
SELECT * FROM `compliancedb`.`tbl_compliance_xref`
where Compliance_Xref_ID=p_Compliance_Xref_ID and `tbl_compliance_xref`.`level`=2 and Comp_Category='Section';
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getState`(
p_Country_ID int
)
begin
if(p_Country_ID=0)
then
select *  from tbl_state;
else
select *  from tbl_state where Country_ID= p_Country_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUser`(p_User_ID int)
begin
if(p_User_ID=0)
then
SELECT `tbl_user`.`User_ID`,
    `tbl_user`.`User_Password`,
    `tbl_user`.`First_Name`,
    `tbl_user`.`Middle_Name`,
    `tbl_user`.`Last_Name`,
    `tbl_user`.`Email_ID`,
    `tbl_user`.`Contact_Number`,
    `tbl_user`.`Gender`,
    `tbl_user`.`Is_Active`,
    `tbl_user`.`Last_Login`
FROM `compliancedb`.`tbl_user` where Is_Active=1;
else
SELECT `tbl_user`.`User_ID`,
    `tbl_user`.`User_Password`,
    `tbl_user`.`First_Name`,
    `tbl_user`.`Middle_Name`,
    `tbl_user`.`Last_Name`,
    `tbl_user`.`Email_ID`,
    `tbl_user`.`Contact_Number`,
    `tbl_user`.`Gender`,
    `tbl_user`.`Is_Active`,
    `tbl_user`.`Last_Login`
FROM `compliancedb`.`tbl_user` 
where User_ID = p_User_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserassignedGroup`(p_User_ID int)
begin
SELECT a.User_Group_ID,a.User_Group_Name
FROM `compliancedb`.`tbl_user_group` a left join tbl_user_group_members b on a.User_Group_ID=b.User_Group_ID
WHERE b.User_ID =p_User_ID ;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserGroup`(p_User_Group_ID int)
begin
if(p_User_Group_ID=0)
then
SELECT `tbl_user_group`.`User_Group_ID`,
    `tbl_user_group`.`User_Group_Name`,
    `tbl_user_group`.`User_Group_Description`,
    `tbl_user_group`.`Role_ID`
FROM `compliancedb`.`tbl_user_group`where Is_Active=1;
else
SELECT `tbl_user_group`.`User_Group_ID`,
    `tbl_user_group`.`User_Group_Name`,
    `tbl_user_group`.`User_Group_Description`,
    `tbl_user_group`.`Role_ID`,
    `tbl_user_group`.`Is_Active`
FROM `compliancedb`.`tbl_user_group`
WHERE `User_Group_ID` = p_User_Group_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserGroupRole`()
begin
select Role_ID,Role_Name from tbl_role where Is_Group_Role=1 and Is_Active=1;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserRole`(p_User_ID int)
begin
select a.Role_ID,Role_Name from tbl_user_role_map a left join tbl_role b on a.Role_ID=b.Role_ID where User_ID=p_User_ID;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getVendorJoin`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select
Company_Name,
Company_Code, 
Parent_Company_ID, 
Description,
level,
Is_Leaf,
Type,
Last_Updated_Date,
 logo,
User_ID, 
Is_Active ,
Is_Vendor
from tbl_org_hier;
else 
select tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_COde, 
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Type, 
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor,
tbl_company_details.Company_Details_ID,
tbl_company_details.Org_Hier_ID, 
Formal_Name, 
Calender_StartDate,
Calender_EndDate,
Auditing_Frequency,
Website,
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
Industry_Type_ID

from tbl_org_hier 
inner join  tbl_company_Details  on tbl_company_details.Org_Hier_ID = tbl_org_hier.Org_Hier_ID

where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insert_User_Menu_Map`(p_UserGroup_Id int,p_Menu_Id int)
begin
INSERT INTO `compliancedb`.`tbl_usergroup_menu_map`
(`User_Group_ID`,
`Menu_ID`)
VALUES
(p_UserGroup_Id,p_Menu_Id);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertComplianceAuditTrail`(
p_Compliance_Audit_ID int ,
p_Comp_Schedule_Instance int,
p_Penalty_nc varchar(150),
p_Audit_Remarks varchar(150),
p_Audit_artefacts varchar(150),
p_Audit_Date datetime,
p_Version int,
p_Reviewer_ID int,
p_Review_Comments varchar(500),
p_Audit_Status varchar(10),
p_Compliance_Xref_ID int ,
p_Org_ID int ,
p_Auditor_ID int,
p_User_ID int ,
p_Is_Active bit,
p_Action_Type varchar(10)
)
begin
insert into tbl_Compliance_Audit_AuditTrail
(
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date ,
Audit_Status,
Compliance_Xref_ID,
Org_ID,
Auditor_ID,
User_ID,
Is_Active,
Action_Type
)
values
(
p_Comp_Schedule_Instance,
p_Penalty_nc,
p_Audit_Remarks,
p_Audit_artefacts,
p_Audit_Date,
p_Version,
p_Reviewer_ID,
p_Review_Comments,NOW(),
p_Audit_Status,
p_Compliance_Xref_ID,
p_Org_ID,
p_Auditor_ID,
p_User_ID,
p_Is_Active,
p_Action_Type
);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertComplianceXrefAuditTrail`(
p_Compliance_Xref_ID int ,
p_Comp_Category varchar(45),
p_Comp_Description varchar(45),
p_Is_Header tinyint,
p_level varchar(5),
p_Comp_Order int(3),
p_Option_ID int,
p_Risk_Category varchar(45),
p_Risk_Description varchar(100),
p_Recurrence varchar(45),
p_Form varchar(45),
p_Type varchar(45),
p_Is_Best_Practice tinyint,
p_Version int(3),
p_Effective_Start_Date datetime,
p_Effective_End_Date datetime,
p_Country_ID int,
p_State_ID int,
p_City_ID int,
p_User_ID int ,
p_Action_Type varchar(10),
p_Is_Active bit
)
begin
insert into tbl_compliance_xref_audittrail(Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID, Action_Type,Is_Active )

values(p_Comp_Category, p_Comp_Description,p_Is_Header,p_level,p_Comp_Order,p_Option_ID,p_Risk_Category,
p_Risk_Description,p_Recurrence,p_Form,p_Type,p_Is_Best_Practice ,p_Version,p_Effective_Start_Date,p_Effective_End_Date,
p_Country_ID ,p_State_ID ,p_City_ID ,p_User_ID,p_Action_Type,p_Is_Active,Now()  );

end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertLoginData`(
p_User_ID int,
p_User_Password varchar(10),
p_First_Name varchar(45),
p_Middle_Name varchar(45),
p_Last_Name varchar(45),
p_Email_ID varchar(100),
p_Contact_Number varchar(50),
p_Company_ID int,
p_Gender varchar(45),
p_Is_Active bit,
p_Last_Login datetime
)
BEGIN
insert into tbl_user values
(
p_User_Password,
p_First_Name,
p_Middle_Name,
p_Last_Name,
p_Email_ID,
p_Contact_Number,
p_Contact_ID,
p_Gender,
p_Is_Active,
p_Last_Login);
Select @@IDENTITY;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertRolePrivilege`(p_Role_ID int,p_Privilege_ID int,p_Is_Active bit)
begin
INSERT INTO `compliancedb`.`tbl_role_priv_map`
(`Is_Active`,
`Role_ID`,
`Privilege_ID`)
VALUES
(p_Is_Active,p_Role_ID,p_Privilege_ID);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateBranchAuditorMapping`(
p_Flag char(1),
p_Branch_Allocation_ID int ,
p_Org_Hier_ID int ,
p_Auditor_ID int ,
p_Financial_Year datetime,
p_Is_Active bit,
p_UpdatedByLogin_ID int,
p_Allocation_Date datetime
)
begin
if(p_Flag = 'I') then
insert into tbl_branch_auditor_mapping
(
Branch_Allocation_ID,
Org_Hier_ID,
Auditor_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date
)
values
(
p_Branch_Allocation_ID,
p_Org_Hier_ID,
p_Auditor_ID,
p_Financial_Year,
p_Is_Active,
p_UpdatedByLogin_ID,
p_Allocation_Date
);
else
update tbl_branch_auditor_mapping set
Branch_Allocation_ID=p_Branch_Allocation_ID,
Org_Hier_ID=p_Org_Hier_ID,
Auditor_ID=p_Auditor_ID,
Financial_Year=p_Financial_Year,
Is_Active=p_Is_Active,
UpdatedByLogin_ID=p_UpdatedByLogin_ID,
Allocation_Date=p_Allocation_Date
where Branch_Allocation_ID=p_Branch_Allocation_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateBranchLocation`(
p_Flag char(1),
p_Location_ID int,
p_Location_Name varchar(75),
p_Address varchar(450), 
p_Country_ID int, 
p_State_ID int, 
p_City_ID int, 
p_Postal_Code int,
p_Branch_Coordinates1 varchar (100),
p_Branch_Coordinates2 varchar (100),
p_Branch_CoordinateURL varchar (100),
p_Org_Hier_ID int
)
begin
if(p_Flag = 'I')then
insert into tbl_branch_location
(
Location_ID,
Location_Name,
Address,
Country_ID,
State_ID,
City_ID,
Postal_Code,
Branch_Coordinates1,
Branch_Coordinates2,
Branch_CoordinateURL,
Org_Hier_ID
)
values
(
p_Location_ID,
p_Location_Name,
p_Address,
p_Country_ID,
p_State_ID,
p_City_ID,
p_Postal_Code,
p_Branch_Coordinates1,
p_Branch_Coordinates2,
p_Branch_CoordinateURL,
p_Org_Hier_ID
);
select last_insert_id();
else
update tbl_branch_location set
Location_Name=p_Location_Name,
Address=p_Address,
Country_ID=p_Country_ID,
State_ID=p_State_ID,
City_ID=p_City_ID,
Postal_Code=p_Postal_Code,
Branch_Coordinates1= p_Branch_Coordinates1,
Branch_Coordinates2=p_Branch_Coordinates2,
Branch_CoordinateURL= p_Branch_CoordinateURL,
Org_Hier_ID=p_Org_Hier_ID
where Location_ID=p_Location_ID;
select  row_count();
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateCompanyDetails`(
p_Flag char (1),
p_Company_Details_ID int ,
p_Org_Hier_ID int ,
p_Formal_Name varchar(45),
p_Calender_StartDate datetime,
p_Calender_EndDate datetime,
p_Auditing_Frequency varchar(45),
p_Website varchar(45),
p_Company_Email_ID varchar(45),
p_Company_ContactNumber1 varchar(45),
p_Company_ContactNumber2 varchar(45),
p_Industry_Type_ID int,
p_Is_Active bit
)
begin
if(p_Flag ='I') then
insert into tbl_company_details
(
Org_Hier_ID,
Formal_Name, 
Calender_StartDate, 
Calender_EndDate, 
Auditing_Frequency, 
Website, 
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
Industry_Type_ID 
)
values
(
p_Org_Hier_ID,
p_Formal_Name, 
p_Calender_StartDate, 
p_Calender_EndDate, 
p_Auditing_Frequency, 
p_Website, 
p_Company_Email_ID,
p_Company_ContactNumber1,
p_Company_ContactNumber2,
p_Industry_Type_ID 
);
select last_insert_id();
else 
update tbl_company_details set
Company_Details_ID=p_Company_Details_ID,
Org_Hier_ID=p_Org_Hier_ID,
Formal_Name=p_Formal_Name,
Calender_StartDate= p_Calender_StartDate, 
Calender_EndDate=p_Calender_EndDate,
Auditing_Frequency= p_Auditing_Frequency,
Website= p_Website,
Company_Email_ID= p_Company_Email_ID,
Company_ContactNumber1=p_Company_ContactNumber1,
Company_ContactNumber2=p_Company_ContactNumber2,
Industry_Type_ID = p_Industry_Type_ID 

where Company_Details_ID=p_Company_Details_ID;
select row_count();
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceAudit`(
p_flag char(1),
p_Compliance_Audit_ID int ,
p_Comp_Schedule_Instance int,
p_Penalty_nc varchar(150),
p_Audit_Remarks varchar(150),
p_Audit_artefacts varchar(150),
p_Audit_Date datetime,
p_Version int,
p_Reviewer_ID int,
p_Review_Comments varchar(500),
p_Audit_Status varchar(450),
p_Compliance_Xref_ID int ,
p_Org_Hier_ID int ,
p_Auditor_ID int,
p_Is_Active bit,
p_Part_Compliance_Percent decimal,
p_Vendor_ID int
)
begin
if(p_flag = 'I') then
insert into tbl_compliance_audit
(
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date ,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,
Is_Active,
Part_Compliance_Percent,
Vendor_ID
)
values
(
p_Comp_Schedule_Instance,
p_Penalty_nc,
p_Audit_Remarks,
p_Audit_artefacts,
p_Audit_Date,
p_Version,
p_Reviewer_ID,
p_Review_Comments,
NOW(),
p_Audit_Status,
p_Compliance_Xref_ID,
p_Org_Hier_ID,
p_Auditor_ID,
p_Is_Active,
p_Part_Compliance_Percent,
p_Vendor_ID
);
else
INSERT INTO tbl_Compliance_Audit_AuditTrail
(
Select
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date ,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,
Is_Active,
Part_Compliance_Percent,
p_Vendor_ID,
"update" As 'Action_Type'
from tbl_compliance_audit Where Compliance_Audit_ID = p_Compliance_Audit_ID);
update tbl_compliance_audit set 
Comp_Schedule_Instance= p_Comp_Schedule_Instance,
Penalty_nc=p_Penalty_nc,
Audit_Remarks=p_Audit_Remarks,
Audit_artefacts=p_Audit_artefacts,
Audit_Date=p_Audit_Date,
Version=p_Version,
Reviewer_ID=p_Reviewer_ID,
Review_Comments=p_Review_Comments,
Last_Updated_Date=NOW(),
Audit_Status=p_Audit_Status,
Compliance_Xref_ID=p_Compliance_Xref_ID,
Org_ID=p_Org_ID,
Auditor_ID=p_Auditor_ID,
Is_Active =p_Is_Active ,
Part_Compliance_Percent=p_Part_Compliance_Percent ,
Vendor_ID=p_Vendor_ID
where Compliance_Audit_ID= p_Compliance_Audit_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceBranchMapping`(
p_Org_Hier_ID int ,
p_Compliance_Xref_ID int ,
p_Financial_Year year,
p_Is_Active bit,
p_UpdatedByLogin_ID int,
p_Vendor_Id int,
p_Auditing_start_date date,
p_Auditing_end_date date
)
begin
insert into tbl_compliance_branch_mapping
(
Org_Hier_ID,
Compliance_Xref_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date,
Vendor_ID,
Auditing_start_date,
Auditing_end_date
)
values
(
p_Org_Hier_ID,
p_Compliance_Xref_ID,
p_Financial_Year,
p_Is_Active,
p_UpdatedByLogin_ID,
now(),
p_Vendor_Id,
p_Auditing_start_date,
p_Auditing_end_date);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceTypeMapping`(
p_Flag char(1),
p_compliance_type_map_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Type_ID int
)
begin
if(p_Flag = 'I') then
insert into tbl_Compliance_Type_mapping
(

Org_Hier_ID,
Compliance_Type_ID
)
values
(

p_Org_Hier_ID,
p_Compliance_Type_ID
);
select last_insert_id();
else
update tbl_Compliance_Type_mapping set

Org_Hier_ID=p_Org_Hier_ID,
Compliance_Type_ID=p_Compliance_Type_ID
where compliance_type_map_ID=p_compliance_type_map_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceXref`(
p_Flag char(1),
p_Compliance_Xref_ID int ,
p_Comp_Category varchar(45),
p_Compliance_Title varchar(450),
p_Comp_Description varchar(800),
p_compl_def_consequence varchar(800),
p_Is_Header tinyint,
p_level int(3),
p_Comp_Order int(3),
p_Compliance_Parent_ID int,
p_Risk_Category varchar(45),
p_Risk_Description varchar(100),
p_Recurrence varchar(45),
p_Form varchar(45),
p_Type varchar(45),
p_Is_Best_Practice tinyint,
p_Version int(3),
p_Effective_Start_Date datetime,
p_Effective_End_Date datetime,
p_Country_ID int,
p_State_ID int,
p_City_ID int,
p_User_ID int ,
p_Is_Active bit,
p_Compliance_Type_ID int
)
begin
if(p_Flag ='I') then

insert into tbl_compliance_xref(Comp_Category,Compliance_Title,Comp_Description,compl_def_consequence,Is_Header,level,Comp_Order,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID, Is_Active,Last_Updated_Date,Compliance_Parent_ID, Compliance_Type_ID )

values(p_Comp_Category,p_Compliance_Title, p_Comp_Description,p_compl_def_consequence,p_Is_Header,p_level,p_Comp_Order,p_Risk_Category,
p_Risk_Description,p_Recurrence,p_Form,p_Type,p_Is_Best_Practice ,p_Version,p_Effective_Start_Date,p_Effective_End_Date,
p_Country_ID ,p_State_ID ,p_City_ID ,p_User_ID,p_Is_Active,Now(),p_Compliance_Parent_ID , p_Compliance_Type_ID);
select last_insert_id();
else

INSERT INTO tbl_compliance_xref_audittrail(Select Comp_Category,Compliance_Title,Comp_Description,p_compl_def_consequence,Is_Header,level,Comp_Order,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,Statesp_insertupdateComplianceXref_ID ,City_ID ,User_ID,Is_Active,"update" As 'Action_Type'
from tbl_compliance_xref Where Compliance_Xref_ID = Compliance_Xref_ID);

update tbl_compliance_xref set

Comp_Category=p_Comp_Category,Compliance_Title=p_Compliance_Title, Comp_Description=p_Comp_Description,compl_def_consequence=p_compl_def_consequence,Is_Header=p_Is_Header,level=p_level,Comp_Order=p_Comp_Order,Risk_Category=p_Risk_Category,
Risk_Description=p_Risk_Description,Recurrence=p_Recurrence,Form=p_Form,Type=p_Type,Is_Best_Practice=p_Is_Best_Practice ,Version=p_Version,Effective_Start_Date=p_Effective_Start_Date,Effective_End_Date=p_Effective_End_Date,
Country_ID=p_Country_ID ,State_ID=p_State_ID ,City_ID=p_City_ID ,Last_Updated_Date=Now(),User_ID=p_User_ID, Is_Active=p_Is_Active,
Compliance_Type_ID=p_Compliance_Type_ID
where Compliance_Xref_ID=p_Compliance_Xref_ID;
select row_count();
end if;

end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateOrganizationHier`(
 p_Flag char(1),
 p_Org_Hier_ID int,
 p_Company_Name varchar(45),
 p_Company_Code int,
 p_Parent_Company_ID int,
 p_Description varchar(1000),
 p_level int,
 p_Is_Leaf tinyint,
 p_Type varchar(50),
 p_Last_Updated_Date datetime,
 p_logo varchar(100),
 p_User_ID int,
 p_Is_Active bit,
 p_Is_Delete bit,
 p_Is_Vendor bit
)
begin
if(p_Flag = 'I')then
insert into tbl_org_hier
(
Company_Name,
Company_Code,
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Type,
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor)
values
(
p_Company_Name,
p_Company_Code, 
p_Parent_Company_ID, 
p_Description, 
p_level,
p_Is_Leaf,
p_Type,
now(),
p_logo,
p_User_ID,
p_Is_Active,
p_Is_Delete,
p_Is_Vendor);
select last_insert_id();
else
update tbl_org_hier
set
tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID,
Company_Name=p_Company_Name,
Company_Code=p_Company_Code,
Parent_Company_ID=p_Parent_Company_ID,
Description=p_Description,
level=p_level,
Is_Leaf=p_Is_Leaf, 
Type = p_Type,
Last_Updated_Date=now(),
 logo= p_logo,
User_ID=p_User_ID,
Is_Active=p_Is_Active,
Is_Delete = p_Is_Delete,
Is_Vendor = p_Is_Vendor
where tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID ;
select row_count();
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateRole`(p_flag char(1),p_Role_ID int,p_Role_Name varchar(45),p_Is_Active bit,p_Is_Group_Role bit)
begin
if(p_flag='I')
then
INSERT INTO `compliancedb`.`tbl_role`
(`Role_Name`,
`Is_Active`,
`Is_Group_Role`)
VALUES
(p_Role_Name,p_Is_Active,p_Is_Group_Role);
select last_insert_id();
else
UPDATE `compliancedb`.`tbl_role`
SET
`Role_Name` = p_Role_Name,
`Is_Active` = p_Is_Active,
`Is_Group_Role` = p_Is_Group_Role
WHERE `Role_ID` = p_Role_ID ;
select last_insert_id();
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateUser`(
p_flag char(1),
p_User_ID int,
p_User_Password varchar(10),
p_First_Name varchar(45),
p_Middle_Name varchar(45),
p_Last_Name varchar(45),
p_Email_ID varchar(100),
p_Contact_Number varchar(50),
p_Gender varchar(45),
p_Is_Active bit,
p_Photo varchar(100),
p_compnay_Id int
)
begin
if(p_flag='I')
then
if exists(select Email_ID from `tbl_user` where Email_ID=p_Email_ID)
then
select "EXISTS";
else
INSERT INTO `tbl_user`(`User_ID`,
`User_Password`,
`First_Name`,
`Middle_Name`,
`Last_Name`,
`Email_ID`,
`Contact_Number`,
`Gender`,
`Is_Active`,
`Last_Login`,
`Photo`,
`Company_ID`)
VALUES(p_User_ID,p_User_Password,p_First_Name,p_Middle_Name,p_Last_Name,p_Email_ID,
p_Contact_Number,p_Gender,p_Is_Active,now(),p_Photo,p_compnay_Id);
select last_insert_id();
end if;
else
UPDATE `tbl_user`
SET
`First_Name` = p_First_Name,
`Middle_Name` = p_Middle_Name,
`Last_Name` = p_Last_Name,
`Contact_Number` = p_Contact_Number,
`Is_Active` = p_Is_Active,
`Last_Login` = now(),
`Photo`=p_Photo
WHERE `User_ID` = p_User_ID;
select row_count();
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateUserGroup`(p_flag char(1),p_User_Group_ID int,p_User_Group_Name varchar(45),
p_User_Group_Description varchar(45),p_Role_ID int,p_Is_Active bit)
begin
if(p_flag='I')
then
INSERT INTO `compliancedb`.`tbl_user_group`
(`User_Group_Name`,
`User_Group_Description`,
`Role_ID`,
`Is_Active`)
VALUES
(p_User_Group_Name,p_User_Group_Description,p_Role_ID,p_Is_Active);
else
UPDATE `compliancedb`.`tbl_user_group`
SET
`User_Group_Name` = p_User_Group_Name,
`User_Group_Description` = p_User_Group_Description,
`Role_ID` = p_Role_ID
WHERE `User_Group_ID` = p_User_Group_ID;
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateVendorForBranch`(
p_Flag char(1),
p_Vendor_Branch_ID int,
p_Vendor_ID int,
p_Branch_ID int,
p_Start_Date datetime,
p_End_Date datetime,
p_Is_Active bit 
)
begin
if(p_Flag = 'I')then
insert into tbl_vendor_branch_mapping
(
Vendor_Branch_ID ,
Vendor_ID ,
Branch_ID,
Start_Date,
End_Date,
Is_Active
)
values
(
p_Vendor_Branch_ID ,
p_Vendor_ID ,
p_Branch_ID,
p_Start_Date,
p_End_Date ,
p_Is_Active 
);
select last_insert_id();
else
update tbl_vendor_branch_mapping set
Vendor_ID= p_Vendor_ID ,
Branch_ID =p_Branch_ID,
Start_Date=p_Start_Date,
End_Date=p_End_Date ,
Is_Active=p_Is_Active 
where Vendor_Branch_ID =p_Vendor_Branch_ID;
select last_insert_id();
end if;
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertUserGroupMembers`(p_User_ID int,p_User_Group_ID int)
begin
INSERT INTO `compliancedb`.`tbl_user_group_members`
(`User_ID`,
`User_Group_ID`)
VALUES
(p_User_ID,p_User_Group_ID);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertUserRole`(p_Role_ID int,p_User_ID int)
begin
INSERT INTO `compliancedb`.`tbl_user_role_map`
(`Role_ID`,
`User_ID`)
VALUES
(p_Role_ID,p_User_ID);
end$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updatePassword`(
p_User_ID int,
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
update tbl_user set User_Password = p_User_Password where User_ID = p_User_ID;
end$$
DELIMITER ;
