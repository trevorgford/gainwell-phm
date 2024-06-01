create or alter procedure tenant_create
    @code varchar(50),
    @description varchar(50),
    @userId int
as

if not exists ( select * from tenants where code = @code )
insert into tenants ( code, description )
values      ( @code, @description );

go
