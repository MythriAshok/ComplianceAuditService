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
DROP SCHEMA IF EXISTS `compliancedb` ;

-- -----------------------------------------------------
-- Schema compliancedb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `compliancedb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `compliancedb` ;

-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_user` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_user` (
  `User_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `User_Password` VARCHAR(10) NULL DEFAULT NULL,
  `First_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Middle_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Last_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Email_ID` VARCHAR(100) NULL DEFAULT NULL,
  `Contact_Number` VARCHAR(50) NULL DEFAULT NULL,
  `Company_ID` INT(11) NULL DEFAULT NULL,
  `Gender` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Last_Login` DATETIME NULL DEFAULT NULL,
  `Photo` VARCHAR(100) NULL DEFAULT NULL,
  PRIMARY KEY (`User_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_org_hier`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_org_hier` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_org_hier` (
  `Org_Hier_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Company_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Company_Code` INT(11) NULL DEFAULT NULL,
  `Parent_Company_ID` INT(11) NULL DEFAULT NULL,
  `Description` VARCHAR(450) NULL DEFAULT NULL,
  `level` INT(3) NULL DEFAULT NULL,
  `Is_Leaf` BIT(1) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `User_ID` INT(11) NOT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `logo` VARCHAR(100) NULL DEFAULT NULL,
  `Is_Delete` BIT(1) NULL DEFAULT NULL,
  `Is_Vendor` BIT(1) NULL DEFAULT NULL,
  `Type` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`Org_Hier_ID`),
  INDEX `User_ID` (`User_ID` ASC),
  CONSTRAINT `fk_User`
    FOREIGN KEY (`User_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_branch_auditor_mapping`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_branch_auditor_mapping` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_branch_auditor_mapping` (
  `Branch_Allocation_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Auditor_ID` INT(11) NOT NULL,
  `Financial_Year` DATETIME NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `UpdatedByLogin_ID` INT(11) NULL DEFAULT NULL,
  `Allocation_Date` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`Branch_Allocation_ID`),
  INDEX `Org_Hier_ID` (`Org_Hier_ID` ASC),
  INDEX `Auditor_ID` (`Auditor_ID` ASC),
  INDEX `UpdatedByLogin_ID` (`UpdatedByLogin_ID` ASC),
  CONSTRAINT `tbl_branch_auditor_mapping_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_branch_auditor_mapping_ibfk_2`
    FOREIGN KEY (`Auditor_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_branch_auditor_mapping_ibfk_3`
    FOREIGN KEY (`UpdatedByLogin_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_country`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_country` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_country` (
  `Country_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Country_Code` VARCHAR(5) NOT NULL,
  `Country_Name` VARCHAR(70) NOT NULL,
  PRIMARY KEY (`Country_ID`),
  UNIQUE INDEX `Country_Code_UNIQUE` (`Country_Code` ASC))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_state`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_state` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_state` (
  `State_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `State_Code` VARCHAR(5) NOT NULL,
  `State_Name` VARCHAR(70) NULL DEFAULT NULL,
  `Country_ID` INT(11) NOT NULL,
  PRIMARY KEY (`State_ID`),
  UNIQUE INDEX `State_Code_UNIQUE` (`State_Code` ASC),
  INDEX `FK_Country` (`Country_ID` ASC),
  CONSTRAINT `FK_Country`
    FOREIGN KEY (`Country_ID`)
    REFERENCES `compliancedb`.`tbl_country` (`Country_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_city`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_city` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_city` (
  `City_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `City_Name` VARCHAR(70) NULL DEFAULT NULL,
  `State_ID` INT(11) NOT NULL,
  PRIMARY KEY (`City_ID`),
  INDEX `FK_State` (`State_ID` ASC),
  CONSTRAINT `FK_State`
    FOREIGN KEY (`State_ID`)
    REFERENCES `compliancedb`.`tbl_state` (`State_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_branch_location`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_branch_location` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_branch_location` (
  `Location_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Location_Name` VARCHAR(75) NULL DEFAULT NULL,
  `Address` VARCHAR(450) NULL DEFAULT NULL,
  `Country_ID` INT(11) NOT NULL,
  `State_ID` INT(11) NOT NULL,
  `City_ID` INT(11) NOT NULL,
  `Postal_Code` VARCHAR(10) NULL DEFAULT NULL,
  `Branch_Coordinates1` VARCHAR(100) NULL DEFAULT NULL,
  `Branch_Coordinates2` VARCHAR(100) NULL DEFAULT NULL,
  `Branch_CoordinateURL` VARCHAR(100) NULL DEFAULT NULL,
  `Org_Hier_ID` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Location_ID`),
  INDEX `Country_ID` (`Country_ID` ASC),
  INDEX `State_ID` (`State_ID` ASC),
  INDEX `City_ID` (`City_ID` ASC),
  INDEX `fk_OrgID` (`Org_Hier_ID` ASC),
  CONSTRAINT `fk_OrgID`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_branch_location_ibfk_1`
    FOREIGN KEY (`Country_ID`)
    REFERENCES `compliancedb`.`tbl_country` (`Country_ID`),
  CONSTRAINT `tbl_branch_location_ibfk_2`
    FOREIGN KEY (`State_ID`)
    REFERENCES `compliancedb`.`tbl_state` (`State_ID`),
  CONSTRAINT `tbl_branch_location_ibfk_3`
    FOREIGN KEY (`City_ID`)
    REFERENCES `compliancedb`.`tbl_city` (`City_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_industry_type_master`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_industry_type_master` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_industry_type_master` (
  `Industry_Type_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Industry_Name` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`Industry_Type_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_company_details`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_company_details` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_company_details` (
  `Company_Details_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Formal_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Calender_StartDate` DATETIME NULL DEFAULT NULL,
  `Calender_EndDate` DATETIME NULL DEFAULT NULL,
  `Auditing_Frequency` VARCHAR(45) NULL DEFAULT NULL,
  `Website` VARCHAR(45) NULL DEFAULT NULL,
  `Company_Email_ID` VARCHAR(45) NULL DEFAULT NULL,
  `Company_ContactNumber1` VARCHAR(45) NULL DEFAULT NULL,
  `Company_ContactNumber2` VARCHAR(45) NULL DEFAULT NULL,
  `Industry_Type_ID` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Company_Details_ID`),
  INDEX `Org_Hier_ID` (`Org_Hier_ID` ASC),
  INDEX `Industry_Type_ID` (`Industry_Type_ID` ASC),
  CONSTRAINT `tbl_company_details_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_company_details_ibfk_2`
    FOREIGN KEY (`Industry_Type_ID`)
    REFERENCES `compliancedb`.`tbl_industry_type_master` (`Industry_Type_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_compliance_type`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_compliance_type` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_compliance_type` (
  `Compliance_Type_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Compliance_Type_Name` VARCHAR(50) NULL DEFAULT NULL,
  `Industry_Type_ID` INT(11) NOT NULL,
  `Country_ID` INT(11) NOT NULL,
  `Audit_Frequency` VARCHAR(50) NULL DEFAULT NULL,
  `End_Date` DATE NULL DEFAULT NULL,
  `Start_Date` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`Compliance_Type_ID`),
  INDEX `Industry_Type_ID` (`Industry_Type_ID` ASC),
  INDEX `Country_ID` (`Country_ID` ASC),
  CONSTRAINT `tbl_compliance_type_ibfk_1`
    FOREIGN KEY (`Industry_Type_ID`)
    REFERENCES `compliancedb`.`tbl_industry_type_master` (`Industry_Type_ID`),
  CONSTRAINT `tbl_compliance_type_ibfk_2`
    FOREIGN KEY (`Country_ID`)
    REFERENCES `compliancedb`.`tbl_country` (`Country_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_compliance_xref`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_compliance_xref` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_compliance_xref` (
  `Compliance_Xref_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Comp_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Comp_Description` VARCHAR(800) NULL DEFAULT NULL,
  `Is_Header` BIT(1) NULL DEFAULT NULL,
  `level` INT(3) NULL DEFAULT NULL,
  `Comp_Order` INT(3) NULL DEFAULT NULL,
  `Risk_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Risk_Description` VARCHAR(100) NULL DEFAULT NULL,
  `Periodicity` VARCHAR(50) NULL DEFAULT NULL,
  `Is_Best_Practice` BIT(1) NULL DEFAULT NULL,
  `Version` INT(3) NULL DEFAULT NULL,
  `Effective_Start_Date` DATETIME NULL DEFAULT NULL,
  `Effective_End_Date` DATETIME NULL DEFAULT NULL,
  `Country_ID` INT(11) NULL DEFAULT NULL,
  `State_ID` INT(11) NULL DEFAULT NULL,
  `City_ID` INT(11) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `User_ID` INT(11) NOT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Compliance_Title` VARCHAR(450) NULL DEFAULT NULL,
  `Compliance_Parent_ID` INT(11) NOT NULL,
  `compl_def_consequence` VARCHAR(1000) NULL DEFAULT NULL,
  PRIMARY KEY (`Compliance_Xref_ID`),
  INDEX `User_ID` (`User_ID` ASC),
  CONSTRAINT `tbl_compliance_xref_ibfk_1`
    FOREIGN KEY (`User_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_xref_comp_type_mapping`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_xref_comp_type_mapping` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_xref_comp_type_mapping` (
  `Xref_Comp_Type_Map_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Compliance_Type_ID` INT(11) NOT NULL,
  `Compliance_Xref_ID` INT(11) NOT NULL,
  PRIMARY KEY (`Xref_Comp_Type_Map_ID`),
  INDEX `Compliance_Type_ID` (`Compliance_Type_ID` ASC),
  INDEX `Compliance_Xref_ID` (`Compliance_Xref_ID` ASC),
  CONSTRAINT `tbl_xref_comp_type_mapping_ibfk_1`
    FOREIGN KEY (`Compliance_Type_ID`)
    REFERENCES `compliancedb`.`tbl_compliance_type` (`Compliance_Type_ID`),
  CONSTRAINT `tbl_xref_comp_type_mapping_ibfk_2`
    FOREIGN KEY (`Compliance_Xref_ID`)
    REFERENCES `compliancedb`.`tbl_compliance_xref` (`Compliance_Xref_ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_compliance_audit`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_compliance_audit` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_compliance_audit` (
  `Compliance_Audit_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Xref_Comp_Type_Map_ID` INT(11) NOT NULL,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Auditor_ID` INT(11) NOT NULL,
  `Audit_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Followup_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Remarks` VARCHAR(1000) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Version` INT(11) NULL DEFAULT NULL,
  `Compliance_Status` VARCHAR(100) NULL DEFAULT NULL,
  `Applicability` VARCHAR(10) NULL DEFAULT NULL,
  `Start_Date` DATETIME NULL DEFAULT NULL,
  `End_Date` DATETIME NULL DEFAULT NULL,
  `Vendor_ID` INT(11) NOT NULL,
  `Risk_Category` VARCHAR(10) NULL DEFAULT NULL,
  `Evidences` VARCHAR(1000) NULL DEFAULT NULL,
  PRIMARY KEY (`Compliance_Audit_ID`),
  INDEX `Xref_Comp_Type_Map_ID` (`Xref_Comp_Type_Map_ID` ASC),
  INDEX `Org_Hier_ID` (`Org_Hier_ID` ASC),
  INDEX `Auditor_ID` (`Auditor_ID` ASC),
  INDEX `Vendor_ID` (`Vendor_ID` ASC),
  CONSTRAINT `tbl_compliance_audit_ibfk_1`
    FOREIGN KEY (`Xref_Comp_Type_Map_ID`)
    REFERENCES `compliancedb`.`tbl_xref_comp_type_mapping` (`Xref_Comp_Type_Map_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_2`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_3`
    FOREIGN KEY (`Auditor_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_compliance_audit_ibfk_4`
    FOREIGN KEY (`Vendor_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_compliance_audit_audittrail`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_compliance_audit_audittrail` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_compliance_audit_audittrail` (
  `Compliance_Audit_Trail_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Xref_Comp_Type_Map_ID` INT(11) NOT NULL,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Auditor_ID` INT(11) NOT NULL,
  `Audit_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Followup_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Remarks` VARCHAR(1000) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Version` INT(11) NULL DEFAULT NULL,
  `Compliance_Status` VARCHAR(100) NULL DEFAULT NULL,
  `Applicability` VARCHAR(10) NULL DEFAULT NULL,
  `Start_Date` DATETIME NULL DEFAULT NULL,
  `End_Date` DATETIME NULL DEFAULT NULL,
  `Vendor_ID` INT(11) NOT NULL,
  `Risk_Category` VARCHAR(10) NULL DEFAULT NULL,
  `Evidences` VARCHAR(1000) NULL DEFAULT NULL,
  `Action_Type` VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (`Compliance_Audit_Trail_ID`),
  INDEX `Xref_Comp_Type_Map_ID` (`Xref_Comp_Type_Map_ID` ASC),
  INDEX `Org_Hier_ID` (`Org_Hier_ID` ASC),
  INDEX `Auditor_ID` (`Auditor_ID` ASC),
  INDEX `Vendor_ID` (`Vendor_ID` ASC),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_1`
    FOREIGN KEY (`Xref_Comp_Type_Map_ID`)
    REFERENCES `compliancedb`.`tbl_xref_comp_type_mapping` (`Xref_Comp_Type_Map_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_2`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_3`
    FOREIGN KEY (`Auditor_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_compliance_audit_audittrail_ibfk_4`
    FOREIGN KEY (`Vendor_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_compliance_branch_mapping`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_compliance_branch_mapping` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_compliance_branch_mapping` (
  `Branch_Mapping_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Financial_Year` YEAR(4) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `UpdatedByLogin_ID` INT(11) NULL DEFAULT NULL,
  `Allocation_Date` DATETIME NULL DEFAULT NULL,
  `Vendor_ID` INT(11) NULL DEFAULT NULL,
  `Auditing_start_date` DATE NULL DEFAULT NULL,
  `Auditing_end_date` DATE NULL DEFAULT NULL,
  `Xref_Comp_Type_Map_ID` INT(11) NOT NULL,
  PRIMARY KEY (`Branch_Mapping_ID`),
  INDEX `Org_Hier_ID` (`Org_Hier_ID` ASC),
  INDEX `UpdatedByLogin_ID` (`UpdatedByLogin_ID` ASC),
  INDEX `Vendor_ID` (`Vendor_ID` ASC),
  INDEX `Xref_Comp_Type_Map_ID` (`Xref_Comp_Type_Map_ID` ASC),
  CONSTRAINT `Vendor_ID`
    FOREIGN KEY (`Vendor_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `tbl_compliance_branch_mapping_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_compliance_branch_mapping_ibfk_3`
    FOREIGN KEY (`UpdatedByLogin_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_compliance_branch_mapping_ibfk_4`
    FOREIGN KEY (`Xref_Comp_Type_Map_ID`)
    REFERENCES `compliancedb`.`tbl_xref_comp_type_mapping` (`Xref_Comp_Type_Map_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_compliance_type_mapping`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_compliance_type_mapping` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_compliance_type_mapping` (
  `compliance_type_map_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Compliance_Type_ID` INT(11) NOT NULL,
  PRIMARY KEY (`compliance_type_map_ID`),
  INDEX `Org_Hier_ID` (`Org_Hier_ID` ASC),
  INDEX `Compliance_Type_ID` (`Compliance_Type_ID` ASC),
  CONSTRAINT `tbl_compliance_type_mapping_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_compliance_type_mapping_ibfk_2`
    FOREIGN KEY (`Compliance_Type_ID`)
    REFERENCES `compliancedb`.`tbl_compliance_type` (`Compliance_Type_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_compliance_xref_audittrail`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_compliance_xref_audittrail` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_compliance_xref_audittrail` (
  `Compliance_Xref_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Comp_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Comp_Description` VARCHAR(800) NULL DEFAULT NULL,
  `Is_Header` BIT(1) NULL DEFAULT NULL,
  `level` INT(3) NULL DEFAULT NULL,
  `Comp_Order` INT(3) NULL DEFAULT NULL,
  `Risk_Category` VARCHAR(45) NULL DEFAULT NULL,
  `Risk_Description` VARCHAR(100) NULL DEFAULT NULL,
  `Periodicity` VARCHAR(50) NULL DEFAULT NULL,
  `Is_Best_Practice` BIT(1) NULL DEFAULT NULL,
  `Version` INT(3) NULL DEFAULT NULL,
  `Effective_Start_Date` DATETIME NULL DEFAULT NULL,
  `Effective_End_Date` DATETIME NULL DEFAULT NULL,
  `Country_ID` INT(11) NULL DEFAULT NULL,
  `State_ID` INT(11) NULL DEFAULT NULL,
  `City_ID` INT(11) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `User_ID` INT(11) NOT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Action_Type` VARCHAR(10) NULL DEFAULT NULL,
  `Audit_Type` VARCHAR(45) NULL DEFAULT NULL,
  `Compliance_Title` VARCHAR(450) NULL DEFAULT NULL,
  `Compliance_Parent_ID` INT(11) NULL DEFAULT NULL,
  `compl_def_consequence` VARCHAR(1000) NULL DEFAULT NULL,
  PRIMARY KEY (`Compliance_Xref_ID`),
  INDEX `User_ID` (`User_ID` ASC),
  CONSTRAINT `tbl_compliance_xref_audittrail_ibfk_1`
    FOREIGN KEY (`User_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_custom_audit`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_custom_audit` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_custom_audit` (
  `Custom_Audit_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Org_Hier_ID` INT(11) NOT NULL,
  `Auditor_ID` INT(11) NOT NULL,
  `Audit_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Followup_Date` DATETIME NULL DEFAULT NULL,
  `Audit_Remarks` VARCHAR(1000) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Version` INT(11) NULL DEFAULT NULL,
  `Compliance_Status` VARCHAR(100) NULL DEFAULT NULL,
  `Applicability` VARCHAR(10) NULL DEFAULT NULL,
  `Start_Date` DATETIME NULL DEFAULT NULL,
  `End_Date` DATETIME NULL DEFAULT NULL,
  `Risk_Category` VARCHAR(10) NULL DEFAULT NULL,
  `Vendor_ID` INT(11) NOT NULL,
  `Evidences` VARCHAR(1000) NULL DEFAULT NULL,
  PRIMARY KEY (`Custom_Audit_ID`),
  INDEX `Org_Hier_ID` (`Org_Hier_ID` ASC),
  INDEX `Auditor_ID` (`Auditor_ID` ASC),
  INDEX `Vendor_ID` (`Vendor_ID` ASC),
  CONSTRAINT `tbl_custom_audit_ibfk_1`
    FOREIGN KEY (`Org_Hier_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_custom_audit_ibfk_2`
    FOREIGN KEY (`Auditor_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_custom_audit_ibfk_3`
    FOREIGN KEY (`Vendor_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_custom_xref`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_custom_xref` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_custom_xref` (
  `Custom_Xref_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Comp_Category` VARCHAR(50) NULL DEFAULT NULL,
  `Comp_Description` VARCHAR(50) NULL DEFAULT NULL,
  `Is_Header` BIT(1) NULL DEFAULT NULL,
  `Level` INT(11) NULL DEFAULT NULL,
  `Comp_Order` INT(11) NULL DEFAULT NULL,
  `Periodicity` VARCHAR(50) NULL DEFAULT NULL,
  `Is_Best_Practice` BIT(1) NULL DEFAULT NULL,
  `Version` INT(11) NULL DEFAULT NULL,
  `Effective_Start_Date` DATETIME NULL DEFAULT NULL,
  `Effective_End_Date` DATETIME NULL DEFAULT NULL,
  `Country_ID` INT(11) NULL DEFAULT NULL,
  `State_ID` INT(11) NULL DEFAULT NULL,
  `City_ID` INT(11) NULL DEFAULT NULL,
  `Last_Updated_Date` DATETIME NULL DEFAULT NULL,
  `User_ID` INT(11) NOT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Compliance_Title` VARCHAR(50) NULL DEFAULT NULL,
  `Compliance_Parent_ID` INT(11) NULL DEFAULT NULL,
  `Comp_def_consequence` VARCHAR(1000) NULL DEFAULT NULL,
  PRIMARY KEY (`Custom_Xref_ID`),
  INDEX `User_ID` (`User_ID` ASC),
  CONSTRAINT `tbl_custom_xref_ibfk_1`
    FOREIGN KEY (`User_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_menus`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_menus` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_menus` (
  `Menu_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Parent_MenuID` INT(11) NULL DEFAULT NULL,
  `Menu_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Page_URL` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `icon` VARCHAR(100) NULL DEFAULT NULL,
  PRIMARY KEY (`Menu_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_privilege`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_privilege` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_privilege` (
  `Privilege_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Privilege_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Privilege_Type` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Privilege_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_role`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_role` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_role` (
  `Role_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Role_Name` VARCHAR(45) NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Is_Group_Role` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Role_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_role_priv_map`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_role_priv_map` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_role_priv_map` (
  `Role_Priv_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  `Role_ID` INT(11) NOT NULL,
  `Privilege_ID` INT(11) NOT NULL,
  PRIMARY KEY (`Role_Priv_ID`),
  INDEX `Role_ID` (`Role_ID` ASC),
  INDEX `Privilege_ID` (`Privilege_ID` ASC),
  CONSTRAINT `tbl_role_priv_map_ibfk_1`
    FOREIGN KEY (`Role_ID`)
    REFERENCES `compliancedb`.`tbl_role` (`Role_ID`),
  CONSTRAINT `tbl_role_priv_map_ibfk_2`
    FOREIGN KEY (`Privilege_ID`)
    REFERENCES `compliancedb`.`tbl_privilege` (`Privilege_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_user_group`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_user_group` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_user_group` (
  `User_Group_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `User_Group_Name` VARCHAR(45) NULL DEFAULT NULL,
  `User_Group_Description` VARCHAR(450) NULL DEFAULT NULL,
  `Role_ID` INT(11) NOT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`User_Group_ID`),
  INDEX `Role_ID` (`Role_ID` ASC),
  CONSTRAINT `tbl_user_group_ibfk_1`
    FOREIGN KEY (`Role_ID`)
    REFERENCES `compliancedb`.`tbl_role` (`Role_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_user_group_members`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_user_group_members` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_user_group_members` (
  `User_Group_Members_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `User_ID` INT(11) NOT NULL,
  `User_Group_ID` INT(11) NOT NULL,
  PRIMARY KEY (`User_Group_Members_ID`),
  INDEX `User_ID` (`User_ID` ASC),
  INDEX `User_Group_ID` (`User_Group_ID` ASC),
  CONSTRAINT `tbl_user_group_members_ibfk_1`
    FOREIGN KEY (`User_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`),
  CONSTRAINT `tbl_user_group_members_ibfk_2`
    FOREIGN KEY (`User_Group_ID`)
    REFERENCES `compliancedb`.`tbl_user_group` (`User_Group_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_user_role_map`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_user_role_map` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_user_role_map` (
  `User_Role_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Role_ID` INT(11) NOT NULL,
  `User_ID` INT(11) NOT NULL,
  PRIMARY KEY (`User_Role_ID`),
  INDEX `Role_ID` (`Role_ID` ASC),
  INDEX `User_ID` (`User_ID` ASC),
  CONSTRAINT `tbl_user_role_map_ibfk_1`
    FOREIGN KEY (`Role_ID`)
    REFERENCES `compliancedb`.`tbl_role` (`Role_ID`),
  CONSTRAINT `tbl_user_role_map_ibfk_2`
    FOREIGN KEY (`User_ID`)
    REFERENCES `compliancedb`.`tbl_user` (`User_ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_usergroup_menu_map`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_usergroup_menu_map` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_usergroup_menu_map` (
  `UserGroup_Menu_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `User_Group_ID` INT(11) NOT NULL,
  `Menu_ID` INT(11) NOT NULL,
  PRIMARY KEY (`UserGroup_Menu_ID`),
  INDEX `User_Group_Id_idx` (`User_Group_ID` ASC),
  INDEX `Menu_ID_idx` (`Menu_ID` ASC),
  CONSTRAINT `Menu_ID`
    FOREIGN KEY (`Menu_ID`)
    REFERENCES `compliancedb`.`tbl_menus` (`Menu_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `User_Group_Id`
    FOREIGN KEY (`User_Group_ID`)
    REFERENCES `compliancedb`.`tbl_user_group` (`User_Group_ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `compliancedb`.`tbl_vendor_branch_mapping`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `compliancedb`.`tbl_vendor_branch_mapping` ;

CREATE TABLE IF NOT EXISTS `compliancedb`.`tbl_vendor_branch_mapping` (
  `Vendor_Branch_ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Branch_ID` INT(11) NOT NULL,
  `Vendor_ID` INT(11) NOT NULL,
  `Start_Date` DATETIME NULL DEFAULT NULL,
  `End_Date` DATETIME NULL DEFAULT NULL,
  `Is_Active` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Vendor_Branch_ID`),
  INDEX `Branch_ID` (`Branch_ID` ASC),
  INDEX `Vendor_ID` (`Vendor_ID` ASC),
  CONSTRAINT `tbl_vendor_branch_mapping_ibfk_1`
    FOREIGN KEY (`Branch_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`),
  CONSTRAINT `tbl_vendor_branch_mapping_ibfk_2`
    FOREIGN KEY (`Vendor_ID`)
    REFERENCES `compliancedb`.`tbl_org_hier` (`Org_Hier_ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 0
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
