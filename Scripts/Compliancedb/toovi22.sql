DROP procedure IF EXISTS `compliancedb`.`sp_insertupdateComplianceTypeMapping`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceTypeMapping`(
p_Flag char(1),
p_compliance_type_map_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Type_ID int
)
begin
if(p_Flag = 'I') 
then
if exists(select * from `compliance_type_mapping` where Org_Hier_ID=p_Org_Hier_ID and Compliance_Type_ID= p_Compliance_Type_ID)
then
select "EXISTS";
else
insert into Compliance_Type_mapping
(
Org_Hier_ID,
Compliance_Type_ID
)
values
(
p_Org_Hier_ID,
p_Compliance_Type_ID
);
select last_insert_id();
end if;
else
update Compliance_Type_mapping set
Org_Hier_ID=p_Org_Hier_ID,
Compliance_Type_ID=p_Compliance_Type_ID
where compliance_type_map_ID=p_compliance_type_map_ID;
end if;
end$$
DELIMITER ;





USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_getVendorJoin`;

DELIMITER $$
USE `compliancedb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getVendorJoin`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select
Company_Name,
Company_Code, 
Parent_Company_ID, 
Description,
level,
Is_Leaf,
Type,
Last_Updated_Date,
logo,
User_ID, 
Is_Active ,
Is_Vendor
from org_hier;
else 
select org_hier.*,company_details.*, branch_location.*
from org_hier 
inner join  company_Details  on company_details.Org_Hier_ID = org_hier.Org_Hier_ID

inner join  branch_location  on branch_location.Org_Hier_ID = org_hier.Org_Hier_ID
where org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$
DELIMITER ;


