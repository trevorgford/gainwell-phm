create or alter procedure tempSession_create
    @sessionId uniqueidentifier,
    @userId int
as

insert into tempSessions ( sessionId, userId, createdTimestamp )
values      ( @sessionId, @userId, sysutcdatetime() );

go
