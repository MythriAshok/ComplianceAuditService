USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getSpecificBranchListDropDown`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificBranchListDropDown`(p_Parent_Company_ID int)
begin 


 
select Company_Name, Org_Hier_ID,Industry_Type,Is_Active ,logo from tbl_org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0 and Is_Active=1
 and Parent_Company_ID= p_Parent_Company_ID ;

end$$

DELIMITER 