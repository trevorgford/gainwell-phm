if not exists ( select * from sys.tables where name = 'assessments' )
create table assessments (
    assessmentId int identity not null,
    tenantId int not null,
    versionName varchar(50) null,
    code varchar(50) null,
    description varchar(100) not null,
    isCurrent bit not null constraint df_assessments_isCurrent default 0,
    active bit not null constraint df_assessments_active default 1,
    constraint pk_assessments primary key ( assessmentId ),
    constraint fk_assessments_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
