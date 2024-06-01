create or alter procedure member_create
    @tenantId int,
    @firstName nvarchar(50),
    @lastName nvarchar(50),
    @dateOfBirth date = null
as

insert into members ( tenantId, firstName, lastName, dateOfBirth )
values      ( @tenantId, @firstName, @lastName, @dateOfBirth );

go
