if not exists ( select * from sys.tables where name = 'goals' )
create table goals (
    goalId int identity not null,
    tenantId int not null,
    code varchar(50) null,
    description varchar(50) not null,
    active bit not null constraint df_goals_active default 1,
    constraint pk_goals primary key ( goalId ),
    constraint fk_goals_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
