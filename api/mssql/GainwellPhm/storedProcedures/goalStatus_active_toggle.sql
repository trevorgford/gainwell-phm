create or alter procedure goalStatus_active_toggle
    @goalStatusId int,
    @active bit,
    @userId int
as

update goalStatuses set active = @active where goalStatusId = @goalStatusId;

go
