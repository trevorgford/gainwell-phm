create or alter procedure problem_active_toggle
    @problemId int,
    @active bit
as

update problems set active = @active where problemId = @problemId;

go
