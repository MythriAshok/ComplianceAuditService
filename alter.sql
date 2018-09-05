







alter table tbl_compliance_audit  add column  Part_Compliance_Percent decimal;



DROP procedure IF EXISTS `auditmoduledb`.`sp_insertupdateComplianceAudit`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insertupdateComplianceAudit`(
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
p_Audit_Status varchar(450),
p_Compliance_Xref_ID int ,
p_Org_Hier_ID int ,
p_Auditor_ID int,

p_Is_Active bit,
p_Part_Compliance_Percent decimal
)
begin
if(p_flag = 'I') then
insert into tbl_compliance_audit
(
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date ,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,

Is_Active,
Part_Compliance_Percent
)
values
(
p_Comp_Schedule_Instance,
p_Penalty_nc,
p_Audit_Remarks,
p_Audit_artefacts,
p_Audit_Date,
p_Version,
p_Reviewer_ID,
p_Review_Comments,
NOW(),
p_Audit_Status,
p_Compliance_Xref_ID,
p_Org_Hier_ID,
p_Auditor_ID,

p_Is_Active,
p_Part_Compliance_Percent
);
else
INSERT INTO tbl_Compliance_Audit_AuditTrail
(
Select
Comp_Schedule_Instance,
Penalty_nc,
Audit_Remarks,
Audit_artefacts,
Audit_Date,
Version,
Reviewer_ID,
Review_Comments,
Last_Updated_Date ,
Audit_Status,
Compliance_Xref_ID,
Org_Hier_ID,
Auditor_ID,

Is_Active,
Part_Compliance_Percent,
"update" As 'Action_Type'
from tbl_compliance_audit Where Compliance_Audit_ID = p_Compliance_Audit_ID);
update tbl_compliance_audit set 
Comp_Schedule_Instance= p_Comp_Schedule_Instance,
Penalty_nc=p_Penalty_nc,
Audit_Remarks=p_Audit_Remarks,
Audit_artefacts=p_Audit_artefacts,
Audit_Date=p_Audit_Date,
Version=p_Version,
Reviewer_ID=p_Reviewer_ID,
Review_Comments=p_Review_Comments,
Last_Updated_Date=NOW(),
Audit_Status=p_Audit_Status,
Compliance_Xref_ID=p_Compliance_Xref_ID,
Org_ID=p_Org_ID,
Auditor_ID=p_Auditor_ID,

Is_Active =p_Is_Active ,
Part_Compliance_Percent=p_Part_Compliance_Percent 
where Compliance_Audit_ID= p_Compliance_Audit_ID;
end if;
end$$

DELIMITER ;
