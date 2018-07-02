use auditmoduledb;
Drop procedure if exists 'sp_createPrivilege'
delimiter /
create procedure sp_createPrivilege(PrivilegeName varchar(45),PrivilegeType varchar(45), IsActive tinyint(4))
begin
INSERT INTO `auditmoduledb`.`tbl_privilege`
(`Privilege_Name`,`Privilege_Type`,`Is_Active`)
VALUES(PrivilegeName,PrivilegeType,IsActive);
end/

Drop procedure if exists 'sp_createMenus'
delimiter /
create procedure sp_createMenus(ParentMenuID int(11),MenuName varchar(45), PageURL varchar(45),IsActive bit,UserGroupID int(11))
begin
INSERT INTO `auditmoduledb`.`tbl_menus`
(`Parent_MenuID`,`Menu_Name`,`Page_URL`,`Is_Active`,`User_Group_ID`)
VALUES(ParentMenuID,MenuName,PageURL,IsActive,UserGroupID);
end/

Drop procedure if exists 'sp_getMenus'
delimiter /
create procedure sp_getMenus()
begin
SELECT `tbl_menus`.`iD`,
    `tbl_menus`.`Parent_MenuID`,
    `tbl_menus`.`Menu_Name`,
    `tbl_menus`.`Page_URL`,
    `tbl_menus`.`Is_Active`,
    `tbl_menus`.`User_Group_ID`
FROM `auditmoduledb`.`tbl_menus`;
end/

create procedure sp_updatemenus()
begin
UPDATE `auditmoduledb`.`tbl_menus`
SET
`iD` = <{iD: }>,
`Parent_MenuID` = <{Parent_MenuID: }>,
`Menu_Name` = <{Menu_Name: }>,
`Page_URL` = <{Page_URL: }>,
`Is_Active` = <{Is_Active: }>,
`User_Group_ID` = <{User_Group_ID: }>
WHERE `iD` = <{expr}>;

end/

delimiter /
create procedure sp_deletemenus()
begin
DELETE FROM `auditmoduledb`.`tbl_menus`
WHERE <{where_expression}>;

end/

delimiter /
create procedure sp_createroles()
begin
INSERT INTO `auditmoduledb`.`tbl_role`
(`Role_ID`,`Role_Name`,`Is_Active`,`Is_Group_Role`)
VALUES(<{Role_ID: }>,<{Role_Name: }>,<{Is_Active: }>,<{Is_Group_Role: }>);
end/

delimiter /
create procedure sp_updateroles()
begin
UPDATE `auditmoduledb`.`tbl_role`
SET
`Role_ID` = <{Role_ID: }>,
`Role_Name` = <{Role_Name: }>,
`Is_Active` = <{Is_Active: }>,
`Is_Group_Role` = <{Is_Group_Role: }>
WHERE `Role_ID` = <{expr}>;

end/

delimiter /
create procedure sp_deleteroles()
begin
DELETE FROM `auditmoduledb`.`tbl_role`
WHERE <{where_expression}>;

end/

delimiter /
create procedure sp_getroles()
begin
SELECT `tbl_role`.`Role_ID`,
    `tbl_role`.`Role_Name`,
    `tbl_role`.`Is_Active`,
    `tbl_role`.`Is_Group_Role`
FROM `auditmoduledb`.`tbl_role`;

end/

delimiter /
create procedure sp_createuser()
begin
INSERT INTO `auditmoduledb`.`tbl_user`
(`User_ID`,
`User_Password`,
`First_Name`,
`Middle_Name`,
`Last_Name`,
`Email_ID`,
`Contact_Number`,
`Gender`,
`Is_Active`,
`Last_Login`)
VALUES
(p_User_ID,p_User_Password,p_First_Name,p_Middle_Name,p_Last_Name,p_Email_ID,
p_Contact_Number,p_Gender,p_Is_Active,now());

end/

delimiter /
create procedure sp_deletuser()
begin
DELETE FROM `auditmoduledb`.`tbl_role`
WHERE <{where_expression}>;

end /

delimiter /
create procedure sp_updateuser()
begin
UPDATE `auditmoduledb`.`tbl_role`
SET
`Role_ID` = <{Role_ID: }>,
`Role_Name` = <{Role_Name: }>,
`Is_Active` = <{Is_Active: }>,
`Is_Group_Role` = <{Is_Group_Role: }>
WHERE `Role_ID` = <{expr}>;

end/ 

delimiter /
create procedure sp_getusers()
begin
SELECT `tbl_role`.`Role_ID`,
    `tbl_role`.`Role_Name`,
    `tbl_role`.`Is_Active`,
    `tbl_role`.`Is_Group_Role`
FROM `auditmoduledb`.`tbl_role`;

end /

delimiter /
create procedure sp_getuser()
begin
SELECT `tbl_role`.`Role_ID`,
    `tbl_role`.`Role_Name`,
    `tbl_role`.`Is_Active`,
    `tbl_role`.`Is_Group_Role`
FROM `auditmoduledb`.`tbl_role` where `Role_ID` = <{expr}> ;
end /

delimiter /
create procedure sp_getuserrole()
begin
SET @Role_ID_to_select = <{row_id}>;
SELECT tbl_role_priv_map.*
    FROM tbl_role_priv_map, tbl_role
    WHERE `tbl_role`.`Role_ID` = `tbl_role_priv_map`.`Role_ID`
          AND tbl_role.Role_ID = @Role_ID_to_select;
SELECT tbl_user_group_members.*
    FROM tbl_user_group, tbl_user_group_members, tbl_role
    WHERE `tbl_role`.`Role_ID` = `tbl_user_group`.`Role_ID`
          AND `tbl_user_group`.`User_Group_ID` = `tbl_user_group_members`.`User_Group_ID`
          AND tbl_role.Role_ID = @Role_ID_to_select;
SELECT tbl_user_group.*
    FROM tbl_user_group, tbl_role
    WHERE `tbl_role`.`Role_ID` = `tbl_user_group`.`Role_ID`
          AND tbl_role.Role_ID = @Role_ID_to_select;
SELECT tbl_user_role_map.*
    FROM tbl_user_role_map, tbl_role
    WHERE `tbl_role`.`Role_ID` = `tbl_user_role_map`.`Role_ID`
          AND tbl_role.Role_ID = @Role_ID_to_select;
SELECT tbl_role.*
    FROM tbl_role
    WHERE tbl_role.Role_ID = @Role_ID_to_select;
end /

delimiter /
create procedure sp_createusergroup()
begin
INSERT INTO `auditmoduledb`.`tbl_user_group`
(`User_Group_ID`,`User_Group_Name`,`User_Group_Description`,`Role_ID`)
VALUES(<{User_Group_ID: }>,<{User_Group_Name: }>,<{User_Group_Description: }>,<{Role_ID: }>);
end/

delimiter /
create procedure sp_deleteusergroup()
begin
DELETE FROM `auditmoduledb`.`tbl_user_group`
WHERE <{where_expression}>;

end /

delimiter /
create procedure sp_updateusergroup()
begin
UPDATE `auditmoduledb`.`tbl_user_group`
SET
`User_Group_ID` = <{User_Group_ID: }>,
`User_Group_Name` = <{User_Group_Name: }>,
`User_Group_Description` = <{User_Group_Description: }>,
`Role_ID` = <{Role_ID: }>
WHERE `User_Group_ID` = <{expr}>;

end /

create procedure sp_getusergroup()
begin

end /

create procedure sp_selectusergroup()
begin

end /

create procedure sp_createusergroup()
begin

end/

delimiter ;