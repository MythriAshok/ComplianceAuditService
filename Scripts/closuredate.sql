Use compliancedb;


Use compliancedb;
DROP 	procedure if exists `sp_closure_date`;
DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_closure_date`
(
p_Company_ID int

)
begin
if(p_Company_ID=0)
then
select
*
from org_hier ;
else
select * from compliance_audit where 
compliance_audit.Audit_Followup_Date is null and  Org_Hier_ID in 
(Select org_hier.Org_Hier_ID from org_hier where Parent_Company_ID = p_Company_ID)
and
Vendor_ID in
(Select org_hier.Org_Hier_ID from org_hier where Parent_Company_ID = p_Company_ID);

end if;
end$$
DELIMITER ;

call sp_closure_date(58);

and
compliance_audit.Audit_Followup_Date = NULL;
