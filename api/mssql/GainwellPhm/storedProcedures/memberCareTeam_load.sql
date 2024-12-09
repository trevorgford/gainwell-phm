create or alter procedure memberCareTeam_load
    @memberId int
as

select      t.memberId as id,
            m.firstName,
            m.lastName,
            p.personId,
            p.firstName as personFirstName,
            p.lastName as personLastName,
            p.title
from        memberCareTeam t
inner join  members m            on t.memberId = m.memberId
inner join  people p             on t.personId = p.personId
where       t.memberId = @memberId;

go

exec memberCareTeam_load @memberId = 1;
