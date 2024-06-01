create or alter procedure barrier_create
    @tenantId int,
    @code varchar(50) = null,
    @description varchar(50)
as

insert into barriers ( tenantId, code, description )
values      ( @tenantId, @code, @description );

go
