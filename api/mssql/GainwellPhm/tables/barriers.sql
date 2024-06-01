if not exists ( select * from sys.tables where name = 'barriers' )
create table barriers (
    barrierId int identity not null,
    tenantId int not null,
    code varchar(50) null,
    description varchar(50) not null,
    active bit not null constraint df_barriers_active default 1,
    constraint pk_barriers primary key ( barrierId ),
    constraint fk_barriers_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
