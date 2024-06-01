create or alter procedure goal_load
    @id int
as

select  goalId as id,
        tenantId,
        code,
        description,
        active 
from    goals 
where   goalId = @id;

go
