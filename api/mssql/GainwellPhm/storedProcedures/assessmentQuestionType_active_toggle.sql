create or alter procedure assessmentQuestionType_active_toggle
    @questionTypeId int,
    @active bit
as

update assessmentQuestionTypes set active = @active where questionTypeId = @questionTypeId;

go
