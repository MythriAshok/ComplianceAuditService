ALTER TABLE `auditmoduledb`.`tbl_country` 
ADD UNIQUE INDEX `Country_Code_UNIQUE` (`Country_Code` ASC);

ALTER TABLE `auditmoduledb`.`tbl_state` 
CHANGE COLUMN `State_Code` `State_Code` VARCHAR(5) NOT NULL ,
ADD UNIQUE INDEX `State_Code_UNIQUE` (`State_Code` ASC);

ALTER TABLE `auditmoduledb`.`tbl_org_hier` 
CHANGE COLUMN `Description` `Description` VARCHAR(450) NULL DEFAULT NULL ;

ALTER TABLE `auditmoduledb`.`tbl_user_group` 
CHANGE COLUMN `User_Group_Description` `User_Group_Description` VARCHAR(450) NULL DEFAULT NULL ;
