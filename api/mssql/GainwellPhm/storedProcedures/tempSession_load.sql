create or alter procedure tempSession_load
    @sessionId uniqueidentifier,
    @tenantId int
as

begin transaction

    select      s.userId 
    from        tempSessions s
    inner join  userTenants t                   on  s.userId = t.userId
                                                and t.tenantId = @tenantId
    where       sessionId = @sessionId 
    and         datediff(minute, createdTimestamp, sysutcdatetime()) < 10;

    delete from tempSessions where sessionId = @sessionId;

commit transaction

go
