use auditmoduledb;

Drop Procedure if exists `sp_getCountry`;
Delimiter /
create procedure sp_getCountry()
begin
select *  from tbl_Country;
end/
Delimiter ;



Drop Procedure if exists `sp_getState`;
Delimiter /
create procedure sp_getState
(
p_Country_ID int
)
begin
select *  from tbl_state where Country_ID= p_Country_ID;
end/
Delimiter ;



Drop Procedure if exists `sp_getCity`;
Delimiter /
create procedure sp_getCity
(
p_State_ID int
)
begin
select *  from tbl_city where State_ID = p_State_ID;
end/
Delimiter ;



Drop Procedure if exists `sp_insertupdateBranchLocation`;
Delimiter /
create procedure sp_insertupdateBranchLocation
(
p_Flag char(1),
p_Location_ID int,
p_Location_Name varchar(75),
p_Address varchar(450), 
p_Country_ID int, 
p_State_ID int, 
p_City_ID int, 
p_Postal_Code int,
p_Branch_Coordinates1 varchar (100),
p_Branch_Coordinates2 varchar (100),
p_Branch_CoordinateURL varchar (100)
)

begin
if(p_Flag = 'I')then

insert into tbl_branch_location(Location_ID,Location_Name,Address,Country_ID,State_ID,City_ID,
Postal_Code,Branch_Coordinates1,Branch_Coordinates2,Branch_CoordinateURL)

values(p_Location_ID,p_Location_Name,p_Address,p_Country_ID,p_State_ID,p_City_ID,
p_Postal_Code,p_Branch_Coordinates1,p_Branch_Coordinates2,p_Branch_CoordinateURL);
select last_insert_id();

else
update tbl_branch_location set

Location_Name=p_Location_Name,Address=p_Address,Country_ID=p_Country_ID,State_ID=p_State_ID,City_ID=p_City_ID,
Postal_Code=p_Postal_Code,
Branch_Coordinates1= p_Branch_Coordinates1,Branch_Coordinates2=p_Branch_Coordinates2,
Branch_CoordinateURL= p_Branch_CoordinateURL
where Location_ID=p_Location_ID;
select last_insert_id();

end if;

end/
Delimiter ;


Drop Procedure if exists `sp_getBranchLocation`;
Delimiter /
create procedure sp_getBranchLocation
(
p_Location_ID int
)
begin
if(p_Location_ID=0)
then
select
LocationID, 
Location_Name, 
Address,Country_ID, 
State_ID,Postal_Code, 
Branch_Coordinates1,  
Branch_Coordinates2,  
Branch_CoordinatesURL 
from tbl_branch_location;
else
select
LocationID,
Location_Name, 
Address,
Country_ID,
State_ID,
Postal_Code, 
Branch_Coordinates1,  
Branch_Coordinates2,  
Branch_CoordinatesURL 
from tbl_branch_location
where City_ID = p_City_ID and Country_ID = p_Country_ID and State_ID = p_State_ID;
end if;
end/
Delimiter ;


Drop Procedure if exists `sp_deleteBranchLocation`;
delimiter /
create procedure sp_deleteBranchLocation
(
p_Location_ID int
)
delete from tbl_branch_location where Location_ID=p_Location_ID ;

 Delimiter ;


Drop Procedure if exists  `sp_insertupdateOrganizationHier`;
Delimiter /
create procedure sp_insertupdateOrganizationHier
(
 p_Flag char(1),
 p_Org_Hier_ID int,
 p_Company_Name varchar(45),
 p_Company_ID int,
 p_Parent_Company_ID int,
 p_Description varchar(45),
 p_level int,
 p_Is_Leaf tinyint,
 p_Industry_Type varchar(45),
 p_Last_Updated_Date datetime,
 p_Location_ID int,
 p_User_ID int,
 p_Is_Active bit,
 p_Is_Delete bit
)

begin
if(p_Flag = 'I')then

insert into tbl_org_hier(Company_Name, Company_ID, Parent_Company_ID, Description, level,
 Is_Leaf, Industry_Type, Last_Updated_Date, Location_ID, User_ID, Is_Active, Is_Delete)
 values( p_Company_Name, p_Company_ID, p_Parent_Company_ID, p_Description, p_level,
p_Is_Leaf, p_Industry_Type, now(), p_Location_ID, p_User_ID,p_Is_Active,p_Is_Delete);
select last_insert_id();
else

update tbl_org_hier
inner join tbl_Branch_Location on tbl_Branch_Location.Location_ID = tbl_Org_Hier.Location_ID
inner join tbl_Company_Details on tbl_Company_Details.Org_Hier_ID = tbl_Org_Hier.Org_Hier_ID
set
tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID,
Company_Name=p_Company_Name, Company_ID=p_Company_ID, Parent_Company_ID=p_Parent_Company_ID, Description=p_Description, level=p_level,
Is_Leaf=p_Is_Leaf, Industry_Type=p_Industry_Type, Last_Updated_Date=now(),tbl_org_hier.Location_ID=p_Location_ID, User_ID=p_User_ID,
Is_Active=p_Is_Active



where tbl_org_hier.Org_Hier_ID=p_Org_Hier_ID ;
select last_insert_id();
end if;
end/
Delimiter ;


Drop Procedure if exists `sp_getOrganizationHier`;
Delimiter /
create procedure sp_getOrganizationHier
(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Industry_Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from tbl_org_hier;
else 

select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Industry_Type, Last_Updated_Date, Location_ID, User_ID, Is_Active from tbl_org_hier
where Org_Hier_ID = p_Org_Hier_ID;
End If;

end/
Delimiter ;


Drop Procedure if exists `sp_getOrganizationHierJoin`;
Delimiter /
create procedure sp_getOrganizationHierJoin
(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Industry_Type, Last_Updated_Date,tbl_org_hier.Location_ID, User_ID, Is_Active from tbl_org_hier;
else 

select tbl_org_hier.Org_Hier_ID,Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Industry_Type, Last_Updated_Date,tbl_org_hier.Location_ID, User_ID, Is_Active,Is_Delete,tbl_company_details.Company_Details_ID,
tbl_company_details.Org_Hier_ID, Formal_Name, Calender_StartDate, Calender_EndDate, Auditing_Frequency,
 Website, Company_Email_ID,Company_ContactNumber1,Company_ContactNumber2,
tbl_branch_location.Location_ID,Location_Name,Address,Country_ID,State_ID,City_ID,Postal_Code,Branch_Coordinates1,Branch_Coordinates2,
Branch_CoordinateURL
 from tbl_org_hier 

inner join  tbl_company_Details  on tbl_company_details.Org_Hier_ID = tbl_org_hier.Org_Hier_ID

inner join tbl_branch_location on tbl_branch_location.Location_ID = tbl_org_hier.Location_ID

where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;

end/
Delimiter ;


Drop Procedure if exists `sp_getBranchJoin`;
Delimiter /
create procedure sp_getBranchJoin
(
p_Org_Hier_ID int 
)
begin  
if(p_Org_Hier_ID = 0) then
select Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Industry_Type, Last_Updated_Date,tbl_org_hier.Location_ID, User_ID, Is_Active from tbl_org_hier;
else 

select tbl_org_hier.Org_Hier_ID,Company_Name, Company_ID, Parent_Company_ID, Description, level,
Is_Leaf, Industry_Type, Last_Updated_Date,tbl_org_hier.Location_ID, User_ID, Is_Active,Is_Delete,

tbl_branch_location.Location_ID,Location_Name,Address,Country_ID,State_ID,City_ID,Postal_Code,Branch_Coordinates1,Branch_Coordinates2,
Branch_CoordinateURL
 from tbl_org_hier 



inner join tbl_branch_location on tbl_branch_location.Location_ID = tbl_org_hier.Location_ID

where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
End If;

end/
Delimiter ;







Drop Procedure if exists `sp_getGroupCompaniesList`;
Delimiter /
create procedure sp_getGroupCompaniesList()

begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active from tbl_org_hier where Parent_Company_ID=0 and Is_Delete = 0;
end/
Delimiter ;


Drop Procedure if exists `sp_getCompaniesList`;
Delimiter /
create procedure sp_getCompaniesList()

begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active from tbl_org_hier where level= 2 and Is_Delete = 0;
end/
Delimiter ;


Drop Procedure if exists `sp_getBranchList`;
Delimiter /
create procedure sp_getBranchList()

begin  

select Company_Name, Org_Hier_ID,Industry_Type,Is_Active from tbl_org_hier where level=3 and Is_Delete = 0;
end/
Delimiter ;




Drop Procedure if exists `sp_getGroupCompaniesListDropDown`;
Delimiter /
create procedure sp_getGroupCompaniesListDropDown()
begin
select Org_Hier_ID, Company_Name  from tbl_org_hier where Parent_Company_ID=0;
end/
Delimiter ;






Drop Procedure if exists `sp_getCompaniesListDropDown`;
Delimiter /
create procedure sp_getCompaniesListDropDown
(
p_Parent_Company_ID int
)
begin
select Org_Hier_ID,Company_Name  from tbl_org_hier where Parent_Company_ID= p_Parent_Company_ID;
end/
Delimiter ;



Drop Procedure if exists  sp_deleteOrganizationHier;
delimiter /
create procedure sp_deleteOrganizationHier
(
p_Org_Hier_ID int
)
begin
update tbl_org_hier set
Is_Delete=1
where tbl_org_hier.Org_Hier_ID= p_Org_Hier_ID;
end/
Delimiter ;



Drop Procedure if exists sp_insertupdateCompanyDetails;
Delimiter /
create procedure sp_insertupdateCompanyDetails
(
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
p_Is_Active bit
)

begin
if(p_Flag ='I') then

insert into tbl_company_details(Org_Hier_ID,Formal_Name, Calender_StartDate, 
Calender_EndDate, Auditing_Frequency, Website, Company_Email_ID,
Company_ContactNumber1,Company_ContactNumber2)

values(p_Org_Hier_ID,p_Formal_Name, p_Calender_StartDate, 
p_Calender_EndDate, p_Auditing_Frequency, p_Website, p_Company_Email_ID,
p_Company_ContactNumber1,p_Company_ContactNumber2);
select last_insert_id();

else 
update tbl_company_details set

Company_Details_ID=p_Company_Details_ID,Org_Hier_ID=p_Org_Hier_ID,Formal_Name=p_Formal_Name,Calender_StartDate= p_Calender_StartDate, 
Calender_EndDate=p_Calender_EndDate,Auditing_Frequency= p_Auditing_Frequency,Website= p_Website,Company_Email_ID= p_Company_Email_ID,
Company_ContactNumber1=p_Company_ContactNumber1,Company_ContactNumber2=p_Company_ContactNumber2
where Company_Details_ID=p_Company_Details_ID;
select last_insert_id();
end if;

end/
delimiter ;


Drop Procedure if exists sp_getCompanyDetails;
Delimiter /
create procedure sp_getCompanyDetails
(
p_Company_Details_ID int
)
begin
if(p_Company_Details=0)
then
select Org_Hier_ID, Industry_Type,Formal_Name, Calender_StartDate, 
Calender_EndDate, Auditing_Frequency, Website, Company_EmailID,
Company_ContactNumber1,Company_ContactNumber2 from tbl_company_details;
else
select Org_Hier_ID, Industry_Type,Formal_Name, Calender_StartDate, 
Calender_EndDate, Auditing_Frequency, Website, Company_EmailID,
Company_ContactNumber1,Company_ContactNumber2 from tbl_company_details
where Company_Details_ID = p_Company_Details_ID;
end if;
end
delimiter ;






Drop Procedure if exists sp_insertupdateComplianceOptionsXref;
Delimiter /
create procedure sp_insertupdateComplianceOptionsXref
(
p_Flag char (1),
p_Compliance_Opt_Xerf_ID int  ,
p_Optiond_Text varchar(45),
p_Option_Order varchar(45),
p_Compliance_Xref_ID int
)

begin 
if(p_Flag ='I') then

insert into tbl_compliance_options_xref(Compliance_Opt_Xerf_ID, Optiond_Text, Option_Order, Compliance_Xref_ID)

values(p_Compliance_Opt_Xerf_ID, p_Optiond_Text, p_Option_Order, p_Compliance_Xref_ID);

else
update tbl_compliance_options_xref set

Compliance_Opt_Xerf_ID=p_Compliance_Opt_Xerf_ID, Optiond_Text=p_Optiond_Text, Option_Order=p_Option_Order, Compliance_Xref_ID=p_Compliance_Xref_ID
where Compliance_Opt_Xerf_ID=p_Compliance_Opt_Xerf_ID;

end if;

end/

delimiter ;

Drop Procedure if exists sp_getComplianceOptionsXref;
Delimiter /
create procedure sp_getComplianceOptionsXref
(
p_Compliance_Opt_Xerf_ID int  
)
begin
if(p_Compliance_Opt_Xerf_ID = 0)
then
select  Optiond_Text, Option_Order, Compliance_Xref_ID from compliance_options_xref;
else
select  Optiond_Text, Option_Order, Compliance_Xref_ID from compliance_options_xref
where Compliance_Opt_Xerf_ID=p_Compliance_Opt_Xerf_ID;
end if;
end/
delimiter ;

Drop Procedure if exists sp_deleteComplianceOptionsXref;
delimiter /
create procedure sp_deleteComplianceOptionsXref
(
p_Compliance_Opt_Xerf_ID int
)
begin
delete from tbl_compliance_xref where Compliance_Opt_Xerf_ID=p_Compliance_Opt_Xerf_ID;
end/
delimiter ;


Drop Procedure if exists sp_insertupdateComplianceAudit;
Delimiter /
create procedure sp_insertupdateComplianceAudit
(
p_flag char(1),
p_Compliance_Audit_ID int ,
p_Comp_Schedule_Instance int,
p_Penalty_nc varchar(150),
p_Audit_Remarks varchar(150),
p_Audit_artefacts varchar(150),
p_Audit_Date datetime,
p_Version int,
p_Reviewer_ID int,
p_Review_Comments varchar(500),
p_Audit_Status varchar(10),
p_Compliance_Xref_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Opt_Xref_ID int ,
p_Auditor_ID int,
p_User_ID int ,
Is_Active bit
)

begin
if(p_flag = 'I') then

insert into tbl_compliance_audit(Comp_Schedule_Instance,Penalty_nc,Audit_Remarks ,Audit_artefacts,
Audit_Date,Version,Reviewer_ID ,Review_Comments,Last_Updated_Date ,
Audit_Status,Compliance_Xref_ID,Org_Hier_ID ,Compliance_Opt_Xref_ID,Auditor_ID,User_ID,Is_Active )

values(p_Comp_Schedule_Instance,p_Penalty_nc,p_Audit_Remarks ,p_Audit_artefacts,
p_Audit_Date,p_Version,p_Reviewer_ID ,p_Review_Comments,NOW() ,
p_Audit_Status,p_Compliance_Xref_ID,p_Org_Hier_ID ,p_Compliance_Opt_Xref_ID,p_Auditor_ID,p_User_ID,p_Is_Active );

else

INSERT INTO tbl_Compliance_Audit_AuditTrail(Select Comp_Schedule_Instance,Penalty_nc,Audit_Remarks ,Audit_artefacts,
Audit_Date,Version,Reviewer_ID ,Review_Comments,Last_Updated_Date ,
Audit_Status,Compliance_Xref_ID,Org_Hier_ID ,Compliance_Opt_Xref_ID,Auditor_ID,User_ID,Is_Active ,"update" As 'Action_Type'
from tbl_compliance_audit Where Compliance_Audit_ID = p_Compliance_Audit_ID);

update tbl_compliance_audit set Comp_Schedule_Instance= p_Comp_Schedule_Instance,Penalty_nc=p_Penalty_nc,Audit_Remarks=p_Audit_Remarks ,Audit_artefacts=p_Audit_artefacts,
Audit_Date=p_Audit_Date,Version=p_Version,Reviewer_ID=p_Reviewer_ID ,Review_Comments=p_Review_Comments,Last_Updated_Date=NOW(),
Audit_Status=p_Audit_Status,Compliance_Xref_ID=p_Compliance_Xref_ID,Org_ID=p_Org_ID ,Compliance_Opt_Xref_ID=p_Compliance_Opt_Xref_ID,Auditor_ID=p_Auditor_ID,User_ID=p_User_ID, Is_Active =p_Is_Active  
where Compliance_Audit_ID= p_Compliance_Audit_ID;
end if;

end/
delimiter ;

Drop Procedure if exists sp_insertComplianceAuditTrail;
Delimiter /
create procedure sp_insertComplianceAuditTrail
(
p_Compliance_Audit_ID int ,
p_Comp_Schedule_Instance int,
p_Penalty_nc varchar(150),
p_Audit_Remarks varchar(150),
p_Audit_artefacts varchar(150),
p_Audit_Date datetime,
p_Version int,
p_Reviewer_ID int,
p_Review_Comments varchar(500),
p_Audit_Status varchar(10),
p_Compliance_Xref_ID int ,
p_Org_ID int ,
p_Compliance_Opt_Xref_ID int ,
p_Auditor_ID int,
p_User_ID int ,
p_Is_Active bit,
p_Action_Type varchar(10)
)
begin

insert into tbl_Compliance_Audit_AuditTrail(Comp_Schedule_Instance,Penalty_nc,Audit_Remarks ,Audit_artefacts,
Audit_Date,Version,Reviewer_ID ,Review_Comments,Last_Updated_Date ,
Audit_Status,Compliance_Xref_ID,Org_ID ,Compliance_Opt_Xref_ID,Auditor_ID,User_ID,Is_Active ,Action_Type)

values(p_Comp_Schedule_Instance,p_Penalty_nc,p_Audit_Remarks ,p_Audit_artefacts,
p_Audit_Date,p_Version,p_Reviewer_ID ,p_Review_Comments,NOW() ,
p_Audit_Status,p_Compliance_Xref_ID,p_Org_ID ,p_Compliance_Opt_Xref_ID,p_Auditor_ID,p_User_ID,p_Is_Active ,p_Action_Type);

end/
delimiter ;


Drop Procedure if exists sp_getComplianceAuditTrail;
delimiter /
create procedure sp_getComplianceAuditTrail
(
p_Compliance_Audit_ID int
)
begin
if(p_Compliance_Audit_ID =0)
then
select Comp_Schedule_Instance,Penalty_nc,Audit_Remarks ,Audit_artefacts,
Audit_Date,Version,Reviewer_ID ,Review_Comments,Last_Updated_Date ,
Audit_Status,Compliance_Xref_ID,Org_ID ,Compliance_Opt_Xref_ID,Auditor_ID,User_ID ,Is_Active ,Action_Type
from tbl_Compliance_Audit_AuditTrail;
else
select Comp_Schedule_Instance,Penalty_nc,Audit_Remarks ,Audit_artefacts,
Audit_Date,Version,Reviewer_ID ,Review_Comments,Last_Updated_Date ,
Audit_Status,Compliance_Xref_ID,Org_ID ,Compliance_Opt_Xref_ID,Auditor_ID,User_ID ,Is_Active ,Action_Type
from tbl_Compliance_Audit_AuditTrail
where Compliance_Audit_ID=p_Compliance_Audit_ID;
end if;
end/
delimiter ;

Drop Procedure if exists sp_deleteComplianceAuditTrail;
delimiter /
create procedure sp_deleteComplianceAuditTrail
(
p_Compliance_Audit_ID int
)
begin
update tbl_Compliance_Audit_AuditTrail set Is_Actice = 0 where Compliance_Audit_ID=p_Compliance_Audit_ID;
end/
delimiter ;



Drop Procedure if exists sp_getComplianceAudit;
Delimiter /
create procedure sp_getComplianceAudit
(
p_Compliance_Audit_ID int 
)

begin
if(p_Compliance_Audit_ID=0)
then
select Comp_Schedule_Instance,Penalty_nc,Audit_Remarks ,Audit_artefacts,
Audit_Date,Version,Reviewer_ID ,Review_Comments,Last_Updated_Date ,
Audit_Status,Compliance_Xref_ID,Org_ID ,Compliance_Opt_Xref_ID,Auditor_ID,User_ID ,Is_Active 
from tbl_compliance_audit ;
else
select Comp_Schedule_Instance,Penalty_nc,Audit_Remarks ,Audit_artefacts,
Audit_Date,Version,Reviewer_ID ,Review_Comments,Last_Updated_Date ,
Audit_Status,Compliance_Xref_ID,Org_ID ,Compliance_Opt_Xref_ID,Auditor_ID,User_ID ,Is_Active 
from tbl_compliance_audit 
where Compliance_Audit_ID=p_Compliance_Audit_ID;
end if;
end/

delimiter ;


Drop Procedure if exists sp_deleteComplianceAudit;
delimiter /
create procedure sp_deleteComplianceAudit
(
p_Compliance_Audit_ID int
)
begin
update tbl_compliance_audit set Is_Active  =0 where Compliance_Audit_ID=p_Compliance_Audit_ID;
end/
delimiter ;

Drop Procedure if exists sp_insertupdateComplianceXref;
delimiter /
create procedure sp_insertupdateComplianceXref
(
p_Flag char(1),
p_Compliance_Xref_ID int ,
p_Comp_Category varchar(45),
p_Comp_Description varchar(45),
p_Is_Header tinyint,
p_level varchar(5),
p_Comp_Order int(3),
p_Option_ID int,
p_Risk_Category varchar(45),
p_Risk_Description varchar(100),
p_Recurrence varchar(45),
p_Form varchar(45),
p_Type varchar(45),
p_Is_Best_Practice tinyint,
p_Version int(3),
p_Effective_Start_Date datetime,
p_Effective_End_Date datetime,
p_Country_ID int,
p_State_ID int,
p_City_ID int,
p_User_ID int ,
p_Is_Active bit
)

begin
if(p_Flag ='I') then

insert into tbl_compliance_xref(Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID, Is_Active )

values(p_Comp_Category, p_Comp_Description,p_Is_Header,p_level,p_Comp_Order,p_Option_ID,p_Risk_Category,
p_Risk_Description,p_Recurrence,p_Form,p_Type,p_Is_Best_Practice ,p_Version,p_Effective_Start_Date,p_Effective_End_Date,
p_Country_ID ,p_State_ID ,p_City_ID ,p_User_ID,Is_Active,Now() );

else

INSERT INTO tbl_compliance_xref_audittrail(Select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID,Is_Active,"update" As 'Action_Type'
from tbl_compliance_xref Where Compliance_Xref_ID = Compliance_Xref_ID);

update tbl_compliance_xref set

Comp_Category=p_Comp_Category, Comp_Description=p_Comp_Description,Is_Header=p_Is_Header,level=p_level,Comp_Order=p_Comp_Order,Option_ID=p_Option_ID,Risk_Category=p_Risk_Category,
Risk_Description=p_Risk_Description,Recurrence=p_Recurrence,Form=p_Form,Type=p_Type,Is_Best_Practice=p_Is_Best_Practice ,Version=p_Version,Effective_Start_Date=p_Effective_Start_Date,Effective_End_Date=p_Effective_End_Date,
Country_ID=p_Country_ID ,State_ID=p_State_ID ,City_ID=p_City_ID ,Last_Updated_Date=Now(),User_ID=p_User_ID, Is_Active=p_Is_Active
where Compliance_Xref_ID=p_Compliance_Xref_ID;
end if;

end/

delimiter ;


Drop Procedure if exists sp_insertComplianceXrefAuditTrail;
delimiter /
create procedure sp_insertComplianceXrefAuditTrail
(
p_Compliance_Xref_ID int ,
p_Comp_Category varchar(45),
p_Comp_Description varchar(45),
p_Is_Header tinyint,
p_level varchar(5),
p_Comp_Order int(3),
p_Option_ID int,
p_Risk_Category varchar(45),
p_Risk_Description varchar(100),
p_Recurrence varchar(45),
p_Form varchar(45),
p_Type varchar(45),
p_Is_Best_Practice tinyint,
p_Version int(3),
p_Effective_Start_Date datetime,
p_Effective_End_Date datetime,
p_Country_ID int,
p_State_ID int,
p_City_ID int,
p_User_ID int ,
p_Action_Type varchar(10),
p_Is_Active bit
)

begin
insert into tbl_compliance_xref_audittrail(Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,User_ID, Action_Type,Is_Active )

values(p_Comp_Category, p_Comp_Description,p_Is_Header,p_level,p_Comp_Order,p_Option_ID,p_Risk_Category,
p_Risk_Description,p_Recurrence,p_Form,p_Type,p_Is_Best_Practice ,p_Version,p_Effective_Start_Date,p_Effective_End_Date,
p_Country_ID ,p_State_ID ,p_City_ID ,p_User_ID,p_Action_Type,p_Is_Active,Now()  );

end/

delimiter ;


Drop Procedure if exists sp_getComplianceXrefAuditTrail;
delimiter /
create procedure sp_getComplianceXrefAuditTrail
(
p_Compliance_Xref_ID int
)
begin
if(p_Compliance_Xref_ID=0)
then
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID, Action_Type,Is_Active from tbl_compliance_xref_audittrail;
else
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID, Action_Type,Is_Active from tbl_compliance_xref_audittrail
where Compliance_Xref_ID = p_Compliance_Xref_ID;
end if;
end/ 

delimiter ;


Drop Procedure if exists sp_deleteComplianceXrefAuditTrail;
Delimiter /
create procedure sp_deleteComplianceXrefAuditTrail
(
p_Compliance_Xref_ID int
)

begin
update tbl_compliance_xref_audittrail set Is_Active = 0 where Compliance_Xref_ID=p_Compliance_Xref_ID;

end/
delimiter ;

Drop Procedure if exists sp_getComplianceXref;
delimiter /
create procedure sp_getComplianceXref
(
p_Compliance_Xref_ID int 
)
begin
if(p_Compliance_Xref_ID=0)
then
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID,Is_Active from tbl_compliance_xref;
else
select Comp_Category, Comp_Description,Is_Header,level,Comp_Order,Option_ID,Risk_Category,
Risk_Description,Recurrence,Form,Type,Is_Best_Practice ,Version,Effective_Start_Date,Effective_End_Date,
Country_ID ,State_ID ,City_ID ,Last_Updated_Date,User_ID,Is_Active from tbl_compliance_xref
where Compliance_Xref_ID= p_Compliance_Xref_ID;
end if;
end/


delimiter ;


Drop Procedure if exists sp_insertupdateBranchAuditorMapping;
delimiter /
create procedure sp_insertupdateBranchAuditorMapping
(
p_Flag char(1),
p_Branch_Allocation_ID int ,
p_Org_Hier_ID int ,
p_Auditor_ID int ,
p_Financial_Year datetime,
p_Is_Active bit,
p_Login_ID int,
p_Allocation_Date datetime
)

begin
if(p_Flag = 'I') then

insert into tbl_branch_auditor_mapping(Branch_Allocation_ID,Org_Hier_ID,Auditor_ID,Financial_Year,Is_Active,
UpdatedByLogin_ID,Allocation_Date)

values(p_Branch_Allocation_ID,p_Org_Hier_ID,p_Auditor_ID,p_Financial_Year,p_Is_Active,
p_Login_ID,p_Allocation_Date);

else
update tbl_branch_auditor_mapping set

Branch_Allocation_ID=p_Branch_Allocation_ID,Org_Hier_ID=p_Org_Hier_ID,Auditor_ID=p_Auditor_ID,Financial_Year=p_Financial_Year,Is_Active=p_Is_Active,
UpdatedByLogin_ID=p_Login_ID,Allocation_Date=p_Allocation_Date
where Branch_Allocation_ID=p_Branch_Allocation_ID;

end if;

end/


delimiter ;

Drop Procedure if exists sp_getBranchAuditorMapping;
Delimiter /
create procedure sp_getBranchAuditorMapping
(
p_Branch_Allocation_ID int 
)
begin
if(p_Branch_Allocation_ID=0)
then
select Org_Hier_ID,Auditor_ID,Financial_Year,Is_Active,
UpdatedByLogin_ID,Allocation_Date
from tbl_compliance_branch_mapping; 
else
select Org_Hier_ID,Auditor_ID,Financial_Year,Is_Active,
UpdatedByLogin_ID,Allocation_Date
from tbl_compliance_branch_mapping 
where Branch_Allocation_ID=p_Branch_Allocation_ID;
end if;
end/


delimiter ;

Drop Procedure if exists sp_deleteBranchAuditorMapping;
Delimiter /
create procedure sp_deleteBranchAuditorMapping
(
p_Branch_Allocation_ID int 
)
begin
update tbl_compliance_branch_mapping set Is_Active = 0 where Branch_Allocation_ID=p_Branch_Allocation_ID;
end/

delimiter ;

Drop Procedure if exists sp_insertupdateComplianceBranchMapping;
Delimiter /
create procedure sp_insertupdateComplianceBranchMapping
(
p_Flag char(1),
p_Branch_Mapping_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Xref_ID int ,
p_Financial_Year datetime,
p_Is_Active bit,
p_Login_ID int,
p_Allocation_Date datetime
)

begin
if(p_Flag = 'I') then

insert into tbl_branch_auditor_mapping(Branch_Allocation_ID,Org_Hier_ID,Compliance_Xref_ID,Financial_Year,Is_Active,
UpdatedByLogin_ID,Allocation_Date)

values(p_Branch_Allocation_ID,p_Org_Hier_ID,p_Compliance_Xref_ID,p_Financial_Year,p_Is_Active,
p_Login_ID,p_Allocation_Date);

else
update tbl_branch_auditor_mapping set

Branch_Allocation_ID=p_Branch_Allocation_ID,Org_Hier_ID=p_Org_Hier_ID,Compliance_Xref_ID=p_Compliance_Xref_ID,Financial_Year=p_Financial_Year,Is_Active=p_Is_Active,
UpdatedByLogin_ID=p_Login_ID,Allocation_Date=p_Allocation_Date
where p_Branch_Mapping_ID=p_Branch_Mapping_ID;

end if;

end/


delimiter ;

Drop Procedure if exists sp_getComplianceBranchMapping;
Delimiter /
create procedure sp_getComplianceBranchMapping
(
p_Branch_Mapping_ID int 
)
begin
if(p_Branch_Mapping_ID=0)
then
select Org_Hier_ID,Compliance_Xref_ID,Financial_Year,Is_Active,
UpdatedByLogin_ID,Allocation_Date
from tbl_compliance_branch_mapping ;
else
select Org_Hier_ID,Compliance_Xref_ID,Financial_Year,Is_Active,
UpdatedByLogin_ID,Allocation_Date
from tbl_compliance_branch_mapping 
where Branch_Mapping_ID=p_Branch_Mapping_ID;
end if;
end/

delimiter ;

Drop Procedure if exists sp_deleteComplianceBranchMapping;
delimiter /
create procedure sp_deleteComplianceBranchMapping
(
p_Branch_Mapping_ID int 
)
begin
update tbl_compliance_branch_mapping set Is_Active = 0 where Branch_Mapping_ID=p_Branch_Mapping_ID;
end/


delimiter ;

Drop Procedure if exists insertLoginData;
delimiter /
create procedure insertLoginData
(
p_User_ID int,
p_User_Password varchar(10),
p_First_Name varchar(45),
p_Middle_Name varchar(45),
p_Last_Name varchar(45),
p_Email_ID varchar(100),
p_Contact_Number varchar(50),
p_Company_ID int,
p_Gender varchar(45),
p_Is_Active bit,
p_Last_Login datetime
)
BEGIN
insert into tbl_user values(p_User_Password,p_First_Name, p_Middle_Name,p_Last_Name,
p_Email_ID,p_Contact_Number,p_Contact_ID, p_Gender,p_Is_Active,p_Last_Login);
Select @@IDENTITY;
END/

delimiter ;

Drop Procedure if exists sp_getLoginData;
delimiter /
create procedure sp_getLoginData
(
p_User_ID int ,
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
select * from tbl_user where Email_ID= p_Email_ID and User_Password = p_User_Password;
end/

delimiter ;

Drop Procedure if exists sp_fetchchangePassword;
delimiter /
create procedure sp_fetchchangePassword
(
p_User_ID int,
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
select User_ID from tbl_user where Email_ID= p_Email_ID and User_Password=p_User_Password;

end/



delimiter ;

Drop Procedure if exists sp_updatePassword;
delimiter /
create procedure sp_updatePassword
(
p_User_ID int,
p_Email_ID varchar(100),
p_User_Password varchar(10)
)
begin
update tbl_user set User_Password = p_User_Password where User_ID = p_User_ID;
end/
delimiter ; 


drop procedure if exists sp_DeactivateOrgHier;
delimiter /
create procedure sp_DeactivateOrgHier
(
p_Org_Hier_ID int
)
begin
update tbl_org_hier set Is_Active = 0 where Org_Hier_ID=p_Org_Hier_ID;
end/
delimiter ;



drop procedure if exists sp_ActivateOrgHier;
delimiter /
create procedure sp_ActivateOrgHier
(
p_Org_Hier_ID int
)
begin
update tbl_org_hier set Is_Active = 1 where Org_Hier_ID=p_Org_Hier_ID;
end/
delimiter ;



  
