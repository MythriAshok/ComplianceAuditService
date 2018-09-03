use auditmoduledb;

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_insertupdateOrganizationHier`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateOrganizationHier`(
 p_Flag char(1),
 p_Org_Hier_ID int,
 p_Company_Name varchar(45),
 p_Company_Code int,
 p_Parent_Company_ID int,
 p_Description varchar(1000),
 p_level int,
 p_Is_Leaf tinyint,
 p_Type varchar(50),
 p_Last_Updated_Date datetime,
 p_logo varchar(100),
 p_User_ID int,
 p_Is_Active bit,
 p_Is_Delete bit,
 p_Is_Vendor bit
)
begin
if(p_Flag = 'I')then
insert into tbl_org_hier
(
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
Is_Active,
Is_Delete,
Is_Vendor)
values
(
p_Company_Name,
p_Company_Code, 
p_Parent_Company_ID, 
p_Description, 
p_level,
p_Is_Leaf,
p_Type,
now(),
p_logo,
p_User_ID,
p_Is_Active,
p_Is_Delete,
p_Is_Vendor);
select last_insert_id();
else
update tbl_org_hier
set
tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID,
Company_Name=p_Company_Name,
Company_Code=p_Company_Code,
Parent_Company_ID=p_Parent_Company_ID,
Description=p_Description,
level=p_level,
Is_Leaf=p_Is_Leaf, 
Type = p_Type,
Last_Updated_Date=now(),
 logo= p_logo,
User_ID=p_User_ID,
Is_Active=p_Is_Active,
Is_Delete = p_Is_Delete,
Is_Vendor = p_Is_Vendor
where tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID ;
select row_count();
end if;
end$$

DELIMITER ;


DROP procedure IF EXISTS `auditmoduledb`.`sp_getIndustryTypeList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getIndustryTypeList`()
begin  

select *from tbl_industry_type_master ;

end$$

DELIMITER ;



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getGroupHierJoin`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupHierJoin`(
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
Is_Active,
Is_Vendor 
from tbl_org_hier;
else 
select tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_COde, 
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Type,
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor
from tbl_org_hier 
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_insertupdateCompanyDetails`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateCompanyDetails`(
p_Flag char (1),
p_Company_Details_ID int ,
p_Org_Hier_ID int ,
p_Formal_Name varchar(45),
p_Calender_StartDate datetime,
p_Calender_EndDate datetime,
p_Auditing_Frequency varchar(45),
p_Website varchar(45),
p_Company_Email_ID varchar(45),
p_Company_ContactNumber1 varchar(45),
p_Company_ContactNumber2 varchar(45),
p_Industry_Type_ID int,
p_Is_Active bit
)
begin
if(p_Flag ='I') then
insert into tbl_company_details
(
Org_Hier_ID,
Formal_Name, 
Calender_StartDate, 
Calender_EndDate, 
Auditing_Frequency, 
Website, 
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
Industry_Type_ID 
)
values
(
p_Org_Hier_ID,
p_Formal_Name, 
p_Calender_StartDate, 
p_Calender_EndDate, 
p_Auditing_Frequency, 
p_Website, 
p_Company_Email_ID,
p_Company_ContactNumber1,
p_Company_ContactNumber2,
p_Industry_Type_ID 
);
select last_insert_id();
else 
update tbl_company_details set
Company_Details_ID=p_Company_Details_ID,
Org_Hier_ID=p_Org_Hier_ID,
Formal_Name=p_Formal_Name,
Calender_StartDate= p_Calender_StartDate, 
Calender_EndDate=p_Calender_EndDate,
Auditing_Frequency= p_Auditing_Frequency,
Website= p_Website,
Company_Email_ID= p_Company_Email_ID,
Company_ContactNumber1=p_Company_ContactNumber1,
Company_ContactNumber2=p_Company_ContactNumber2,
Industry_Type_ID = p_Industry_Type_ID 

where Company_Details_ID=p_Company_Details_ID;
select row_count();
end if;
end$$

DELIMITER ;





USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getParticularGroupCompaniesList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getParticularGroupCompaniesList`(p_Org_Hier_ID int)
begin  

select Company_Name, Org_Hier_ID,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 and
Org_Hier_ID = p_Org_Hier_ID;


end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getGroupCompanyListDropDown`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListDropDown`()
begin  

select Company_Name, Org_Hier_ID,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 and Is_Active = 1 ;


end$$

DELIMITER ;


-- procedure sp_getSpecificBranchList
-- -----------------------------------------------------

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getSpecificBranchList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificBranchList`(p_Parent_Company_ID int)
begin 
if(p_Parent_Company_ID=0)
then
select Company_Name, Org_Hier_ID,Type,Is_Active,logo from tbl_org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0;
else
 
select Company_Name, Org_Hier_ID,Type,Is_Active ,logo from tbl_org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0
 and Parent_Company_ID= p_Parent_Company_ID ;
 end if;
end$$

DELIMITER ;




USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getSpecificVendorList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificVendorList`(p_Parent_Company_ID int)
begin 
if(p_Parent_Company_ID=0)
then
select Company_Name, Org_Hier_ID,Type,Is_Active,logo, Is_Vendor from tbl_org_hier where level=3 and Is_Vendor=1 and Is_Delete = 0;
else
 
select Company_Name, Org_Hier_ID,Type,Is_Active,logo, Is_Vendor from tbl_org_hier where level=3 and Is_Vendor=1  and Is_Delete = 0
 and Parent_Company_ID= p_Parent_Company_ID ;
 end if;
end$$

DELIMITER ;



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getVendorsForBranch`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getVendorsForBranch`(p_Branch_ID int)
begin 
if(p_Branch_ID=0)
then
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,tbl_vendor_branch_mapping.Is_Active ,
Company_Name,Type,logo 
 from tbl_vendor_branch_mapping
inner join tbl_org_hier on tbl_org_hier.Org_Hier_ID = tbl_vendor_branch_mapping.Vendor_ID;
else
select Vendor_Branch_ID,
Branch_ID,
Vendor_ID, Start_Date,End_Date,tbl_vendor_branch_mapping.Is_Active ,
Company_Name,Type,logo 
 from tbl_vendor_branch_mapping
inner join tbl_org_hier on tbl_org_hier.Org_Hier_ID = tbl_vendor_branch_mapping.Vendor_ID
where Branch_ID= p_Branch_ID  and tbl_vendor_branch_mapping.Is_Active = 1;
 end if;
end$$

DELIMITER ;




USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getComplianceList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getComplianceList`(p_Country_ID int, p_Industry_Type_ID int)
begin 
if(p_Country_ID=0 && p_Industry_Type_ID=0)
then
select * from tbl_Compliance_Type ;
else
select Compliance_Type_ID,
Compliance_Type_Name,
Industry_Type_ID,Country_ID,Audit_Frequency,End_Date, Start_Date 

from tbl_Compliance_Type
 

where Country_ID= p_Country_ID and Industry_Type_ID=p_Industry_Type_ID ;
 end if;
end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_insertupdateComplianceTypeMapping`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceTypeMapping`(
p_Flag char(1),
p_compliance_type_map_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Type_ID int
)
begin
if(p_Flag = 'I') then
insert into tbl_Compliance_Type_mapping
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
update tbl_Compliance_Type_mapping set

Org_Hier_ID=p_Org_Hier_ID,
Compliance_Type_ID=p_Compliance_Type_ID
where compliance_type_map_ID=p_compliance_type_map_ID;
end if;
end$$

DELIMITER ;




USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getAssignedCompliances`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getAssignedCompliances`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select * from tbl_compliance_type_mapping;
else
select Org_Hier_ID, compliance_type_map_ID,tbl_compliance_type_mapping.Compliance_Type_ID,
Compliance_Type_Name
from tbl_compliance_type_mapping
inner join tbl_compliance_type on tbl_compliance_type.Compliance_Type_ID = tbl_compliance_type_mapping.Compliance_Type_ID

where Org_Hier_ID= p_Org_Hier_ID ;
 end if;
end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getDefaultIndustryType`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getDefaultIndustryType`(p_Org_Hier_ID int)
begin 
if(p_Org_Hier_ID = 0)
then
select Org_Hier_ID,tbl_company_details.Industry_Type_ID, Industry_Name
from tbl_company_details

inner join tbl_industry_type_master on tbl_industry_type_master.Industry_Type_ID = tbl_company_details.Industry_Type_ID;
else
select Org_Hier_ID,tbl_company_details.Industry_Type_ID, Industry_Name
from tbl_company_details
inner join tbl_industry_type_master on tbl_industry_type_master.Industry_Type_ID = tbl_company_details.Industry_Type_ID

where Org_Hier_ID= p_Org_Hier_ID ;
 end if;
end$$

DELIMITER ;


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
Company_Name,Type,logo 
 from tbl_vendor_branch_mapping
inner join tbl_org_hier on tbl_org_hier.Org_Hier_ID = tbl_vendor_branch_mapping.Branch_ID
where Vendor_ID= p_Vendor_ID ;
 end if;
end$$

DELIMITER ;



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getVendorJoin`;

DELIMITER $$
USE `auditmoduledb`$$
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
from tbl_org_hier;
else 
select tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_COde, 
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Type, 
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor,
tbl_company_details.Company_Details_ID,
tbl_company_details.Org_Hier_ID, 
Formal_Name, 
Calender_StartDate,
Calender_EndDate,
Auditing_Frequency,
Website,
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
Industry_Type_ID

from tbl_org_hier 
inner join  tbl_company_Details  on tbl_company_details.Org_Hier_ID = tbl_org_hier.Org_Hier_ID

where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getOrganizationHier`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getOrganizationHier`(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from tbl_org_hier;
else 

select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from tbl_org_hier
where Org_Hier_ID = p_Org_Hier_ID;
End If;

end$$

DELIMITER ;



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getOrganizationHierJoin`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getOrganizationHierJoin`(
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
from tbl_org_hier;
else 
select tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_COde, 
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Industry_Type, 
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor,
tbl_company_details.Company_Details_ID,
tbl_company_details.Org_Hier_ID, 
Formal_Name, 
Industry_Type_ID,
Calender_StartDate,
Calender_EndDate,
Auditing_Frequency,
Website,
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
tbl_branch_location.Location_ID,
tbl_branch_location.Org_Hier_ID,
Location_Name,
Address,
Country_ID,
State_ID,
City_ID,
Postal_Code,
Branch_Coordinates1,
Branch_Coordinates2,
Branch_CoordinateURL
from tbl_org_hier 
inner join  tbl_company_Details  on tbl_company_details.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
inner join tbl_branch_location on tbl_branch_location.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;

USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getBranchJoin`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchJoin`(
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
from 
tbl_org_hier;
else 
select 
tbl_org_hier.Org_Hier_ID,
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
Is_Active,
Is_Delete,
Is_Vendor,
tbl_branch_location.Org_Hier_ID,
Location_ID,
Location_Name,
Address,
Country_ID,
State_ID,
City_ID,
Postal_Code,
Branch_Coordinates1,
Branch_Coordinates2,
Branch_CoordinateURL
from tbl_org_hier 
inner join tbl_branch_location on tbl_branch_location.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getCompaniesList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getCompaniesList`(p_Parent_Company_ID int)
begin  
select Company_Name, Org_Hier_ID,Type,Is_Active from tbl_org_hier where level= 2 and Parent_Company_ID=p_Parent_Company_ID;
end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getBranchList`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getBranchList`()
begin  
select Company_Name, Org_Hier_ID,Type,Is_Active from tbl_org_hier where level=3 and Is_Delete = 0;
end$$

DELIMITER ;




USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getOrganizationHierJoin`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getOrganizationHierJoin`(
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
from tbl_org_hier;
else 
select tbl_org_hier.Org_Hier_ID,
Company_Name, 
Company_COde, 
Parent_Company_ID,
Description,
level,
Is_Leaf, 
Type, 
Last_Updated_Date,
logo,
User_ID, 
Is_Active,
Is_Delete,
Is_Vendor,
tbl_company_details.Company_Details_ID,
tbl_company_details.Org_Hier_ID, 
Formal_Name, 
Calender_StartDate,
Calender_EndDate,
Auditing_Frequency,
Website,
Company_Email_ID,
Company_ContactNumber1,
Company_ContactNumber2,
tbl_branch_location.Location_ID,
tbl_branch_location.Org_Hier_ID,
Location_Name,
Address,
Country_ID,
State_ID,
City_ID,
Postal_Code,
Branch_Coordinates1,
Branch_Coordinates2,
Branch_CoordinateURL,
Industry_Type_ID
from tbl_org_hier 
inner join  tbl_company_Details  on tbl_company_details.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
inner join tbl_branch_location on tbl_branch_location.Org_Hier_ID = tbl_org_hier.Org_Hier_ID
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;
end$$

DELIMITER ;





USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getGroupCompanyListActiveDeactive`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getGroupCompanyListActiveDeactive`()
begin
select Company_Name, Org_Hier_ID,Type,Is_Active, logo from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0 ;
end$$

DELIMITER ;



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getSpecificBranchListDropDown`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificBranchListDropDown`(p_Parent_Company_ID int)
begin 
select Company_Name, Org_Hier_ID,Type,Is_Active ,logo from tbl_org_hier where level=3 and Is_Delete = 0 and Is_Vendor=0 and Is_Active=1
 and Parent_Company_ID= p_Parent_Company_ID ;
end$$

DELIMITER ;

 



USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_DeleteComplianceTypes`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteComplianceTypes`(p_Org_Hier_ID int)
begin
DELETE FROM `auditmoduledb`.`tbl_compliance_type_mapping`
WHERE Org_Hier_ID= p_Org_Hier_ID;
end$$

DELIMITER ;


USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_DeactivateVendorForBranch`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeactivateVendorForBranch`(
p_Branch_ID int,
p_Vendor_ID int

)
begin
update tbl_vendor_branch_mapping set 
Is_Active = 0,
End_Date= now()
 where Branch_ID = p_Branch_ID and Vendor_ID = p_Vendor_ID ;
end$$

DELIMITER ;




USE `auditmoduledb`;
DROP procedure IF EXISTS `auditmoduledb`.`sp_getSpecificVendorListDropDown`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getSpecificVendorListDropDown`(p_Parent_Company_ID int,p_Branch_ID int)
begin 
select * from tbl_org_hier where Parent_Company_ID=p_Parent_Company_ID and Is_Vendor=1 and level=3 and Is_Delete = 0 and Is_Active = 1 and Org_Hier_ID Not In
(select Vendor_ID from tbl_vendor_branch_mapping where Branch_ID=p_Branch_ID and tbl_vendor_branch_mapping.Is_Active = 1);
end$$

DELIMITER ;




