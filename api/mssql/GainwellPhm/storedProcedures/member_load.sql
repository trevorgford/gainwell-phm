create or alter procedure member_load
    @id int
as

select  memberId as id,
        firstName,
        lastName,
        dateOfBirth,
        active
from    members 
where   memberId = @id;

go
