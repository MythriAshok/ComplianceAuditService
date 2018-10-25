use compliancedb;
drop procedure if exists sp_getDefaultCompanyLists;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultCompanyLists`(p_Org_Hier_ID int)
begin 
if( p_Org_Hier_ID=0)
then
select * from org_hier where level = 2;
else
select org_hier.*, company_details.*, branch_location.*, Country_Name, State_Name, City_Name from org_hier
inner join company_details on company_details.Org_Hier_ID = org_hier.Org_Hier_ID
inner join branch_location on branch_location.Org_Hier_ID = org_hier.Org_Hier_ID 
inner join Country on  Country.Country_ID= branch_location.Country_ID 
inner join State on  State.State_ID= branch_location.State_ID 
inner join city on  city.City_ID= branch_location.City_ID 
where org_hier.Org_Hier_ID=p_Org_Hier_ID;
end if;
end$$
DELIMITER ;
