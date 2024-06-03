create or alter procedure interventionStatuses_load
    @tenantId int
as

select  interventionStatusId as id,
        code,
        description
from    interventionStatuses
where   tenantId = @tenantId
and     active = 1;

go
