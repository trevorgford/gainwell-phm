create or alter procedure assessmentQuestion_active_toggle
    @questionId int,
    @active bit
as

update assessmentQuestions set active = @active where questionId = @questionId;

go
