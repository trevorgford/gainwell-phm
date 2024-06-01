create or alter procedure outcome_create
    @tenantId int,
    @code varchar(50) = null,
    @description varchar(50)
as

insert into outcomes ( tenantId, code, description )
values      ( @tenantId, @code, @description );

go
