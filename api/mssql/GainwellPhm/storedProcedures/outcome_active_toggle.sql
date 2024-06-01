create or alter procedure outcome_active_toggle
    @outcomeId int,
    @active bit
as

update outcomes set active = @active where outcomeId = @outcomeId;

go
