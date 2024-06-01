create or alter procedure outcomes_load
    @tenantId int = 5
as

select  outcomeId as id,
        tenantId,
        code,
        description
from    outcomes
where   tenantId = @tenantId
and     active = 1;

go
