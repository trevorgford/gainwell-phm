create or alter procedure goalStatuses_load
    @tenantId int
as

select  goalStatusId as id,
        code,
        description 
from    goalStatuses 
where   tenantId = @tenantId
and     active = 1;

go
