

USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchCount`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchCount`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0 )
then
select * from org_hier ;
else
select * from org_hier where level = 3 and Is_Vendor = 0 and Parent_Company_ID = p_Org_Hier_ID;

end if;
end$$
DELIMITER ;




USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getCompliancedBranchCount`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompliancedBranchCount`(p_Org_Hier_ID int, p_Start_Date datetime, p_End_Date datetime,
 p_Compliance_Type_ID int)
begin
if(p_Org_Hier_ID = 0 )
then
select * from org_hier ;
else
select distinct org_hier.Org_Hier_ID, Company_Name,compliance_audit.Org_Hier_ID, compliance_audit.Vendor_ID, Version , Compliance_Status
from org_hier
inner join compliance_audit on compliance_audit.Org_Hier_ID = org_hier.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
where
Compliance_Status ='Compliant' and compliance_audit.Applicability = 'Yes' and 
compliance_audit.Start_Date >= p_Start_Date and compliance_audit.End_Date >= p_End_Date and Compliance_Type_ID= p_Compliance_Type_ID and
compliance_audit.Org_Hier_ID =compliance_audit.Vendor_ID and
org_hier.Parent_Company_ID = p_Org_Hier_ID and
compliance_audit.Xref_Comp_Type_Map_ID in(

select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where 
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version)
and org_hier.Org_Hier_ID  in (

select compliance_audit.Org_Hier_ID  from compliance_audit  where 
compliance_audit.Org_Hier_ID =compliance_audit.Vendor_ID and
compliance_audit.Xref_Comp_Type_Map_ID in(

select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where 
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version) and
compliance_audit.Org_Hier_ID not in(

select compliance_audit.Org_Hier_ID   from compliance_audit
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
where compliance_audit.Applicability = 'Yes' and 
compliance_audit.Start_Date >= p_Start_Date and compliance_audit.End_Date <= p_End_Date  and  Compliance_Status != 'Compliant' and
Compliance_Type_ID= p_Compliance_Type_ID and
compliance_audit.Xref_Comp_Type_Map_ID in(

select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where 
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version)
)))));
end if;
end$$
DELIMITER ;





USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getNonCompliancedPartiallyCompliancedBranchCount`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getNonCompliancedPartiallyCompliancedBranchCount`(p_Org_Hier_ID int, p_Start_Date datetime, p_End_Date datetime,
 p_Compliance_Type_ID int)
begin
if(p_Org_Hier_ID = 0 )
then
select * from org_hier ;
else
select distinct org_hier.Org_Hier_ID, Company_Name,compliance_audit.Org_Hier_ID, compliance_audit.Vendor_ID, Version , Compliance_Status
from org_hier
inner join compliance_audit on compliance_audit.Org_Hier_ID = org_hier.Org_Hier_ID
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
where
Compliance_Status !='Compliant' and compliance_audit.Applicability = 'Yes' and 
compliance_audit.Start_Date >= p_Start_Date and compliance_audit.End_Date <= p_End_Date and Compliance_Type_ID= p_Compliance_Type_ID and
compliance_audit.Org_Hier_ID =compliance_audit.Vendor_ID and
org_hier.Parent_Company_ID = p_Org_Hier_ID and
compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where 
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version)
and org_hier.Org_Hier_ID  in (
select compliance_audit.Org_Hier_ID  from compliance_audit  where 
compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where 
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version) and
compliance_audit.Org_Hier_ID  in(
select compliance_audit.Org_Hier_ID   from compliance_audit
inner join xref_comp_type_mapping on xref_comp_type_mapping.Xref_Comp_Type_Map_ID = compliance_audit.Xref_Comp_Type_Map_ID
where compliance_audit.Applicability = 'Yes' and 
compliance_audit.Start_Date >= p_Start_Date and compliance_audit.End_Date <= p_End_Date  and  Compliance_Status != 'Compliant' and
Compliance_Type_ID= p_Compliance_Type_ID and
compliance_audit.Xref_Comp_Type_Map_ID in(
select compliance_audit.Xref_Comp_Type_Map_ID from compliance_audit where 
compliance_audit.Xref_Comp_Type_Map_ID 
group by compliance_audit.Xref_Comp_Type_Map_ID having (compliance_audit.Version) = max(compliance_audit.Version)
)))));
end if;
end$$
DELIMITER ;



USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getBranchCountFromAuditTable`;
DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchCountFromAuditTable`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0 )
then
select * from org_hier ;
else
select distinct compliance_audit.Org_Hier_ID, org_hier.*   from org_hier
inner join compliance_audit on compliance_audit.Org_Hier_ID = org_hier.Org_Hier_ID
 where level = 3 and Is_Vendor = 0  and Parent_Company_ID = 58 ;

end if;
end$$
DELIMITER ;

















