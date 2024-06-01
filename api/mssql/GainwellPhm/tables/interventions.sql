if not exists ( select * from sys.tables where name = 'interventions' )
create table interventions (
    interventionId int identity not null,
    tenantId int not null,
    code varchar(50) null,
    description varchar(50) not null,
    active bit not null constraint df_interventions_active default 1,
    constraint pk_interventions primary key ( interventionId ),
    constraint fk_interventions_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
