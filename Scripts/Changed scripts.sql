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

DELIMITER ;

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


USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getMenus`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getMenus`(p_User_ID int,p_Parent_MenuID int)
begin
SELECT distinct `tbl_menus`.`Menu_ID`,`Parent_MenuID`,Menu_Name,Page_URL,Is_Active,icon
FROM `tbl_menus` left join tbl_usergroup_menu_map on tbl_menus.Menu_ID=tbl_usergroup_menu_map.Menu_ID
WHERE `User_Group_ID` in (select User_Group_ID from tbl_user_group_members where User_ID = p_User_ID) and Parent_MenuID=p_Parent_MenuID;
end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getLoginData`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getLoginData`(
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
select * from tbl_user where Email_ID= p_Email_ID and User_Password = p_User_Password;
update tbl_user set Last_Login=now() where User_ID in(Select User_ID where Email_ID= p_Email_ID);
end$$

DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_insertupdateComplianceXref`;

DELIMITER $$
USE `auditmoduledb`$$
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
p_Audit_Type_ID int
)
begin
if(p_Flag ='I') then

insert into tbl_compliance_xref(Comp_Category,Compliance_Title,Comp_Description,compl_def_consequence,Is_Header,level,Comp_Order,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID, Is_Active,Last_Updated_Date,Compliance_Parent_ID, Audit_Type_ID )

values(p_Comp_Category,p_Compliance_Title, p_Comp_Description,p_compl_def_consequence,p_Is_Header,p_level,p_Comp_Order,p_Risk_Category,
p_Risk_Description,p_Recurrence,p_Form,p_Type,p_Is_Best_Practice ,p_Version,p_Effective_Start_Date,p_Effective_End_Date,
p_Country_ID ,p_State_ID ,p_City_ID ,p_User_ID,p_Is_Active,Now(),p_Compliance_Parent_ID , p_Audit_Type_ID);
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
Audit_Type_ID=p_Audit_Type_ID
where Compliance_Xref_ID=p_Compliance_Xref_ID;
select row_count();
end if;

end$$

DELIMITER ;




