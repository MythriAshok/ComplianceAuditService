USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompiledBranchComplianceAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompiledBranchComplianceAuditReport`
(p_Org_Hier_ID int,p_Compliance_Status varchar(100),p_Start_Date datetime, p_End_Date datetime, p_Compliance_Type_ID int, p_Vendor_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;
elseif(p_Compliance_Status= 'Compliant')
then
select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name,
xref_comp_type_mapping.*, compliance_type.Compliance_Type_ID,compliance_type.Audit_Frequency,compliance_type.Compliance_Type_Name 
from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
inner join compliance_type on compliance_type.Compliance_Type_ID = xref_comp_type_mapping.Compliance_Type_ID
where  compliance_audit.Org_Hier_ID= p_Org_Hier_ID and compliance_audit.Start_Date >= p_Start_Date
and compliance_audit.End_Date <= p_End_Date and Compliance_Status= p_Compliance_Status 

and compliance_audit.Xref_Comp_Type_Map_ID in(
select xref_comp_type_mapping.Xref_Comp_Type_Map_ID from xref_comp_type_mapping where Compliance_Type_ID=p_Compliance_Type_ID)
and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Org_Hier_ID = p_Org_Hier_ID  and compliance_audit.Vendor_ID and
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));


elseif(p_Compliance_Status= 'Non Compliant')
then
select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name,
xref_comp_type_mapping.*, compliance_type.Compliance_Type_ID,compliance_type.Audit_Frequency,compliance_type.Compliance_Type_Name 
from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
inner join compliance_type on compliance_type.Compliance_Type_ID = xref_comp_type_mapping.Compliance_Type_ID
where  compliance_audit.Org_Hier_ID= p_Org_Hier_ID and compliance_audit.Start_Date >= p_Start_Date
and compliance_audit.End_Date <= p_End_Date and Compliance_Status= p_Compliance_Status and compliance_audit.Xref_Comp_Type_Map_ID in(
select xref_comp_type_mapping.Xref_Comp_Type_Map_ID from xref_comp_type_mapping where Compliance_Type_ID=p_Compliance_Type_ID)
and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Org_Hier_ID = p_Org_Hier_ID and compliance_audit.Vendor_ID and
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));



elseif(p_Compliance_Status= 'Partially Compliant')
then
select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name,
xref_comp_type_mapping.*, compliance_type.Compliance_Type_ID,compliance_type.Audit_Frequency,compliance_type.Compliance_Type_Name 
from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
inner join compliance_type on compliance_type.Compliance_Type_ID = xref_comp_type_mapping.Compliance_Type_ID
where  compliance_audit.Org_Hier_ID= p_Org_Hier_ID and compliance_audit.Start_Date >= p_Start_Date
and compliance_audit.End_Date <= p_End_Date and Compliance_Status= p_Compliance_Status and compliance_audit.Xref_Comp_Type_Map_ID in(
select xref_comp_type_mapping.Xref_Comp_Type_Map_ID from xref_comp_type_mapping where Compliance_Type_ID=p_Compliance_Type_ID)
and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Org_Hier_ID = p_Org_Hier_ID and compliance_audit.Vendor_ID and
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));

end if;
end$$
DELIMITER ;


USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getDetailedBranchComplianceAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDetailedBranchComplianceAuditReport`
(p_Org_Hier_ID int,
p_Start_Date datetime,
p_End_Date datetime ,
p_Compliance_Type_ID int,
p_Vendor_ID int
)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;
else
select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name,
xref_comp_type_mapping.*, compliance_type.Compliance_Type_ID,compliance_type.Audit_Frequency,compliance_type.Compliance_Type_Name 
from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
inner join compliance_type on compliance_type.Compliance_Type_ID = xref_comp_type_mapping.Compliance_Type_ID
where  compliance_audit.Org_Hier_ID = p_Org_Hier_ID and compliance_audit.Start_Date >= p_Start_Date 
and compliance_audit.End_Date <= p_End_Date
and compliance_audit.Xref_Comp_Type_Map_ID in(
select xref_comp_type_mapping.Xref_Comp_Type_Map_ID from xref_comp_type_mapping where Compliance_Type_ID=p_Compliance_Type_ID)
and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Org_Hier_ID = p_Org_Hier_ID
  and compliance_audit.Vendor_ID = p_Vendor_ID and
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));
end if;
end$$	
DELIMITER ;




USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getDetailedBranchCompliance_ACTAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDetailedBranchCompliance_ACTAuditReport`(p_Org_Hier_ID int, p_Vendor_ID int)
begin 
if(p_Org_Hier_ID = 0 && p_Vendor_ID = 0)
then
select * from compliance_audit ;
else
select distinct compliance_xref.*    from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = p_Org_Hier_ID and Vendor_ID = p_Vendor_ID)));
end if;
end$$
DELIMITER ;




USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getACTCompliancedBranchCompliance_ACTAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getACTCompliancedBranchCompliance_ACTAuditReport`(p_Org_Hier_ID int,p_Compliance_Status varchar(100),
p_Vendor_ID int)
begin 
if(p_Org_Hier_ID = 0 && p_Vendor_ID = 0)
then
select * from compliance_audit ;
elseif(p_Compliance_Status= 'Compliant')
then
select distinct  compliance_xref.*     from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = p_Org_Hier_ID and Vendor_ID = p_Vendor_ID and
compliance_audit.Compliance_Status=p_Compliance_Status)));
elseif(p_Compliance_Status= 'Non Compliant')
then

select distinct  compliance_xref.*    from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = p_Org_Hier_ID and Vendor_ID = p_Vendor_ID and
 compliance_audit.Compliance_Status=p_Compliance_Status)));
elseif(p_Compliance_Status= 'Partially Compliant')
then

select distinct  compliance_xref.*    from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = p_Org_Hier_ID and Vendor_ID = p_Vendor_ID and
compliance_audit.Compliance_Status=p_Compliance_Status)));

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
select distinct  compliance_xref.*     from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = compliance_xref.Compliance_Xref_ID
where compliance_xref.Compliance_Xref_ID in(
select distinct Compliance_Parent_ID from compliance_xref where compliance_xref.Compliance_Xref_ID in(
select xref_comp_type_mapping.Compliance_Xref_ID from xref_comp_type_mapping where xref_comp_type_mapping.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where Org_Hier_ID = p_Org_Hier_ID and 
compliance_audit.Compliance_Status= 'Partially_Compliant')));
end if;
end$$
DELIMITER ;










USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getpieDetailedBranchComplianceAuditReport`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getpieDetailedBranchComplianceAuditReport`
(p_Org_Hier_ID int

)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;


else

select compliance_audit.*,  compliance_xref.*, Org_Hier.Company_Name from compliance_audit
inner join Org_Hier on Org_Hier.Org_Hier_ID = compliance_audit.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
inner join compliance_xref on compliance_xref.Compliance_Xref_ID = xref_comp_type_mapping.Compliance_Xref_ID
where  compliance_audit.Org_Hier_ID = p_Org_Hier_ID




 and compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version));

end if;
end$$





USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getYearofAuditReports`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getYearofAuditReports`
(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from compliance_audit ;
else
select compliance_audit.Start_Date, compliance_audit.End_Date,
year(compliance_audit.Start_Date), year(compliance_audit.End_Date),
monthname(compliance_audit.Start_Date),monthname(compliance_audit.End_Date),
 compliance_audit.Org_Hier_ID from compliance_audit
where Org_Hier_ID =  p_Org_Hier_ID;


end if;
end$$











