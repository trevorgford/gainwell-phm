create or alter procedure intervention_active_toggle
    @interventionId int,
    @active bit
as

update interventions set active = @active where interventionId = @interventionId;

go
