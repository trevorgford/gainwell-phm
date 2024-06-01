create or alter procedure member_active_toggle
    @id int,
    @active bit
as

update members set active = @active where memberId = @id;

go
