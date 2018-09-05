USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateComplianceTypeWithIndustry`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceTypeWithIndustry`(
p_Flag char(1),
p_Compliance_Type_ID int,
p_Compliance_Type_Name varchar(75),
p_Industry_Type_ID int, 
p_Country_ID int, 
p_Audit_Frequency varchar(50),
p_Start_Date date,
p_End_Date date

)
begin
if(p_Flag = 'I')then
insert into compliance_type
(
Compliance_Type_ID,
Compliance_Type_Name ,
Industry_Type_ID, 
Country_ID,
Audit_Frequency ,
Start_Date ,
End_Date
)
values
(
p_Compliance_Type_ID,
p_Compliance_Type_Name ,
p_Industry_Type_ID, 
p_Country_ID,
p_Audit_Frequency ,
p_Start_Date ,
p_End_Date
);
select last_insert_id();
else
update compliance_type set
Compliance_Type_ID=p_Compliance_Type_ID,
Compliance_Type_Name=p_Compliance_Type_Name ,
Industry_Type_ID=p_Industry_Type_ID, 
Country_ID=p_Country_ID,
Audit_Frequency =p_Audit_Frequency ,
Start_Date =p_Start_Date ,
End_Date=p_End_Date
where Compliance_Type_ID=p_Compliance_Type_ID;
select  row_count();
end if;
end$$

DELIMITER ;






USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompanyLists`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanyLists`(p_Parent_Company_ID int)
begin 
if( p_Parent_Company_ID=0)
then
select * from org_hier where level = 2 and Is_Delete=0;
else
select * from org_hier where level = 2 and Is_Delete=0 and Parent_Company_ID=p_Parent_Company_ID  ;
end if;
end$$

DELIMITER ;