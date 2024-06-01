create or alter procedure people_load
    @tenantId int = 5
as

select  personId as id,
        tenantId,
        firstName,
        lastName,
        title
from    people
where   tenantId = @tenantId
and     active = 1;

go
