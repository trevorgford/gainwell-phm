if not exists ( select * from sys.tables where name = 'userTenants' )
create table userTenants (
    userId int not null,
    tenantId int not null,
    constraint pk_userTenants primary key ( userId, tenantId ),
    constraint fk_userTenants_users foreign key ( userId ) references users ( userId ),
    constraint fk_userTenants_tenants foreign key ( tenantId ) references tenants ( tenantId )
);
go
