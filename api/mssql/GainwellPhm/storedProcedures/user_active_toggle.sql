create or alter procedure user_active_toggle
    @id int,
    @active bit
as

update users set active = @active where userId = @id;

go
