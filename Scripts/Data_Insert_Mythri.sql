
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
('Trivendrum',(select State_ID from tbl_state where State_Name = 'Kerala'));

insert into tbl_city (City_Name, State_ID)  values 
('Calicut',(select State_ID from tbl_state where State_Name = 'Kerala'));

insert into tbl_city (City_Name, State_ID)  values 
('Vegas',(select State_ID from tbl_state where State_Name = 'California'));

insert into tbl_city (City_Name, State_ID)  values 
('LosAngeles',(select State_ID from tbl_state where State_Name = 'California'));

insert into tbl_city (City_Name, State_ID)  values 
('Mascow',(select State_ID from tbl_state where State_Name = 'Detroit'));