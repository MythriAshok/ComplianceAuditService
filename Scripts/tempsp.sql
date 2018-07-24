Drop Procedure if exists sp_getActs;
delimiter /
create procedure sp_getActs()
begin
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
    `tbl_compliance_xref`.`Comp_Category`,
    `tbl_compliance_xref`.`Comp_Description`,
    `tbl_compliance_xref`.`Is_Header`,
    `tbl_compliance_xref`.`level`,
    `tbl_compliance_xref`.`Comp_Order`,
    `tbl_compliance_xref`.`Risk_Category`,
    `tbl_compliance_xref`.`Risk_Description`,
    `tbl_compliance_xref`.`Version`,
    `tbl_compliance_xref`.`Effective_Start_Date`,
    `tbl_compliance_xref`.`Effective_End_Date`,
    `tbl_compliance_xref`.`Country_ID`,
    `tbl_compliance_xref`.`State_ID`,
    `tbl_compliance_xref`.`City_ID`,
    `tbl_compliance_xref`.`Last_Updated_Date`,
    `tbl_compliance_xref`.`User_ID`,
    `tbl_compliance_xref`.`Is_Active`,
    `tbl_compliance_xref`.`Compliance_Title`,
    `tbl_compliance_xref`.`Compliance_Parent_ID`
FROM `auditmoduledb`.`tbl_compliance_xref`
where Comp_Category='Act' and `tbl_compliance_xref`.`level`=1;
end /
delimiter ;

Drop Procedure if exists sp_getSections;
delimiter /
create procedure sp_getSections(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
then
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
    `tbl_compliance_xref`.`Comp_Category`,
    `tbl_compliance_xref`.`Comp_Description`,
    `tbl_compliance_xref`.`Is_Header`,
    `tbl_compliance_xref`.`level`,
    `tbl_compliance_xref`.`Comp_Order`,
    `tbl_compliance_xref`.`Risk_Category`,
    `tbl_compliance_xref`.`Risk_Description`,
    `tbl_compliance_xref`.`Version`,
    `tbl_compliance_xref`.`Effective_Start_Date`,
    `tbl_compliance_xref`.`Effective_End_Date`,
    `tbl_compliance_xref`.`Country_ID`,
    `tbl_compliance_xref`.`State_ID`,
    `tbl_compliance_xref`.`City_ID`,
    `tbl_compliance_xref`.`Last_Updated_Date`,
    `tbl_compliance_xref`.`User_ID`,
    `tbl_compliance_xref`.`Is_Active`,
    `tbl_compliance_xref`.`Compliance_Title`,
    `tbl_compliance_xref`.`Compliance_Parent_ID`
FROM `auditmoduledb`.`tbl_compliance_xref`
where Comp_Category='Section' and `tbl_compliance_xref`.`level`=2;
else
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
  `tbl_compliance_xref`.`Compliance_Title`
  FROM `auditmoduledb`.`tbl_compliance_xref`
  where Comp_Category='Section' and `tbl_compliance_xref`.`level`=2 and Compliance_Parent_ID=p_Compliance_Parent_ID;
end if;
end /
delimiter ;

Drop Procedure if exists sp_getRules;
delimiter /
create procedure sp_getRules(p_Compliance_Parent_ID int)
begin
if(p_Compliance_Parent_ID=0)
then
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
    `tbl_compliance_xref`.`Comp_Category`,
    `tbl_compliance_xref`.`Comp_Description`,
    `tbl_compliance_xref`.`Is_Header`,
    `tbl_compliance_xref`.`level`,
    `tbl_compliance_xref`.`Comp_Order`,
    `tbl_compliance_xref`.`Risk_Category`,
    `tbl_compliance_xref`.`Risk_Description`,
    `tbl_compliance_xref`.`Recurrence`,
    `tbl_compliance_xref`.`Form`,
    `tbl_compliance_xref`.`Type`,
    `tbl_compliance_xref`.`Is_Best_Practice`,
    `tbl_compliance_xref`.`Version`,
    `tbl_compliance_xref`.`Effective_Start_Date`,
    `tbl_compliance_xref`.`Effective_End_Date`,
    `tbl_compliance_xref`.`Country_ID`,
    `tbl_compliance_xref`.`State_ID`,
    `tbl_compliance_xref`.`City_ID`,
    `tbl_compliance_xref`.`Last_Updated_Date`,
    `tbl_compliance_xref`.`User_ID`,
    `tbl_compliance_xref`.`Is_Active`,
    `tbl_compliance_xref`.`Compliance_Title`,
    `tbl_compliance_xref`.`Compliance_Parent_ID`,
    `tbl_compliance_xref`.`compl_def_consequence`
FROM `auditmoduledb`.`tbl_compliance_xref`
where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3;
else
SELECT `tbl_compliance_xref`.`Compliance_Xref_ID`,
  `tbl_compliance_xref`.`Compliance_Title`
  FROM `auditmoduledb`.`tbl_compliance_xref`
  where Comp_Category='Rule' and `tbl_compliance_xref`.`level`=3 and Compliance_Parent_ID=p_Compliance_Parent_ID;
  end if;
end /
delimiter ;

Drop Procedure if exists sp_getAuditorforBranch;
delimiter /
create procedure sp_getAuditorforBranch(p_Branch_Id int)
begin
SELECT `tbl_branch_auditor_mapping`.`Branch_Allocation_ID`,
    `tbl_branch_auditor_mapping`.`Auditor_ID`
FROM `auditmoduledb`.`tbl_branch_auditor_mapping`
where  `tbl_branch_auditor_mapping`.`Org_Hier_ID`=p_Branch_Id ;
end /
delimiter ;

Drop Procedure if exists sp_insertActandRuleforBranch;
delimiter /
create procedure sp_insertActandRuleforBranch(p_Compliance_Xref_ID int,p_Org_Hier_ID int,p_Auditor_ID int,p_User_ID int)
begin
INSERT INTO `auditmoduledb`.`tbl_compliance_audit`
(`Compliance_Xref_ID`,
`Org_Hier_ID`,
`Auditor_ID`,
`User_ID`,
`Is_Active`,
`Last_Updated_Date`)
VALUES(p_Compliance_Xref_ID,p_Org_Hier_ID,p_Auditor_ID,p_User_ID,now());
end /


Drop Procedure if exists sp_getRuleforBranch;
delimiter /
create procedure sp_getRuleforBranch(p_Compliance_Xref_ID int,p_Org_ID int)
begin
select Compliance_Xref_ID,Compliance_Title from tbl_compliance_xref where Compliance_Parent_ID=p_Compliance_Xref_ID and
Compliance_Xref_ID in (select Compliance_Xref_ID from tbl_compliance_audit where p_Org_ID);
end/