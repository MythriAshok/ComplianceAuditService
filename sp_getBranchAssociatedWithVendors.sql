USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getBranchAssociatedWithVendors`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchAssociatedWithVendors`(p_Vendor_ID int)
begin 
if(p_Vendor_ID=0)
then
select * from tbl_vendor_branch_mapping ;
else
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,tbl_vendor_branch_mapping.Is_Active ,
Company_Name,Industry_Type,logo 
 from tbl_vendor_branch_mapping
inner join tbl_org_hier on tbl_org_hier.Org_Hier_ID = tbl_vendor_branch_mapping.Branch_ID
where Vendor_ID= p_Vendor_ID ;
 end if;
end$$

DELIMITER ;
