ALTER TABLE `auditmoduledb`.`tbl_country` 
ADD UNIQUE INDEX `Country_Code_UNIQUE` (`Country_Code` ASC);

ALTER TABLE `auditmoduledb`.`tbl_state` 
CHANGE COLUMN `State_Code` `State_Code` VARCHAR(5) NOT NULL ,
ADD UNIQUE INDEX `State_Code_UNIQUE` (`State_Code` ASC);

ALTER TABLE `auditmoduledb`.`tbl_org_hier` 
CHANGE COLUMN `Description` `Description` VARCHAR(1000) NULL DEFAULT NULL ;


-- -----------------------------------------------------
-- procedure sp_insertupdateOrganizationHier
-- -----------------------------------------------------

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_insertupdateOrganizationHier`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateOrganizationHier`(
 p_Flag char(1),
 p_Org_Hier_ID int,
 p_Company_Name varchar(45),
 p_Company_Code int,
 p_Parent_Company_ID int,
 p_Description varchar(1000),
 p_level int,
 p_Is_Leaf tinyint,
 p_Industry_Type varchar(45),
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
Industry_Type, 
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
p_Industry_Type,
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
Industry_Type=p_Industry_Type,
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

ALTER TABLE `auditmoduledb`.`tbl_user_group` 
CHANGE COLUMN `User_Group_Description` `User_Group_Description` VARCHAR(450) NULL DEFAULT NULL ;

Alter table `auditmoduledb`.`tbl_compliance_xref`
add column `Compliance_Title`  VARCHAR(450) NULL DEFAULT NULL ;

Alter table `auditmoduledb`.`tbl_compliance_xref`
add column `Compliance_Parent_ID` int not null;

ALTER TABLE `auditmoduledb`.`tbl_compliance_xref` 
CHANGE COLUMN `level` `level` INT(3) NULL DEFAULT NULL ;

ALTER TABLE `auditmoduledb`.`tbl_compliance_xref` 
CHANGE COLUMN `Comp_Description` `Comp_Description` VARCHAR(800) NULL DEFAULT NULL ;

Alter table `auditmoduledb`.`tbl_compliance_xref`
add column `compl_def_consequence`  VARCHAR(800) NULL DEFAULT NULL ;

ALTER TABLE `auditmoduledb`.`tbl_menus` 
ADD COLUMN `icon` VARCHAR(100) NULL DEFAULT NULL AFTER `User_Group_ID`;

ALTER TABLE `auditmoduledb`.`tbl_compliance_audit` 
DROP FOREIGN KEY `tbl_compliance_audit_ibfk_3`;
ALTER TABLE `auditmoduledb`.`tbl_compliance_audit` 
DROP COLUMN `Compliance_Opt_Xref_ID`,
DROP INDEX `Compliance_Opt_Xref_ID` ;

CREATE TABLE `auditmoduledb`.`tbl_usergroup_menu_map` (
  `UserGroup_Menu_ID` INT NOT NULL AUTO_INCREMENT,
  `User_Group_ID` INT NOT NULL,
  `Menu_ID` INT NOT NULL,
  PRIMARY KEY (`UserGroup_Menu_ID`),
  INDEX `User_Group_Id_idx` (`User_Group_ID` ASC),
  INDEX `Menu_ID_idx` (`Menu_ID` ASC),
  CONSTRAINT `User_Group_Id`
    FOREIGN KEY (`User_Group_ID`)
    REFERENCES `auditmoduledb`.`tbl_user_group` (`User_Group_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `Menu_ID`
    FOREIGN KEY (`Menu_ID`)
    REFERENCES `auditmoduledb`.`tbl_menus` (`Menu_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);
    
ALTER TABLE `auditmoduledb`.`tbl_menus` 
DROP FOREIGN KEY `tbl_menus_ibfk_1`;
ALTER TABLE `auditmoduledb`.`tbl_menus` 
DROP COLUMN `User_Group_ID`,
DROP INDEX `User_Group_ID` ;


ALTER TABLE `auditmoduledb`.`tbl_company_details` 
ADD COLUMN `Compliance_Audit_Type` VARCHAR(100) NULL AFTER `Company_ContactNumber2`;


ALTER TABLE `auditmoduledb`.`tbl_compliance_audit` 
CHANGE COLUMN `Audit_Status` `Audit_Status` VARCHAR(450) NULL DEFAULT NULL ;




alter table tbl_org_hier drop foreign key  `tbl_org_hier_ibfk_2`;


alter table tbl_org_hier add foreign key  fk_User(User_ID) references tbl_user(User_ID);


alter table tbl_org_hier drop column Location_ID;
alter table tbl_branch_location add column Org_Hier_ID int;

alter  table tbl_branch_location add foreign key fk_OrgID(Org_Hier_ID) references tbl_org_hier(Org_Hier_ID);

alter table tbl_org_hier add column Is_Vendor bit;

drop table tbl_vendor_branch_mapping;

drop table if exists tbl_vendor_branch_mapping;
create table tbl_vendor_branch_mapping
(
Vendor_Branch_ID int primary key auto_increment not null,
Branch_ID int not null,
foreign key (Branch_ID) references tbl_org_hier(Org_Hier_ID),
Vendor_ID int not null,
foreign key (Vendor_ID) references tbl_org_hier(Org_Hier_ID),
Start_Date datetime,
End_Date datetime,
Is_Active bit
);

alter table tbl_compliance_xref add column Audit_Type varchar (100) null after compl_def_consequence;

alter table tbl_user add column Photo varchar (100) null default null;


alter table tbl_org_hier add column logo varchar (100) null default null;




