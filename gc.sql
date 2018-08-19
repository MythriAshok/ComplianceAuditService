USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getGroupCompaniesList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompaniesList`()
begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1;

end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getParticularGroupCompaniesList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getParticularGroupCompaniesList`(p_Org_Hier_ID int)
begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 and
Org_Hier_ID = p_Org_Hier_ID;


end$$

DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getGroupCompanyListDropDown`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListDropDown`()
begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 ;


end$$
DELIMITER ;








USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getGroupCompanyListActiveDeactive`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListActiveDeactive`()
begin
select Company_Name, Org_Hier_ID,Industry_Type,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 ;
end$$

DELIMITER ;