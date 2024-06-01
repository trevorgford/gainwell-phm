create or alter procedure interventions_load
    @tenantId int = 5
as

select  interventionId as id,
        tenantId,
        code,
        description
from    interventions
where   tenantId = @tenantId
and     active = 1;

go
