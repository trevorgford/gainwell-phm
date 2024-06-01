create or alter procedure goal_active_toggle
    @goalId int,
    @active bit
as

update goals set active = @active where goalId = @goalId;

go
