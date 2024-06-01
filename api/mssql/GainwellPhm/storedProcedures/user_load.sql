create or alter procedure user_load
    @id int
as

select      u.userId as id,
            u.userName,
            u.firstName,
            u.lastName,
            u.email,
            u.active,
            t.tenantId,
            t.code,
            t.description
from        users u
left join   userTenants ut          on  u.userId = ut.userId
left join   tenants t               on  ut.tenantId = t.tenantId
                                    and t.active = 1
where       u.userId = @id;

go
