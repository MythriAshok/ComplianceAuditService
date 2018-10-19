drop procedure if exists sp_getCompaniesListDropDown;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompaniesListDropDown`(
p_Parent_Company_ID int
)
begin
select org_hier.Org_Hier_ID,Company_Name,Calender_StartDate, Calender_EndDate  from org_hier
inner join company_details on company_details.Org_Hier_ID = org_hier.Org_Hier_ID 
 where Parent_Company_ID= p_Parent_Company_ID and Is_Active=1 and Is_Delete = 0 and level =2;
end$$
DELIMITER ;



drop  PROCEDURE if exists`sp_getDefaultCompanyLists`;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultCompanyLists`(p_Org_Hier_ID int)
begin 
if( p_Org_Hier_ID=0)
then
select * from org_hier where level = 2;
else
select org_hier.Org_Hier_ID,Company_Name,branch_location.Country_ID,Calender_StartDate, Calender_EndDate,
branch_location.State_ID,branch_location.City_ID, Country_Name, State_Name, City_Name from org_hier
inner join company_details on company_details.Org_Hier_ID = org_hier.Org_Hier_ID
inner join branch_location on branch_location.Org_Hier_ID = org_hier.Org_Hier_ID 
inner join Country on  Country.Country_ID= branch_location.Country_ID 
inner join State on  State.State_ID= branch_location.State_ID 
inner join city on  city.City_ID= branch_location.City_ID 
where org_hier.Org_Hier_ID=p_Org_Hier_ID;
end if;
end$$
DELIMITER ;



USE compliancedb;
drop  PROCEDURE if exists`sp_getParticularComplianceType`;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getParticularComplianceType`(p_Compliance_Type_ID int)
begin 
if( p_Compliance_Type_ID=0)
then
select * from compliance_Type ;
else
select * from compliance_Type 
where Compliance_Type_ID=p_Compliance_Type_ID;
end if;
end$$
DELIMITER ;



















