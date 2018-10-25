use compliancedb;

create table AuditCalender
(
Audit_Calender_ID int auto_increment primary key,
Org_Hier_ID int ,
foreign key (Org_Hier_ID) references org_hier(Org_Hier_ID),
Compliance_Type_ID int,
foreign key (Compliance_Type_ID) references compliance_type(Compliance_Type_ID)on delete cascade on update cascade,
Start_Date datetime,
End_Date datetime,
Audit_Year year);



USE `compliancedb`;
DROP procedure IF EXISTS `compliancedb`.`sp_insertUpdateAuditCalender`

DELIMITER $$
USE `compliancedb`$$
CREATE  PROCEDURE `sp_insertUpdateAuditCalender`(
p_Flag char(1),
p_Audit_Calender_ID int ,
p_Org_Hier_ID int ,
p_Compliance_Type_ID int ,
p_Start_Date datetime,
p_End_Date datetime,
p_Audit_Year year
)
begin
if(p_Flag = 'I') then
insert into AuditCalender
(
Audit_Calender_ID,
Org_Hier_ID,
Compliance_Type_ID,
Start_Date,
End_Date,
Audit_Year
)
values
(
p_Audit_Calender_ID  ,
p_Org_Hier_ID  ,
p_Compliance_Type_ID ,
p_Start_Date,
p_End_Date ,
p_Audit_Year
);
select last_insert_id();
else
update AuditCalender set
Audit_Calender_ID=p_Audit_Calender_ID,
Org_Hier_ID=p_Org_Hier_ID,
Compliance_Type_ID=p_Compliance_Type_ID,
Start_Date=p_Start_Date,
End_Date=p_End_Date,
Audit_Year=p_Audit_Year

where Org_Hier_ID=p_Org_Hier_ID;
end if;
end$$

DELIMITER ;





