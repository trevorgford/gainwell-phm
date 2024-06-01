if not exists ( select * from sys.tables where name = 'people' )
create table people (
    personId int identity not null,
    tenantId int not null,
    firstName varchar(50) not null,
    lastName varchar(50) not null,
    title varchar(50) null,
    userId int null,
    active bit not null constraint df_people_active default 1,
    constraint pk_people primary key ( personId ),
    constraint fk_people_tenants foreign key ( tenantId ) references tenants ( tenantId ),
    constraint fk_people_users foreign key ( userId ) references users ( userId )
);
go
