create or alter procedure person_create
    @tenantId int,
    @firstName varchar(50),
    @lastName varchar(50),
    @title varchar(50) = null
as

insert into people ( tenantId, firstName, lastName, title )
values      ( @tenantId, @firstName, @lastName, @title );

go
