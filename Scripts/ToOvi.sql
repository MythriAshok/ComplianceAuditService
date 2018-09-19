DELIMITER $$
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





DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceList`(p_Country_ID int, p_Industry_Type_ID int)
begin 
if(p_Country_ID=0 && p_Industry_Type_ID=0)
then
select * from Compliance_Type ;
else
select Compliance_Type_ID,
Compliance_Type_Name,
Compliance_Type.Industry_Type_ID,Compliance_Type.Country_ID,Audit_Frequency,End_Date, Start_Date , Industry_Name, Country_Name

from Compliance_Type
 
inner join country on country.Country_ID = Compliance_Type.Country_ID
inner join industry_type_master on industry_type_master.Industry_Type_ID = Compliance_Type.Industry_Type_ID
where Compliance_Type.Country_ID= p_Country_ID and Compliance_Type.Industry_Type_ID=p_Industry_Type_ID ;
 end if;
end$$
DELIMITER ;





USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompiledBranchComplianceAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompiledBranchComplianceAuditReport`(p_Org_Hier_ID int,p_Compliance_Status varchar(100))
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;
elseif(p_Compliance_Status= 'Complianced')
then
select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
where  compliance_audit.Org_Hier_ID= p_Org_Hier_ID and Compliance_Status= p_Compliance_Status and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));

elseif(p_Compliance_Status= 'Non_Complianced')
then
select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
where  compliance_audit.Org_Hier_ID= p_Org_Hier_ID and Compliance_Status= p_Compliance_Status and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));


elseif(p_Compliance_Status= 'Partially_Complianced')
then
select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
where  compliance_audit.Org_Hier_ID= p_Org_Hier_ID and Compliance_Status= p_Compliance_Status and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));
end if;
end$$
DELIMITER ;

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getDetailedBranchComplianceAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDetailedBranchComplianceAuditReport`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;


else

select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
where  compliance_audit.Org_Hier_ID= p_Org_Hier_ID and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));

end if;
end$$	
DELIMITER ;






USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getDetailedBranchCompliance_ACTAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDetailedBranchCompliance_ACTAuditReport`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;
else
select distinct  compliance_xref.*, Org_Hier.Company_Name     from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = p_Org_Hier_ID)));
end if;
end$$
DELIMITER ;




USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getACTCompliancedBranchCompliance_ACTAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getACTCompliancedBranchCompliance_ACTAuditReport`(p_Org_Hier_ID int,p_Compliance_Status varchar(100))
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;
elseif(p_Compliance_Status= 'Complianced')
then
select distinct  compliance_xref.*, Org_Hier.Company_Name     from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = 70 and compliance_audit.Compliance_Status='Complianced')));
elseif(p_Compliance_Status= 'Non_Complianced')
then

select distinct  compliance_xref.*, Org_Hier.Company_Name     from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = 70 and compliance_audit.Compliance_Status='Non_Complianced')));
elseif(p_Compliance_Status= 'Partially_Complianced')
then

select distinct  compliance_xref.*, Org_Hier.Company_Name     from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = 70 and compliance_audit.Compliance_Status='Partially_Complianced')));

end if;
end$$
DELIMITER ;





USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getACT_Partially_CompliancedBranchCompliance_ACTAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getACT_Partially_CompliancedBranchCompliance_ACTAuditReport`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;
else
select distinct  compliance_xref.*, Org_Hier.Company_Name     from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = p_Org_Hier_ID and compliance_audit.Compliance_Status='partially_complianced')));
end if;
end$$
DELIMITER ;


