create or alter procedure assessmentChoice_active_toggle
    @choiceId int,
    @active bit
as

update assessmentChoices set active = @active where choiceId = @choiceId;

go
