use auditmoduledb;

/*Store procedure for insert and updating the user table, p_flag will indicates the which operation to perform.
'I' indicates the insert and 'U' indicates the update.alter
Before inserting it checks for exists with the Email_ID column in user table
created by ojeshwini  */
Drop procedure if exists 'sp_insertupdateUser'
Delimiter /
create procedure sp_insertupdateUser(p_flag char(1),p_User_ID int,p_User_Password varchar(10),p_First_Name varchar(45),
p_Middle_Name varchar(45),p_Last_Name varchar(45),p_Email_ID varchar(100),p_Contact_Number varchar(50),
p_Gender varchar(45),p_Is_Active bit)
begin
if(p_flag='I')
then
if exists(select Email_ID from `auditmoduledb`.`tbl_user` where Email_ID=EmailID)
then
select "EXISTS";
else
INSERT INTO `auditmoduledb`.`tbl_user`(`User_ID`,
`User_Password`,
`First_Name`,
`Middle_Name`,
`Last_Name`,
`Email_ID`,
`Contact_Number`,
`Gender`,
`Is_Active`,
`Last_Login`)
VALUES(p_User_ID,p_User_Password,p_First_Name,p_Middle_Name,p_Last_Name,p_Email_ID,
p_Contact_Number,p_Gender,p_Is_Active,now());
select "Sucessfully Created New User";
end if;
else
UPDATE `auditmoduledb`.`tbl_user`
SET
`User_Password` = p_User_Password,
`First_Name` = p_First_Name,
`Middle_Name` = p_Middle_Name,
`Last_Name` = p_Last_Name,
`Email_ID` = p_Email_ID,
`Contact_Number` = p_Contact_Number,
`Gender` = p_Gender,
`Is_Active` = p_Is_Active,
`Last_Login` = now()
WHERE `User_ID` = p_User_ID;
end if;
end /

Drop procedure if exists 'sp_deleteUser'
Delimiter /
create procedure sp_deleteUser(p_User_ID int)
begin
update `auditmoduledb`.`tbl_user` set Is_Active=0 
where User_ID = p_User_ID;
end /

Drop procedure if exists 'sp_getUser'
Delimiter /
create procedure sp_getUser(p_User_ID int)
begin
if(p_User_ID=0)
then
SELECT `tbl_user`.`User_ID`,
    `tbl_user`.`User_Password`,
    `tbl_user`.`First_Name`,
    `tbl_user`.`Middle_Name`,
    `tbl_user`.`Last_Name`,
    `tbl_user`.`Email_ID`,
    `tbl_user`.`Contact_Number`,
    `tbl_user`.`Gender`,
    `tbl_user`.`Is_Active`,
    `tbl_user`.`Last_Login`
FROM `auditmoduledb`.`tbl_user`;
else
SELECT `tbl_user`.`User_ID`,
    `tbl_user`.`User_Password`,
    `tbl_user`.`First_Name`,
    `tbl_user`.`Middle_Name`,
    `tbl_user`.`Last_Name`,
    `tbl_user`.`Email_ID`,
    `tbl_user`.`Contact_Number`,
    `tbl_user`.`Gender`,
    `tbl_user`.`Is_Active`,
    `tbl_user`.`Last_Login`
FROM `auditmoduledb`.`tbl_user`
where User_ID = p_User_ID;
end if;
end /


Drop procedure if exists 'sp_insertupdateRole'
Delimiter /
create procedure sp_insertupdateRole(p_flag char(1),p_Role_ID int,p_Role_Name varchar(45),p_Is_Active bit,p_Is_Group_Role bit)
begin
if(p_flag='I')
then
INSERT INTO `auditmoduledb`.`tbl_role`
(`Role_ID`,
`Role_Name`,
`Is_Active`,
`Is_Group_Role`)
VALUES
(p_Role_ID,p_Role_Name,p_Is_Active,p_Is_Group_Role);
else
UPDATE `auditmoduledb`.`tbl_role`
SET
`Role_Name` = p_Role_Name,
`Is_Active` = p_Is_Active,
`Is_Group_Role` = p_Is_Group_Role
WHERE `Role_ID` = p_Role_ID ;
end if;
end /

Drop procedure if exists 'sp_deleteRole'
Delimiter /
create procedure sp_deleteRole(p_Role_ID int)
begin
UPDATE `auditmoduledb`.`tbl_role`
SET
`Is_Active` = 0
WHERE `Role_ID` = p_Role_ID ;
end /

Drop procedure if exists 'sp_getRole'
Delimiter /
create procedure sp_getRole(p_Role_ID int)
begin
if(p_Role_ID=0)
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
WHERE `Role_ID` = p_Role_ID ;
end if;
end /


Drop procedure if exists 'sp_insertupdateUserGroup'
Delimiter /
create procedure sp_insertupdateUserGroup(p_flag char(1),User_Group_ID int,p_User_Group_Name varchar(45),
User_Group_Description varchar(45),p_Role_ID int)
begin
if(p_flag='I')
then
INSERT INTO `auditmoduledb`.`tbl_user_group`
(`User_Group_Name`,
`User_Group_Description`,
`Role_ID`)
VALUES
(p_User_Group_Name,p_User_Group_Description,p_Role_ID);
else
UPDATE `auditmoduledb`.`tbl_user_group`
SET
`User_Group_Name` = p_User_Group_Name,
`User_Group_Description` = p_User_Group_Description,
`Role_ID` = p_Role_ID
WHERE `User_Group_ID` = p_User_Group_ID;
end if;
end /

Drop procedure if exists 'sp_getUserGroup'
Delimiter /
create procedure sp_getRole(p_User_Group_ID int)
begin
if(p_User_Group_ID=0)
then
SELECT `tbl_user_group`.`User_Group_ID`,
    `tbl_user_group`.`User_Group_Name`,
    `tbl_user_group`.`User_Group_Description`,
    `tbl_user_group`.`Role_ID`
FROM `auditmoduledb`.`tbl_user_group`;
else
SELECT `tbl_user_group`.`User_Group_ID`,
    `tbl_user_group`.`User_Group_Name`,
    `tbl_user_group`.`User_Group_Description`,
    `tbl_user_group`.`Role_ID`
FROM `auditmoduledb`.`tbl_user_group`
WHERE `User_Group_ID` = p_User_Group_ID;
end if;
end /

Drop procedure if exists 'sp_getMenus'
Delimiter /
create procedure sp_getMenus(p_User_Group_ID int)
begin
if(p_User_Group_ID=0)
then
SELECT `tbl_menus`.`Menu_ID`,
    `tbl_menus`.`Parent_MenuID`,
    `tbl_menus`.`Menu_Name`,
    `tbl_menus`.`Page_URL`,
    `tbl_menus`.`Is_Active`,
    `tbl_menus`.`User_Group_ID`
FROM `auditmoduledb`.`tbl_menus`;
else
SELECT `tbl_menus`.`Menu_ID`,
    `tbl_menus`.`Parent_MenuID`,
    `tbl_menus`.`Menu_Name`,
    `tbl_menus`.`Page_URL`,
    `tbl_menus`.`Is_Active`,
    `tbl_menus`.`User_Group_ID`
FROM `auditmoduledb`.`tbl_menus`
WHERE `User_Group_ID` = p_User_Group_ID;
end if;
end /

Drop procedure if exists 'sp_getRolePrivilege'
Delimiter /
create procedure sp_getRolePrivilege(p_Role_ID int)
begin
select Privilege_Name,Privilege_Type,Is_Active from tbl_role_priv_map a left join tbl_privilege b 
on a.Role_ID=b.Role_ID where a.Role_ID=p_Role_ID;
end /

Drop procedure if exists 'sp_getUserRole'
Delimiter /
create procedure sp_getUserRole(p_User_ID int)
begin
select Role_ID from tbl_user_role_map where User_ID=p_User_ID;
end /