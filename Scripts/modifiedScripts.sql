-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema auditmoduledb
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `auditmoduledb` ;

-- -----------------------------------------------------
-- Schema auditmoduledb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `auditmoduledb` DEFAULT CHARACTER SET utf8mb4 ;
USE `auditmoduledb` ;

-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_country`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_country` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_country` (
  `Country_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Country_Code` VARCHAR(5) NOT NULL,
  `Country_Name` VARCHAR(70) NOT NULL,
  PRIMARY KEY (`Country_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_state`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_state` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_state` (
  `State_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `State_Code` VARCHAR(5) NULL DEFAULT NULL,
  `State_Name` VARCHAR(70) NULL DEFAULT NULL,
  `Country_ID` INT(11) NOT NULL,
  PRIMARY KEY (`State_ID`),
  CONSTRAINT `FK_Country`
    FOREIGN KEY (`Country_ID`)
    REFERENCES `auditmoduledb`.`tbl_country` (`Country_ID`))
ENGINE = InnoDB;

CREATE INDEX `FK_Country` ON `auditmoduledb`.`tbl_state` (`Country_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_city`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_city` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_city` (
  `City_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `City_Name` VARCHAR(70) NULL DEFAULT NULL,
  `State_ID` INT(11) NOT NULL,
  PRIMARY KEY (`City_ID`),
  CONSTRAINT `FK_State`
    FOREIGN KEY (`State_ID`)
    REFERENCES `auditmoduledb`.`tbl_state` (`State_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE INDEX `FK_State` ON `auditmoduledb`.`tbl_city` (`State_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_branch_location`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_branch_location` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_branch_location` (
  `Location_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Location_Name` VARCHAR(75) NULL DEFAULT NULL,
  `Adress` VARCHAR(450) NULL DEFAULT NULL,
  `Country_ID` INT(11) NOT NULL,
  `State_ID` INT(11) NOT NULL,
  `City_ID` INT(11) NOT NULL,
  `Postal_Code` VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (`Location_ID`),
  CONSTRAINT `tbl_branch_location_ibfk_1`
    FOREIGN KEY (`Country_ID`)
    REFERENCES `auditmoduledb`.`tbl_country` (`Country_ID`),
  CONSTRAINT `tbl_branch_location_ibfk_2`
    FOREIGN KEY (`State_ID`)
    REFERENCES `auditmoduledb`.`tbl_state` (`State_ID`),
  CONSTRAINT `tbl_branch_location_ibfk_3`
    FOREIGN KEY (`City_ID`)
    REFERENCES `auditmoduledb`.`tbl_city` (`City_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE INDEX `Country_ID` ON `auditmoduledb`.`tbl_branch_location` (`Country_ID` ASC);

CREATE INDEX `State_ID` ON `auditmoduledb`.`tbl_branch_location` (`State_ID` ASC);

CREATE INDEX `City_ID` ON `auditmoduledb`.`tbl_branch_location` (`City_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_user` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_user` (
  `User_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `User_Password` VARCHAR(10) NULL DEFAULT NULL,
  `First_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Middle_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Last_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Email_ID` VARCHAR(100) NULL DEFAULT NULL,
  `Contact_Number` VARCHAR(50) NULL DEFAULT NULL,
  `Gender` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Last_Login` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`User_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_org_hier`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_org_hier` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_org_hier` (
  `Org_Hier_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Company_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Company_ID` INT(11) NULL DEFAULT NULL,
  `Parent_Company_ID` INT(11) NULL DEFAULT NULL,
  `Description` VARCHAR(45) NULL DEFAULT NULL,
  `level` INT(3) NULL DEFAULT NULL,
  `Is_Leaf` BIT(1) NULL DEFAULT NULL,
  `Industry_Type` VARCHAR(45) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `Location_ID` INT(11) NOT NULL,
  `User_ID` INT(11) NOT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Org_Hier_ID`),
  CONSTRAINT `tbl_org_hier_ibfk_1`
    FOREIGN KEY (`Location_ID`)
    REFERENCES `auditmoduledb`.`tbl_branch_location` (`Location_ID`),
  CONSTRAINT `tbl_org_hier_ibfk_2`
    FOREIGN KEY (`User_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE INDEX `Location_ID` ON `auditmoduledb`.`tbl_org_hier` (`Location_ID` ASC);

CREATE INDEX `User_ID` ON `auditmoduledb`.`tbl_org_hier` (`User_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_branch_auditor_mapping`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_branch_auditor_mapping` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_branch_auditor_mapping` (
  `Branch_Allocation_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Auditor_ID` INT(11) NOT NULL,
  `Financial_Year` DATETIME NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Login_ID` INT(11) NULL DEFAULT NULL,
  `Allocation_Date` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`Branch_Allocation_ID`),
  CONSTRAINT `tbl_branch_auditor_mapping_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `auditmoduledb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_branch_auditor_mapping_ibfk_2`
    FOREIGN KEY (`Auditor_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_branch_auditor_mapping_ibfk_3`
    FOREIGN KEY (`Login_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`))
ENGINE = InnoDB;

CREATE INDEX `Org_Hier_ID` ON `auditmoduledb`.`tbl_branch_auditor_mapping` (`Org_Hier_ID` ASC);

CREATE INDEX `Auditor_ID` ON `auditmoduledb`.`tbl_branch_auditor_mapping` (`Auditor_ID` ASC);

CREATE INDEX `Login_ID` ON `auditmoduledb`.`tbl_branch_auditor_mapping` (`Login_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_company_details`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_company_details` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_company_details` (
  `Company_Details_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Industry_Type` VARCHAR(45) NULL DEFAULT NULL,
  `Formal_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Calender_StartDate` DATETIME NULL DEFAULT NULL,
  `Calender_EndDate` DATETIME NULL DEFAULT NULL,
  `Auditing_Frequency` VARCHAR(45) NULL DEFAULT NULL,
  `Website` VARCHAR(45) NULL DEFAULT NULL,
  `Company_Email_ID` VARCHAR(45) NULL DEFAULT NULL,
  `Company_ContactNumber1` VARCHAR(45) NULL DEFAULT NULL,
  `Company_ContactNumber2` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`Company_Details_ID`),
  CONSTRAINT `tbl_company_details_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `auditmoduledb`.`tbl_org_hier` (`Org_Hier_ID`))
ENGINE = InnoDB;

CREATE INDEX `Org_Hier_ID` ON `auditmoduledb`.`tbl_company_details` (`Org_Hier_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_compliance_xref`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_compliance_xref` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_compliance_xref` (
  `Compliance_Xref_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Comp_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Comp_Description` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Header` BIT(1) NULL DEFAULT NULL,
  `level` VARCHAR(5) NULL DEFAULT NULL,
  `Comp_Order` INT(3) NULL DEFAULT NULL,
  `Option_ID` INT(11) NULL DEFAULT NULL,
  `Risk_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Risk_Description` VARCHAR(100) NULL DEFAULT NULL,
  `Recurrence` VARCHAR(45) NULL DEFAULT NULL,
  `Form` VARCHAR(45) NULL DEFAULT NULL,
  `Type` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Best_Practice` BIT(1) NULL DEFAULT NULL,
  `Version` INT(3) NULL DEFAULT NULL,
  `Effective_Start_Date` DATETIME NULL DEFAULT NULL,
  `Effective_End_Date` DATETIME NULL DEFAULT NULL,
  `Country_ID` INT(11) NULL DEFAULT NULL,
  `State_ID` INT(11) NULL DEFAULT NULL,
  `City_ID` INT(11) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `User_ID` INT(11) NOT NULL,
  PRIMARY KEY (`Compliance_Xref_ID`),
  CONSTRAINT `tbl_compliance_xref_ibfk_1`
    FOREIGN KEY (`User_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE INDEX `User_ID` ON `auditmoduledb`.`tbl_compliance_xref` (`User_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_compliance_options_xref`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_compliance_options_xref` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_compliance_options_xref` (
  `Compliance_Opt_Xref_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Option_Text` VARCHAR(45) NULL DEFAULT NULL,
  `Option_Order` INT(3) NULL DEFAULT NULL,
  `Compliance_Xref_ID` INT(11) NOT NULL,
  PRIMARY KEY (`Compliance_Opt_Xref_ID`),
  CONSTRAINT `tbl_compliance_options_xref_ibfk_1`
    FOREIGN KEY (`Compliance_Xref_ID`)
    REFERENCES `auditmoduledb`.`tbl_compliance_xref` (`Compliance_Xref_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE INDEX `Compliance_Xref_ID` ON `auditmoduledb`.`tbl_compliance_options_xref` (`Compliance_Xref_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_compliance_audit`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_compliance_audit` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_compliance_audit` (
  `Compliance_Audit_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Comp_Schedule_Instance` INT(11) NULL DEFAULT NULL,
  `Penalty_nc` VARCHAR(150) NULL DEFAULT NULL,
  `Audit_Remarks` VARCHAR(150) NULL DEFAULT NULL,
  `Audit_artefacts` VARCHAR(150) NULL DEFAULT NULL,
  `Audit_Date` DATETIME NULL DEFAULT NULL,
  `Version` INT(11) NULL DEFAULT NULL,
  `Reviewer_ID` INT(11) NULL DEFAULT NULL,
  `Review_Comments` VARCHAR(500) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Status` VARCHAR(10) NULL DEFAULT NULL,
  `Compliance_Xref_ID` INT(11) NOT NULL,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Compliance_Opt_Xref_ID` INT(11) NOT NULL,
  `Auditor_ID` INT(11) NOT NULL,
  `User_ID` INT(11) NOT NULL,
  PRIMARY KEY (`Compliance_Audit_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_1`
    FOREIGN KEY (`Compliance_Xref_ID`)
    REFERENCES `auditmoduledb`.`tbl_compliance_xref` (`Compliance_Xref_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_2`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `auditmoduledb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_3`
    FOREIGN KEY (`Compliance_Opt_Xref_ID`)
    REFERENCES `auditmoduledb`.`tbl_compliance_options_xref` (`Compliance_Opt_Xref_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_4`
    FOREIGN KEY (`Auditor_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_5`
    FOREIGN KEY (`User_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 2;

CREATE INDEX `Compliance_Xref_ID` ON `auditmoduledb`.`tbl_compliance_audit` (`Compliance_Xref_ID` ASC);

CREATE INDEX `Org_Hier_ID` ON `auditmoduledb`.`tbl_compliance_audit` (`Org_Hier_ID` ASC);

CREATE INDEX `Compliance_Opt_Xref_ID` ON `auditmoduledb`.`tbl_compliance_audit` (`Compliance_Opt_Xref_ID` ASC);

CREATE INDEX `Auditor_ID` ON `auditmoduledb`.`tbl_compliance_audit` (`Auditor_ID` ASC);

CREATE INDEX `User_ID` ON `auditmoduledb`.`tbl_compliance_audit` (`User_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_compliance_audit_audittrail`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_compliance_audit_audittrail` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_compliance_audit_audittrail` (
  `Compliance_Audit_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Comp_Schedule_Instance` INT(11) NULL DEFAULT NULL,
  `Penalty_nc` VARCHAR(150) NULL DEFAULT NULL,
  `Audit_Remarks` VARCHAR(150) NULL DEFAULT NULL,
  `Audit_artefacts` VARCHAR(150) NULL DEFAULT NULL,
  `Audit_Date` DATETIME NULL DEFAULT NULL,
  `Version` INT(11) NULL DEFAULT NULL,
  `Reviewer_ID` INT(11) NULL DEFAULT NULL,
  `Review_Comments` VARCHAR(500) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Status` VARCHAR(10) NULL DEFAULT NULL,
  `Compliance_Xref_ID` INT(11) NOT NULL,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Compliance_Opt_Xref_ID` INT(11) NOT NULL,
  `Auditor_ID` INT(11) NOT NULL,
  `User_ID` INT(11) NOT NULL,
  `Action_Type` VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (`Compliance_Audit_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_1`
    FOREIGN KEY (`Compliance_Xref_ID`)
    REFERENCES `auditmoduledb`.`tbl_compliance_xref` (`Compliance_Xref_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_2`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `auditmoduledb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_3`
    FOREIGN KEY (`Compliance_Opt_Xref_ID`)
    REFERENCES `auditmoduledb`.`tbl_compliance_options_xref` (`Compliance_Opt_Xref_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_4`
    FOREIGN KEY (`Auditor_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_5`
    FOREIGN KEY (`User_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE INDEX `Compliance_Xref_ID` ON `auditmoduledb`.`tbl_compliance_audit_audittrail` (`Compliance_Xref_ID` ASC);

CREATE INDEX `Org_Hier_ID` ON `auditmoduledb`.`tbl_compliance_audit_audittrail` (`Org_Hier_ID` ASC);

CREATE INDEX `Compliance_Opt_Xref_ID` ON `auditmoduledb`.`tbl_compliance_audit_audittrail` (`Compliance_Opt_Xref_ID` ASC);

CREATE INDEX `Auditor_ID` ON `auditmoduledb`.`tbl_compliance_audit_audittrail` (`Auditor_ID` ASC);

CREATE INDEX `User_ID` ON `auditmoduledb`.`tbl_compliance_audit_audittrail` (`User_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_compliance_branch_mapping`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_compliance_branch_mapping` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_compliance_branch_mapping` (
  `Branch_Mapping_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Compliance_Xref_ID` INT(11) NOT NULL,
  `Financial_Year` DATETIME NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Login_ID` INT(11) NULL DEFAULT NULL,
  `Allocation_Date` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`Branch_Mapping_ID`),
  CONSTRAINT `tbl_compliance_branch_mapping_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `auditmoduledb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_compliance_branch_mapping_ibfk_2`
    FOREIGN KEY (`Compliance_Xref_ID`)
    REFERENCES `auditmoduledb`.`tbl_compliance_xref` (`Compliance_Xref_ID`),
  CONSTRAINT `tbl_compliance_branch_mapping_ibfk_3`
    FOREIGN KEY (`Login_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`))
ENGINE = InnoDB;

CREATE INDEX `Org_Hier_ID` ON `auditmoduledb`.`tbl_compliance_branch_mapping` (`Org_Hier_ID` ASC);

CREATE INDEX `Compliance_Xref_ID` ON `auditmoduledb`.`tbl_compliance_branch_mapping` (`Compliance_Xref_ID` ASC);

CREATE INDEX `Login_ID` ON `auditmoduledb`.`tbl_compliance_branch_mapping` (`Login_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_compliance_xref_audittrail`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_compliance_xref_audittrail` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_compliance_xref_audittrail` (
  `Compliance_Xref_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Comp_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Comp_Description` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Header` BIT(1) NULL DEFAULT NULL,
  `level` VARCHAR(5) NULL DEFAULT NULL,
  `Comp_Order` INT(3) NULL DEFAULT NULL,
  `Option_ID` INT(11) NULL DEFAULT NULL,
  `Risk_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Risk_Description` VARCHAR(100) NULL DEFAULT NULL,
  `Recurrence` VARCHAR(45) NULL DEFAULT NULL,
  `Form` VARCHAR(45) NULL DEFAULT NULL,
  `Type` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Best_Practice` BIT(1) NULL DEFAULT NULL,
  `Version` INT(3) NULL DEFAULT NULL,
  `Effective_Start_Date` DATETIME NULL DEFAULT NULL,
  `Effective_End_Date` DATETIME NULL DEFAULT NULL,
  `Country_ID` INT(11) NULL DEFAULT NULL,
  `State_ID` INT(11) NULL DEFAULT NULL,
  `City_ID` INT(11) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `User_ID` INT(11) NOT NULL,
  `Action_Type` VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (`Compliance_Xref_ID`),
  CONSTRAINT `tbl_compliance_xref_audittrail_ibfk_1`
    FOREIGN KEY (`User_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;

CREATE INDEX `User_ID` ON `auditmoduledb`.`tbl_compliance_xref_audittrail` (`User_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_role`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_role` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_role` (
  `Role_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Role_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Is_Group_Role` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Role_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_user_group`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_user_group` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_user_group` (
  `User_Group_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `User_Group_Name` VARCHAR(45) NULL DEFAULT NULL,
  `User_Group_Description` VARCHAR(45) NULL DEFAULT NULL,
  `Role_ID` INT(11) NOT NULL,
  PRIMARY KEY (`User_Group_ID`),
  CONSTRAINT `tbl_user_group_ibfk_1`
    FOREIGN KEY (`Role_ID`)
    REFERENCES `auditmoduledb`.`tbl_role` (`Role_ID`))
ENGINE = InnoDB;

CREATE INDEX `Role_ID` ON `auditmoduledb`.`tbl_user_group` (`Role_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_menus`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_menus` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_menus` (
  `Menu_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Parent_MenuID` INT(11) NULL DEFAULT NULL,
  `Menu_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Page_URL` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `User_Group_ID` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Menu_ID`),
  CONSTRAINT `tbl_menus_ibfk_1`
    FOREIGN KEY (`User_Group_ID`)
    REFERENCES `auditmoduledb`.`tbl_user_group` (`User_Group_ID`))
ENGINE = InnoDB;

CREATE INDEX `User_Group_ID` ON `auditmoduledb`.`tbl_menus` (`User_Group_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_privilege`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_privilege` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_privilege` (
  `Privilege_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Privilege_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Privilege_Type` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Privilege_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_role_priv_map`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_role_priv_map` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_role_priv_map` (
  `Role_Priv_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Role_ID` INT(11) NOT NULL,
  `Privilege_ID` INT(11) NOT NULL,
  PRIMARY KEY (`Role_Priv_ID`),
  CONSTRAINT `tbl_role_priv_map_ibfk_1`
    FOREIGN KEY (`Role_ID`)
    REFERENCES `auditmoduledb`.`tbl_role` (`Role_ID`),
  CONSTRAINT `tbl_role_priv_map_ibfk_2`
    FOREIGN KEY (`Privilege_ID`)
    REFERENCES `auditmoduledb`.`tbl_privilege` (`Privilege_ID`))
ENGINE = InnoDB;

CREATE INDEX `Role_ID` ON `auditmoduledb`.`tbl_role_priv_map` (`Role_ID` ASC);

CREATE INDEX `Privilege_ID` ON `auditmoduledb`.`tbl_role_priv_map` (`Privilege_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_user_group_members`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_user_group_members` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_user_group_members` (
  `User_Group_Members_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `User_ID` INT(11) NOT NULL,
  `User_Group_ID` INT(11) NOT NULL,
  PRIMARY KEY (`User_Group_Members_ID`),
  CONSTRAINT `tbl_user_group_members_ibfk_1`
    FOREIGN KEY (`User_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_user_group_members_ibfk_2`
    FOREIGN KEY (`User_Group_ID`)
    REFERENCES `auditmoduledb`.`tbl_user_group` (`User_Group_ID`))
ENGINE = InnoDB;

CREATE INDEX `User_ID` ON `auditmoduledb`.`tbl_user_group_members` (`User_ID` ASC);

CREATE INDEX `User_Group_ID` ON `auditmoduledb`.`tbl_user_group_members` (`User_Group_ID` ASC);


-- -----------------------------------------------------
-- Table `auditmoduledb`.`tbl_user_role_map`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `auditmoduledb`.`tbl_user_role_map` ;

CREATE TABLE IF NOT EXISTS `auditmoduledb`.`tbl_user_role_map` (
  `User_Role_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Role_ID` INT(11) NOT NULL,
  `User_ID` INT(11) NOT NULL,
  PRIMARY KEY (`User_Role_ID`),
  CONSTRAINT `tbl_user_role_map_ibfk_1`
    FOREIGN KEY (`Role_ID`)
    REFERENCES `auditmoduledb`.`tbl_role` (`Role_ID`),
  CONSTRAINT `tbl_user_role_map_ibfk_2`
    FOREIGN KEY (`User_ID`)
    REFERENCES `auditmoduledb`.`tbl_user` (`User_ID`))
ENGINE = InnoDB;

CREATE INDEX `Role_ID` ON `auditmoduledb`.`tbl_user_role_map` (`Role_ID` ASC);

CREATE INDEX `User_ID` ON `auditmoduledb`.`tbl_user_role_map` (`User_ID` ASC);


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
