USE `auditmoduledb`;
DROP procedure IF EXISTS `sp_getLoginData`;

DELIMITER $$
USE `auditmoduledb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_getLoginData`(
p_Email_ID varchar(100),
p_User_Password varchar(100)
)
begin
select * from tbl_user where Email_ID= p_Email_ID and User_Password = p_User_Password and Is_Active=1;
end$$

DELIMITER ;