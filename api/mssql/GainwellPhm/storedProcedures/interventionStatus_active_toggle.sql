create or alter procedure interventionStatus_active_toggle
    @interventionStatusId int,
    @active bit,
    @userId int
as

update interventionStatuses set active = @active where interventionStatusId = @interventionStatusId;

go
