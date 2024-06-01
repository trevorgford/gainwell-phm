create or alter procedure barriers_load
    @tenantId int = 5
as

select  barrierId as id,
        tenantId,
        code,
        description
from    barriers
where   tenantId = @tenantId
and     active = 1;

go
