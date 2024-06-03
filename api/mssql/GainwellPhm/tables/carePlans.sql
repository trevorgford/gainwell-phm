if not exists ( select * from sys.tables where name = 'carePlans' )
create table carePlans (
    carePlanId int identity not null,
    memberId int not null,
    problemId int not null,
    carePlanName varchar(100) null,
    active bit not null constraint df_carePlans_active default 1,
    constraint pk_carePlans primary key ( carePlanId ),
    constraint fk_carePlans_members foreign key ( memberId ) references members ( memberId ),
    constraint fk_carePlans_problems foreign key ( problemId ) references problems ( problemId )
);
go
