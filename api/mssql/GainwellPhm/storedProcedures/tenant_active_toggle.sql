create or alter procedure tenant_active_toggle
    @id int,
    @active bit
as

update tenants set active = @active where tenantId = @id;

go
