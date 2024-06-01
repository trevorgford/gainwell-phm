create or alter procedure memberCareTeam_person_add
    @memberId int,
    @personId int
as

if not exists ( select * from memberCareTeam where memberId = @memberId and personId = @personId )
insert into memberCareTeam ( memberId, personId )
values      ( @memberId, @personId );

go
