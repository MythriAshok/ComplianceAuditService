
use auditmoduledb;


insert into tbl_Country (Country_Code, Country_Name) values('101','India');

insert into tbl_Country (Country_Code, Country_Name) values('102','UK');

select * from tbl_country;




insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('201','Karnataka',(select Country_ID from tbl_Country where Country_Name = 'India'));

insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('202','Kerala',(select Country_ID from tbl_Country where Country_Name = 'India'));


insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('203','California',(select Country_ID from tbl_Country where Country_Name = 'UK'));


insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('204','Detroit',(select Country_ID from tbl_Country where Country_Name = 'UK'));

select * from tbl_state;


insert into tbl_city (City_Name, State_ID)  values 
('Hassan',(select State_ID from tbl_state where State_Name = 'Karnataka'));

insert into tbl_city (City_Name, State_ID)  values 
('Mysore',(select State_ID from tbl_state where State_Name = 'Karnataka'));

insert into tbl_city (City_Name, State_ID)  values 
('Tumakuru',(select State_ID from tbl_state where State_Name = 'Karnataka'));

insert into tbl_city (City_Name, State_ID)  values 
('Trivendrum',(select State_ID from tbl_state where State_Name = 'Kerala'));

insert into tbl_city (City_Name, State_ID)  values 
('Calicut',(select State_ID from tbl_state where State_Name = 'Kerala'));

insert into tbl_city (City_Name, State_ID)  values 
('Vegas',(select State_ID from tbl_state where State_Name = 'California'));

insert into tbl_city (City_Name, State_ID)  values 
('LosAngeles',(select State_ID from tbl_state where State_Name = 'California'));

insert into tbl_city (City_Name, State_ID)  values 
('Mascow',(select State_ID from tbl_state where State_Name = 'Detroit'));

select * from tbl_city;


insert into tbl_user ( User_Password,First_Name,Middle_Name,Last_Name,Email_ID,Contact_Number,Company_ID,Gender,Is_Active,Last_Login)
values('pass','ovi','hathwar','p','ovi','8971089120','1','Female',1,now());

select * from tbl_user;

insert into tbl_Branch_Location(Location_Name,Address,Country_ID,State_ID,City_ID,Postal_Code,
Branch_Coordinates1,Branch_Coordinates2,Branch_CoordinateURL) values ('IndraNagar','#196',1, 1, 2,572008,'22.36','22.236','abc.com');

select * from tbl_branch_location;

insert into tbl_org_hier (Company_Name, Company_Code, Parent_Company_ID, Description, level, Is_Leaf,Industry_Type,Last_Updated_Date,Location_ID,
User_ID,Is_Active,Is_Delete) values('Paajaka IT Service ',25, 0, 'Description', 1, 0, 'IT Service', now(),1, 1, 1, 0);

select * from tbl_org_hier;

insert into tbl_company_details(Org_Hier_ID,Formal_Name,Calender_StartDate,Calender_EndDate,Auditing_Frequency,Website,Company_Email_ID,
Company_ContactNumber1,Company_ContactNumber2) values(1, 'PaajakaConsulting IT Service', ('2018-05-23 00:00:00'), ('2018-06-23 00:00:00'),'Quarterly','abc.com', 'abcd.com',
'266092','8971089120');

select * from tbl_Company_Details;



