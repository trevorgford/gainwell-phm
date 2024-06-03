if not exists ( select * from sys.tables where name = 'carePlanBarriers' )
create table carePlanBarriers (
    carePlanBarrierId int identity not null,
    carePlanGoalId int not null,
    barrierId int not null,
    active bit not null constraint df_carePlanBarriers_active default 1,
    constraint pk_carePlanBarriers primary key ( carePlanBarrierId ),
    constraint fk_carePlanBarriers_carePlanGoals foreign key ( carePlanGoalId ) references carePlanGoals ( carePlanGoalId ),
    constraint fk_carePlanBarriers_barriers foreign key ( barrierId ) references barriers ( barrierId )
);
go
