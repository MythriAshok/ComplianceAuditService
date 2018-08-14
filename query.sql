alter table tbl_compliance_audit drop column Audit_Status;


alter table tbl_compliance_audit add column Audit_Status varchar(450);


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getVendorsForBranch`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getVendorsForBranch`(p_Branch_ID int)
begin 
if(p_Branch_ID=0)
then
select * from tbl_vendor_branch_mapping ;
else
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,tbl_vendor_branch_mapping.Is_Active ,
Company_Name,Industry_Type,logo 
 from tbl_vendor_branch_mapping
inner join tbl_org_hier on tbl_org_hier.Org_Hier_ID = tbl_vendor_branch_mapping.Vendor_ID
where Branch_ID= p_Branch_ID ;
 end if;
end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getAllCompanyBrnachAssignedtoAuditor`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAllCompanyBrnachAssignedtoAuditor`(
p_Auditor_ID int 
)
begin  
select * from tbl_org_hier where 
Org_Hier_ID IN(Select Org_Hier_ID from tbl_org_hier where Is_Active = 1 and Is_Vendor= 0 and level =3 ) 
Union
select * from tbl_org_hier where 
Org_Hier_ID IN(
Select distinct Parent_Company_ID from tbl_org_hier where 
Org_Hier_ID in (Select Org_Hier_ID from tbl_org_hier)) and Is_Active = 1 and Is_Vendor= 0 and level =2;
end$$

DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getDefaultCompanyLists`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultCompanyLists`(p_Org_Hier_ID int)
begin 
if( p_Org_Hier_ID=0)
then
select * from tbl_org_hier where level = 2;
else
select tbl_org_hier.Org_Hier_ID,Company_Name,tbl_branch_location.Country_ID, State_ID, City_ID, Country_Name from tbl_org_hier
inner join tbl_branch_location on tbl_branch_location.Org_Hier_ID = tbl_org_hier.Org_Hier_ID 
inner join tbl_Country on  tbl_Country.Country_ID= tbl_branch_location.Country_ID 
where tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID;
end if;
end$$

DELIMITER ;
