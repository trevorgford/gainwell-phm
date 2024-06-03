create or alter procedure members_search
    @searchText varchar(100),
    @tenantId int = 5
as

select      m.memberId as id,
            m.firstName,
            m.lastName,
            m.dateOfBirth
from        members m
where       m.tenantId = @tenantId
and         m.active = 1
and         m.firstName like @searchText + '%'

union

select      m.memberId as id,
            m.firstName,
            m.lastName,
            m.dateOfBirth
from        members m
where       m.tenantId = @tenantId
and         m.active = 1
and         m.lastName like @searchText + '%'

order by    firstName, 
            lastName;

go
