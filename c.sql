USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getCompanyLists`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompanyLists`(p_Parent_Company_ID int)
begin 
if( p_Parent_Company_ID=0)
then
select * from tbl_org_hier where level = 2;
else
select * from tbl_org_hier where Parent_Company_ID=p_Parent_Company_ID  ;
end if;
end$$

DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getCompaniesListDropDown`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompaniesListDropDown`(
p_Parent_Company_ID int
)
begin
select Org_Hier_ID,Company_Name  from tbl_org_hier where Parent_Company_ID= p_Parent_Company_ID and Is_Active=1 and Is_Delete = 0 and level =2;
end$$

DELIMITER ;
