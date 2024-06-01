create or alter procedure users_load

as

select  userId as id,
        userName,
        firstName,
        lastName,
        email
from    users
where   active = 1;

go
