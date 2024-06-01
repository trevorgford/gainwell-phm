if not exists ( select * from sys.tables where name = 'outcomes' )
create table outcomes (
    outcomeId int identity not null,
    tenantId int not null,
    code varchar(50) null,
    description varchar(50) not null,
    active bit not null constraint df_outcomes_active default 1,
    constraint pk_outcomes primary key ( outcomeId ),
    constraint fk_outcomes_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
