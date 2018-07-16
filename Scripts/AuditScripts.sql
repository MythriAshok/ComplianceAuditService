create database auditmoduledb;


use auditmoduledb;



create table tbl_Country
(
Country_ID int Not Null AUTO_INCREMENT PRIMARY KEY,
Country_Code varchar(5) not null,
Country_Name varchar(70) not null
);



create table tbl_State
(State_ID int not null auto_increment primary key,
State_Code varchar(5),
State_Name varchar(70),
Country_ID int not null,
Foreign key FK_Country (Country_ID) references tbl_Country(Country_ID)
);












create table tbl_Branch_Location
(
Location_ID int not null auto_increment primary key,
Location_Name varchar(75),
Adress varchar(450),
Country_ID int not null,
Foreign key  (Country_ID) references tbl_Country(Country_ID),
State_ID int not null,
Foreign key (State_ID) references tbl_State(State_ID),
City_ID int not null,
Foreign key (City_ID) references tbl_City(City_ID)
on update cascade
on delete cascade,
Postal_Code varchar(10),
Branch_Coordinates1 varchar(100),
Branch_Coordinates2 varchar(100),
Branch_CoordinateURL varchar(100)
);
 


alter table tbl_Branch_Location change Adress Address varchar(450);

select * from tbl_branch_location;



create table tbl_User
(
User_ID int not null auto_increment primary key,
User_Password varchar(10),
First_Name varchar(45),
Middle_Name varchar(45),
Last_Name varchar(45),
Email_ID varchar(100),
Contact_Number varchar(50),
Company_ID int,
Gender varchar(45),
Is_Active bit,
Last_Login datetime
);

insert into tbl_user (User_ID, User_Password,First_Name,Middle_Name,Last_Name,Email_ID,Contact_Number,Company_ID,Gender,Is_Active,Last_Login)
values(1,'pass','ovi','hathwar','p','ovi','8971089120','1','Female',1,now());
select* from tbl_user;


create table tbl_Org_Hier
(
Org_Hier_ID int not null auto_increment primary key,
Company_Name varchar(45),
Company_ID int,
Parent_Company_ID int,
Description varchar(45),
level int(3),
Is_Leaf bit,
Industry_Type varchar(45), 
Last_Updated_Date datetime,
Location_ID int not null,
Foreign key (Location_ID) references tbl_Branch_Location(Location_ID),
User_ID int not null,
Foreign key (User_ID) references tbl_User(User_ID)
on update cascade
on delete cascade,
Is_Active bit
);



alter table tbl_org_hier add column Is_Delete bit ;
select * from tbl_org_hier;

create table tbl_Company_Details
(
Company_Details_ID int not null auto_increment primary key  ,
Org_Hier_ID int not null ,
Foreign key (Org_Hier_ID) references tbl_Org_Hier(Org_Hier_ID),

Formal_Name varchar(45),
Calender_StartDate datetime,
Calender_EndDate datetime,
Auditing_Frequency varchar(45),
Website varchar(45),
Company_Email_ID varchar(45),
Company_ContactNumber1 varchar(45),
Company_ContactNumber2 varchar(45)

);



select * from tbl_company_details;

create table tbl_Compliance_Xref
(
Compliance_Xref_ID int not null auto_increment primary key,
Comp_Category varchar(45),
Comp_Description varchar(45),
Is_Header bit,
level varchar(5),
Comp_Order int(3),
Option_ID int,
Risk_Category varchar(45),
Risk_Description varchar(100),
Recurrence varchar(45),
Form varchar(45),
Type varchar(45),
Is_Best_Practice bit,
Version int(3),
Effective_Start_Date datetime,
Effective_End_Date datetime,
Country_ID int,
State_ID int,
City_ID int,
Last_Updated_Date datetime,
User_ID int not null,
Foreign key (User_ID) references tbl_User(User_ID)
on update cascade
on delete cascade,
Is_Active bit
);


create table tbl_Compliance_Xref_AuditTrail
(
Compliance_Xref_ID int not null auto_increment primary key,
Comp_Category varchar(45),
Comp_Description varchar(45),
Is_Header bit,
level varchar(5),
Comp_Order int(3),
Option_ID int,
Risk_Category varchar(45),
Risk_Description varchar(100),
Recurrence varchar(45),
Form varchar(45),
Type varchar(45),
Is_Best_Practice bit,
Version int(3),
Effective_Start_Date datetime,
Effective_End_Date datetime,
Country_ID int,
State_ID int,
City_ID int,
Last_Updated_Date datetime,
User_ID int not null,
Foreign key (User_ID) references tbl_User(User_ID)
on update cascade
on delete cascade,
Is_Active bit,
Action_Type varchar(10)
);



create table tbl_Compliance_Options_Xref
(
Compliance_Opt_Xref_ID int not null auto_increment primary key,
Option_Text varchar(45),
Option_Order int(3),
Compliance_Xref_ID int not null,
Foreign key (Compliance_Xref_ID) references tbl_Compliance_Xref(Compliance_Xref_ID)
on update cascade
on delete cascade
);






create table tbl_Compliance_Audit
(
Compliance_Audit_ID int not null auto_increment primary key,
Comp_Schedule_Instance int,
Penalty_nc varchar(150),
Audit_Remarks varchar(150),
Audit_artefacts varchar(150),
Audit_Date datetime,
Version int,
Reviewer_ID int,
Review_Comments varchar(500),
Last_Updated_Date datetime,
Audit_Status varchar(10),
Compliance_Xref_ID int not null,
Foreign key (Compliance_Xref_ID) references tbl_Compliance_Xref(Compliance_Xref_ID),
Org_Hier_ID int not null,
Foreign key (Org_Hier_ID) references tbl_Org_Hier(Org_Hier_ID),
Compliance_Opt_Xref_ID int not null,
Foreign key (Compliance_Opt_Xref_ID) references tbl_Compliance_Options_Xref(Compliance_Opt_Xref_ID),
Auditor_ID int not null,
Foreign key (Auditor_ID) references tbl_User(User_ID),
User_ID int not null,
Foreign key (User_ID) references tbl_User(User_ID)
on update cascade
on delete cascade,
Is_Active bit
);


create table tbl_Compliance_Audit_AuditTrail
(
Compliance_Audit_ID int not null auto_increment primary key,
Comp_Schedule_Instance int,
Penalty_nc varchar(150),
Audit_Remarks varchar(150),
Audit_artefacts varchar(150),
Audit_Date datetime,
Version int,
Reviewer_ID int,
Review_Comments varchar(500),
Last_Updated_Date datetime,
Audit_Status varchar(10),
Compliance_Xref_ID int not null,
Foreign key (Compliance_Xref_ID) references tbl_Compliance_Xref(Compliance_Xref_ID),
Org_Hier_ID int not null,
Foreign key (Org_Hier_ID) references tbl_Org_Hier(Org_Hier_ID),
Compliance_Opt_Xref_ID int not null,
Foreign key (Compliance_Opt_Xref_ID) references tbl_Compliance_Options_Xref(Compliance_Opt_Xref_ID),
Auditor_ID int not null,
Foreign key (Auditor_ID) references tbl_User(User_ID),
User_ID int not null,
Foreign key (User_ID) references tbl_User(User_ID)
on update cascade
on delete cascade,
Is_Active bit,
Action_Type varchar (10)
);


create table tbl_Role
(
Role_ID int not null auto_increment primary key,
Role_Name varchar(45),
Is_Active bit,
Is_Group_Role bit
);




create table tbl_Privilege
(
Privilege_ID int not null auto_increment primary key,
Privilege_Name varchar(45),
Privilege_Type varchar(45),
Is_Active bit
);




create table tbl_Role_Priv_Map
(
Role_Priv_ID int not null auto_increment primary key,
Is_Active bit,
Role_ID int not null,
Foreign key (Role_ID) references tbl_Role(Role_ID),
Privilege_ID int not null,
Foreign key (Privilege_ID) references tbl_Privilege(Privilege_ID)
);



create table tbl_User_Role_Map
(
User_Role_ID int not null auto_increment primary key,
Role_ID int not null,
Foreign key (Role_ID) references tbl_Role(Role_ID),
User_ID int not null,
Foreign key (User_ID) references tbl_User(User_ID)
);




create table tbl_User_Group
(
User_Group_ID int not null auto_increment primary key,
User_Group_Name varchar(45),
User_Group_Description varchar(45),
Role_ID int not null,
Foreign key (Role_ID) references tbl_Role(Role_ID),
Is_Active bit
);




create table tbl_User_Group_Members
(
User_Group_Members_ID int not null auto_increment primary key,
User_ID int not null,
Foreign key (User_ID) references tbl_User(User_ID),
User_Group_ID int not null,
Foreign key (User_Group_ID) references tbl_User_Group(User_Group_ID)
);


create table tbl_Menus
(
Menu_ID int not null auto_increment primary key,
Parent_MenuID int,
Menu_Name varchar(45),
Page_URL varchar(45),
Is_Active bit,
User_Group_ID int,
Foreign key (User_Group_ID) references tbl_User_Group(User_Group_ID)
);

drop table tbl_Branch_Auditor_Mapping
create table tbl_Branch_Auditor_Mapping
(
Branch_Allocation_ID int not null auto_increment primary key,
Org_Hier_ID int not null,
Foreign key (Org_Hier_ID) references tbl_Org_Hier(Org_Hier_ID),
Auditor_ID int not null,
Foreign key (Auditor_ID ) references tbl_User(User_ID),
Financial_Year datetime,
Is_Active bit,
UpdatedByLogin_ID int,
Foreign key (Login_ID ) references tbl_User(User_ID),
Allocation_Date datetime
);


drop table tbl_Compliance_Branch_Mapping
create table tbl_Compliance_Branch_Mapping
(
Branch_Mapping_ID int not null auto_increment primary key,
Org_Hier_ID int not null,
Foreign key (Org_Hier_ID) references tbl_Org_Hier(Org_Hier_ID),
Compliance_Xref_ID int not null,
Foreign key (Compliance_Xref_ID) references tbl_Compliance_Xref(Compliance_Xref_ID),
Financial_Year datetime ,
Is_Active bit,
UpdatedByLogin_ID int,
Foreign key (Login_ID ) references tbl_User(User_ID),
Allocation_Date datetime
);


