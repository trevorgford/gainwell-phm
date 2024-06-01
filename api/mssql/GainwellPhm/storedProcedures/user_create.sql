create or alter procedure user_create
    @username varchar(50),
    @firstName varchar(50),
    @lastName varchar(50),
    @email varchar(50) = null,
    @passwordHash varchar(64) = null,
    @passwordSalt char(10) = null
as

if not exists ( select * from users where userName = @username )
insert into users ( userName, firstName, lastName, email, passwordHash, passwordSalt )
values      ( @username, @firstName, @lastName, @email, @passwordHash, @passwordSalt );

go
