USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getSpecificVendorList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificVendorList`(p_Parent_Company_ID int)
begin 
if(p_Parent_Company_ID=0)
then
select Company_Name, Org_Hier_ID,Industry_Type,Is_Active,logo, Is_Vendor from tbl_org_hier where level=3 and Is_Vendor=1 and Is_Delete = 0;
else
 
select Company_Name, Org_Hier_ID,Industry_Type,Is_Active,logo, Is_Vendor from tbl_org_hier where level=3 and Is_Vendor=1  and Is_Delete = 0
 and Parent_Company_ID= p_Parent_Company_ID ;
 end if;
end$$

DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getSpecificVendorListDropDown`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificVendorListDropDown`(p_Parent_Company_ID int)
begin 

 
select Company_Name, Org_Hier_ID,Industry_Type,Is_Active,logo, Is_Vendor from tbl_org_hier where level=3 and Is_Vendor=1  and Is_Delete = 0
 and Parent_Company_ID= p_Parent_Company_ID and Is_Active = 1 ;
 
end$$

DELIMITER ;