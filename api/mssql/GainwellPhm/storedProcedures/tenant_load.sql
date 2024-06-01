create or alter procedure tenant_load
    @id int
as

select  tenantId as id,
        code,
        description
from    tenants
where   tenantId = @id;

go
