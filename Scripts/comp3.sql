INSERT INTO `compliancedb`.`tbl_privilege`
(`Privilege_Name`,
`Privilege_Type`,
`Is_Active`)
VALUES
("Create","",1);

INSERT INTO `compliancedb`.`tbl_privilege`
(`Privilege_Name`,
`Privilege_Type`,
`Is_Active`)
VALUES
("Update","",1);

INSERT INTO `compliancedb`.`tbl_privilege`
(`Privilege_Name`,
`Privilege_Type`,
`Is_Active`)
VALUES
("Read","",1);

INSERT INTO `compliancedb`.`tbl_privilege`
(`Privilege_Name`,
`Privilege_Type`,
`Is_Active`)
VALUES
("Delete","",1);



insert into tbl_Country (Country_Code, Country_Name) values('101','India');

insert into tbl_Country (Country_Code, Country_Name) values('102','UK');

insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('201','Karnataka',(select Country_ID from tbl_Country where Country_Name = 'India'));

insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('202','Kerala',(select Country_ID from tbl_Country where Country_Name = 'India'));


insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('203','California',(select Country_ID from tbl_Country where Country_Name = 'UK'));


insert into tbl_state (State_Code, State_Name,Country_ID)  values 
('204','Detroit',(select Country_ID from tbl_Country where Country_Name = 'UK'));

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

INSERT INTO `tbl_role` VALUES 
(1,'Admin',1,1),
(2,'SME',1,1),
(3,'Auditor',1,1),
(4,'padmin',1,1);

INSERT INTO `tbl_role_priv_map` VALUES (1,1,1,1);
INSERT INTO `tbl_role_priv_map` VALUES (2,1,2,1);
INSERT INTO `tbl_role_priv_map` VALUES (3,1,3,1);





INSERT INTO `tbl_user` VALUES   
 (1,'pass','Admin',NULL,'Adani','admin@gmail.com','1234567890',0,'Male',1,'2018-07-17 15:33:10',NULL),
 (2,'mypass','SME',NULL,'Adani','sme@gmail.com','0987654321',2,'Female',1,'2018-07-17 15:34:36',NULL),
 (3,'passmy','Auditor',NULL,'Adani','auditor@gmail.com','9517538462',3,'Female',1,'2018-08-04 21:17:24','picture.jpg'),
  (4,'passmy','Cadmin',NULL,'Adani','cadmin@gmail.com','9517538462',3,'Female',1,'2018-08-04 21:17:24','picture.jpg');
  
  INSERT INTO `tbl_user_group` VALUES 
(1,'Admin','GroupAdmin with Role Group Admin',1,1),
(2,'SME','BranchAdmin with BranchAdmin Role',3,1),
(3,'Auditor','BranchAdmin with BranchAdmin Role',3,1),
(4,'Cadmin','BranchAdmin with BranchAdmin Role',4,1);

INSERT INTO `tbl_user_group_members` VALUES(4,4,4), (1,1,1),(2,2,2),(3,3,3);

INSERT INTO `tbl_menus` VALUES (1,0,'User Management','/Common/dashboard',1,'product_icon.png'),
(2,1,'Manage Role','/UserManagement/AddRoles',1,'ManageRoles.png'),
(3,1,'Manage User Group','/UserManagement/AddUserGroup',1,'manageGroup.png'),
(4,1,'Add User','/UserManagement/AddUser',1,'adduser.JPG'),
(5,1,'Manage User ','/UserManagement/ListofUsers',1,'manageUsers.png'),
(6,0,'Group Management','/Common/dashboard',1,'about_icon.png'),
(7,6,'Add Group Company','/ManageOrganization/AddGroupCompany',1,'manageGroupCompany.png'),
(8,6,'Manage Group Company','/ManageOrganization/GroupCompanyList',1,'manageGroupCompany.png'),
(9,0,'Company Management','/Common/dashboard',1,'about_icon.png'),
(10,9,'Add Company','/ManageOrganization/AddCompany',1,'manageCompany.png'),
(11,9,'Manage Company','/ManageOrganization/SelectGroupCompany',1,'manageCompany.png'),
(12,9,'Add Branch','/ManageOrganization/AddBranch',1,'manageBranch.png'),
(13,9,'Manage Branch','/ManageOrganization/SelectCompany',1,'manageBranch.png'),
(14,9,'Add Vendor','/ManageOrganization/AddVendor',1,'manageBranch.png'),
(15,9,'Manage Vendor','/ManageOrganization/SelectCompanyForVendor',1,'manageBranch.png'),
(16,0,'Acts and Rules','/Common/dashboard',1,'product_icon.png'),
(17,16,'Add Act','/ComplianceManagement/CreateActs',1,'manageActs.png'),
(18,16,'Manage Compliance','/ComplianceManagement/ListofCompliance',1,'manageRules.png'),
(19,16,'Assign Rules to Branch','/ComplianceManagement/SMEdashboard',1,'manageActs.png'),
(20,0,'Auditing','/Common/dashboard',1,'audit_tool.png'),
(21,20,'Auditing','/AuditManagement/addCompanyBranch',1,'Reports.png'),
(22,0,'Reports','',1,'content_icon.png'),
(23,9,'Assign Vendor','ManageOrganization/AssignVendorForBranch',1, 'Reports.png'),
(24,9,'Delete Vendor','ManageOrganization/DessignVendorForBranch',1, 'Reports.png');

INSERT INTO `tbl_usergroup_menu_map` VALUES 
(1,1,1),(2,1,2),(3,1,3),(4,1,4),(5,1,5),(6,1,6),(7,1,7),(8,1,8),
(9,1,9),(10,1,10),(11,1,11),(12,1,12),(13,1,13),(14,1,14),(15,1,15),(16,1,16),(17,1,17),(18,1,18),(19,1,19),
(20,1,20),(21,1,21),(22,1,22),(23,2,16),(24,2,17),(25,2,18),(26,2,19),
(27,3,20),(28,3,21),(29,1,23),(30,1,24);



INSERT INTO `compliancedb`.`tbl_industry_type_master` (`Industry_Type_ID`, `Industry_Name`) VALUES ('1', 'Manufacturing');
INSERT INTO `compliancedb`.`tbl_industry_type_master` (`Industry_Type_ID`, `Industry_Name`) VALUES ('2', 'IT');
INSERT INTO `compliancedb`.`tbl_industry_type_master` (`Industry_Type_ID`, `Industry_Name`) VALUES ('3', 'Textiles');


INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Labour Compliance', 1, 1, 12, ('2020-12-31'), ('2018-01-01'));
 
 INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Statutory Compliance', 1, 1, 2, ('2020-12-31'), ('2018-01-01'));
INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Financial Compliance', 1, 1, 4, ('2020-12-31'), ('2018-01-01'));
INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Labour Compliance', 2, 1, 1, ('2020-12-31'), ('2018-01-01'));
INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Statutory Compliance', 2, 1, 4, ('2020-12-31'), ('2018-01-01'));
INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Labour Compliance', 1, 2, 12, ('2020-12-31'), ('2018-01-01'));
INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Financial Complianve', 1, 2, 6, ('2020-12-31'), ('2018-01-01'));
INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Statutoty Compliance', 2, 2, 4, ('2020-12-31'), ('2018-01-01'));
INSERT INTO `compliancedb`.`tbl_compliance_type` (`Compliance_Type_Name`, `Industry_Type_ID`, `Country_ID`, `Audit_Frequency`, `End_Date`, `Start_Date`)
 VALUES ('Financial Compliance', 2, 2, 1, ('2020-12-31'), ('2018-01-01'));




INSERT INTO `tbl_compliance_xref` VALUES
(1,'Act',NULL,1,1,1,'High',NULL,NULL,NULL,NULL,NULL,1,'2000-01-01 15:40:57','9999-12-31 15:40:57',
1,0,0,'2018-07-18 16:09:30',1,1,'Employees Provident Fund And Miscellaneous Provisions Act, 1952',0,NULL,NULL),
(2,'Act',NULL,1,1,1,'Low','Low Risk',NULL,NULL,NULL,NULL,1,'2000-01-01 15:40:57','9999-12-31 15:40:57'
,1,0,0,'2018-07-18 16:12:52',1,1,'Employment Exchanges (Compulsory Notification Of Vacancies) Act, 
1959',0,NULL,NULL),
(4,'Act',NULL,1,1,1,'Medium','Medium Risk',NULL,NULL,NULL,'\0',1,'2000-01-01 00:00:00','9999-12-31 00:00:00'
,1,0,0,'2018-07-18 16:19:49',1,1,'The Equal Remuneration Act, 1976',0,NULL,NULL),
(5,'Section',NULL,1,2,2,'High','HighRisk',NULL,NULL,NULL,NULL,1,'2000-01-01 00:00:00','9999-12-31 00:00:00',
1,0,0,'2018-07-18 23:52:42',1,1,'Employees Provident Funds Scheme, 1952,
 Employees Deposit-Linked Insurance Scheme, 1976, Employees Pension Scheme , 1995',1,NULL,NULL),
(6,'Rule','As per EPS Section 6,6A 6C, EPFS Chapter 5 Para 29,30,36 (1) Chapter IX Para 76,Para 3 And 4',
'\0',3,3,'High','High Risk','15th of every month',NULL,'Remittance','\0',1,'2000-01-01 00:00:00',
'9999-12-31 00:00:00',1,0,0,'2018-07-18 23:55:54',1,1,'Remittance Of PF Contribution',
5,'Delay in payment of contribution will attract an interest of 12% as per section 7Q and damages upto 25',NULL),
(7,'Rule','EPFS Para 26 (2) And Para 36 (2) (a) requires that every employee other than the excluded employees 
shall be made member of the Employees Provident Fund Scheme on joining an establishment. The information
 needs to be updated on the EPFO website and a copy of the ECR Part B should be obtained online.','\0',
 3,0,'1','High Risk','16th of every month',NULL,'Return','\0',1,'2000-01-01 12:00:00','9999-12-31 12:00:00',
 1,0,0,'2018-07-19 15:11:53',1,1,'Inclusion Of New Joiners To The Provident Fund Scheme',
 5,'Employer shall be punishable with imprisonment for a term which may extend to one year or with 
 fine which may extend to four thousand rupees or with both as per Section 76 of EPFS
 (where an offence has been committed by a company every person, who at the time, the offence was committed 
 was in charge of and was responsible to the company for conduct of the business of the company as well as
 the company, shall be liable to be proceeded against and punished accordingly)',NULL),
 (8,'Act',NULL,1,1,1,'High','High Risk',NULL,NULL,NULL,'\0',1,'2000-01-01 17:02:00',
 '9999-12-31 17:23:00',1,0,0,'2018-07-19 17:25:22',1,1,'The Payment Of Bonus Act, 1965',0,NULL,NULL),
 (9,'Section',NULL,1,2,2,'High','High Risk',NULL,NULL,NULL,'\0',1,'2000-01-01 17:02:00',
 '9999-12-31 17:23:00',1,0,0,'2018-07-19 17:26:51',1,1,'RuleSection',8,NULL,NULL),
 (10,'Section',NULL,1,2,2,'Low',
 'Low Risk',NULL,NULL,NULL,'\0',1,'2000-01-01 17:43:00','9999-12-31 17:43:00',1,0,0,'2018-07-19 17:43:36',1,1,
 'Employment Exchanges (Compulsory Notification Of Vacancies) Rules, 1960',2,NULL,NULL),
 (11,'Section',NULL,1,2,2,'Medium','Medium Risk',NULL,NULL,NULL,'\0',1,'2000-01-01 00:00:00',
 '9999-12-31 00:00:00',1,0,0,'2018-07-19 20:08:33',1,1,'The Equal Remuneration Rules, 1976',4,NULL,NULL),
 (12,'Rule','Section 4 of the Act and Rule 6 requires that every employer shall submit quarterly returns 
 in Form ER-1 to the Local Employment Exchange for Quarter ending March, June, September and December
 Section 8 of the Act and Rule 6 requires that every employer shall maintain an up-to-date register in 
 relation to the workers employed in Form D. Section 4 and 5 of the Act also requires that the employer
 pay equal remuneration to men and women workers for the same work or work of a similar nature and not to 
 discriminate between men and women workers while recruiting','\0',3,0,'Medium','Medium Risk','Every Month',
 'Form D','Register','\0',1,'2000-01-01 00:00:00','9999-12-31 00:00:00',1,0,0,'2018-07-19 20:33:01',1,1,
 'Equal Remuneration And Register To Be Maintained By The Employer',11,'Failure to comply with the provisions,
 may attract imprisonment from one month to three months or fine from of ten thousand rupees to twenty thousand 
 rupees. (where an offence has been omitted by a company every person, who at the time, the offence was committed
 was in charge of and was responsible to the company for conduct of the business of the company as well as the
 company, shall be liable to be proceeded against and punished accordingly)',NULL),
 (13,'Rule','des','\0',3,0,'High',NULL,'every month',NULL,NULL,'\0',1,'2018-07-13 00:00:00',
 '2018-07-12 00:00:00',1,0,0,'2018-07-22 17:28:27',1,1,'rule3',5,'con',NULL),
 (14,'Section','des',1,2,2,'High','sec',NULL,NULL,NULL,'\0',1,
 '2018-07-06 00:00:00','2018-07-27 00:00:00',1,0,0,'2018-07-22 17:29:38',1,1,
 'se1',2,NULL,NULL),(15,'Section',NULL,1,2,2,NULL,NULL,NULL,NULL,NULL,'\0',1,
 '0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,0,'2018-07-27 15:21:28',1,1,'se2',1,NULL,NULL),
 (16,'Act',NULL,1,1,1,'High','High',NULL,NULL,NULL,'\0',1,'9999-12-31 00:00:00',
 '2000-01-01 00:00:00',1,0,0,'2018-07-28 15:31:02',1,1,'The Sales Promotion Employees
 (Conditions Of Service) Act, 1976',0,NULL,NULL),
 (17,'Section',NULL,1,2,2,'High','High',NULL,NULL,NULL,'\0',1,
 '9999-12-31 00:00:00','2000-01-01 00:00:00',1,0,0,'2018-07-28 15:31:02',1,
 1,'RuleSection',16,NULL,NULL),
 (18,'Section',NULL,1,2,2,'High','High risk',NULL,NULL,NULL,'\0',1,'2000-01-01 00:00:00',
 '9999-12-31 00:00:00',1,0,0,'2018-07-28 15:35:26',1,1,'The Sales Promotion Employees
 (Conditions Of Service) Rules,1976',16,NULL,NULL),
 (19,'Rule','des','\0',3,0,'High','high disk','every month',NULL,'return','\0',1,
 '0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,0,'2018-07-29 14:04:00',1,1,'rule1',18,'cons',NULL);
 
 
 





