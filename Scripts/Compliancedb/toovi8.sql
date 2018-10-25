DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceTypeMapping`(
p_Flag char(1),
p_compliance_type_map_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Type_ID int
)
begin
if(p_Flag = 'I') then
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
else
update Compliance_Type_mapping set

Org_Hier_ID=p_Org_Hier_ID,
Compliance_Type_ID=p_Compliance_Type_ID
where compliance_type_map_ID=p_compliance_type_map_ID;
end if;
end$$
DELIMITER ;
