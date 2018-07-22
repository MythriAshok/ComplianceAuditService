ALTER TABLE `auditmoduledb`.`tbl_country` 
ADD UNIQUE INDEX `Country_Code_UNIQUE` (`Country_Code` ASC);

ALTER TABLE `auditmoduledb`.`tbl_state` 
CHANGE COLUMN `State_Code` `State_Code` VARCHAR(5) NOT NULL ,
ADD UNIQUE INDEX `State_Code_UNIQUE` (`State_Code` ASC);

ALTER TABLE `auditmoduledb`.`tbl_org_hier` 
CHANGE COLUMN `Description` `Description` VARCHAR(450) NULL DEFAULT NULL ;

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

