if not exists ( select * from sys.tables where name = 'problems' )
create table problems (
    problemId int identity not null,
    tenantId int not null,
    code varchar(50) null,
    description varchar(50) not null,
    active bit not null constraint df_problems_active default 1,
    constraint pk_problems primary key ( problemId ),
    constraint fk_problems_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
