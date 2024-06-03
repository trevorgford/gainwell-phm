if not exists ( select * from sys.tables where name = 'interventionStatuses' )
create table interventionStatuses (
    interventionStatusId int identity not null,
    tenantId int not null,
    code varchar(50) null,
    description varchar(50) not null,
    initial bit not null constraint df_interventionStatuses_initial default 0,
    active bit not null constraint df_interventionStatuses_active default 1,
    constraint pk_interventionStatuses primary key ( interventionStatusId ),
    constraint fk_interventionStatuses_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
