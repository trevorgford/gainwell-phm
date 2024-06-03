if not exists ( select * from sys.tables where name = 'goalStatuses' )
create table goalStatuses (
    goalStatusId int identity not null,
    tenantId int not null,
    code varchar(50) null,
    description varchar(50) not null,
    initial bit not null constraint df_goalStatuses_initial default 0,
    active bit not null constraint df_goalStatuses_active default 1,
    constraint pk_goalStatuses primary key ( goalStatusId ),
    constraint fk_goalStatuses_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
