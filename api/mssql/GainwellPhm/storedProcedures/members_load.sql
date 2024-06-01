create or alter procedure members_load
    @tenantId int = 5
as

select  memberId as id,
        firstName,
        lastName,
        dateOfBirth
from    members
where   tenantId = @tenantId
and     active = 1;

go
