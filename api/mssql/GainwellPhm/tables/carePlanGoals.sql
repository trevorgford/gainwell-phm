if not exists ( select * from sys.tables where name = 'carePlanGoals' )
create table carePlanGoals (
    carePlanGoalId int identity not null,
    carePlanId int not null,
    goalId int not null,
    goalStatusId int not null,
    dueDate datetime2 null,
    active bit not null constraint df_carePlanGoals_active default 1,
    constraint pk_carePlanGoals primary key ( carePlanGoalId ),
    constraint fk_carePlanGoals_carePlans foreign key ( carePlanId ) references carePlans ( carePlanId ),
    constraint fk_carePlanGoals_goals foreign key ( goalId ) references goals ( goalId ),
    constraint fk_carePlanGoals_goalStatuses foreign key ( goalStatusId ) references goalStatuses ( goalStatusId )
);
go
