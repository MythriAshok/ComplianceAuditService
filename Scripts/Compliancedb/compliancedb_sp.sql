-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema compliancedb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema compliancedb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `compliancedb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `compliancedb` ;
USE `compliancedb` ;

-- -----------------------------------------------------
-- procedure getComplainceActlist
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`getComplainceActlist`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getComplainceActlist`(
p_Compliance_Type_ID int,
p_Org_Hier_ID int,
p_Vendor_ID int
)
BEGIN
select * from compliance_xref where Compliance_Xref_ID in
(select Compliance_Parent_ID from compliance_xref where  Compliance_Xref_ID in 
(Select Compliance_Xref_ID from  xref_comp_type_mapping where Compliance_Type_ID=p_Compliance_Type_ID and Xref_Comp_Type_Map_ID in 
(Select Xref_Comp_Type_Map_ID from compliance_branch_mapping where Org_Hier_ID=p_Org_Hier_ID and Vendor_ID=p_Vendor_ID
)));
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getspecificcompliance
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`getspecificcompliance`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getspecificcompliance`(p_Compliance_Xref_ID int)
BEGIN
select * from compliance_xref where Compliance_Xref_ID=p_Compliance_Xref_ID;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_ActivateOrgHier
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_ActivateOrgHier`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ActivateOrgHier`(
p_Org_Hier_ID int
)
begin
update org_hier set Is_Active = 1 where Org_Hier_ID=p_Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_ActivateVendorForBranch
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_ActivateVendorForBranch`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ActivateVendorForBranch`(
p_Vendor_Branch_ID int
)
begin
update vendor_branch_mapping set 
Is_Active = 1,
Effective_Start_Date= now()
 where Vendor_Branch_ID = p_Vendor_Branch_ID ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_ActivateVendorForCompany
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_ActivateVendorForCompany`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ActivateVendorForCompany`(
p_Org_Hier_ID int
)
begin
update org_hier  

inner join company_details on company_details.Org_Hier_ID = org_hier.Org_Hier_ID  
set
Is_Active = 1 ,
Calender_StartDate = now() 
where org_hier.Org_Hier_ID=p_Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeactivateOrgHier
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeactivateOrgHier`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeactivateOrgHier`(
p_Org_Hier_ID int
)
begin
update org_hier set Is_Active = 0 where Org_Hier_ID=p_Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeactivateVendorForBranch
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeactivateVendorForBranch`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeactivateVendorForBranch`(
p_Branch_ID int,
p_Vendor_ID int

)
begin
update vendor_branch_mapping set 
Is_Active = 0,
End_Date= now()
 where Branch_ID = p_Branch_ID and Vendor_ID = p_Vendor_ID ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeactivateVendorForCompany
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeactivateVendorForCompany`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeactivateVendorForCompany`(
p_Org_Hier_ID int
)
begin
update org_hier  
set
Is_Active = 0 
where org_hier.Org_Hier_ID=p_Org_Hier_ID;
update company_details
set
Calender_EndDate = now() 
where company_details.Org_Hier_ID=p_Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeleteComplianceBranchMapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeleteComplianceBranchMapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteComplianceBranchMapping`(
p_Org_Hier_ID int ,
p_Vendor_ID int
)
begin
DELETE FROM `compliancedb`.`compliance_branch_mapping`
WHERE Org_Hier_ID=p_Org_Hier_ID and Vendor_ID=p_Vendor_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeleteComplianceTypes
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeleteComplianceTypes`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteComplianceTypes`(p_Org_Hier_ID int)
begin
DELETE FROM `compliancedb`.`compliance_type_mapping`
WHERE Org_Hier_ID= p_Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeleteRolePrivilege
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeleteRolePrivilege`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteRolePrivilege`(p_Role_ID int)
begin
DELETE FROM `compliancedb`.`role_priv_map`
WHERE Role_ID= p_Role_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeleteUserGroupMembers
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeleteUserGroupMembers`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUserGroupMembers`(p_User_ID int)
begin
DELETE FROM `compliancedb`.`user_group_members`
WHERE User_ID= p_User_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeleteUserRole
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeleteUserRole`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUserRole`(p_User_ID int)
begin
DELETE FROM `compliancedb`.`user_role_map`
WHERE User_ID=p_User_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeleteUsergroup
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeleteUsergroup`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUsergroup`(p_User_Group_ID int)
begin
DELETE FROM `compliancedb`.`user_group`
WHERE User_Group_ID= p_User_Group_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_DeleteVendorForCompany
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_DeleteVendorForCompany`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteVendorForCompany`(
p_Org_Hier_ID int

)
begin
update org_hier set 
Is_Delete = 1
where org_hier.Org_Hier_ID = p_Org_Hier_ID;
update company_details set
company_details.Calender_EndDate= now()
 where company_details.Org_Hier_ID = p_Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteBranchAuditorMapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteBranchAuditorMapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteBranchAuditorMapping`(
p_Branch_Allocation_ID int 
)
begin
update compliance_branch_mapping set Is_Active = 0 where Branch_Allocation_ID=p_Branch_Allocation_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteBranchLocation
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteBranchLocation`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteBranchLocation`(
p_Location_ID int
)
begin
delete from branch_location where Location_ID=p_Location_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteComplianceAudit
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteComplianceAudit`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceAudit`(
p_Compliance_Audit_ID int
)
begin
update compliance_audit set Is_Active  =0 where Compliance_Audit_ID=p_Compliance_Audit_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteComplianceAuditTrail
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteComplianceAuditTrail`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceAuditTrail`(
p_Compliance_Audit_ID int
)
begin
update Compliance_Audit_AuditTrail set Is_Actice = 0 where Compliance_Audit_ID=p_Compliance_Audit_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteComplianceOptionsXref
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteComplianceOptionsXref`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceOptionsXref`(
p_Compliance_Opt_Xerf_ID int
)
begin
delete from compliance_xref where Compliance_Opt_Xerf_ID=p_Compliance_Opt_Xerf_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteComplianceXrefAuditTrail
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteComplianceXrefAuditTrail`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteComplianceXrefAuditTrail`(
p_Compliance_Xref_ID int
)
begin
update compliance_xref_audittrail set Is_Active = 0 where Compliance_Xref_ID=p_Compliance_Xref_ID;

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteOrganizationHier
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteOrganizationHier`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteOrganizationHier`(
p_Org_Hier_ID int
)
begin
update org_hier set
Is_Delete=1
where org_hier.Org_Hier_ID= p_Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteRole
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteRole`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteRole`(p_Role_ID int)
begin
UPDATE `compliancedb`.`role`
SET
`Is_Active` = 0
WHERE `Role_ID` = p_Role_ID ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_deleteUser
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_deleteUser`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_deleteUser`(p_User_ID int)
begin
update `compliancedb`.`user` set Is_Active=0 
where User_ID = p_User_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_delete_xref_comp_type_mapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_delete_xref_comp_type_mapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_delete_xref_comp_type_mapping`(
p_Compliance_Type_ID int)
BEGIN
delete from xref_comp_type_mapping where Compliance_Type_ID=p_Compliance_Type_ID ;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_fetchchangePassword
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_fetchchangePassword`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_fetchchangePassword`(
p_User_ID int,
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
select User_ID from user where Email_ID= p_Email_ID and User_Password=p_User_Password;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getActs
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getActs`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getActs`(
p_Compliance_Xref_ID int
)
begin
if(p_Compliance_Xref_ID=0)
then
SELECT * FROM `compliancedb`.`compliance_xref`
where Comp_Category='Act' and `compliance_xref`.`level`=1;
else
SELECT *
FROM `compliancedb`.`compliance_xref`
where Comp_Category='Act' and `compliance_xref`.`level`=1 and Compliance_Xref_ID=p_Compliance_Xref_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getAllCompanyBrnachAssignedtoAuditor
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getAllCompanyBrnachAssignedtoAuditor`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllCompanyBrnachAssignedtoAuditor`(
p_Auditor_ID int 
)
begin  
select * from org_hier where 
Org_Hier_ID IN(Select Org_Hier_ID from org_hier where Is_Active = 1 and Is_Vendor= 0 and level =3 ) 
Union
select * from org_hier where 
Org_Hier_ID IN(
Select distinct Parent_Company_ID from org_hier where 
Org_Hier_ID in (Select Org_Hier_ID from org_hier)) and Is_Active = 1 and Is_Vendor= 0 and level =2;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getAllUser
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getAllUser`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllUser`(P_Company_ID int)
begin
SELECT `user`.`User_ID`,
    `user`.`User_Password`,
    `user`.`First_Name`,
    `user`.`Middle_Name`,
    `user`.`Last_Name`,
    `user`.`Email_ID`,
    `user`.`Contact_Number`,
    `user`.`Company_ID`,
    `user`.`Gender`,
    `user`.`Is_Active`,
    `user`.`Last_Login`,
    `user`.`Photo`
FROM `compliancedb`.`user` where `user`.`Company_ID`=P_Company_ID and Is_Active=1;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getAssignedCompliances
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getAssignedCompliances`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAssignedCompliances`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_type_mapping;
else
select Org_Hier_ID, compliance_type_map_ID,compliance_type_mapping.Compliance_Type_ID,
Compliance_Type_Name
from compliance_type_mapping
inner join compliance_type on compliance_type.Compliance_Type_ID = compliance_type_mapping.Compliance_Type_ID

where Org_Hier_ID= p_Org_Hier_ID ;
 end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getAuditorforBranch
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getAuditorforBranch`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAuditorforBranch`(p_Branch_Id int)
begin
SELECT `branch_auditor_mapping`.`Branch_Allocation_ID`,
    `branch_auditor_mapping`.`Auditor_ID`
FROM `compliancedb`.`branch_auditor_mapping`
where  `branch_auditor_mapping`.`Org_Hier_ID`=p_Branch_Id ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getBranchAssociatedWithVendors
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchAssociatedWithVendors`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchAssociatedWithVendors`(p_Vendor_ID int)
begin 
if(p_Vendor_ID=0)
then
select * from vendor_branch_mapping ;
else
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,vendor_branch_mapping.Is_Active ,
Company_Name,Type,logo 
 from vendor_branch_mapping
inner join org_hier on org_hier.Org_Hier_ID = vendor_branch_mapping.Branch_ID
where Vendor_ID= p_Vendor_ID ;
 end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getBranchAuditorMapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchAuditorMapping`;

DELIMITER $$
USE `compliancedb`$$
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
from compliance_branch_mapping; 
else
select 
Org_Hier_ID,
Auditor_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date
from compliance_branch_mapping 
where Branch_Allocation_ID=p_Branch_Allocation_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getBranchJoin
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchJoin`;

DELIMITER $$
USE `compliancedb`$$
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
org_hier;
else 
select 
org_hier.Org_Hier_ID,
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
branch_location.Org_Hier_ID,
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
from org_hier 
inner join branch_location on branch_location.Org_Hier_ID = org_hier.Org_Hier_ID
where org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getBranchList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchList`()
begin  
select Company_Name, Org_Hier_ID,Type,Is_Active from org_hier where level=3 and Is_Delete = 0;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getBranchLocation
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchLocation`;

DELIMITER $$
USE `compliancedb`$$
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
FROM `branch_location`
where p_Org_Hier_ID=Org_Hier_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getBranchesAssignedforAuditor
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchesAssignedforAuditor`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchesAssignedforAuditor`(p_Auditor_ID int)
begin
select * from org_hier where 
Org_Hier_ID IN(Select Org_Hier_ID from branch_auditor_mapping where Auditor_ID=p_Auditor_ID) ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getCity
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCity`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCity`(
p_State_ID int
)
begin
if(p_State_ID=0)
then
select *  from city;
else
select *  from city where State_ID = p_State_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getCompaniesList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompaniesList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompaniesList`(p_Parent_Company_ID int)
begin  
select Company_Name, Org_Hier_ID,Type,Is_Active from org_hier where level= 2 and Parent_Company_ID=p_Parent_Company_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getCompaniesListDropDown
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompaniesListDropDown`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompaniesListDropDown`(
p_Parent_Company_ID int
)
begin
select Org_Hier_ID,Company_Name  from org_hier where Parent_Company_ID= p_Parent_Company_ID and Is_Active=1 and Is_Delete = 0 and level =2;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getCompanieyList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompanieyList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanieyList`()
begin  
select Company_Name, Org_Hier_ID,Industry_Type,Is_Active from org_hier where level= 2 and Is_Delete=0 ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getCompanyLists
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompanyLists`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanyLists`(p_Parent_Company_ID int)
begin 
if( p_Parent_Company_ID=0)
then
select * from org_hier where level = 2;
else
select * from org_hier where Parent_Company_ID=p_Parent_Company_ID  ;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getCompanyListsforBranch
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompanyListsforBranch`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanyListsforBranch`(p_Org_Hier_ID int)
begin 
if( p_Org_Hier_ID=0)
then
select * from org_hier where level = 2;
else
select * from org_hier where Org_Hier_ID=p_Org_Hier_ID ;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceAudit
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceAudit`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceAudit`(
p_Compliance_Audit_ID int 
)
begin
if(p_Compliance_Audit_ID=0)
then
select * from compliance_audit ;
else
select * from compliance_audit 
where Compliance_Audit_ID=p_Compliance_Audit_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceAuditTrail
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceAuditTrail`;

DELIMITER $$
USE `compliancedb`$$
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
from Compliance_Audit_AuditTrail;
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
from Compliance_Audit_AuditTrail
where Compliance_Audit_ID=p_Compliance_Audit_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceBranchMapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceBranchMapping`;

DELIMITER $$
USE `compliancedb`$$
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
from compliance_branch_mapping ;
else
select 
Org_Hier_ID,
Compliance_Xref_ID,
Financial_Year,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date
from compliance_branch_mapping 
where Branch_Mapping_ID=p_Branch_Mapping_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceList`(p_Country_ID int, p_Industry_Type_ID int)
begin 
if(p_Country_ID=0 && p_Industry_Type_ID=0)
then
select * from Compliance_Type ;
else
select Compliance_Type_ID,
Compliance_Type_Name,
Industry_Type_ID,Country_ID,Audit_Frequency,End_Date, Start_Date 

from Compliance_Type
 

where Country_ID= p_Country_ID and Industry_Type_ID=p_Industry_Type_ID ;
 end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceType
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceType`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceType`(p_compliance_type_ID int)
begin
if(p_compliance_type_ID=0)
then
Select * from compliance_type;
else
Select * from Compliance_type where Compliance_Type_ID=p_compliance_type_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceXref
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceXref`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceXref`(
p_Audit_Type_ID int 
)
begin
if(p_Audit_Type_ID=0)
then
SELECT `compliance_xref`.`Compliance_Xref_ID`,
    `compliance_xref`.`Comp_Category`,
    `compliance_xref`.`Comp_Description`,
    `compliance_xref`.`Is_Header`,
    `compliance_xref`.`level`,
    `compliance_xref`.`Comp_Order`,
    `compliance_xref`.`Risk_Category`,
    `compliance_xref`.`Risk_Description`,
    `compliance_xref`.`Recurrence`,
    `compliance_xref`.`Form`,
    `compliance_xref`.`Type`,
    `compliance_xref`.`Is_Best_Practice`,
    `compliance_xref`.`Version`,
    `compliance_xref`.`Effective_Start_Date`,
    `compliance_xref`.`Effective_End_Date`,
    `compliance_xref`.`Country_ID`,
    `compliance_xref`.`State_ID`,
    `compliance_xref`.`City_ID`,
    `compliance_xref`.`Last_Updated_Date`,
    `compliance_xref`.`User_ID`,
    `compliance_xref`.`Is_Active`,
    `compliance_xref`.`Compliance_Title`,
    `compliance_xref`.`Compliance_Parent_ID`,
    `compliance_xref`.`compl_def_consequence`,
    `compliance_xref`.`Audit_Type_ID`
FROM `compliancedb`.`compliance_xref`;
else
SELECT `compliance_xref`.`Compliance_Xref_ID`,
    `compliance_xref`.`Comp_Category`,
    `compliance_xref`.`Comp_Description`,
    `compliance_xref`.`Is_Header`,
    `compliance_xref`.`level`,
    `compliance_xref`.`Comp_Order`,
    `compliance_xref`.`Risk_Category`,
    `compliance_xref`.`Risk_Description`,
    `compliance_xref`.`Recurrence`,
    `compliance_xref`.`Form`,
    `compliance_xref`.`Type`,
    `compliance_xref`.`Is_Best_Practice`,
    `compliance_xref`.`Version`,
    `compliance_xref`.`Effective_Start_Date`,
    `compliance_xref`.`Effective_End_Date`,
    `compliance_xref`.`Country_ID`,
    `compliance_xref`.`State_ID`,
    `compliance_xref`.`City_ID`,
    `compliance_xref`.`Last_Updated_Date`,
    `compliance_xref`.`User_ID`,
    `compliance_xref`.`Is_Active`,
    `compliance_xref`.`Compliance_Title`,
    `compliance_xref`.`Compliance_Parent_ID`,
    `compliance_xref`.`compl_def_consequence`,
    `compliance_xref`.`Audit_Type_ID`
FROM `compliancedb`.`compliance_xref`
where Audit_Type_ID= p_Audit_Type_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceXrefAuditTrail
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceXrefAuditTrail`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceXrefAuditTrail`(
p_Compliance_Xref_ID int
)
begin
if(p_Compliance_Xref_ID=0)
then
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID, Action_Type,Is_Active from compliance_xref_audittrail;
else
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID, Action_Type,Is_Active from compliance_xref_audittrail
where Compliance_Xref_ID = p_Compliance_Xref_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceXrefData
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceXrefData`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceXrefData`(
p_Org_Hier_ID int,
p_Vendor_ID int,
p_compliancety_ID int,
p_Compliance_Parent_ID int 
)
begin
select * from compliance_xref where Compliance_Parent_ID=p_Compliance_Parent_ID and Compliance_Xref_ID in 
(Select Compliance_Xref_ID from  xref_comp_type_mapping where Compliance_Type_ID=p_compliancety_ID and Xref_Comp_Type_Map_ID in 
(Select Xref_Comp_Type_Map_ID from compliance_branch_mapping where Org_Hier_ID=p_Org_Hier_ID and Vendor_ID=p_Vendor_ID
));
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getComplianceXreftype
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getComplianceXreftype`;

DELIMITER $$
USE `compliancedb`$$
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
SELECT * FROM `compliancedb`.`compliance_xref`
where Compliance_Xref_ID in(Select Compliance_Xref_ID from xref_comp_type_mapping where Compliance_Type_ID=p_Audit_Type_ID) and Country_ID=p_Country_ID;
else
SELECT * FROM `compliancedb`.`compliance_xref`
where Compliance_Xref_ID in(Select Compliance_Xref_ID from xref_comp_type_mapping where Compliance_Type_ID=p_Audit_Type_ID)  and Country_ID=p_Country_ID and State_ID=p_State_ID and City_ID=p_City_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getCountry
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCountry`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCountry`()
begin
select *  from Country;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getDefaultCompanyLists
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getDefaultCompanyLists`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultCompanyLists`(p_Org_Hier_ID int)
begin 
if( p_Org_Hier_ID=0)
then
select * from org_hier where level = 2;
else
select org_hier.Org_Hier_ID,Company_Name,branch_location.Country_ID,branch_location.State_ID,branch_location.City_ID, Country_Name, State_Name, City_Name from org_hier
inner join branch_location on branch_location.Org_Hier_ID = org_hier.Org_Hier_ID 
inner join Country on  Country.Country_ID= branch_location.Country_ID 
inner join State on  State.State_ID= branch_location.State_ID 
inner join city on  city.City_ID= branch_location.City_ID 
where org_hier.Org_Hier_ID=p_Org_Hier_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getDefaultIndustryType
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getDefaultIndustryType`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultIndustryType`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select Org_Hier_ID,company_details.Industry_Type_ID, Industry_Name
from company_details

inner join industry_type_master on industry_type_master.Industry_Type_ID = company_details.Industry_Type_ID;
else
select Org_Hier_ID,company_details.Industry_Type_ID, Industry_Name
from company_details
inner join industry_type_master on industry_type_master.Industry_Type_ID = company_details.Industry_Type_ID

where Org_Hier_ID= p_Org_Hier_ID ;
 end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getGroupCompaniesList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getGroupCompaniesList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompaniesList`()
begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active, logo from org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1;

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getGroupCompaniesListDropDown
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getGroupCompaniesListDropDown`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompaniesListDropDown`()
begin
select Org_Hier_ID, Company_Name  from org_hier where Parent_Company_ID=0;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getGroupCompanyListActiveDeactive
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getGroupCompanyListActiveDeactive`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListActiveDeactive`()
begin
select Company_Name, Org_Hier_ID,Type,Is_Active, logo from org_hier where Parent_Company_ID=0 and Is_Delete = 0 ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getGroupCompanyListDropDown
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getGroupCompanyListDropDown`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListDropDown`()
begin  

select Company_Name, Org_Hier_ID,Is_Active, logo from org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 ;


end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getGroupHierJoin
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getGroupHierJoin`;

DELIMITER $$
USE `compliancedb`$$
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
from org_hier;
else 
select org_hier.Org_Hier_ID,
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
from org_hier 
where org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getIndustryTypeList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getIndustryTypeList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getIndustryTypeList`()
begin  

select *from industry_type_master ;

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getLoginData
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getLoginData`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getLoginData`(
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
select * from user where Email_ID= p_Email_ID and User_Password = p_User_Password;
update user set Last_Login=now() where User_ID in(Select User_ID where Email_ID= p_Email_ID);
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getMenulist
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getMenulist`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getMenulist`()
BEGIN
SELECT Menu_ID,Parent_MenuID,Menu_Name,Page_URL,Is_Active,icon 
FROM `menus`
where Parent_MenuID>0;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getMenus
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getMenus`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getMenus`(p_User_ID int,p_Parent_MenuID int)
begin
SELECT distinct `menus`.`Menu_ID`,`Parent_MenuID`,Menu_Name,Page_URL,Is_Active,icon
FROM `menus` left join usergroup_menu_map on menus.Menu_ID=usergroup_menu_map.Menu_ID
WHERE `User_Group_ID` in (select User_Group_ID from user_group_members where User_ID = p_User_ID) and `Parent_MenuID`=p_Parent_MenuID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getOrganizationHier
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getOrganizationHier`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getOrganizationHier`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from org_hier;
else 

select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from org_hier
where Org_Hier_ID = p_Org_Hier_ID;
End If;

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getOrganizationHierJoin
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getOrganizationHierJoin`;

DELIMITER $$
USE `compliancedb`$$
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
from org_hier;
else 
select org_hier.Org_Hier_ID,
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
company_details.Company_Details_ID,
company_details.Org_Hier_ID, 
Formal_Name, 
Calender_StartDate,
Calender_EndDate,
Auditing_Frequency,
Website,
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
branch_location.Location_ID,
branch_location.Org_Hier_ID,
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
from org_hier 
inner join  company_Details  on company_details.Org_Hier_ID = org_hier.Org_Hier_ID
inner join branch_location on branch_location.Org_Hier_ID = org_hier.Org_Hier_ID
where org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getParticularGroupCompaniesList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getParticularGroupCompaniesList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getParticularGroupCompaniesList`(p_Org_Hier_ID int)
begin  

select Company_Name, Org_Hier_ID,Is_Active, logo from org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 and
Org_Hier_ID = p_Org_Hier_ID;


end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getPrivilege
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getPrivilege`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getPrivilege`()
begin
select Privilege_ID,Privilege_Name,Privilege_Type,Is_Active from  privilege where Is_Active=1;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getRole
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getRole`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRole`(p_Role_ID int)
begin
if(p_Role_ID=0)
then
SELECT `role`.`Role_ID`,
    `role`.`Role_Name`,
    `role`.`Is_Active`,
    `role`.`Is_Group_Role`
FROM `compliancedb`.`role` where Is_Active=1;
else
SELECT `role`.`Role_ID`,
    `role`.`Role_Name`,
    `role`.`Is_Active`,
    `role`.`Is_Group_Role`
FROM `compliancedb`.`role`
WHERE `Role_ID` = p_Role_ID ;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getRoleList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getRoleList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRoleList`(p_flag int)
begin
if(p_flag=0)
then
select Role_ID,Role_Name from role where Is_Group_Role=0 and Is_Active=1;
else
select Role_ID,Role_Name from role where Is_Group_Role=1 and Is_Active=1;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getRolePrivilege
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getRolePrivilege`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRolePrivilege`(p_Role_ID int)
begin
select a.Privilege_ID,Privilege_Name,Privilege_Type,b.Is_Active from role_priv_map a left join privilege b 
on a.Privilege_ID=b.Privilege_ID where a.Role_ID=p_Role_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getRuleforBranch
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getRuleforBranch`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRuleforBranch`(p_Org_ID int,p_vendor_ID int)
begin
select Compliance_Xref_ID,Compliance_Title from compliance_xref where `compliance_xref`.`level`=2 and
Compliance_Xref_ID in(select Compliance_Xref_ID from xref_comp_type_mapping where Xref_Comp_Type_Map_ID in (select Xref_Comp_Type_Map_ID from compliance_branch_mapping where Org_Hier_ID= p_Org_ID and Vendor_ID=p_vendor_ID));
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getRules
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getRules`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRules`(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
then
SELECT * FROM `compliancedb`.`compliance_xref`
where Comp_Category='Rule' and `compliance_xref`.`level`=2;
else
SELECT `compliance_xref`.`Compliance_Xref_ID`,
  `compliance_xref`.`Compliance_Title`
  FROM `compliancedb`.`compliance_xref`
  where Comp_Category='Rule' and `compliance_xref`.`level`=2 and Compliance_Parent_ID=p_Compliance_Parent_ID;
  end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getSpecificBranchList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getSpecificBranchList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificBranchList`(p_Parent_Company_ID int)
begin 
if(p_Parent_Company_ID=0)
then
select Company_Name, Org_Hier_ID,Type,Is_Active,logo from org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0;
else
 
select Company_Name, Org_Hier_ID,Type,Is_Active ,logo from org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0
 and Parent_Company_ID= p_Parent_Company_ID ;
 end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getSpecificBranchListDropDown
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getSpecificBranchListDropDown`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificBranchListDropDown`(p_Parent_Company_ID int)
begin 
select Company_Name, Org_Hier_ID,Type,Is_Active ,logo from org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0 and Is_Active=1
 and Parent_Company_ID= p_Parent_Company_ID ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getSpecificVendorList
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getSpecificVendorList`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificVendorList`(p_Parent_Company_ID int)
begin 
if(p_Parent_Company_ID=0)
then
select Company_Name, Org_Hier_ID,Type,Is_Active,logo, Is_Vendor from org_hier where level=3 and Is_Vendor=1 and Is_Delete = 0;
else
 
select Company_Name, Org_Hier_ID,Type,Is_Active,logo, Is_Vendor from org_hier where level=3 and Is_Vendor=1  and Is_Delete = 0
 and Parent_Company_ID= p_Parent_Company_ID ;
 end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getSpecificVendorListDropDown
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getSpecificVendorListDropDown`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificVendorListDropDown`(p_Parent_Company_ID int,p_Branch_ID int)
begin 
select * from org_hier where Parent_Company_ID=p_Parent_Company_ID and Is_Vendor=1 and level=3 and Is_Delete = 0 and Is_Active = 1 and Org_Hier_ID Not In
(select Vendor_ID from vendor_branch_mapping where Branch_ID=p_Branch_ID and vendor_branch_mapping.Is_Active = 1);
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getState
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getState`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getState`(
p_Country_ID int
)
begin
if(p_Country_ID=0)
then
select *  from state;
else
select *  from state where Country_ID= p_Country_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getUser
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getUser`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUser`(p_User_ID int)
begin
if(p_User_ID=0)
then
SELECT `user`.`User_ID`,
    `user`.`User_Password`,
    `user`.`First_Name`,
    `user`.`Middle_Name`,
    `user`.`Last_Name`,
    `user`.`Email_ID`,
    `user`.`Contact_Number`,
    `user`.`Gender`,
    `user`.`Is_Active`,
    `user`.`Last_Login`
FROM `compliancedb`.`user` where Is_Active=1;
else
SELECT `user`.`User_ID`,
    `user`.`User_Password`,
    `user`.`First_Name`,
    `user`.`Middle_Name`,
    `user`.`Last_Name`,
    `user`.`Email_ID`,
    `user`.`Contact_Number`,
    `user`.`Gender`,
    `user`.`Is_Active`,
    `user`.`Last_Login`
FROM `compliancedb`.`user` 
where User_ID = p_User_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getUserGroup
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getUserGroup`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserGroup`(p_User_Group_ID int)
begin
if(p_User_Group_ID=0)
then
SELECT `user_group`.`User_Group_ID`,
    `user_group`.`User_Group_Name`,
    `user_group`.`User_Group_Description`,
    `user_group`.`Role_ID`
FROM `compliancedb`.`user_group`where Is_Active=1;
else
SELECT `user_group`.`User_Group_ID`,
    `user_group`.`User_Group_Name`,
    `user_group`.`User_Group_Description`,
    `user_group`.`Role_ID`,
    `user_group`.`Is_Active`
FROM `compliancedb`.`user_group`
WHERE `User_Group_ID` = p_User_Group_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getUserGroupRole
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getUserGroupRole`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserGroupRole`()
begin
select Role_ID,Role_Name from role where Is_Group_Role=1 and Is_Active=1;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getUserRole
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getUserRole`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserRole`(p_User_ID int)
begin
select a.Role_ID,Role_Name from user_role_map a left join role b on a.Role_ID=b.Role_ID where User_ID=p_User_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getUserassignedGroup
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getUserassignedGroup`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getUserassignedGroup`(p_User_ID int)
begin
SELECT a.User_Group_ID,a.User_Group_Name
FROM `compliancedb`.`user_group` a left join user_group_members b on a.User_Group_ID=b.User_Group_ID
WHERE b.User_ID =p_User_ID ;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getVendorJoin
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getVendorJoin`;

DELIMITER $$
USE `compliancedb`$$
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
from org_hier;
else 
select org_hier.Org_Hier_ID,
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
company_details.Company_Details_ID,
company_details.Org_Hier_ID, 
Formal_Name, 
Calender_StartDate,
Calender_EndDate,
Auditing_Frequency,
Website,
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
Industry_Type_ID

from org_hier 
inner join  company_Details  on company_details.Org_Hier_ID = org_hier.Org_Hier_ID

where org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getVendorsForBranch
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getVendorsForBranch`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getVendorsForBranch`(p_Branch_ID int)
begin 
if(p_Branch_ID=0)
then
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,vendor_branch_mapping.Is_Active ,
Company_Name,Type,logo 
 from vendor_branch_mapping
inner join org_hier on org_hier.Org_Hier_ID = vendor_branch_mapping.Vendor_ID;
else
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,vendor_branch_mapping.Is_Active ,
Company_Name,Type,logo 
 from vendor_branch_mapping
inner join org_hier on org_hier.Org_Hier_ID = vendor_branch_mapping.Vendor_ID
where Branch_ID= p_Branch_ID  and vendor_branch_mapping.Is_Active = 1;
 end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_get_xref_comp_type_mapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_get_xref_comp_type_mapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_get_xref_comp_type_mapping`(p_Compliance_Type_ID int)
BEGIN
Select * from xref_comp_type_mapping where Compliance_Type_ID=p_Compliance_Type_ID;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_getlineitems
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getlineitems`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getlineitems`(p_Compliance_Parent_ID int)
BEGIN
if(p_Compliance_Parent_ID=0)
then
SELECT * FROM `compliancedb`.`compliance_xref`
where `compliance_xref`.`level`=2;
else
select * from `compliancedb`.`compliance_xref`
where Compliance_Parent_ID=p_Compliance_Parent_ID and `compliance_xref`.`level`=2;
end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertComplianceAuditTrail
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertComplianceAuditTrail`;

DELIMITER $$
USE `compliancedb`$$
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
insert into Compliance_Audit_AuditTrail
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

-- -----------------------------------------------------
-- procedure sp_insertComplianceXrefAuditTrail
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertComplianceXrefAuditTrail`;

DELIMITER $$
USE `compliancedb`$$
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
insert into compliance_xref_audittrail(Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID, Action_Type,Is_Active )

values(p_Comp_Category, p_Comp_Description,p_Is_Header,p_level,p_Comp_Order,p_Option_ID,p_Risk_Category,
p_Risk_Description,p_Recurrence,p_Form,p_Type,p_Is_Best_Practice ,p_Version,p_Effective_Start_Date,p_Effective_End_Date,
p_Country_ID ,p_State_ID ,p_City_ID ,p_User_ID,p_Action_Type,p_Is_Active,Now()  );

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertLoginData
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertLoginData`;

DELIMITER $$
USE `compliancedb`$$
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
insert into user values
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

-- -----------------------------------------------------
-- procedure sp_insertRolePrivilege
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertRolePrivilege`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertRolePrivilege`(p_Role_ID int,p_Privilege_ID int,p_Is_Active bit)
begin
INSERT INTO `compliancedb`.`role_priv_map`
(`Is_Active`,
`Role_ID`,
`Privilege_ID`)
VALUES
(p_Is_Active,p_Role_ID,p_Privilege_ID);
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertUserGroupMembers
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertUserGroupMembers`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertUserGroupMembers`(p_User_ID int,p_User_Group_ID int)
begin
INSERT INTO `compliancedb`.`user_group_members`
(`User_ID`,
`User_Group_ID`)
VALUES
(p_User_ID,p_User_Group_ID);
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertUserRole
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertUserRole`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertUserRole`(p_Role_ID int,p_User_ID int)
begin
INSERT INTO `compliancedb`.`user_role_map`
(`Role_ID`,
`User_ID`)
VALUES
(p_Role_ID,p_User_ID);
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insert_User_Menu_Map
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insert_User_Menu_Map`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insert_User_Menu_Map`(p_UserGroup_Id int,p_Menu_Id int)
begin
INSERT INTO `compliancedb`.`usergroup_menu_map`
(`User_Group_ID`,
`Menu_ID`)
VALUES
(p_UserGroup_Id,p_Menu_Id);
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateBranchAuditorMapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateBranchAuditorMapping`;

DELIMITER $$
USE `compliancedb`$$
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
insert into branch_auditor_mapping
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
update branch_auditor_mapping set
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

-- -----------------------------------------------------
-- procedure sp_insertupdateBranchLocation
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateBranchLocation`;

DELIMITER $$
USE `compliancedb`$$
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
insert into branch_location
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
update branch_location set
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

-- -----------------------------------------------------
-- procedure sp_insertupdateCompanyDetails
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateCompanyDetails`;

DELIMITER $$
USE `compliancedb`$$
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
insert into company_details
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
update company_details set
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

-- -----------------------------------------------------
-- procedure sp_insertupdateComplianceAudit
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateComplianceAudit`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceAudit`(
p_flag char(1),
p_Compliance_Audit_ID int(11) , 
p_Xref_Comp_Type_Map_ID int(11), 
p_Org_Hier_ID int(11) ,
p_Auditor_ID int(11) ,
p_Audit_Followup_Date datetime ,
p_Audit_Remarks varchar(1000) ,
p_Is_Active bit(1) ,
p_Version int(11) ,
p_Compliance_Status varchar(100) ,
p_Applicability varchar(10) ,
p_Start_Date datetime ,
p_End_Date datetime ,
p_Vendor_ID int(11) ,
p_Risk_Category varchar(10) ,
p_Evidences varchar(1000)
)
Begin
if(p_flag = 'I') then
insert into compliance_audit
(
Xref_Comp_Type_Map_ID,
Org_Hier_ID,
Auditor_ID,
Audit_Date,
Audit_Followup_Date,
Audit_Remarks,
Is_Active,
Version,
Compliance_Status,
Applicability, 
Start_Date, 
End_Date, 
Vendor_ID, 
Risk_Category, 
Evidences
)
values
(
p_Xref_Comp_Type_Map_ID,
p_Org_Hier_ID,
p_Auditor_ID,
Now(),
p_Audit_Followup_Date,
p_Audit_Remarks,
p_Is_Active,
p_Version,
p_Compliance_Status,
p_Applicability, 
p_Start_Date, 
p_End_Date, 
p_Vendor_ID, 
p_Risk_Category, 
p_Evidences
);
else
INSERT INTO Compliance_Audit_AuditTrail
(
select
Xref_Comp_Type_Map_ID,
Org_Hier_ID,
Auditor_ID,
Audit_Date,
Audit_Followup_Date,
Audit_Remarks,
Is_Active,
Version,
Compliance_Status,
Applicability, 
Start_Date, 
End_Date, 
Vendor_ID, 
Risk_Category, 
Evidences,
"update" As 'Action_Type'
from compliance_audit Where Compliance_Audit_ID = p_Compliance_Audit_ID);
update compliance_audit set 
Xref_Comp_Type_Map_ID=p_Xref_Comp_Type_Map_ID, 
Org_Hier_ID=p_Org_Hier_ID,
Auditor_ID=p_Auditor_ID,
Audit_Date=Now(),
Audit_Followup_Date=p_Audit_Followup_Date,
Audit_Remarks=p_Audit_Remarks,
Is_Active=p_Is_Active,
Version=p_Version,
Compliance_Status=p_Compliance_Status,
Applicability= p_Applicability,
Start_Date= p_Start_Date,
End_Date= p_End_Date,
Vendor_ID= p_Vendor_ID,
Risk_Category= p_Risk_Category,
Evidences=p_Evidences
where Compliance_Audit_ID= p_Compliance_Audit_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateComplianceBranchMapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateComplianceBranchMapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceBranchMapping`(
p_Org_Hier_ID int ,
p_Compliance_ID int ,
p_Is_Active bit,
p_UpdatedByLogin_ID int,
p_Vendor_Id int,
p_Compliancetypeid int
)
begin
DECLARE p_xref_comp_type_map_Id int;
set p_xref_comp_type_map_Id=(Select Xref_Comp_Type_Map_ID from xref_comp_type_mapping where Compliance_Xref_ID=p_Compliance_ID and Compliance_Type_ID=p_Compliancetypeid);
insert into compliance_branch_mapping
(
Org_Hier_ID,
Xref_Comp_Type_Map_ID,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date,
Vendor_ID
)
values
(
p_Org_Hier_ID,
p_xref_comp_type_map_Id,
p_Is_Active,
p_UpdatedByLogin_ID,
now(),
p_Vendor_Id
);
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateComplianceTypeMapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateComplianceTypeMapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceTypeMapping`(
p_Flag char(1),
p_compliance_type_map_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Type_ID int
)
begin
if(p_Flag = 'I') then
insert into Compliance_Type_mapping
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
update Compliance_Type_mapping set

Org_Hier_ID=p_Org_Hier_ID,
Compliance_Type_ID=p_Compliance_Type_ID
where compliance_type_map_ID=p_compliance_type_map_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateComplianceXref
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateComplianceXref`;

DELIMITER $$
USE `compliancedb`$$
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
p_Periodicity varchar(50),
p_Is_Best_Practice tinyint,
p_Version int(3),
p_Effective_Start_Date datetime,
p_Effective_End_Date datetime,
p_Country_ID int,
p_State_ID int,
p_City_ID int,
p_User_ID int ,
p_Is_Active bit
)
begin
if(p_Flag ='I') then

insert into compliance_xref(Comp_Category,Compliance_Title,Comp_Description,compl_def_consequence,Is_Header,level,Comp_Order,Risk_Category,
Risk_Description,Periodicity,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID, Is_Active,Last_Updated_Date,Compliance_Parent_ID )

values(p_Comp_Category,p_Compliance_Title, p_Comp_Description,p_compl_def_consequence,p_Is_Header,p_level,p_Comp_Order,p_Risk_Category,
p_Risk_Description,p_Periodicity,p_Is_Best_Practice ,p_Version,p_Effective_Start_Date,p_Effective_End_Date,
p_Country_ID ,p_State_ID ,p_City_ID ,p_User_ID,p_Is_Active,Now(),p_Compliance_Parent_ID );
select last_insert_id();
else

INSERT INTO compliance_xref_audittrail(Select Comp_Category,Compliance_Title,Comp_Description,p_compl_def_consequence,Is_Header,level,Comp_Order,Risk_Category,
Risk_Description,Periodicity,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,Statesp_insertupdateComplianceXref_ID ,City_ID ,User_ID,Is_Active,"update" As 'Action_Type'
from compliance_xref Where Compliance_Xref_ID = Compliance_Xref_ID);

update compliance_xref set

Comp_Category=p_Comp_Category,Compliance_Title=p_Compliance_Title, Comp_Description=p_Comp_Description,compl_def_consequence=p_compl_def_consequence,Is_Header=p_Is_Header,level=p_level,Comp_Order=p_Comp_Order,Risk_Category=p_Risk_Category,
Risk_Description=p_Risk_Description,Periodicity=p_Periodicity,Is_Best_Practice=p_Is_Best_Practice ,Version=p_Version,Effective_Start_Date=p_Effective_Start_Date,Effective_End_Date=p_Effective_End_Date,
Country_ID=p_Country_ID ,State_ID=p_State_ID ,City_ID=p_City_ID ,Last_Updated_Date=Now(),User_ID=p_User_ID, Is_Active=p_Is_Active
where Compliance_Xref_ID=p_Compliance_Xref_ID;
select row_count();
end if;

end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateOrganizationHier
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateOrganizationHier`;

DELIMITER $$
USE `compliancedb`$$
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
insert into org_hier
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
update org_hier
set
org_hier.Org_Hier_ID=p_Org_Hier_ID,
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
where org_hier.Org_Hier_ID=p_Org_Hier_ID ;
select row_count();
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateRole
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateRole`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateRole`(p_flag char(1),p_Role_ID int,p_Role_Name varchar(45),p_Is_Active bit,p_Is_Group_Role bit)
begin
if(p_flag='I')
then
INSERT INTO `compliancedb`.`role`
(`Role_Name`,
`Is_Active`,
`Is_Group_Role`)
VALUES
(p_Role_Name,p_Is_Active,p_Is_Group_Role);
select last_insert_id();
else
UPDATE `compliancedb`.`role`
SET
`Role_Name` = p_Role_Name,
`Is_Active` = p_Is_Active,
`Is_Group_Role` = p_Is_Group_Role
WHERE `Role_ID` = p_Role_ID ;
select last_insert_id();
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateUser
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateUser`;

DELIMITER $$
USE `compliancedb`$$
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
if exists(select Email_ID from `user` where Email_ID=p_Email_ID)
then
select "EXISTS";
else
INSERT INTO `user`(`User_ID`,
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
UPDATE `user`
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

-- -----------------------------------------------------
-- procedure sp_insertupdateUserGroup
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateUserGroup`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateUserGroup`(p_flag char(1),p_User_Group_ID int,p_User_Group_Name varchar(45),
p_User_Group_Description varchar(45),p_Role_ID int,p_Is_Active bit)
begin
if(p_flag='I')
then
INSERT INTO `compliancedb`.`user_group`
(`User_Group_Name`,
`User_Group_Description`,
`Role_ID`,
`Is_Active`)
VALUES
(p_User_Group_Name,p_User_Group_Description,p_Role_ID,p_Is_Active);
else
UPDATE `compliancedb`.`user_group`
SET
`User_Group_Name` = p_User_Group_Name,
`User_Group_Description` = p_User_Group_Description,
`Role_ID` = p_Role_ID
WHERE `User_Group_ID` = p_User_Group_ID;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sp_insertupdateVendorForBranch
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateVendorForBranch`;

DELIMITER $$
USE `compliancedb`$$
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
insert into vendor_branch_mapping
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
update vendor_branch_mapping set
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

-- -----------------------------------------------------
-- procedure sp_updatePassword
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_updatePassword`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_updatePassword`(
p_User_ID int,
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
update user set User_Password = p_User_Password where User_ID = p_User_ID;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure spinsertxref_comp_type_mapping
-- -----------------------------------------------------

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`spinsertxref_comp_type_mapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spinsertxref_comp_type_mapping`(
p_Compliance_Type_ID int,
p_Compliance_Xref_ID int
)
BEGIN
insert into xref_comp_type_mapping(Compliance_Type_ID,Compliance_Xref_ID) values(p_Compliance_Type_ID,p_Compliance_Xref_ID);
END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
