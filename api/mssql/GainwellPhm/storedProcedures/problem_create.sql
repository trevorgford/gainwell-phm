create or alter procedure problem_create
    @tenantId int,
    @code varchar(50) = null,
    @description varchar(50)
as

insert into problems ( tenantId, code, description )
values      ( @tenantId, @code, @description );

go
