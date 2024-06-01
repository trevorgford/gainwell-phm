create or alter procedure barrier_load
    @id int
as

select  barrierId as id,
        tenantId,
        code,
        description,
        active 
from    barriers 
where   barrierId = @id;

go
