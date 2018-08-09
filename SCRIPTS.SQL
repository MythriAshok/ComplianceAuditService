Use auditmoduledb;

ALTER TABLE `auditmoduledb`.`tbl_compliance_xref` 
DROP FOREIGN KEY `tbl_compliance_xref_ibfk_2`;
ALTER TABLE `auditmoduledb`.`tbl_compliance_xref` 
DROP COLUMN `Audit_Type_ID`,
DROP INDEX `tbl_compliance_xref_ibfk_2_idx` ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getRules`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRules`(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
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
    `tbl_compliance_xref`.`compl_def_consequence`
FROM `auditmoduledb`.`tbl_compliance_xref`
where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3;
else
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
  `tbl_compliance_xref`.`Compliance_Title`
  FROM `auditmoduledb`.`tbl_compliance_xref`
  where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3 and Compliance_Parent_ID=p_Compliance_Parent_ID;
  end if;
end$$

USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_insertupdateComplianceBranchMapping`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceBranchMapping`(
p_Org_Hier_ID int ,
p_Compliance_Xref_ID int ,
p_Is_Active bit,
p_UpdatedByLogin_ID int
)
begin
if exists(select Compliance_Xref_ID from `tbl_compliance_branch_mapping` where Org_Hier_ID=p_Org_Hier_ID and Compliance_Xref_ID=p_Compliance_Xref_ID)
then
select "EXISTS";
else
insert into tbl_compliance_branch_mapping
(
Org_Hier_ID,
Compliance_Xref_ID,
Is_Active,
UpdatedByLogin_ID,
Allocation_Date)
values
(
p_Org_Hier_ID,
p_Compliance_Xref_ID,
p_Is_Active,
p_UpdatedByLogin_ID,
now());
end if;
end$$

DELIMITER ;



DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getRuleforBranch`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRuleforBranch`(p_Org_ID int)
begin
select Compliance_Xref_ID,Compliance_Title from tbl_compliance_xref where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3 and
Compliance_Xref_ID in (select Compliance_Xref_ID from tbl_compliance_branch_mapping where Org_Hier_ID= p_Org_ID);
end$$

DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_insert_User_Menu_Map`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insert_User_Menu_Map`(p_UserGroup_Id int,p_Menu_Id int)
begin
INSERT INTO `auditmoduledb`.`tbl_usergroup_menu_map`
(`User_Group_ID`,
`Menu_ID`)
VALUES
(p_UserGroup_Id,p_Menu_Id);
end$$

DELIMITER ;

