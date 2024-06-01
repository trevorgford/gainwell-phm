create or alter procedure outcome_load
    @id int
as

select  outcomeId as id,
        tenantId,
        code,
        description,
        active 
from    outcomes 
where   outcomeId = @id;

go
