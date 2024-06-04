create or alter procedure assessment_active_toggle
    @assessmentId int,
    @active bit
as

update assessments set active = @active where assessmentId = @assessmentId;

go
