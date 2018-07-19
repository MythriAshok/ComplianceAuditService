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
CHANGE COLUMN `Comp_Description` `Comp_Description` VARCHAR(450) NULL DEFAULT NULL ;

Alter table `auditmoduledb`.`tbl_compliance_xref`
add column `compl_def_consequence`  VARCHAR(1000) NULL DEFAULT NULL ;
