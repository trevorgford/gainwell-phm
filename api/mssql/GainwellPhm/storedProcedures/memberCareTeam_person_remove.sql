create or alter procedure memberCareTeam_person_remove
    @memberId int,
    @personId int
as

delete from memberCareTeam
where       memberId = @memberId
and         personId = @personId;

go
