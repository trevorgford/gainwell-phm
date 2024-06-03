create or alter procedure goalStatus_create
    @tenantId int,
    @description varchar(50),
    @code varchar(50) = null,
    @userId int
as

if not exists ( select * from goalStatuses where description = @description and tenantId = @tenantId )
insert into goalStatuses ( tenantId, code, description )
values      ( @tenantId, @code, @description );

go
