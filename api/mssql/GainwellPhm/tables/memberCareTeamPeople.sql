if not exists ( select * from sys.tables where name = 'memberCareTeam' )
create table memberCareTeam (
    memberId int not null,
    personId int not null,
    constraint pk_memberCareTeam primary key ( memberId, personId ),
    constraint fk_memberCareTeam_members foreign key ( memberId ) references members ( memberId ),
    constraint fk_memberCareTeam_people foreign key ( personId ) references people ( personId )
);
go
