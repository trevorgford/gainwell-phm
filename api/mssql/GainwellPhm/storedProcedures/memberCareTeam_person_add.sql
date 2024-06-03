create or alter procedure memberCareTeam_person_add
    @memberId int,
    @tenantId int,
    @firstName varchar(50),
    @lastName varchar(50),
    @title varchar(50),
    @personId int output
as

insert into people ( tenantId, firstName, lastName, title )
values      ( @tenantId, @firstName, @lastName, @title );

set @personId = scope_identity();

if not exists ( select * from memberCareTeam where memberId = @memberId and personId = @personId )
insert into memberCareTeam ( memberId, personId )
values      ( @memberId, @personId );

go
