create or alter procedure goal_create
    @tenantId int,
    @code varchar(50) = null,
    @description varchar(50)
as

insert into goals ( tenantId, code, description )
values      ( @tenantId, @code, @description );

go
