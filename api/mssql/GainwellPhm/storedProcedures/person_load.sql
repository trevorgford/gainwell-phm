create or alter procedure person_load
    @id int
as

select  personId as Id,
        tenantId,
        firstName,
        lastName,
        title,
        active
from    people
where   personId = @id;

go
