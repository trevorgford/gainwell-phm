create or alter procedure interventionStatus_create
    @tenantId int,
    @description varchar(50),
    @code varchar(50) = null,
    @userId int
as

if not exists ( select * from interventionStatuses where description = @description and tenantId = @tenantId )
insert into interventionStatuses ( tenantId, code, description )
values      ( @tenantId, @code, @description );

go
