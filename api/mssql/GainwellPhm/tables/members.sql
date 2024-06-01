if not exists ( select * from sys.tables where name = 'members' )
create table members (
    memberId int identity not null,
    tenantId int not null,
    firstName varchar(50) not null,
    lastName varchar(50) not null,
    dateOfBirth date,
    userId int null,
    active bit not null constraint df_members_active default 1,
    constraint pk_members primary key ( memberId ),
    constraint fk_members_tenants foreign key ( tenantId ) references tenants ( tenantId ),
    constraint fk_members_users foreign key ( userId ) references users ( userId )
);
go
