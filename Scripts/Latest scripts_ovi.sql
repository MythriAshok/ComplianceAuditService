ALTER TABLE `auditmoduledb`.`audit_type` 
CHANGE COLUMN `Audit_Type_ID` `Compliance_Type_ID` INT(11) NOT NULL ,
CHANGE COLUMN `Audit_Type_Name` `Compliance_Type_Name` VARCHAR(100) NULL DEFAULT NULL , RENAME TO  `auditmoduledb`.`compliance_type` ;

ALTER TABLE `auditmoduledb`.`tbl_compliance_xref` 
DROP FOREIGN KEY `tbl_compliance_xref_ibfk_2`;
ALTER TABLE `auditmoduledb`.`tbl_compliance_xref` 
CHANGE COLUMN `Audit_Type_ID` `Compliance_Type_ID` INT(11) NULL DEFAULT NULL ;
ALTER TABLE `auditmoduledb`.`tbl_compliance_xref` 
ADD CONSTRAINT `tbl_compliance_xref_ibfk_2`
  FOREIGN KEY (`Compliance_Type_ID`)
  REFERENCES `auditmoduledb`.`compliance_type` (`Compliance_Type_ID`)
  ON DELETE CASCADE
  ON UPDATE CASCADE;

USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getRules`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getRules`(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
then
SELECT * FROM `auditmoduledb`.`tbl_compliance_xref`
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
DROP procedure IF EXISTS `sp_getSections`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSections`(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
then
SELECT * FROM `auditmoduledb`.`tbl_compliance_xref`
where Comp_Category='Section' and `tbl_compliance_xref`.`level`=2;
else
SELECT *  FROM `auditmoduledb`.`tbl_compliance_xref`
  where Comp_Category='Section' and `tbl_compliance_xref`.`level`=2 and Compliance_Parent_ID=p_Compliance_Parent_ID;
end if;
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

alter table tbl_compliance_branch_mapping 
add column Vendor_ID int,
Add constraint
FOREIGN KEY Vendor_ID (Vendor_ID)
REFERENCES tbl_org_hier(Org_Hier_ID)
ON DELETE cascade
ON UPDATE cascade;

alter table tbl_compliance_branch_mapping change Finanical_year Financial_Year  year;
alter table tbl_compliance_branch_mapping add column Auditing_start_date date;
alter table tbl_compliance_branch_mapping add column Auditing_end_date date;


USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getComplianceType`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE PROCEDURE `sp_getComplianceType`()
begin
Select * from compliance_type;
end$$
DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getSpecifiySection`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecifiySection`(p_Compliance_Xref_ID int)
BEGIN
SELECT * FROM `auditmoduledb`.`tbl_compliance_xref`
where Compliance_Xref_ID=p_Compliance_Xref_ID and `tbl_compliance_xref`.`level`=2 and Comp_Category='Section';
END$$

DELIMITER ;

