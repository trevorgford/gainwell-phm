create or alter procedure goals_load
    @tenantId int = 5
as

select  goalId as id,
        tenantId,
        code,
        description
from    goals
where   tenantId = @tenantId
and     active = 1;

go
