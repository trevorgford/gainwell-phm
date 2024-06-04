create or alter procedure assessment_create
    @tenantId int,
    @versionName varchar(50) = null,
    @code varchar(50) = null,
    @description varchar(100),
    @userId int
as

if not exists ( select * from assessments where tenantId = @tenantId and description = @description and versionName = @versionName )
insert into assessments ( tenantId, versionName, code, description )
values      ( @tenantId, @versionName, @code, @description );

go
