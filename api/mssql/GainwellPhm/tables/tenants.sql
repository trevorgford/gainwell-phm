if not exists ( select * from sys.tables where name = 'tenants' )
create table tenants (
    tenantId int identity not null,
    code varchar(50) not null,
    description varchar(50) not null,
    active bit not null constraint df_tenants_active default 1,
    constraint pk_tenants primary key ( tenantId )
);
go
