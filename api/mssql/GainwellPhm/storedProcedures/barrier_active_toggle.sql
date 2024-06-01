create or alter procedure barrier_active_toggle
    @barrierId int,
    @active bit
as

update barriers set active = @active where barrierId = @barrierId;

go
