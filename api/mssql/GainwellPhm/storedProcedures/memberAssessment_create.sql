create or alter procedure memberAssessment_create
    @memberId int,
    @assessmentId int,
    @userId int
as

declare @d datetime2 = sysutcdatetime();

insert into memberAssessments ( memberId, assessmentId, createdTimestamp, modifiedTimestamp )
values      ( @memberId, @assessmentId, @d, @d );

go
