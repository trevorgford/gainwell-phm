if not exists ( select * from sys.tables where name = 'tempSessions' )
create table tempSessions (
    sessionId uniqueidentifier not null,
    userId int not null,
    createdTimestamp datetime2 not null
);
go

if not exists ( select * from sys.indexes where name = 'IX_tempSessions_sessionId' )
create index IX_tempSessions_tempSessionId on tempSessions ( sessionId );
go
