create or alter procedure tenants_load

as

select  tenantId as id,
        code,
        description
from    tenants
where   active = 1;

go
