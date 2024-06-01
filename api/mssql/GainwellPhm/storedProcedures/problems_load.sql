create or alter procedure problems_load
    @tenantId int = 5
as

select  problemId as id,
        tenantId,
        code,
        description
from    problems
where   tenantId = @tenantId
and     active = 1;

go
