use auditmoduledb;

Drop procedure if exists 'sp_insertupdatePrivilege'
Delimiter /
create procedure sp_insertupdatePrivilege(p_flag char(1),p_Privilege_ID int(11),p_Privilege_Name varchar(45),p_Privilege_Type varchar(45),p_Is_Active bit)
begin
if(p_flag='I')
then
INSERT INTO `auditmoduledb`.`tbl_privilege`
(`Privilege_Name`,`Privilege_Type`,`Is_Active`)
VALUES(p_PrivilegeName,p_PrivilegeType,p_IsActive);
else
UPDATE `auditmoduledb`.`tbl_privilege`
SET
`Privilege_Name` = p_PrivilegeName,
`Privilege_Type` = p_PrivilegeType,
`Is_Active` = p_IsActive
WHERE `Privilege_ID` = p_PrivilegeID;
end if;
end/

Drop procedure if exists 'sp_deletePrivilege'
Delimiter /
create procedure sp_deletePrivilege(p_PrivilegeID int(11))
begin
DELETE FROM `auditmoduledb`.`tbl_privilege`
WHERE `Privilege_ID` = p_PrivilegeID;
end/

Drop procedure if exists 'sp_getPrivilege'
Delimiter /
create procedure sp_getPrivilege(p_PrivilegeID int(11))
begin
if(p_PrivilegeID=0)
then
SELECT `tbl_privilege`.`Privilege_ID`,
    `tbl_privilege`.`Privilege_Name`,
    `tbl_privilege`.`Privilege_Type`,
    `tbl_privilege`.`Is_Active`
FROM `auditmoduledb`.`tbl_privilege`;
else
SELECT `tbl_privilege`.`Privilege_ID`,
    `tbl_privilege`.`Privilege_Name`,
    `tbl_privilege`.`Privilege_Type`,
    `tbl_privilege`.`Is_Active`
FROM `auditmoduledb`.`tbl_privilege`
WHERE `Privilege_ID` = p_PrivilegeID;
end if;
end /

Drop procedure if exists 'sp_insertupdateRoles'
Delimiter /
create procedure sp_insertupdateRoles(p_flag int,p_RoleID int(11),p_RoleName varchar(45),p_IsActive tinyint(4),p_Is_Group_Role tinyint(4))
begin
if(p_flag='I')
then
INSERT INTO `auditmoduledb`.`tbl_role`
(`Role_Name`,`Is_Active`,`Is_Group_Role`)
VALUES(p_RoleName,p_IsActive,p_Is_Group_Role);
else
UPDATE `auditmoduledb`.`tbl_role`
SET
`Role_Name` = p_RoleName,
`Is_Active` = p_IsActive,
`Is_Group_Role` = p_Is_Group_Role
WHERE `Role_ID` = p_RoleID;
end if;
end /

Drop procedure if exists 'sp_deleteRoles'
Delimiter /
create procedure sp_deleteRoles(p_RoleID int(11))
begin
DELETE FROM `auditmoduledb`.`tbl_role`
WHERE `Role_ID` = RoleID;
end /

Drop procedure if exists 'sp_getRoles'
Delimiter /
create procedure sp_getRoles(RoleID int(11))
begin
if(RoleID=0)
then
SELECT `tbl_role`.`Role_ID`,
    `tbl_role`.`Role_Name`,
    `tbl_role`.`Is_Active`,
    `tbl_role`.`Is_Group_Role`
FROM `auditmoduledb`.`tbl_role`;
else
SELECT `tbl_role`.`Role_ID`,
    `tbl_role`.`Role_Name`,
    `tbl_role`.`Is_Active`,
    `tbl_role`.`Is_Group_Role`
FROM `auditmoduledb`.`tbl_role`
WHERE `Role_ID` = RoleID;
end if;
end /

Drop procedure if exists sp_createUser
Delimiter /
create procedure sp_createUser(UserPassword varchar(10),FirstName varchar(45),MiddleName varchar(45),Last_Name varchar(45),
EmailID varchar(100),ContactNumber varchar(50),Gender varchar(45),IsActive bit)
begin
if exists(select Email_ID from `auditmoduledb`.`tbl_user` where Email_ID=EmailID)
then
select "EXISTS";
else
INSERT INTO `auditmoduledb`.`tbl_user`
(`User_ID`,`User_Password`,`First_Name`,`Middle_Name`,`Last_Name`,
`Email_ID`,`Contact_Number`,`Gender`,`Is_Active`,`Last_Login`)
VALUES(UserPassword,FirstName,MiddleName,Last_Name,EmailID,ContactNumber,Gender,IsActive,now());
end if;
end /