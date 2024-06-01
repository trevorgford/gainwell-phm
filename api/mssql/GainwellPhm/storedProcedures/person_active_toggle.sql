create or alter procedure person_active_toggle
    @id int,
    @active bit
as

update people set active = @active where personId = @id;

go
