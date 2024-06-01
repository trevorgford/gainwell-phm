if not exists ( select * from sys.tables where name = 'users' )
create table users (
    userId int identity not null,
    userName varchar(50) not null,
    firstName varchar(50) not null,
    lastName varchar(50) not null,
    email varchar(50) null,
    passwordHash varchar(64) null,
    passwordSalt char(10) null,
    active bit not null constraint df_users_active default 1,
    constraint pk_users primary key ( userId )
);
go
